using Newtonsoft.Json;

namespace DTO
{
    public class DoctorDTO
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("initials")]
        public string Initials { get; set; }

        [JsonProperty("cabinet")]
        public int Cabinet { get; set; }

        [JsonProperty("speciality")]
        public string Speciality { get; set; }

        [JsonProperty("region")]
        public int Region { get; set; }
    }
}