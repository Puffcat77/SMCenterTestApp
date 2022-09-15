using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMCenterTestApp.DTO;
using SMCenterTestApp.Repositories;

namespace SMCenterTestApp.Controllers
{
    public class PatientsController : Controller
    {
        // GET: Patients
        public async Task<IActionResult> List()
        {
            return View(new PatientRepository().List());
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PatientDTO? patientDTO = new PatientRepository().GetById(id);
            if (patientDTO == null)
            {
                return NotFound();
            }

            return View(patientDTO);
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [
                Bind("Id,FirstName,LastName,Surname," +
                "Address,BirthDate,Sex,RegionNumber")
            ] PatientDTO patient)
        {
            if (ModelState.IsValid)
            {
                new PatientRepository().Add(patient);
                return Ok();
            }
            throw new InvalidCastException();
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            PatientDTO? patientDTO = new PatientRepository().GetById(id);
            if (patientDTO == null)
            {
                return NotFound();
            }
            return View(patientDTO);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Id,FirstName,LastName,Surname," +
            "Address,BirthDate,Sex,RegionId")] PatientEditDTO patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    new PatientRepository().Edit(patient);
                    return Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (new PatientRepository().GetById(patient.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PatientRepository? repo = new PatientRepository();

            PatientDTO? patientDTO = repo.GetById(id);
            if (patientDTO == null)
            {
                return NotFound();
            }

            return View(patientDTO);
        }

        // POST: Patients/Delete/5
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