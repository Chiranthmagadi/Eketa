using Elekta.Appointment.Data;
using Elekta.Appointment.Services.Requests;
using System;

namespace Elekta.Appointment.Services.Validation
{
    public class AppointmentRequestValidator : IAppointmentRequestValidator
    {
        private readonly AppointmentDbContext _context;

        public AppointmentRequestValidator(AppointmentDbContext context)
        {
            _context = context;
        }

        public ValidationResult ValidateMakeAppointmentRequest(AppointmentRequest request)
        {
            var result = new ValidationResult(true);
            if (IsAppointmentNOTLaterEnough(request, ref result))
                return result;
            //if (IsEquipmentNOTAvailable(request, ref result))
            //    return result;
            if (IsAppointmentNOTMadeBetweenCorrectTime(request, ref result))
                return result;
            return result;
        }


        private bool IsAppointmentNOTLaterEnough(AppointmentRequest request, ref ValidationResult result)
        {
            if ((request.BookingDate - DateTime.Now).TotalDays <= 14)
            {
                result.PassedValidation = false;
                result.Errors.Add("Appointments can only be made for 2 weeks later at most!");
                return true;
            }
            return false;
        }

        private bool IsEquipmentNOTAvailable(AppointmentRequest request, ref ValidationResult result)
        {
            throw new NotImplementedException();
        }

        private bool IsAppointmentNOTMadeBetweenCorrectTime(AppointmentRequest request, ref ValidationResult result)
        {
            if (!((request.BookingDate.Hour >= 8) && (request.BookingDate.Hour <= 16)))
            {
                result.PassedValidation = false;
                result.Errors.Add("Appointments can be made between 08:00 and 16:00!");
                return true;
            }
            return false;
        }
    }
}
