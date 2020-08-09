using Elekta.Appointment.Services.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elekta.Appointment.Services.Validation
{
    public interface IAppointmentRequestValidator
    {
        ValidationResult ValidateRequest(AppointmentRequest request);
    }
}
