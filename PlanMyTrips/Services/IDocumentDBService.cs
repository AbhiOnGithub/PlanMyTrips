using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanMyTrips
{
	public interface IDocumentDBService
	{
		Task CreateDatabase(string databaseName);

		Task CreateDocumentCollection(string databaseName, string collectionName);

		Task<List<CityToVisit>> GetCitiesAsync();

		Task SaveCityAsync(CityToVisit city, bool isNewCity);

		Task DeleteCityAsync(string id);
	}
}
