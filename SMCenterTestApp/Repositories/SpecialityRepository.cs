using SMCenterTestApp.Adapters;
using SMCenterTestApp.DAL;
using SMCenterTestApp.DTO;

namespace SMCenterTestApp.Repositories
{
    public class SpecialityRepository
    {
        public SpecialityDTO Add(SpecialityDTO speciality)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                Speciality? specialityDb = db.Specialities
                    .FirstOrDefault(s => s.Name == speciality.Name);
                if (specialityDb == null)
                {
                    db.Specialities.Add(new Speciality
                    {
                        Name = speciality.Name
                    });

                    db.SaveChanges();
                }

                return GetByName(speciality.Name);
            }
        }

        public SpecialityDTO Add(string specialityName)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                Speciality? speciality = db.Specialities
                    .FirstOrDefault(s => s.Name == specialityName);
                if (speciality == null)
                {
                    db.Specialities.Add(new Speciality
                    {
                        Name = specialityName
                    });

                    db.SaveChanges();
                }

                return GetByName(specialityName);
            }
        }

        public SpecialityDTO GetByName(string specialityName)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                return new SpecialityAdapter().ToDTO(db.Specialities
                    .FirstOrDefault(s => s.Name == specialityName));
            }
        }

        public SpecialityDTO GetById(int specialityId)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                return new SpecialityAdapter().ToDTO(db.Specialities
                    .FirstOrDefault(s => s.Id == specialityId));
            }
        }
    }
}