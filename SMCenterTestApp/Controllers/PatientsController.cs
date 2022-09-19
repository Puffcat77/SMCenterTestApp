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
    public class PatientsController : ControllerBase
    {
        PatientRepository patientRepository;

        public PatientsController(MedicDBContext dbContext)
        {
            patientRepository = new PatientRepository(dbContext);
        }

        // POST: Patients
        [HttpPost]
        public async Task<IActionResult> List([FromBody] ListProperties orderProperties)
        {
            if (orderProperties == null)
            {
                throw new ArgumentException("Не заданы параметры списка");
            }

            IEnumerable<PatientDTO>? patients =
                patientRepository.List();

            if (orderProperties.UseOrder)
            {
                switch (orderProperties.OrderProperty.ToLower())
                {
                    case PatientOrderProperties.ID:
                    {
                        patients = patients.OrderBy(d => d.Id);
                        break;
                    }
                    case PatientOrderProperties.FISRT_NAME:
                    {
                        patients = patients
                            .OrderBy(d => d.FirstName);
                        break;
                    }
                    case PatientOrderProperties.LAST_NAME:
                    {
                        patients = patients
                            .OrderBy(d => d.LastName);
                        break;
                    }
                    case PatientOrderProperties.SURNAME:
                    {
                        patients = patients
                            .OrderBy(d => d.Surname);
                        break;
                    }
                    case PatientOrderProperties.ADDRESS:
                    {
                        patients = patients
                            .OrderBy(d => d.Address);
                        break;
                    }
                    case PatientOrderProperties.REGION:
                    {
                        patients = patients
                            .OrderBy(d => d.Region);
                        break;
                    }
                    case PatientOrderProperties.BIRTH_DATE:
                    {
                        patients = patients
                            .OrderBy(d => d.BirthDate);
                        break;
                    }
                    case PatientOrderProperties.SEX:
                    {
                        patients = patients
                            .OrderBy(d => d.Sex);
                        break;
                    }
                    default:
                    {
                        throw new ArgumentException("Неизвестный параметр сортировки");
                    }
                }
                if (orderProperties.OrderByDescending)
                {
                    patients = patients.Reverse();
                }
            }
            return new JsonResult(patients
                .Skip(orderProperties.PageSize * (orderProperties.PageNumber - 1))
                .Take(orderProperties.PageSize).ToList());
        }

        // GET: Patients/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid? id)
        {
            if (id == null)
            {
                return NotFound(LogConstants.PATIENT_ID_IS_NULL);
            }

            PatientDTO? patientDTO = patientRepository.GetById(id);
            if (patientDTO == null)
            {
                return NotFound(string.Format(LogConstants.PATIENT_ID_NOT_IN_DB, id));
            }

            return new JsonResult(patientDTO);
        }

        // POST: Patients/Create
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PatientDTO patient)
        {
            if (ModelState.IsValid)
            {
                return new JsonResult(patientRepository.Add(patient));
            }
            throw new InvalidCastException(LogConstants.PATIENT_CREATE_EXCEPTION);
        }

        // GET: Patients/Edit/5
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            PatientDTO? patientDTO = patientRepository.GetById(id);
            if (patientDTO == null)
            {
                return NotFound(string.Format(LogConstants.PATIENT_ID_NOT_IN_DB, id));
            }
            return new JsonResult(patientDTO);
        }

        // PUT: Patients/Edit/5
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(Guid id,[FromBody] PatientEditDTO patient)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    patient.Id = id;
                    return new JsonResult(patientRepository.Edit(patient));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (patientRepository.GetById(patient.Id) == null)
                    {
                        return NotFound(LogConstants.PATIENT_WAS_DELETED_WHILE_EDIT);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return new JsonResult(patient);
        }

        // GET: Patients/Delete/5
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound(LogConstants.PATIENT_ID_IS_NULL);
            }

            PatientDTO? patientDTO = patientRepository.GetById(id);
            if (patientDTO == null)
            {
                return NotFound(string.Format(LogConstants.PATIENT_ID_NOT_IN_DB, id));
            }

            return new JsonResult(patientDTO);
        }

        // DELETE: Patients/Delete/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            PatientDTO? patientDTO = patientRepository.GetById(id);
            if (patientDTO == null)
            {
                return NotFound(string.Format(LogConstants.PATIENT_ID_NOT_IN_DB, id));
            }

            patientRepository.Remove(id);

            return Ok();
        }
    }
}