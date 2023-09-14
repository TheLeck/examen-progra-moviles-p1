

using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;

namespace examen;

public partial class ModificarCliente : ContentPage
{
    public int iduno;
    public class Cliente
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string foto { get; set; }
    }
    public ModificarCliente(string xml)
	{
        // Deserializar los datos y crear una instancia de Cliente
        XmlSerializer serializer = new XmlSerializer(typeof(Cliente));
        StringReader sr = new StringReader(xml);
        Cliente cliente2 = (Cliente)serializer.Deserialize(sr);


        InitializeComponent();
		nombre.Text= cliente2.nombre;
        direccion.Text = cliente2.direccion;
        telefono.Text = cliente2.telefono;
        correo.Text = cliente2.correo;
        url.Text = cliente2.foto;
        iduno = cliente2.id;
        img.Source = ImageSource.FromUri(new Uri(url.Text));
    }
    private void tienda_Completed(object sender, EventArgs e)
    {
        img.Source = ImageSource.FromUri(new Uri(url.Text));
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
        //var jsonResult = response.Content.ReadAsStringAsync().Result;
        // titulo.Text = jsonResult;
        //Se deserializa la cadena y se convierte en una instancia de WeatherResult
        //var listado = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Cliente>>(jsonResult);
        //Asignamos el nuevo valor de las propiedades
        //datos = listado;
        //titulo.Text = listado.Count.ToString();
        //Console.WriteLine("Numero de usuarios:"+listado.Count);
        //milista.ItemsSource = null;
        //milista.ItemsSource = datos;
        //foreach (Usuario element in listado.)
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        _ = llamadaGetJsonAsync($"https://hjqwpru.000webhostapp.com/update.php?id={iduno}&nombre={nombre.Text}&direccion={direccion.Text}&telefono={telefono.Text}&correo={correo.Text}&foto={url.Text}");
        await Navigation.PopAsync();
    }
}