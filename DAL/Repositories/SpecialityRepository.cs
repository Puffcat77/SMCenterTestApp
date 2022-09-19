using DAL.Adapters;
using DTO;

namespace DAL.Repositories
{
    public class SpecialityRepository
    {
        private MedicDBContext dbContext;

        public SpecialityRepository(MedicDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public SpecialityDTO Add(SpecialityDTO speciality)
        {
            SpecialityDTO? specialityDb = GetByName(speciality.Name);
            if (specialityDb == null)
            {
                dbContext.Specialities.Add(new Speciality
                {
                    Name = speciality.Name
                });

                dbContext.SaveChanges();

                return GetByName(speciality.Name);
            }

            return specialityDb;
        }

        public SpecialityDTO Add(string specialityName)
        {
            SpecialityDTO? speciality = GetByName(specialityName);
            if (speciality == null)
            {
                dbContext.Specialities.Add(new Speciality
                {
                    Name = specialityName
                });

                dbContext.SaveChanges();

                return GetByName(specialityName);
            }

            return speciality;
        }

        public SpecialityDTO GetByName(string specialityName)
        {
            return new SpecialityAdapter().ToDTO(dbContext.Specialities
                .FirstOrDefault(s => s.Name == specialityName));
        }

        public SpecialityDTO GetById(int specialityId)
        {
            return new SpecialityAdapter().ToDTO(dbContext.Specialities
                .FirstOrDefault(s => s.Id == specialityId));
        }
    }
}