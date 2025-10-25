using Pet_Care_Assistant.Views;

namespace Pet_Care_Assistant
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("PetListPage", typeof(PetListPage));
        }
    }
}
