using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SMCenterTestApp.DAL;
using SMCenterTestApp.DTO;
using SMCenterTestApp.Repositories;

namespace SMCenterTestApp.Controllers
{
    public class DoctorsController : Controller
    {
        // GET: Doctors
        public async Task<IActionResult> List()
        {
            return View(new DoctorRepository().List());
        }

        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DoctorDTO? doctorDTO =
                new DoctorRepository().GetById(id.Value);
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
                new DoctorRepository().Add(doctor);
                return Ok();
            }
            throw new InvalidCastException();
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            DoctorDTO? doctorDTO =
                new DoctorRepository().GetById(id.Value);
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
                    new DoctorRepository().Edit(doctor);
                    return Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (new PatientRepository()
                        .GetById(doctor.Id) == null)
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

            DoctorRepository? repo = new DoctorRepository();

            DoctorDTO? doctorDTO = repo.GetById(id.Value);
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
            PatientRepository? repo = new PatientRepository();
            PatientDTO? patientDTO = repo.GetById(id);
            if (patientDTO == null)
            {
                return NotFound();
            }

            repo.Remove(id);

            return Ok();
        }
    }
}