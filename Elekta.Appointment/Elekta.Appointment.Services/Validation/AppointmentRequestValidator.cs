using Elekta.Appointment.Data;
using Elekta.Appointment.Services.Requests;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Elekta.Appointment.Services.Validation
{
    public class AppointmentRequestValidator : IAppointmentRequestValidator
    {
        private readonly AppointmentDbContext _context;

        public AppointmentRequestValidator(AppointmentDbContext context)
        {
            _context = context;
        }

        public ValidationResult ValidateChangeAppointmentRequest(AppointmentRequest request)
        {
            var result = new ValidationResult(true);
            if (IsAppointmentNOTExist(request, ref result))
                return result;
            if (IsAppointmentNOTLessthanGivenDays(request.ChangeAppointmentDate, daysBefore:2, ref result))
                return result;
            if (IsAppointmentNOTLaterEnough(request.ChangeAppointmentDate, ref result))
                return result;
            //if (IsEquipmentNOTAvailable(request, ref result))
            //    return result;

            return result;
        }

        public ValidationResult ValidateCancelAppointmentRequest(AppointmentRequest request)
        {
            var result = new ValidationResult(true);
            if (IsAppointmentNOTExist(request, ref result))
                return result;
            if (IsAppointmentNOTLessthanGivenDays(request.AppointmentDate, daysBefore:3, ref result))
                return result;
            return result;
        }

        public async Task<ValidationResult> ValidateMakeAppointmentRequestAsync(AppointmentRequest request)
        {
            var result = new ValidationResult(true);
            await IsEquipmentNOTAvailableAsync(request);
            if (IsAppointmentNOTLaterEnough(request.AppointmentDate, ref result))
                return result;
            //if (IsEquipmentNOTAvailable(request, ref result))
            //    return result;
            if (IsAppointmentNOTMadeBetweenCorrectTime(request, ref result))
                return result;
            return result;
        }

        private bool IsAppointmentNOTLessthanGivenDays(DateTime appointmentDate, int daysBefore, ref ValidationResult result)
        {
            if ((appointmentDate - DateTime.Now).TotalDays < daysBefore)
            {
                result.PassedValidation = false;
                result.Errors.Add("Appointment cannot be cancelled before 3 days before the appointment date!");
                return true;
            }
            return false;
        }

        private bool IsAppointmentNOTExist(AppointmentRequest request, ref ValidationResult result)
        {
            var isExist = _context.Appointments.Any(c => c.Patient.Id == request.PatientId && c.AppointmentDate == request.AppointmentDate && c.Status == true);
            if (!isExist)
            {
                result.PassedValidation = false;
                result.Errors.Add("The appointment does not exist!");
                return true;
            }
            return false;
        }

        private bool IsAppointmentNOTLaterEnough(DateTime appointmentDate, ref ValidationResult result)
        {
            if ((appointmentDate - DateTime.Now).TotalDays <= 14)
            {
                result.PassedValidation = false;
                result.Errors.Add("Appointments can only be made for 2 weeks later at most!");
                return true;
            }
            return false;
        }

        private bool IsAppointmentNOTMadeBetweenCorrectTime(AppointmentRequest request, ref ValidationResult result)
        {
            if (!((request.AppointmentDate.Hour >= 8) && (request.AppointmentDate.Hour <= 16)))
            {
                result.PassedValidation = false;
                result.Errors.Add("Appointments can be made between 08:00 and 16:00!");
                return true;
            }
            return false;
        }
                    
        private async Task<HttpContent> IsEquipmentNOTAvailableAsync(AppointmentRequest request)
        {
          
            using (var httpClient = new HttpClient())
            {
                //httpClient.BaseAddress = new Uri("http://localhost:3388/equipmentAvailability");
                //httpClient.
                //var httpResponse =  await httpClient.GetAsync();
                //var content = httpResponse.Content;

                var req = new Request
                {
                    AvailabilityDate = request.AppointmentDate
                };
                var content = new StringContent(req.ToString(), System.Text.Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync("http://localhost:3388/equipmentAvailability", content);
                return result.Content;
            }
           
        }

        public class Request
        {
            public DateTime AvailabilityDate { get; set; }
        }
    }
}
