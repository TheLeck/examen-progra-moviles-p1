namespace examen;

public partial class Preferencia : ContentPage
{
	public Preferencia()
	{
		InitializeComponent();
	}

    private void tienda_Completed(object sender, EventArgs e)
    {
		img.Source = ImageSource.FromUri(new Uri(urls.Text));
    }

    private async void savePre_Clicked(object sender, EventArgs e)
    {
        Preferences.Default.Set("nameTienda", tienda.Text);
        Preferences.Default.Set("urlTienda", urls.Text);
        await Navigation.PopAsync();
    }
}