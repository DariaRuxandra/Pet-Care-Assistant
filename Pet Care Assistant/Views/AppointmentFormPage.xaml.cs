using Pet_Care_Assistant.ViewModels;

namespace Pet_Care_Assistant.Views
{
    public partial class AppointmentFormPage : ContentPage
    {
        public AppointmentFormPage(AppointmentFormViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
