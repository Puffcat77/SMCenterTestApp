using DAL.Repositories;
using DTO;

namespace DAL.Adapters
{
    public class DoctorAdapter
    {
        internal async Task<DoctorDTO?> ToDTO(MedicDBContext dbContext, Doctor? doctorDb)
        {
            if (doctorDb == null)
            {
                return null;
            }

            return new DoctorDTO
            {
                Id = doctorDb.Id,
                Initials = doctorDb.Initials,
                Speciality = (await new SpecialityRepository(dbContext)
                    .GetById(doctorDb.SpecialityId)).Name,
                Region = (await new RegionRepository(dbContext)
                    .GetById(doctorDb.RegionId)).Number,
                Cabinet = (await new CabinetRepository(dbContext)
                    .GetById(doctorDb.CabinetId)).Number,
            };
        }

        internal DoctorEditDTO? ToEditDTO(Doctor? doctorDb)
        {
            if (doctorDb == null)
            {
                return null;
            }

            return new DoctorEditDTO
            {
                Id = doctorDb.Id,
                Initials = doctorDb.Initials,
                SpecialityId = doctorDb.SpecialityId,
                RegionId = doctorDb.RegionId,
                CabinetId = doctorDb.CabinetId,
            };
        }
    }
}