using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMCenterTestApp.DAL;
using SMCenterTestApp.DTO;
using SMCenterTestApp.Repositories;

namespace SMCenterTestApp.Controllers
{
    public class DoctorsController : Controller
    {
        DoctorRepository doctorRepository;

        public DoctorsController(MedicDBContext context)
        {
            doctorRepository = new DoctorRepository(context);
        }

        // GET: Doctors
        public async Task<IActionResult> List()
        {
            return View(doctorRepository.List());
        }

        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DoctorDTO? doctorDTO = doctorRepository.GetById(id.Value);
            if (doctorDTO == null)
            {
                return NotFound();
            }

            return View(doctorDTO);
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Initials,CabinetId," +
            "SpecialityId,Region")] DoctorDTO doctor)
        {
            if (ModelState.IsValid)
            {
                doctorRepository.Add(doctor);
                return Ok();
            }
            throw new InvalidCastException();
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            DoctorDTO? doctorDTO = doctorRepository.GetById(id.Value);
            if (doctorDTO == null)
            {
                return NotFound();
            }
            return View(doctorDTO);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Id,Initials," +
            "CabinetId,SpecialityId,RegionId")] DoctorEditDTO doctor)
        {
            if (id != doctor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    doctorRepository.Edit(doctor);
                    return Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (doctorRepository.GetById(doctor.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DoctorDTO? doctorDTO = doctorRepository.GetById(id.Value);
            if (doctorDTO == null)
            {
                return NotFound();
            }

            return View(doctorDTO);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            DoctorDTO? doctorDTO = doctorRepository.GetById(id);
            if (doctorDTO == null)
            {
                return NotFound();
            }

            doctorRepository.Remove(id);

            return Ok();
        }
    }
}