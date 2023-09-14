using System.Xml.Serialization;

namespace examen;

public partial class ModificarProductos : ContentPage
{

    public int iduno;

    public class Producto
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string cantidad { get; set; }
        public string preciocosto { get; set; }
        public string precioventa { get; set; } // igual que en bd
        public string foto { get; set; }
    }

    public ModificarProductos (string xml)
    {
        // Deserializar los datos y crear una instancia de Cliente
        XmlSerializer serializer = new XmlSerializer(typeof(Producto));
        StringReader sr = new StringReader(xml);
        Producto producto = (Producto)serializer.Deserialize(sr);


        InitializeComponent();
        nombre.Text = producto.nombre;
        descripcion.Text = producto.descripcion;
        cantidad.Text = producto.cantidad;
        costo.Text = producto.preciocosto;
        venta.Text = producto.precioventa;
        url.Text = producto.foto;
        iduno = producto.id;
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
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        _ = llamadaGetJsonAsync($"https://hjqwpru.000webhostapp.com/updateproducts.php?id={iduno}&nombre={nombre.Text}&descripcion={descripcion.Text}&cantidad={cantidad.Text}&preciocosto={costo.Text}&precioventa={venta.Text}&foto={url.Text}");
        await Navigation.PopAsync();
    }
}