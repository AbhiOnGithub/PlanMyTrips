using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PlanMyTrips
{
	public partial class App : Application
	{
		public static TripsManager TripsManager { get; private set; }

		public App()
		{
			InitializeComponent();

			TripsManager = new TripsManager(new DocumentDBService());
			MainPage = new NavigationPage(new CityToVisitListPage());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
