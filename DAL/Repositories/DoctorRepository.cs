using Microsoft.EntityFrameworkCore;
using DAL.Adapters;
using DTO;

namespace DAL.Repositories
{
    public class DoctorRepository
    {
        private MedicDBContext dbContext;

        public DoctorRepository(MedicDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<DoctorEditDTO?> Add(DoctorDTO doctor)
        {
            CabinetDTO? cabinet
                = await new CabinetRepository(dbContext).Add(doctor.Cabinet);
            SpecialityDTO? speciality
                = await new SpecialityRepository(dbContext).Add(doctor.Speciality);
            RegionDTO? region
                = await new RegionRepository(dbContext).Add(doctor.Region);

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
                await dbContext.SaveChangesAsync();
            }

            return new DoctorAdapter().ToEditDTO(doctorDb);
        }

        public async Task<DoctorDTO?> GetById(Guid doctorId)
        {
            return await new DoctorAdapter()
                .ToDTO(dbContext, await dbContext.Doctors
                    .FirstOrDefaultAsync(d =>
                        d.Id == doctorId));
        }

        public async Task<DoctorEditDTO?> GetEditDTOById(Guid doctorId)
        {
            return new DoctorAdapter()
                .ToEditDTO(await dbContext.Doctors
                    .FirstOrDefaultAsync(d =>
                        d.Id == doctorId));
        }

        public async Task<DoctorEditDTO?> Edit(DoctorEditDTO doctor)
        {
            Doctor? doctorDb = await dbContext.Doctors
                .FirstOrDefaultAsync(r => r.Id == doctor.Id);
            if (doctorDb == null)
            {
                throw new DbUpdateConcurrencyException();
            }

            doctorDb.Initials = doctor.Initials ?? doctorDb.Initials;
            doctorDb.RegionId = doctor.RegionId ?? doctorDb.RegionId;
            doctorDb.SpecialityId = doctor.SpecialityId ?? doctorDb.RegionId;
            doctorDb.CabinetId = doctor.CabinetId ?? doctorDb.RegionId;

            dbContext.Attach(doctorDb);
            dbContext.Entry(doctorDb).State = EntityState.Modified;

            await dbContext.SaveChangesAsync();


            return await GetEditDTOById(doctor.Id);
        }


        public async void Remove(Guid id)
        {
            Doctor? doctorDb = dbContext.Doctors
                .FirstOrDefault(r => r.Id == id);
            if (doctorDb != null)
            {
                dbContext.Doctors.Remove(doctorDb);

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<DoctorDTO?>> List()
        {
            DoctorAdapter? adapter = new DoctorAdapter();
            return (await dbContext.Doctors
                .ToListAsync())
                .Select(async d => await adapter.ToDTO(dbContext, d))
                .Select(t => t.Result)
                .ToList();
        }
    }
}