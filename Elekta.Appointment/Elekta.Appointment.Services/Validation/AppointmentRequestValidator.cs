using Elekta.Appointment.Data;
using Elekta.Appointment.Services.Requests;

namespace Elekta.Appointment.Services.Validation
{
    public class AppointmentRequestValidator : IAppointmentRequestValidator
    {
        private readonly AppointmentDbContext _context;

        public AppointmentRequestValidator(AppointmentDbContext context)
        {
            _context = context;
        }

        public ValidationResult ValidateRequest(AppointmentRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
