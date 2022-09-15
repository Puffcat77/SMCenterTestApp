using Microsoft.EntityFrameworkCore;
using SMCenterTestApp.Adapters;
using SMCenterTestApp.DAL;
using SMCenterTestApp.DTO;

namespace SMCenterTestApp.Repositories
{
    public class CabinetRepository
    {
        public CabinetDTO Add(CabinetDTO cabinet)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                Cabinet? cabinetDb = db.Cabinets
                    .FirstOrDefault(c => c.Number == cabinet.Number);
                if (cabinetDb == null)
                {
                    db.Cabinets.Add(new Cabinet
                    {
                        Number = cabinet.Number
                    });
                    db.SaveChanges();
                }
                return GetByNumber(cabinet.Number);
            }
        }

        public CabinetDTO Add(int cabinet)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                CabinetDTO dbCabinet = GetByNumber(cabinet);
                if (dbCabinet == null)
                {
                    db.Cabinets.Add(new Cabinet
                    {
                        Number = cabinet
                    });
                    db.SaveChanges();
                }

                return GetByNumber(cabinet);
            }
        }

        public CabinetDTO GetById(int cabinetId)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                return new CabinetAdapter()
                    .ToDTO(db.Cabinets
                        .FirstOrDefault(c =>
                            c.Id == cabinetId));
            }
        }

        public CabinetDTO GetByNumber(int cabinetNumber)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                return new CabinetAdapter()
                    .ToDTO(db.Cabinets
                        .FirstOrDefault(c =>
                            c.Number == cabinetNumber));
            }
        }

        public CabinetDTO Edit(CabinetDTO cabinet)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                Cabinet? cabinetDb = db.Cabinets
                    .FirstOrDefault(c => c.Id == cabinet.Id);
                if (cabinet != null)
                {
                    cabinetDb.Number = cabinet.Number;

                    db.Attach(cabinetDb);
                    db.Entry(cabinetDb).State =
                        EntityState.Modified;

                    db.SaveChanges();
                }

                return GetById(cabinet.Id);
            }
        }

        public void Remove(CabinetDTO cabinet)
        {
            using (MedicDBContext db = new MedicDBContext())
            {
                Cabinet cabinetDb = db.Cabinets
                    .FirstOrDefault(c => c.Id == cabinet.Id);
                if (cabinetDb != null)
                {
                    db.Cabinets.Remove(cabinetDb);

                    db.SaveChanges();
                }
            }
        }
    }
}