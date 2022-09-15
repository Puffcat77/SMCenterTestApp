using Microsoft.EntityFrameworkCore;
using SMCenterTestApp.Adapters;
using SMCenterTestApp.DAL;
using SMCenterTestApp.DTO;

namespace SMCenterTestApp.Repositories
{
    public class PatientRepository
    {
        MedicDBContext dbContext;

        public PatientRepository(MedicDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public PatientEditDTO Add(PatientDTO patient)
        {
            if (patient == null)
            {
                return null;
            }

            RegionDTO? region
                = new RegionRepository().Add(patient.RegionNumber);

            Patient? patientDb = dbContext.Patients
                .FirstOrDefault(p =>
                    p.FirstName == patient.FirstName
                    && p.LastName == patient.LastName
                    && p.Surname == patient.Surname
                    && p.Address == patient.Address
                    && p.Sex == patient.Sex
                    && p.BirthDate == patient.BirthDate
                    && p.RegionId == region.Id);
            if (patientDb == null)
            {
                patientDb = new Patient
                {
                    Id = Guid.NewGuid(),
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Surname = patient.Surname,
                    Address = patient.Address,
                    Sex = patient.Sex,
                    BirthDate = patient.BirthDate,
                    RegionId = region.Id
                };
                dbContext.Patients.Add(patientDb);
                dbContext.SaveChanges();
            }

            return new PatientAdapter().ToEditDTO(patientDb);
        }

        public PatientDTO GetById(Guid? id)
        {
            if (id == null)
            {
                return null;
            }
            Patient patient = dbContext.Patients
                .FirstOrDefault(p => p.Id == id.Value);
            return new PatientAdapter().ToDTO(patient);
        }

        internal List<PatientDTO> List()
        {
            PatientAdapter? adapter = new PatientAdapter();
            return dbContext.Patients
                .ToList()
                .Select(adapter.ToDTO)
                .ToList();
        }

        internal PatientEditDTO Edit(PatientEditDTO patient)
        {
            Patient? patientDb = dbContext 
                .Patients
                .FirstOrDefault(p => p.Id == patient.Id);

            if (patientDb == null)
            {
                throw new DbUpdateConcurrencyException();
            }

            patientDb.FirstName = patient.FirstName;
            patientDb.LastName = patient.LastName;
            patientDb.Surname = patient.Surname;
            patientDb.Address = patient.Address;
            patientDb.BirthDate = patient.BirthDate;
            patientDb.Sex = patient.Sex;
            patientDb.RegionId = patient.RegionId;

            dbContext.Patients.Attach(patientDb);
            dbContext.Entry(patientDb).State = EntityState.Modified;

            dbContext.SaveChanges();

            return GetEditDTOById(patientDb.Id);
        }

        public PatientEditDTO GetEditDTOById(Guid patientId)
        {
            return new PatientAdapter()
                .ToEditDTO(dbContext.Patients
                    .FirstOrDefault(p =>
                        p.Id == patientId));
        }

        public void Remove(Guid id)
        {
            Patient patientDb = dbContext.Patients
                .FirstOrDefault(p => p.Id == id);
            if (patientDb != null)
            {
                dbContext.Patients.Remove(patientDb);

                dbContext.SaveChanges();
            }
        }
    }
}