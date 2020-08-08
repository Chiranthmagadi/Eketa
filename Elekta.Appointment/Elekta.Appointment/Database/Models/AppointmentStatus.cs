using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Elekta.Appointment.Database.Models
{
    public class AppointmentStatus
    {
        [Key]
        public int AppointmentId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Status { get; set; }
        public Patient Patient { get; set; }
        public Equipment Equipment { get; set; }
        public Hospital Hospital { get; set; }

    }
}
