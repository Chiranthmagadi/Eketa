using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Elekta.Appointment.Data.Modles
{
    public class AppointmentModel
    {
        public int  Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public bool Status { get; set; }
        public virtual int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

    }
}
