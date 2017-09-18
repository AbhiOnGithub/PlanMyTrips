using Newtonsoft.Json;

namespace PlanMyTrips
{
    public class CityToVisit
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "planneddays")]
        public string PlannedDays { get; set; }

        [JsonProperty(PropertyName = "actualdays")]
        public int ActualDays { get; set; }

        [JsonProperty(PropertyName = "visited")]
        public bool Visited { get; set; }

        [JsonProperty(PropertyName = "notes")]
        public string Notes { get; set; }
    }
}