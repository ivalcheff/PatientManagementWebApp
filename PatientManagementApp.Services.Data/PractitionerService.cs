
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using PatientManagementApp.Data.Models;
using PatientManagementApp.Data.Repository.Interfaces;
using PatientManagementApp.Services.Data.Interfaces;
using PatientManagementApp.Web.ViewModels.AppointmentViewModels;
using PatientManagementApp.Web.ViewModels.PractitionerViewModels;
using static PatientManagementApp.Common.ModelValidationConstraints.Global;


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


        //TODO: Add index all practitioners for the admin user
        //TODO: Add soft-delete


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
                Patients = practitioner.Patients,
                Specialties = practitioner.PractitionersSpecialties
                    .Select(ps => ps.Specialty.Name).ToList(),
                Appointments = practitioner.Appointments
                    .Select(a => new AppointmentInfoViewModel
                    {
                        Id = a.Id,
                        Description = a.Description,
                        StartDate = a.StartDate.ToString(AppointmentTimeFormat),
                        EndDate = a.EndDate.ToString(AppointmentTimeFormat),
                        PatientFirstName = a.Patient.FirstName,
                        PatientLastName = a.Patient.LastName
                    }).ToList()
            };

            return model;
        }

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
    }
}
