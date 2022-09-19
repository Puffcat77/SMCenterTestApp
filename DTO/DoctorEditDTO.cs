using Newtonsoft.Json;

namespace DTO
{
    public class DoctorEditDTO
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("initials")]
        public string? Initials { get; set; } = null!;

        [JsonProperty("cabinetId")]
        public int? CabinetId { get; set; }

        [JsonProperty("specialityId")]
        public int? SpecialityId { get; set; }

        [JsonProperty("regionId")]
        public int? RegionId { get; set; }
    }
}