using Microsoft.EntityFrameworkCore;
using SMCenterTestApp.Adapters;
using SMCenterTestApp.DAL;
using SMCenterTestApp.DTO;

namespace SMCenterTestApp.Repositories
{
    public class DoctorRepository
    {
        public DoctorEditDTO Add(DoctorDTO doctor)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                CabinetDTO? cabinet
                    = new CabinetRepository().Add(doctor.Cabinet);
                SpecialityDTO? speciality
                    = new SpecialityRepository().Add(doctor.Speciality);
                RegionDTO? region
                    = new RegionRepository().Add(doctor.Region);

                Doctor? doctorDb = db.Doctors
                    .FirstOrDefault(d =>
                        d.Initials == doctor.Initials
                        && d.CabinetId == cabinet.Id
                        && d.SpecialityId == speciality.Id
                        && d.RegionId == region.Id); 
                if (doctorDb == null)
                {
                    doctorDb = new Doctor
                    {
                        Id = Guid.NewGuid(),
                        Initials = doctor.Initials,
                        CabinetId = cabinet.Id,
                        SpecialityId = speciality.Id,
                        RegionId = region.Id
                    };
                    db.Doctors.Add(doctorDb);
                    db.SaveChanges();
                }

                return new DoctorAdapter().ToEditDTO(doctorDb);
            }
        }

        public DoctorDTO GetById(Guid doctorId)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                return new DoctorAdapter()
                    .ToDTO(db.Doctors
                        .FirstOrDefault(d =>
                            d.Id == doctorId));
            }
        }

        public DoctorEditDTO GetEditDTOById(Guid doctorId)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                return new DoctorAdapter()
                    .ToEditDTO(db.Doctors
                        .FirstOrDefault(d =>
                            d.Id == doctorId));
            }
        }

        public DoctorEditDTO Edit(DoctorEditDTO doctor)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                Doctor doctorDb = db.Doctors
                    .FirstOrDefault(r => r.Id == doctor.Id);
                if (doctorDb == null)
                {
                    throw new DbUpdateConcurrencyException();
                }

                doctorDb.RegionId = doctor.RegionId;
                doctorDb.SpecialityId = doctor.SpecialityId;
                doctorDb.CabinetId = doctor.CabinetId;

                db.Attach(doctorDb);
                db.Entry(doctorDb).State = EntityState.Modified;

                db.SaveChanges();


                return GetEditDTOById(doctor.Id);
            }
        }

        public void Remove(Guid id)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                Doctor doctorDb = db.Doctors
                    .FirstOrDefault(r => r.Id == id);
                if (doctorDb != null)
                {
                    db.Doctors.Remove(doctorDb);

                    db.SaveChanges();
                }
            }
        }

        public List<DoctorDTO> List()
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                DoctorAdapter? adapter = new DoctorAdapter();
                return db.Doctors
                    .ToList()
                    .Select(adapter.ToDTO)
                    .ToList();
            }
        }
    }
}