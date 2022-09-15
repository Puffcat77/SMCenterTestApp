namespace SMCenterTestApp.DTO
{
    public class DoctorDTO
    {
        public Guid Id { get; set; }

        public string Initials { get; set; }

        public int Cabinet { get; set; }

        public string Speciality { get; set; }

        public int Region { get; set; }
    }
}