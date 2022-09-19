using DAL.Adapters;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class SpecialityRepository
    {
        private MedicDBContext dbContext;

        public SpecialityRepository(MedicDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<SpecialityDTO?> Add(SpecialityDTO speciality)
        {
            SpecialityDTO? specialityDb = await GetByName(speciality.Name);
            if (specialityDb == null)
            {
                dbContext.Specialities.Add(new Speciality
                {
                    Name = speciality.Name
                });

                await dbContext.SaveChangesAsync();

                return await GetByName(speciality.Name);
            }

            return specialityDb;
        }

        public async Task<SpecialityDTO?> Add(string specialityName)
        {
            SpecialityDTO? speciality = await GetByName(specialityName);
            if (speciality == null)
            {
                dbContext.Specialities.Add(new Speciality
                {
                    Name = specialityName
                });

                await dbContext.SaveChangesAsync();

                return await GetByName(specialityName);
            }

            return speciality;
        }

        public async Task<SpecialityDTO?> GetByName(string specialityName)
        {
            return new SpecialityAdapter().ToDTO(await dbContext.Specialities
                .FirstOrDefaultAsync(s => s.Name == specialityName));
        }

        public async Task<SpecialityDTO?> GetById(int specialityId)
        {
            return new SpecialityAdapter().ToDTO(await dbContext.Specialities
                .FirstOrDefaultAsync(s => s.Id == specialityId));
        }
    }
}