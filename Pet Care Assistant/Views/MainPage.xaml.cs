using Pet_Care_Assistant.ViewModels;

namespace Pet_Care_Assistant.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //BindingContext = new PetFormViewModel();

        }

        private void OnGoToPetProfileClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("PetListPage");
        }
    }
}
