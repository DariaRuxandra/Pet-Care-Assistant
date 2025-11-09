// Pet_Care_Assistant/Views/HealthCheckerPage.xaml.cs
using Microsoft.Maui.Controls;
using Pet_Care_Assistant.ViewModels;

namespace Pet_Care_Assistant.Views
{
    public partial class HealthCheckerPage : ContentPage
    {
        public HealthCheckerPage()
        {
            InitializeComponent();
            this.BindingContext = new HealthCheckerViewModel();
        }

        private void AnimalType_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                var radioButton = sender as RadioButton;
                var viewModel = BindingContext as HealthCheckerViewModel;

                if (viewModel != null && radioButton != null)
                {
                    viewModel.SelectedAnimalType = radioButton.Content.ToString();
                    viewModel.ApplyFiltersCommand.Execute(null);
                }
            }
        }
    }
}