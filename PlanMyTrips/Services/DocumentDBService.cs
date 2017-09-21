using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace PlanMyTrips
{
	public class DocumentDBService : IDocumentDBService
	{
		public List<CityToVisit> cities { get; private set; }

		DocumentClient client;
		Uri collectionLink;

		public DocumentDBService()
		{
			client = new DocumentClient(new Uri(Constants.EndpointUri), Constants.PrimaryKey);
			collectionLink = UriFactory.CreateDocumentCollectionUri(Constants.DatabaseName, Constants.CollectionName);
		}

		public async Task CreateDatabase(string databaseName)
		{
			try
			{
				await client.CreateDatabaseIfNotExistsAsync(new Database
				{
					Id = databaseName
				});
			}
			catch (DocumentClientException ex)
			{
				Debug.WriteLine("Error: ", ex.Message);
			}
		}

		public async Task CreateDocumentCollection(string databaseName, string collectionName)
		{
			try
			{
				// Create collection with 400 RU/s
				await client.CreateDocumentCollectionIfNotExistsAsync(
					UriFactory.CreateDatabaseUri(databaseName),
					new DocumentCollection
					{
						Id = collectionName
					},
					new RequestOptions
					{
						OfferThroughput = 400
					});
			}
			catch (DocumentClientException ex)
			{
				Debug.WriteLine("Error: ", ex.Message);
			}
		}

		public async Task<IList<CityToVisit>> GetCitiesAsync()
		{
			cities = new List<CityToVisit>();

			try
			{
				var query = client.CreateDocumentQuery<CityToVisit>(collectionLink)
								  .AsDocumentQuery();
				while (query.HasMoreResults)
				{
					cities.AddRange(await query.ExecuteNextAsync<CityToVisit>());
				}
			}
			catch (DocumentClientException ex)
			{
				Debug.WriteLine("Error: ", ex.Message);
			}

			return cities;
		}

		public async Task SaveCityAsync(CityToVisit city, bool isNewcity = false)
		{
			try
			{
				if (isNewcity)
				{
					await client.CreateDocumentAsync(collectionLink, city);
				}
				else
				{
					await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(Constants.DatabaseName, Constants.CollectionName, city.Id), city);
				}
			}
			catch (DocumentClientException ex)
			{
				Debug.WriteLine("Error: ", ex.Message);
			}
		}

		public async Task DeleteCityAsync(string id)
		{
			try
			{
				await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(Constants.DatabaseName, Constants.CollectionName, id));
			}
			catch (DocumentClientException ex)
			{
				Debug.WriteLine("Error: ", ex.Message);
			}
		}

		async Task DeleteDocumentCollection()
		{
			try
			{
				await client.DeleteDocumentCollectionAsync(collectionLink);
			}
			catch (DocumentClientException ex)
			{
				Debug.WriteLine("Error: ", ex.Message);
			}
		}

		async Task DeleteDatabase()
		{
			try
			{
				await client.DeleteDatabaseAsync(UriFactory.CreateDatabaseUri(Constants.DatabaseName));
			}
			catch (DocumentClientException ex)
			{
				Debug.WriteLine("Error: ", ex.Message);
			}
		}
	}
}
