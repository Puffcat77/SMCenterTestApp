using DAL;
using DAL.Repositories;
using DTO;
using Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMCenterTestApp.ListHelpers;

namespace SMCenterTestApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        DoctorRepository doctorRepository;

        public DoctorsController(MedicDBContext context)
        {
            doctorRepository = new DoctorRepository(context);
        }

        // POST: Doctors
        [HttpPost]
        public async Task<IActionResult> List([FromBody] ListProperties orderProperties)
        {
            if (orderProperties == null)
            {
                throw new ArgumentException(LogConstants.LIST_PARAMETERS_NOT_SET);
            }

            IEnumerable<DoctorDTO>? doctors
                = doctorRepository.List();

            if (orderProperties.UseOrder)
            {
                switch (orderProperties.OrderProperty.ToLower())
                {
                    case DoctorOrderProperties.ID:
                    {
                        doctors = doctors.OrderBy(d => d.Id);
                        break;
                    }
                    case DoctorOrderProperties.INITIALS:
                    {
                        doctors = doctors.OrderBy(d => d.Initials);
                        break;
                    }
                    case DoctorOrderProperties.CABINET:
                    {
                        doctors = doctors.OrderBy(d => d.Cabinet);
                        break;
                    }
                    case DoctorOrderProperties.SPECIALITY:
                    {
                        doctors = doctors.OrderBy(d => d.Speciality);
                        break;
                    }
                    case DoctorOrderProperties.REGION:
                    {
                        doctors = doctors.OrderBy(d => d.Region);
                        break;
                    }
                    default:
                    {
                        throw new ArgumentException(LogConstants.ORDER_PROPERTY_NOT_RECOGNIZED);
                    }
                }
                if (orderProperties.OrderByDescending)
                {
                    doctors = doctors.Reverse();
                }
            }
            return new JsonResult(doctors
                .Skip(orderProperties.PageSize * (orderProperties.PageNumber - 1))
                .Take(orderProperties.PageSize)
                .ToList());
        }

        // GET: Doctors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid? id)
        {
            if (id == null)
            {
                return NotFound(LogConstants.DOCTOR_ID_IS_NULL);
            }

            DoctorDTO? doctorDTO = doctorRepository.GetById(id.Value);
            if (doctorDTO == null)
            {
                return NotFound(string.Format(LogConstants.DOCTOR_ID_NOT_IN_DB, id.Value));
            }

            return new JsonResult(doctorDTO);
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] DoctorDTO doctor)
        {
            if (ModelState.IsValid)
            {
                return new JsonResult(doctorRepository.Add(doctor));
            }
            throw new InvalidCastException(LogConstants.DOCTOR_CREATE_EXCEPTION);
        }

        // GET: Doctors/Edit/5
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            DoctorDTO? doctorDTO = doctorRepository.GetById(id.Value);
            if (doctorDTO == null)
            {
                return NotFound(string.Format(LogConstants.DOCTOR_ID_NOT_IN_DB, id.Value));
            }
            return new JsonResult(doctorDTO);
        }

        // PUT: Doctors/Edit/5
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] DoctorEditDTO doctor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    doctor.Id = id;
                    return new JsonResult(doctorRepository.Edit(doctor));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (doctorRepository.GetById(doctor.Id) == null)
                    {
                        return NotFound(LogConstants.DOCTOR_WAS_DELETED_WHILE_EDIT);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return new JsonResult(doctor);
        }

        // GET: Doctors/Delete/5
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound(LogConstants.DOCTOR_ID_IS_NULL);
            }

            DoctorDTO? doctorDTO = doctorRepository.GetById(id.Value);
            if (doctorDTO == null)
            {
                return NotFound(string.Format(LogConstants.DOCTOR_ID_NOT_IN_DB, id.Value));
            }

            return new JsonResult(doctorDTO);
        }

        // DELETE: Doctors/Delete/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            DoctorDTO? doctorDTO = doctorRepository.GetById(id);
            if (doctorDTO == null)
            {
                return NotFound(string.Format(LogConstants.DOCTOR_ID_NOT_IN_DB, id));
            }

            doctorRepository.Remove(id);

            return Ok(LogConstants.DOCTOR_SUCCESSFULLY_DELETED);
        }
    }
}