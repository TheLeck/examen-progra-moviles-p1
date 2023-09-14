using System.Collections.ObjectModel;

namespace examen;

public partial class Clientes : ContentPage
{
    public class Cliente
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string correo {  get; set; }
        public string foto {  get; set; }
    }

    ObservableCollection<Cliente> datos = new ObservableCollection<Cliente>();

    public Clientes()
	{
		InitializeComponent();
        Button_Clicked();

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Button_Clicked();
    }

    public async Task llamadaGetJsonAsync(string url)
    {
        //Creamos una instancia de HttpClient
        var client = new HttpClient();
        //Asignamos la URL
        client.BaseAddress = new Uri(url);
        //Llamada asíncrona al sitio
        var response = await client.GetAsync(client.BaseAddress);
        //Nos aseguramos de recibir una respuesta satisfactoria
        response.EnsureSuccessStatusCode();
        //Convertimos la respuesta a una variable string
        var jsonResult = response.Content.ReadAsStringAsync().Result;
        // titulo.Text = jsonResult;
        //Se deserializa la cadena y se convierte en una instancia de WeatherResult
        var listado = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Cliente>>(jsonResult);
        //Asignamos el nuevo valor de las propiedades
        datos = listado;
        //titulo.Text = listado.Count.ToString();
        //Console.WriteLine("Numero de usuarios:"+listado.Count);
        milista.ItemsSource = null;
        milista.ItemsSource = datos;
        //foreach (Usuario element in listado.)
    }

    private async void milista_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var myListView = (ListView)sender;
        var myItem = myListView.SelectedItem;
        int index = datos.IndexOf((Cliente)myItem);
        string action = await DisplayActionSheet("Acciones:", "Cancelar", null, "Eliminar", "Editar");
        if (action == "Eliminar")
        {
            _ = llamadaGetJsonAsync($"https://hjqwpru.000webhostapp.com/eliminar.php?id={datos[index].id}");
            Button_Clicked();
        }
        if (action == "Editar")
        {
            await DisplayAlert("Mensaje", "Seleccionó editar", "ok");
        }
    }

    private async void agregar_cliente(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AgregarCliente());
    }

    private void Button_Clicked()
    {
        try
        {
            _ = llamadaGetJsonAsync("https://hjqwpru.000webhostapp.com/view.php");
        }
        catch
        {
            DisplayAlert("Mensa", "Error al tratar de conectarse", "ok");
        }
    }
}
