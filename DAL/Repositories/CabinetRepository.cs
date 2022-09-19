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

        public async Task<CabinetDTO?> Add(CabinetDTO cabinet)
        {
            CabinetDTO? dbCabinet = await GetByNumber(cabinet.Number);
            if (dbCabinet == null)
            {
                dbContext.Cabinets.Add(new Cabinet
                {
                    Number = cabinet.Number
                });
                await dbContext.SaveChangesAsync();
                return await GetByNumber(cabinet.Number);
            }

            return dbCabinet;
        }

        public async Task<CabinetDTO?> Add(int cabinet)
        {
            CabinetDTO? dbCabinet = await GetByNumber(cabinet);
            if (dbCabinet == null)
            {
                Cabinet? newCabinet = new Cabinet
                {
                    Number = cabinet
                };
                dbContext.Cabinets.Add(newCabinet);
                await dbContext.SaveChangesAsync();
                return await GetByNumber(cabinet);
            }

            return dbCabinet;
        }

        public async Task<CabinetDTO?> GetById(int cabinetId)
        {
            return new CabinetAdapter()
                .ToDTO(await dbContext.Cabinets
                    .FirstOrDefaultAsync(c =>
                        c.Id == cabinetId));
        }

        public async Task<CabinetDTO?> GetByNumber(int cabinetNumber)
        {
            return new CabinetAdapter()
                .ToDTO(await dbContext.Cabinets
                    .FirstOrDefaultAsync(c =>
                        c.Number == cabinetNumber));
        }

        public async Task<CabinetDTO?> Edit(CabinetDTO cabinet)
        {
            Cabinet? cabinetDb = await dbContext.Cabinets
                .FirstOrDefaultAsync(c => c.Id == cabinet.Id);
            if (cabinet != null)
            {
                cabinetDb.Number = cabinet.Number;

                dbContext.Attach(cabinetDb);
                dbContext.Entry(cabinetDb).State =
                    EntityState.Modified;

                await dbContext.SaveChangesAsync();
            }

            return await GetById(cabinet.Id);
        }

        public async void Remove(CabinetDTO cabinet)
        {
            Cabinet? cabinetDb = await dbContext.Cabinets
                .FirstOrDefaultAsync(c => c.Id == cabinet.Id);
            if (cabinetDb != null)
            {
                dbContext.Cabinets.Remove(cabinetDb);

                await dbContext.SaveChangesAsync();
            }
        }
    }
}