using SMCenterTestApp.DAL;
using SMCenterTestApp.DTO;
using SMCenterTestApp.Repositories;

namespace SMCenterTestApp.Adapters
{
    public class PatientAdapter
    {
        internal PatientDTO ToDTO(Patient? patient)
        {
            if (patient == null)
            {
                return null;
            }
            RegionDTO region 
                = new RegionRepository().GetById(patient.RegionId);
            return new PatientDTO
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Surname = patient.Surname,
                BirthDate = patient.BirthDate,
                Sex = patient.Sex,
                Address = patient.Address,
                RegionNumber = region.Number
            };
        }

        internal PatientEditDTO ToEditDTO(Patient patient)
        {
            if (patient == null)
            {
                return null;
            }
            RegionDTO region
                = new RegionRepository().GetById(patient.RegionId);
            return new PatientEditDTO
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Surname = patient.Surname,
                BirthDate = patient.BirthDate,
                Sex = patient.Sex,
                Address = patient.Address,
                RegionId = region.Id
            };
        }

        internal Patient ToDAL(PatientDTO patient)
        {
            if (patient == null)
            {
                return null;
            }
            RegionDTO region
                = new RegionRepository().GetByNumber(patient.RegionNumber);
            if (region == null)
            {
                new RegionRepository().Add(patient.RegionNumber);
                region = new RegionRepository()
                    .GetByNumber(patient.RegionNumber);
            }
            return new Patient
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Surname = patient.Surname,
                BirthDate = patient.BirthDate,
                Sex = patient.Sex,
                Address = patient.Address,
                RegionId = region.Id
            };
        }
    }
}