using DAL.Adapters;
using DTO;

namespace DAL.Repositories
{
    public class RegionRepository
    {
        private MedicDBContext dbContext;

        public RegionRepository(MedicDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public RegionDTO Add(RegionDTO region)
        {
            RegionDTO? regionDb = GetByNumber(region.Number);
            if (regionDb == null)
            {
                dbContext.Regions.Add(new Region
                {
                    Number = region.Number
                });
                dbContext.SaveChanges();
                return GetByNumber(regionDb.Number);
            }
            return regionDb;
        }

        public RegionDTO Add(int regionNumber)
        {
            RegionDTO? regionDb = GetByNumber(regionNumber);
            if (regionDb == null)
            {
                dbContext.Regions.Add(new Region
                {
                    Number = regionNumber
                });
                dbContext.SaveChanges();
                return GetByNumber(regionNumber);
            }

            return regionDb;
        }

        public RegionDTO GetById(int regionId)
        {
            return new RegionAdapter()
                .ToDTO(dbContext.Regions
                    .FirstOrDefault(r =>
                        r.Id == regionId));
        }

        public RegionDTO GetByNumber(int regionNumber)
        {
            return new RegionAdapter()
                .ToDTO(dbContext.Regions
                    .FirstOrDefault(r =>
                        r.Number == regionNumber));
        }

        public RegionDTO Edit(RegionDTO region)
        {
            Region regionDb = dbContext.Regions
                .FirstOrDefault(r => r.Id == region.Id);
            if (region != null)
            {
                regionDb.Number = region.Number;

                dbContext.Attach(regionDb);
                dbContext.Entry(regionDb).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;

                dbContext.SaveChanges();
            }

            return GetById(region.Id);
        }

        public void Remove(RegionDTO region)
        {
            Region regionDb = dbContext.Regions
                .FirstOrDefault(r => r.Id == region.Id);
            if (regionDb != null)
            {
                dbContext.Regions.Remove(regionDb);

                dbContext.SaveChanges();
            }
        }
    }
}