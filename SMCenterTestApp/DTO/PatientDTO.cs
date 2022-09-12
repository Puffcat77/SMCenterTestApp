namespace SMCenterTestApp.DTO
{
	public class PatientDTO
    {
		public Guid Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Surname { get; set; }

		public string Address { get; set; }

		public DateTime BirthDate { get; set; }

		public bool Sex { get; set; }

		public RegionDTO Region { get; set; }
    }
}