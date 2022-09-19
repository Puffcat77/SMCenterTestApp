using Microsoft.EntityFrameworkCore;
using DAL.Adapters;
using DTO;

namespace DAL.Repositories
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
                = new RegionRepository(dbContext).Add(patient.Region);

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

            return new PatientAdapter().ToEditDTO(dbContext, patientDb);
        }

        public PatientDTO GetById(Guid? id)
        {
            if (id == null)
            {
                return null;
            }
            Patient patient = dbContext.Patients
                .FirstOrDefault(p => p.Id == id.Value);
            return new PatientAdapter().ToDTO(dbContext, patient);
        }

        public List<PatientDTO> List()
        {
            PatientAdapter? adapter = new PatientAdapter();
            return dbContext.Patients
                .ToList()
                .Select(p => adapter.ToDTO(dbContext, p))
                .ToList();
        }

        public PatientEditDTO Edit(PatientEditDTO patient)
        {
            Patient? patientDb = dbContext 
                .Patients
                .FirstOrDefault(p => p.Id == patient.Id);

            if (patientDb == null)
            {
                throw new DbUpdateConcurrencyException();
            }

            patientDb.FirstName = patient.FirstName ?? patientDb.FirstName;
            patientDb.LastName = patient.LastName ?? patientDb.LastName;
            patientDb.Surname = patient.Surname ?? patientDb.Surname;
            patientDb.Address = patient.Address ?? patientDb.Address;
            patientDb.BirthDate = patient.BirthDate ?? patientDb.BirthDate;
            patientDb.Sex = patient.Sex ?? patientDb.Sex;
            patientDb.RegionId = patient.RegionId ?? patientDb.RegionId;

            dbContext.Patients.Attach(patientDb);
            dbContext.Entry(patientDb).State = EntityState.Modified;

            dbContext.SaveChanges();

            return GetEditDTOById(patientDb.Id);
        }

        public PatientEditDTO GetEditDTOById(Guid patientId)
        {
            return new PatientAdapter()
                .ToEditDTO(dbContext, dbContext.Patients
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