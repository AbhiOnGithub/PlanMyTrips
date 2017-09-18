using System;
using Xamarin.Forms;

namespace PlanMyTrips
{
    public partial class CityToVisitPage : ContentPage
    {
        bool isNewcity;

        public CityToVisitPage(bool isNew = false)
        {
            InitializeComponent();

            isNewcity = isNew;

        }


        async void OnSaveActivated(object sender, EventArgs e)
        {
            var CityToVisit = (CityToVisit)BindingContext;
            await App.TripsManager.SaveCityAsync(CityToVisit, isNewcity);
            await Navigation.PopAsync();
        }

        async void OnDeleteActivated(object sender, EventArgs e)
        {
            var CityToVisit = (CityToVisit)BindingContext;
            await App.TripsManager.DeleteCityAsync(CityToVisit);
            await Navigation.PopAsync();
        }

        async void OnCancelActivated(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
