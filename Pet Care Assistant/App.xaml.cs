using Pet_Care_Assistant.Services;
using SQLite;

namespace Pet_Care_Assistant
{
    public partial class App : Application
    {
        private readonly SqliteConnectionFactory _connectionFactory;
        public App(SqliteConnectionFactory connectionFactory)
        {
            InitializeComponent();
            _connectionFactory = connectionFactory;
        }

        protected override async void OnStart()
        {
            ISQLiteAsyncConnection database = _connectionFactory.CreateConnection();
            await database.CreateTableAsync<Models.PetDto>();
            base.OnStart();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}