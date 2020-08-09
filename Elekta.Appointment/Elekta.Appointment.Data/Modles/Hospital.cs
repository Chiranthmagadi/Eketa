using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Elekta.Appointment.Data.Modles
{
    public class Hospital
    {
        [Key]
        public int HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string Address { get; set; }

    }
}
