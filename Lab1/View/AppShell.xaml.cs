namespace Lab1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CreatePage), typeof(CreatePage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }
    }
}
