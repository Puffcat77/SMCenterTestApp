using DTO;

namespace DAL.Adapters
{
    public class SpecialityAdapter
    {
        internal SpecialityDTO ToDTO(Speciality? speciality)
        {
            if (speciality == null)
            {
                return null;
            }

            return new SpecialityDTO()
            {
                Id = speciality.Id,
                Name = speciality.Name
            };
        }
    }
}