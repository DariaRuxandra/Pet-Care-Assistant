namespace Pet_Care_Assistant.Views
{
    public partial class AboutUsPage : ContentPage
    {
        public AboutUsPage()
        {
            // Aceast? linie este esen?ial? ?i va func?iona acum
            InitializeComponent();
        }

        /// <summary>
        /// Gestioneaz? evenimentul de click pe sec?iunea "Termeni ?i Condi?ii".
        /// Afi?eaz? sau ascunde textul complet ?i rote?te s?geata.
        /// </summary>
        private async void OnTermsTapped(object sender, TappedEventArgs e)
        {
            // Inverseaz? vizibilitatea con?inutului
            TermsContentLabel.IsVisible = !TermsContentLabel.IsVisible;

            if (TermsContentLabel.IsVisible)
            {
                // Rote?te s?geata în jos
                await ArrowLabel.RotateTo(90, 100); // 100ms
            }
            else
            {
                // Rote?te s?geata la loc
                await ArrowLabel.RotateTo(0, 100); // 100ms
            }
        }
    }
}