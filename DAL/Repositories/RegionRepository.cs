using DAL.Adapters;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class RegionRepository
    {
        private MedicDBContext dbContext;

        public RegionRepository(MedicDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<RegionDTO?> Add(RegionDTO region)
        {
            RegionDTO? regionDb = await GetByNumber(region.Number);
            if (regionDb == null)
            {
                dbContext.Regions.Add(new Region
                {
                    Number = region.Number
                });
                dbContext.SaveChanges();
                return await GetByNumber(regionDb.Number);
            }
            return regionDb;
        }

        public async Task<RegionDTO> Add(int regionNumber)
        {
            RegionDTO? regionDb = await GetByNumber(regionNumber);
            if (regionDb == null)
            {
                dbContext.Regions.Add(new Region
                {
                    Number = regionNumber
                });
                await dbContext.SaveChangesAsync();
                return await GetByNumber(regionNumber);
            }

            return regionDb;
        }

        public async Task<RegionDTO?> GetById(int regionId)
        {
            return new RegionAdapter()
                .ToDTO(await dbContext.Regions
                    .FirstOrDefaultAsync(r =>
                        r.Id == regionId));
        }

        public async Task<RegionDTO?> GetByNumber(int regionNumber)
        {
            return new RegionAdapter()
                .ToDTO(await dbContext.Regions
                    .FirstOrDefaultAsync(r =>
                        r.Number == regionNumber));
        }

        public async Task<RegionDTO?> Edit(RegionDTO region)
        {
            Region? regionDb = await dbContext.Regions
                .FirstOrDefaultAsync(r => r.Id == region.Id);
            if (region != null)
            {
                regionDb.Number = region.Number;

                dbContext.Attach(regionDb);
                dbContext.Entry(regionDb).State = EntityState.Modified;

                await dbContext.SaveChangesAsync();
            }

            return await GetById(region.Id);
        }

        public async void Remove(RegionDTO region)
        {
            Region? regionDb = await dbContext.Regions
                .FirstOrDefaultAsync(r => r.Id == region.Id);
            if (regionDb != null)
            {
                dbContext.Regions.Remove(regionDb);

                await dbContext.SaveChangesAsync();
            }
        }
    }
}