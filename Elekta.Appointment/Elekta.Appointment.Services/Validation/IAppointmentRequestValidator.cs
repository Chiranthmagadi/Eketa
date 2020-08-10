using Elekta.Appointment.Services.Requests;

namespace Elekta.Appointment.Services.Validation
{
    public interface IAppointmentRequestValidator
    {
        ValidationResult ValidateMakeAppointmentRequest(AppointmentRequest request);
        ValidationResult ValidateCancelAppointmentRequest(AppointmentRequest request);
    }
}
