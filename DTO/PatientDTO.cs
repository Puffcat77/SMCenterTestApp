using Newtonsoft.Json;

namespace DTO
{
	public class PatientDTO
	{
		[JsonProperty("id")]
		public Guid Id { get; set; }

		[JsonProperty("firstName")]
		public string FirstName { get; set; }

		[JsonProperty("lastName")]
		public string LastName { get; set; }

		[JsonProperty("surname")]
		public string Surname { get; set; }

		[JsonProperty("address")]
		public string Address { get; set; }

		[JsonProperty("birthDate")]
		public DateTime BirthDate { get; set; }

		[JsonProperty("sex")]
		public bool Sex { get; set; }

		[JsonProperty("region")]
		public int Region { get; set; }
    }
}