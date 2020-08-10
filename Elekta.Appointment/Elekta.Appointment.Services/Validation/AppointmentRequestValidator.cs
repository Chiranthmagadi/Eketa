using Elekta.Appointment.Data;
using Elekta.Appointment.Services.Helper;
using Elekta.Appointment.Services.Requests;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Elekta.Appointment.Services.Validation
{
    public class AppointmentRequestValidator : IAppointmentRequestValidator
    {
        private readonly AppointmentDbContext _context;
        private readonly IHttpHandler httpHandler;

        public AppointmentRequestValidator(AppointmentDbContext context, IHttpHandler httpHandler)
        {
            _context = context;
            this.httpHandler = httpHandler;
        }

        public async Task<ValidationResult> ValidateChangeAppointmentRequestAsync(AppointmentRequest request)
        {
            var result = new ValidationResult(true);
            if (IsAppointmentNOTExist(request, ref result))
                return result;
            if (IsAppointmentNOTLessthanGivenDays(request.ChangeAppointmentDate, daysBefore: 2, ref result))
                return result;
            if (IsAppointmentNOTLaterEnough(request.ChangeAppointmentDate, ref result))
                return result;
            var equipAvail = await CheckEquipmentAvailableAsync(request);
            if (!equipAvail)
            {
                SetValidationForEquipmentAvailability(ref result);
                return result;
            }
            return result;
        }

        public ValidationResult ValidateCancelAppointmentRequest(AppointmentRequest request)
        {
            var result = new ValidationResult(true);
            if (IsAppointmentNOTExist(request, ref result))
                return result;
            if (IsAppointmentNOTLessthanGivenDays(request.AppointmentDate, daysBefore: 3, ref result))
                return result;
            return result;
        }

        public async Task<ValidationResult> ValidateMakeAppointmentRequestAsync(AppointmentRequest request)
        {
            var result = new ValidationResult(true);
            if (IsAppointmentNOTLaterEnough(request.AppointmentDate, ref result))
                return result;
            var equipAvail = await CheckEquipmentAvailableAsync(request);
            if (!equipAvail)
            {
                SetValidationForEquipmentAvailability(ref result);
                return result;
            }
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
            var isExist = _context.Appointments.Any(c => c.PatientId == request.PatientId && c.AppointmentDate == request.AppointmentDate && c.Status == true);
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

        private async Task<bool> CheckEquipmentAvailableAsync(AppointmentRequest request)
        {
            using (var httpClient = new HttpClient())
            {
                var req = new Request
                {
                    AvailabilityDate = request.AppointmentDate
                };
                var json = JsonConvert.SerializeObject(req);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = httpHandler.PostAsync("http://localhost:3388/equipmentAvailability", content);
                var res = await response.Result.Content.ReadAsStringAsync();
                return bool.Parse(res);
            }
        }

        private void SetValidationForEquipmentAvailability(ref ValidationResult result)
        {
            result.PassedValidation = false;
            result.Errors.Add("Equipment is not available at the given time!");
        }


        public class Request
        {
            public DateTime AvailabilityDate { get; set; }
        }
    }
}
