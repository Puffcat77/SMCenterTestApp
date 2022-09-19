using DAL.Repositories;
using DTO;

namespace DAL.Adapters
{
    public class PatientAdapter
    {
        internal async Task<PatientDTO?> ToDTO(MedicDBContext dbContext, Patient? patient)
        {
            if (patient == null)
            {
                return null;
            }
            RegionDTO? region 
                = await new RegionRepository(dbContext).GetById(patient.RegionId);
            return new PatientDTO
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Surname = patient.Surname,
                BirthDate = patient.BirthDate,
                Sex = patient.Sex,
                Address = patient.Address,
                Region = region.Number
            };
        }

        internal async Task<PatientEditDTO?> ToEditDTO(MedicDBContext dbContext,
            Patient? patient)
        {
            if (patient == null)
            {
                return null;
            }
            RegionDTO? region
                = await new RegionRepository(dbContext).GetById(patient.RegionId);
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

        internal async Task<Patient?> ToDAL(MedicDBContext dbContext,
            PatientDTO? patient)
        {
            if (patient == null)
            {
                return null;
            }
            RegionDTO? region
                = await new RegionRepository(dbContext).GetByNumber(patient.Region);
            if (region == null)
            {
                region = await new RegionRepository(dbContext).Add(patient.Region);
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