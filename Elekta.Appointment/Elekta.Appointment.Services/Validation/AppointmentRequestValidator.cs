using Elekta.Appointment.Data;
using Elekta.Appointment.Services.Requests;
using System;
using System.Linq;
using System.Net.Http;

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
            if (IsAppointmentNOTLessthanGivenDays(request.NewAppointmentDate, 2, ref result))
                return result;
            if (IsAppointmentNOTLaterEnough(request.NewAppointmentDate, ref result))
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
            if (IsAppointmentNOTLessthanGivenDays(request.AppointmentDate, 3, ref result))
                return result;
            return result;
        }

        public ValidationResult ValidateMakeAppointmentRequest(AppointmentRequest request)
        {
            var result = new ValidationResult(true);
            if (IsAppointmentNOTLaterEnough(request.AppointmentDate, ref result))
                return result;
            if (IsEquipmentNOTAvailable(request, ref result))
                return result;
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
            var isExist = _context.Appointments.Any(c => c.Patient.PatientId == request.PatientId && c.AppointmentDate == request.AppointmentDate);
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
                    
        private bool IsEquipmentNOTAvailable(AppointmentRequest request, ref ValidationResult result)
        {
          
            using (var httpClient = new HttpClient())
            {
                var httpResponse =  httpClient.GetAsync($"{"http://localhost:3388/equipmentAvailability/"}{request.NewAppointmentDate}");
                var content =  httpResponse.Result.Content.ReadAsStringAsync();
            }
           
        }
    }
}
