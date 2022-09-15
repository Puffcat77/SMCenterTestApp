using Microsoft.EntityFrameworkCore;
using SMCenterTestApp.Adapters;
using SMCenterTestApp.DAL;
using SMCenterTestApp.DTO;

namespace SMCenterTestApp.Repositories
{
    public class PatientRepository
    {
        public PatientEditDTO Add(PatientDTO patient)
        {
            if (patient == null)
            {
                return null;
            }

            using (MedicDBContext db = new MedicDBContext())
            {
                RegionDTO? region
                    = new RegionRepository().Add(patient.RegionNumber);

                Patient? patientDb = db.Patients
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
                    db.Patients.Add(patientDb);
                    db.SaveChanges();
                }

                return new PatientAdapter().ToEditDTO(patientDb);
            }
        }

        public PatientDTO GetById(Guid? id)
        {
            if (id == null)
            {
                return null;
            }
            using (MedicDBContext db = new MedicDBContext())
            {
                Patient patient = db.Patients
                    .FirstOrDefault(p => p.Id == id.Value);
                return new PatientAdapter().ToDTO(patient);
            }
        }

        internal List<PatientDTO> List()
        {
            PatientAdapter? adapter = new PatientAdapter();
            using (MedicDBContext db = new MedicDBContext())
            {
                return db.Patients
                    .ToList()
                    .Select(adapter.ToDTO)
                    .ToList();
            }
        }

        internal PatientEditDTO Edit(PatientEditDTO patient)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                Patient? patientDb = db
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

                db.Patients.Attach(patientDb);
                db.Entry(patientDb).State = EntityState.Modified;

                db.SaveChanges();

                return GetEditDTOById(patientDb.Id);
            }
        }

        public PatientEditDTO GetEditDTOById(Guid patientId)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                return new PatientAdapter()
                    .ToEditDTO(db.Patients
                        .FirstOrDefault(p =>
                            p.Id == patientId));
            }
        }

        public void Remove(Guid id)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                Patient patientDb = db.Patients
                    .FirstOrDefault(p => p.Id == id);
                if (patientDb != null)
                {
                    db.Patients.Remove(patientDb);

                    db.SaveChanges();
                }
            }
        }
    }
}