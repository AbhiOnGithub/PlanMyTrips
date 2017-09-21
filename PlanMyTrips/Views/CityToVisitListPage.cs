using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace PlanMyTrips
{
	public partial class CityToVisitListPage : ContentPage
	{
        private ObservableCollection<CityToVisit> cities;
        
		public CityToVisitListPage()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
            this.IsBusy = true;
			await App.TripsManager.CreateDatabase(Constants.DatabaseName);
			await App.TripsManager.CreateDocumentCollection(Constants.DatabaseName, Constants.CollectionName);
            cities =new ObservableCollection<CityToVisit>(await App.TripsManager.GetCitiesAsync() );

            listView.ItemsSource = cities;
            this.IsBusy = false;
		}

        public async void OnShowAllClicked(object sender, EventArgs e)
        {
			await btnShowAll.ScaleTo(0.95, 50, Easing.CubicOut);
			await btnShowAll.ScaleTo(1, 50, Easing.CubicIn);

            listView.ItemsSource = cities;
        }

		public async void OnVisitedClicked(object sender, EventArgs e)
		{
			await btnVisited.ScaleTo(0.95, 50, Easing.CubicOut);
			await btnVisited.ScaleTo(1, 50, Easing.CubicIn);

            listView.ItemsSource = cities.Where(c => c.Visited.Equals(true) ).ToList();
		}

		public async void OnNotVisitedClicked(object sender, EventArgs e)
		{
			await btnNotVisited.ScaleTo(0.95, 50, Easing.CubicOut);
			await btnNotVisited.ScaleTo(1, 50, Easing.CubicIn);

            listView.ItemsSource = cities.Where(c => c.Visited.Equals(false)).ToList();
		}

		public async void OnCityAdded(object sender, EventArgs e)
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