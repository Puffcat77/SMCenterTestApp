using DTO;
using DAL;
using DAL.Adapters;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CabinetRepository
    {
        MedicDBContext dbContext;

        public CabinetRepository(MedicDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public CabinetDTO Add(CabinetDTO cabinet)
        {
            CabinetDTO dbCabinet = GetByNumber(cabinet.Number);
            if (dbCabinet == null)
            {
                dbContext.Cabinets.Add(new Cabinet
                {
                    Number = cabinet.Number
                });
                dbContext.SaveChanges();
                return GetByNumber(cabinet.Number);
            }

            return dbCabinet;
        }

        public CabinetDTO Add(int cabinet)
        {
            CabinetDTO dbCabinet = GetByNumber(cabinet);
            if (dbCabinet == null)
            {
                Cabinet? newCabinet = new Cabinet
                {
                    Number = cabinet
                };
                dbContext.Cabinets.Add(newCabinet);
                dbContext.SaveChanges();
                return GetByNumber(cabinet);
            }

            return dbCabinet;
        }

        public CabinetDTO GetById(int cabinetId)
        {
            return new CabinetAdapter()
                .ToDTO(dbContext.Cabinets
                    .FirstOrDefault(c =>
                        c.Id == cabinetId));
        }

        public CabinetDTO GetByNumber(int cabinetNumber)
        {
            return new CabinetAdapter()
                .ToDTO(dbContext.Cabinets
                    .FirstOrDefault(c =>
                        c.Number == cabinetNumber));
        }

        public CabinetDTO Edit(CabinetDTO cabinet)
        {
            Cabinet? cabinetDb = dbContext.Cabinets
                .FirstOrDefault(c => c.Id == cabinet.Id);
            if (cabinet != null)
            {
                cabinetDb.Number = cabinet.Number;

                dbContext.Attach(cabinetDb);
                dbContext.Entry(cabinetDb).State =
                    EntityState.Modified;

                dbContext.SaveChanges();
            }

            return GetById(cabinet.Id);
        }

        public void Remove(CabinetDTO cabinet)
        {
            Cabinet cabinetDb = dbContext.Cabinets
                .FirstOrDefault(c => c.Id == cabinet.Id);
            if (cabinetDb != null)
            {
                dbContext.Cabinets.Remove(cabinetDb);

                dbContext.SaveChanges();
            }
        }
    }
}