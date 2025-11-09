using Microsoft.Maui.Controls;
using Pet_Care_Assistant.ViewModels;
using System.Linq;

namespace Pet_Care_Assistant.Views
{
    public partial class ServicesPage : ContentPage
    {
        // Păstrăm referința la ViewModel pentru a accesa datele
        private readonly ServicesViewModel _viewModel;

        public ServicesPage()
        {
            InitializeComponent();

            // 1. Preluarea ViewModel-ului
            _viewModel = (ServicesViewModel)BindingContext;

            // 2. Setarea Drawable-ului pentru GraphicsView
            PriceChartCanvas.Drawable = new PriceChartDrawable
            {
                // La inițializare, îi dăm lista de servicii
                Services = _viewModel.DisplayedServices.ToList()
            };

            // 3. Ne abonăm la evenimentul de schimbare a listei din ViewModel
            _viewModel.PropertyChanged += (sender, e) =>
            {
                // Dacă se schimbă lista afișată (din cauza filtrelor/sortării)
                if (e.PropertyName == nameof(ServicesViewModel.DisplayedServices))
                {
                    // Actualizăm datele în Drawable
                    ((PriceChartDrawable)PriceChartCanvas.Drawable).Services = _viewModel.DisplayedServices.ToList();

                    // Forțăm GraphicsView să se redeseneze
                    PriceChartCanvas.Invalidate();
                }
            };
        }
    }
}