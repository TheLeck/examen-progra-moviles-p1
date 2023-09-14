using System.Collections.ObjectModel;
using System.Xml.Serialization;


namespace examen;

public partial class Productos : ContentPage
{

    public class Producto
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string cantidad { get; set; }
        public string preciocosto { get; set; }
        public string precioventa { get; set; }
        public string foto { get; set; }
    }


    ObservableCollection<Producto> datos = new ObservableCollection<Producto>();
    public Productos()
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
        var listado = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Producto>>(jsonResult);
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
        int index = datos.IndexOf((Producto)myItem);
        string action = await DisplayActionSheet("Acciones:", "Cancelar", null, "Eliminar", "Editar");
        if (action == "Eliminar")
        {
            _ = llamadaGetJsonAsync($"https://hjqwpru.000webhostapp.com/eliminarproducts.php?id={datos[index].id}");
            Button_Clicked();
        }
        if (action == "Editar")
        {
            var product = new Producto();
            product.id = datos[index].id;
            product.nombre = datos[index].nombre;
            product.descripcion = datos[index].descripcion;
            product.cantidad = datos[index].cantidad;
            product.foto = datos[index].foto;
            product.preciocosto = datos[index].preciocosto;
            product.precioventa = datos[index].precioventa;
            // Serializar el objeto Cliente
            XmlSerializer serializer = new XmlSerializer(typeof(Producto));
            StringWriter sw = new StringWriter();
            serializer.Serialize(sw, product);
            string xml = sw.ToString();

            await Navigation.PushAsync(new ModificarProductos(xml));
        }
    }

    private async void agregar_producto(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AgregarProductos());
    }

    private void Button_Clicked()
    {
        try
        {
            _ = llamadaGetJsonAsync("https://hjqwpru.000webhostapp.com/viewproducts.php");
        }
        catch
        {
            DisplayAlert("Mensa", "Error al tratar de conectarse", "ok");
        }
    }
}