using Microsoft.EntityFrameworkCore;
using SMCenterTestApp.Adapters;
using SMCenterTestApp.DAL;
using SMCenterTestApp.DTO;

namespace SMCenterTestApp.Repositories
{
    public class DoctorRepository
    {
        private MedicDBContext dbContext;

        public DoctorRepository(MedicDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public DoctorEditDTO Add(DoctorDTO doctor)
        {
            CabinetDTO? cabinet
                = new CabinetRepository().Add(doctor.Cabinet);
            SpecialityDTO? speciality
                = new SpecialityRepository().Add(doctor.Speciality);
            RegionDTO? region
                = new RegionRepository().Add(doctor.Region);

            Doctor? doctorDb = dbContext.Doctors
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
                dbContext.Doctors.Add(doctorDb);
                dbContext.SaveChanges();
            }

            return new DoctorAdapter().ToEditDTO(doctorDb);
        }

        public DoctorDTO GetById(Guid doctorId)
        {
            return new DoctorAdapter()
                .ToDTO(dbContext.Doctors
                    .FirstOrDefault(d =>
                        d.Id == doctorId));
        }

        public DoctorEditDTO GetEditDTOById(Guid doctorId)
        {
            return new DoctorAdapter()
                .ToEditDTO(dbContext.Doctors
                    .FirstOrDefault(d =>
                        d.Id == doctorId));
        }

        public DoctorEditDTO Edit(DoctorEditDTO doctor)
        {
            Doctor doctorDb = dbContext.Doctors
                .FirstOrDefault(r => r.Id == doctor.Id);
            if (doctorDb == null)
            {
                throw new DbUpdateConcurrencyException();
            }

            doctorDb.RegionId = doctor.RegionId;
            doctorDb.SpecialityId = doctor.SpecialityId;
            doctorDb.CabinetId = doctor.CabinetId;

            dbContext.Attach(doctorDb);
            dbContext.Entry(doctorDb).State = EntityState.Modified;

            dbContext.SaveChanges();


            return GetEditDTOById(doctor.Id);
        }


        public void Remove(Guid id)
        {
            Doctor doctorDb = dbContext.Doctors
                .FirstOrDefault(r => r.Id == id);
            if (doctorDb != null)
            {
                dbContext.Doctors.Remove(doctorDb);

                dbContext.SaveChanges();
            }
        }

        public List<DoctorDTO> List()
        {
            DoctorAdapter? adapter = new DoctorAdapter();
            return dbContext.Doctors
                .ToList()
                .Select(adapter.ToDTO)
                .ToList();
        }
    }
}