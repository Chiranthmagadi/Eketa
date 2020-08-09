using Elekta.Appointment.Services.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elekta.Appointment.Services.Interfaces
{
    public interface IAppointmentService
    {
        void MakeAppointment(AppointmentRequest request);
    }
}
