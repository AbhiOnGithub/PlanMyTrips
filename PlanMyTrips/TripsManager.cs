using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanMyTrips
{
	public class TripsManager
	{
		IDocumentDBService documentDBService;

		public TripsManager(IDocumentDBService service)
		{
			documentDBService = service;
		}

		public Task CreateDatabase(string databaseName)
		{
			return documentDBService.CreateDatabase(databaseName);
		}

		public Task CreateDocumentCollection(string databaseName, string collectionName)
		{
			return documentDBService.CreateDocumentCollection(databaseName, collectionName);
		}

		public Task<IList<CityToVisit>> GetCitiesAsync()
		{
			return documentDBService.GetCitiesAsync();
		}

		public Task SaveCityAsync(CityToVisit city, bool isNewcity = false)
		{
			return documentDBService.SaveCityAsync(city, isNewcity);
		}

		public Task DeleteCityAsync(CityToVisit city)
		{
			return documentDBService.DeleteCityAsync(city.Id);
		}
	}
}