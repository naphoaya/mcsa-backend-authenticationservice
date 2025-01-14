using Newtonsoft.Json;

namespace MCSABackend.DTOs
{
    public class CountriesDto
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public int Name { get; set; }
    }
}
