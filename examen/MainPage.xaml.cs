namespace examen
{
    public partial class MainPage : ContentPage
    {
        private void getName()
        {
            var Tienda = Preferences.Default.Get("nameTienda", "Tienda");
            title.Text = Tienda;
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
        }

        private async void imgbtnconf_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Preferencia());
        }
    }
}