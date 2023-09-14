using System.Collections.ObjectModel;

namespace examen;

public partial class AgregarProductos : ContentPage
{
	public AgregarProductos()
	{
		InitializeComponent();
	}

    private void tienda_Completed(object sender, EventArgs e)
    {
        img.Source = ImageSource.FromUri(new Uri(url.Text));
    }

    public class Producto
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string cantidad { get; set; }
        public string costo { get; set; }
        public string venta { get; set; }
        public string foto { get; set; }
    }


    ObservableCollection<Producto> datos = new ObservableCollection<Producto>();
    public async Task llamadaGetJsonAsync(string url)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(url);
        var response = await client.GetAsync(client.BaseAddress);
        response.EnsureSuccessStatusCode();
        var jsonResult = response.Content.ReadAsStringAsync().Result;
        var listado = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Producto>>(jsonResult);
        datos = listado;
    }
    private async void aceptar(object sender, EventArgs e)
    {
        try
        {
            _ = llamadaGetJsonAsync($"https://hjqwpru.000webhostapp.com/insertproducts.php?snombre={nombre.Text}&descripcion={descripcion.Text}&cantidad={cantidad.Text}&preciocosto={costo.Text}&precioventa={venta.Text}&foto={url.Text}");
            await Navigation.PopAsync();

        }
        catch
        {
            await DisplayAlert("Mensa", "Error al tratar de conectarse", "ok");
        }
    }
}