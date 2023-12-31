﻿namespace examen
{
    public partial class MainPage : ContentPage
    {
        public string ur;
        private void getName() //iniciar todo en la barra superior
        {
            var Tienda = Preferences.Default.Get("nameTienda", "Tienda");
            title.Text = Tienda;
            ur = Preferences.Default.Get("urlTienda", "");
            if (ur != "")
            {
                imglogo.Source = ImageSource.FromUri(new Uri(ur));
            }
        }
        public MainPage()
        {
            InitializeComponent();
            getName();
        }
        protected override void OnAppearing() 
        { 
            base.OnAppearing();
            getName();
            imglogomenu.Source = ImageSource.FromUri(new Uri(ur));
        }
        private async void imgbtnconf_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Preferencia());
        }
        
        private async void productos_clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Productos());
        }
        private async void showclient_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Clientes());
        }
    }
}