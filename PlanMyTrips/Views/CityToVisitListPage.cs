using System;
using Xamarin.Forms;

namespace PlanMyTrips
{
	public partial class CityToVisitListPage : ContentPage
	{
		public CityToVisitListPage()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			await App.TripsManager.CreateDatabase(Constants.DatabaseName);
			await App.TripsManager.CreateDocumentCollection(Constants.DatabaseName, Constants.CollectionName);
			listView.ItemsSource = await App.TripsManager.GetCitiesAsync();
		}

		async void OnCityAdded(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new CityToVisitPage(true)
			{
				BindingContext = new CityToVisit
				{
					Id = Guid.NewGuid().ToString()
				}
			});
		}

		async void OnCitySelected(object sender, SelectedItemChangedEventArgs e)
		{
			await Navigation.PushAsync(new CityToVisitPage
			{
				BindingContext = e.SelectedItem as CityToVisit
			});
		}
	}
}
