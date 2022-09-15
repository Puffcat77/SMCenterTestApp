using SMCenterTestApp.DAL;
using SMCenterTestApp.DTO;
using SMCenterTestApp.Repositories;

namespace SMCenterTestApp.Adapters
{
    public class DoctorAdapter
    {
        internal DoctorDTO ToDTO(Doctor doctorDb)
        {
            if (doctorDb == null)
            {
                return null;
            }

            return new DoctorDTO
            {
                Id = doctorDb.Id,
                Initials = doctorDb.Initials,
                Speciality = new SpecialityRepository()
                    .GetById(doctorDb.SpecialityId).Name,
                Region = new RegionRepository()
                    .GetById(doctorDb.RegionId).Number,
                Cabinet = new CabinetRepository()
                    .GetById(doctorDb.CabinetId).Number,
            };
        }

        internal DoctorEditDTO ToEditDTO(Doctor doctorDb)
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