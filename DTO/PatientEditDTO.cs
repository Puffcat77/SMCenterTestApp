using Newtonsoft.Json;

namespace DTO
{
    public class PatientEditDTO
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("lastName")]
        public string? LastName { get; set; } = null!;

        [JsonProperty("firstName")]
        public string? FirstName { get; set; } = null!;

        [JsonProperty("surname")]
        public string? Surname { get; set; } = null!;

        [JsonProperty("address")]
        public string? Address { get; set; } = null!;

        [JsonProperty("birthDate")]
        public DateTime? BirthDate { get; set; }

        [JsonProperty("sex")]
        public bool? Sex { get; set; }

        [JsonProperty("regionId")]
        public int? RegionId { get; set; }
    }
}