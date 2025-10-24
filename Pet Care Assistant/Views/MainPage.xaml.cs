namespace Pet_Care_Assistant.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnGoToPetProfileClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("SettingsPage");
        }
    }
}
