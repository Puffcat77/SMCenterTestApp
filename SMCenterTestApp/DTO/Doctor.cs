namespace SMCenterTestApp.DTO
{
    public class Doctor
    {
        public Guid Id { get; set; }

        public string Initials { get; set; }

        public Cabinet Cabinet { get; set; }

        public SpecialityDTO Speciality { get; set; }

        public RegionDTO Region { get; set; }
    }
}