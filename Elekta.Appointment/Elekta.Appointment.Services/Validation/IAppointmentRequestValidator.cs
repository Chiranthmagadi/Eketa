using Elekta.Appointment.Services.Requests;
using System.Threading.Tasks;

namespace Elekta.Appointment.Services.Validation
{
    public interface IAppointmentRequestValidator
    {
        Task<ValidationResult> ValidateMakeAppointmentRequestAsync(AppointmentRequest request);
        ValidationResult ValidateCancelAppointmentRequest(AppointmentRequest request);
        ValidationResult ValidateChangeAppointmentRequest(AppointmentRequest request);
    }
}
