
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using PatientManagementApp.Data.Models;
using PatientManagementApp.Data.Repository.Interfaces;
using PatientManagementApp.Services.Data.Interfaces;
using PatientManagementApp.Services.Mapping;
using PatientManagementApp.Web.ViewModels.AppointmentViewModels;
using PatientManagementApp.Web.ViewModels.PatientViewModels;
using PatientManagementApp.Web.ViewModels.PractitionerViewModels;
using static PatientManagementApp.Common.ModelValidationConstraints;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;
using Specialty = PatientManagementApp.Data.Models.Specialty;


namespace PatientManagementApp.Services.Data
{
    public class PractitionerService(IRepository<Practitioner, Guid> practitionerRepository
                                    ,IRepository<Specialty, Guid> specialtiesRepository
                                    ,UserManager<ApplicationUser> userManager) 
        : BaseService, IPractitionerService
    {

        private readonly IRepository<Practitioner, Guid> _practitionerRepository = practitionerRepository;
        private readonly IRepository<Specialty, Guid> _specialtiesRepository =
            specialtiesRepository;
        private readonly UserManager<ApplicationUser> _userManager = userManager;




        //INDEX
        public async Task<IEnumerable<PractitionerDetailsViewModel>>IndexAllPractitionersAsync()
        {
            var practitioners = await this._practitionerRepository
                .GetAllAttached()
                .Where(p => !p.IsDeleted)
                .AsNoTracking()
                .OrderBy(p => p.FirstName)
                .ThenBy(p => p.LastName)
                .To<PractitionerDetailsViewModel>()
                .ToListAsync();

            return practitioners;
        }


        //DETAILS
        public async Task<PractitionerDetailsViewModel> GetPractitionerDetailsByIdAsync(Guid id)
        {
            Practitioner? practitioner = await this._practitionerRepository
                .GetAllAttached()
                .Where(p => p.Id == id)
                .Include(p => p.PractitionersSpecialties)
                .ThenInclude(ps => ps.Specialty)
                .Include(p => p.Appointments)
                .ThenInclude(appointment => appointment.Patient)
                .Include(p => p.User)
                .Include(p => p.Patients)
                .ThenInclude(pt => pt.EmergencyContact)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (practitioner == null)
            {
                throw new Exception("Practitioner not found.");
            }

            var model = new PractitionerDetailsViewModel
            {
                Id = practitioner.Id,
                FirstName = practitioner.FirstName,
                LastName = practitioner.LastName,
                Phone = practitioner.Phone,
                Email = practitioner.User?.Email ?? string.Empty,
                Specialties = practitioner.PractitionersSpecialties
                    .Select(ps => ps.Specialty.Name).ToList(),
                
                Appointments = practitioner.Appointments
                    .Where(a => !a.IsDeleted)
                    .Select(a => new AppointmentInfoViewModel
                    {
                        Id = a.Id,
                        Description = a.Description,
                        StartDate = a.StartDate.ToString(AppointmentTimeFormat),
                        EndDate = a.EndDate.ToString(AppointmentTimeFormat),
                        PatientFirstName = a.Patient.FirstName,
                        PatientLastName = a.Patient.LastName
                    }).ToList(),

                Patients = practitioner.Patients
                    .Where(pt => pt.IsActive)
                    .Select(pt => new PatientDetailsViewModel
                    {
                        Id = pt.Id,
                        FirstName = pt.FirstName,
                        LastName = pt.LastName,
                        Age = pt.Age,
                        PhoneNumber = pt.PhoneNumber,
                        Status = pt.Status
                    }).ToList()

            };

            return model;
        }


        //EDIT
        public async Task<PractitionerEditViewModel?> GetPractitionerEditModelByIdAsync(Guid id)
        {
            var specialties = await this._specialtiesRepository
                .GetAllAttached().ToListAsync();

            var practitioner = await this._practitionerRepository
                .GetAllAttached()
                .Where(p => p.Id == id)
                .Include(p => p.PractitionersSpecialties)
                .Include(p => p.User)
                .Include(p => p.Appointments)
                .AsNoTracking()
                .Select(p => new PractitionerEditViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Phone = p.Phone,
                    Email = p.User.Email,
                    Patients = p.Patients,
                    IsAvailableOnline = p.IsAvailableOnline,
                    SelectedSpecialties = p.PractitionersSpecialties
                        .Select(ps => ps.SpecialtyId).ToList(),
                })
                .FirstOrDefaultAsync();

            if (practitioner != null)
            {
                // Map the specialties to the view model
                practitioner.Specialties = specialties.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name,
                    Selected = practitioner.SelectedSpecialties.Contains(s.Id)
                }).ToList();
            }

            return practitioner;

        }

        public async Task<bool> EditPractitionerAsync(PractitionerEditViewModel model)
        {
            var practitioner = await this._practitionerRepository
                .GetAllAttached()
                .Include(p => p.PractitionersSpecialties)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (practitioner == null)
            {
                return false;
            }

            practitioner.FirstName = model.FirstName;
            practitioner.LastName = model.LastName;
            practitioner.Phone = model.Phone;
            practitioner.User.Email = model.Email;
            practitioner.IsAvailableOnline = model.IsAvailableOnline;

            practitioner.PractitionersSpecialties.Clear();

            foreach (var specialtyId in model.SelectedSpecialties)
            {
                practitioner.PractitionersSpecialties.Add(new PractitionersSpecialties
                {
                    PractitionerId = practitioner.Id,
                    SpecialtyId = specialtyId
                });
            }

            return await this._practitionerRepository.UpdateAsync(practitioner);
        }

        //SOFT-DELETE
        public async Task<DeletePractitionerViewModel?> GetPractitionerDeleteModelAsync(Guid id)
        {
            DeletePractitionerViewModel? practitionerToDelete = await this._practitionerRepository
                .GetAllAttached()
                .Include(p => p.Appointments)
                .Include(p => p.PractitionersSpecialties)
                .Include(p => p.Patients)
                .Include(p => p.User)
                .To<DeletePractitionerViewModel>()
                .FirstOrDefaultAsync(p => p.Id == id);

            return practitionerToDelete;
        }

        public async Task<bool> SoftDeletePractitionerAsync(Guid id)
        {
            Practitioner? practitioner = await this._practitionerRepository
                .GetAllAttached()
                .Include(p => p.Appointments)
                .Include(p => p.PractitionersSpecialties)
                .Include(p => p.Patients)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (practitioner == null)
            {
                return false;
            }

            practitioner.IsDeleted = true;
            practitioner.User.IsDeleted = true;

            //clear all linked entities from their associated data:
            if (practitioner.Appointments != null && practitioner.Appointments.Any())
            {
                foreach (var appointment in practitioner.Appointments)
                {
                    appointment.IsDeleted = true;
                }
            }

            if (practitioner.PractitionersSpecialties != null && practitioner.PractitionersSpecialties.Any())
            {
                practitioner.PractitionersSpecialties.Clear();
            }

            

            return await this._practitionerRepository.UpdateAsync(practitioner);
        }
    }
}
