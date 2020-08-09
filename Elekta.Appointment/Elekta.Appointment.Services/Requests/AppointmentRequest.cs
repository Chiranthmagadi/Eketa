using System;
using System.Collections.Generic;
using System.Text;

namespace Elekta.Appointment.Services.Requests
{
    public class AppointmentRequest
    {
        public int PatientId { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
