using SMCenterTestApp.Adapters;
using SMCenterTestApp.DAL;
using SMCenterTestApp.DTO;

namespace SMCenterTestApp.Repositories
{
    public class RegionRepository
    {
        public RegionDTO Add(RegionDTO region)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                var regionDb = GetByNumber(region.Number);
                if (regionDb == null)
                {
                    db.Regions.Add(new Region
                    {
                        Number = region.Number
                    });
                    db.SaveChanges();
                }
                return GetByNumber(regionDb.Number);
            }
        }

        public RegionDTO Add(int regionNumber)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                Region region = db.Regions
                    .FirstOrDefault(r => r.Number == regionNumber);
                if (region == null)
                {
                    db.Regions.Add(new Region
                    {
                        Number = regionNumber
                    });
                    db.SaveChanges();
                }

                return GetByNumber(regionNumber);
            }
        }

        public RegionDTO GetById(int regionId)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                return new RegionAdapter()
                    .ToDTO(db.Regions
                        .FirstOrDefault(r =>
                            r.Id == regionId));
            }
        }

        public RegionDTO GetByNumber(int regionNumber)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                return new RegionAdapter()
                    .ToDTO(db.Regions
                        .FirstOrDefault(r =>
                            r.Number == regionNumber));
            }
        }

        public RegionDTO Edit(RegionDTO region)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                Region regionDb = db.Regions
                    .FirstOrDefault(r => r.Id == region.Id);
                if (region != null)
                {
                    regionDb.Number = region.Number;

                    db.Attach(regionDb);
                    db.Entry(regionDb).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;

                    db.SaveChanges();
                }

                return GetById(region.Id);
            }
        }

        public void Remove(RegionDTO region)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                Region regionDb = db.Regions
                    .FirstOrDefault(r => r.Id == region.Id);
                if (regionDb != null)
                {
                    db.Regions.Remove(regionDb);

                    db.SaveChanges();
                }
            }
        }
    }
}