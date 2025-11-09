using Pet_Care_Assistant.Services;
using SQLite;

namespace Pet_Care_Assistant
{
    public partial class App : Application
    {
        private readonly SqliteConnectionFactory _connectionFactory;

        // Constructorul tău cu injecția dependențelor (Corect)
        public App(SqliteConnectionFactory connectionFactory)
        {
            InitializeComponent();
            _connectionFactory = connectionFactory;

            // 🔥 FIX-UL PENTRU TEMĂ: Adăugat aici
            // Setăm tema implicită la LIGHT. 
            Application.Current.UserAppTheme = AppTheme.Light;
        }

        // Logica ta pentru baza de date (Corectă)
        protected override async void OnStart()
        {
            ISQLiteAsyncConnection database = _connectionFactory.CreateConnection();
            await database.CreateTableAsync<Models.PetDto>();
            base.OnStart();
        }

        // Logica ta pentru crearea ferestrei (Corectă)
        protected override Window CreateWindow(IActivationState? activationState)
        {
            // Setează pagina principală aici
            return new Window(new AppShell());
        }
    }
}