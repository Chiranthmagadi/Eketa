using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Elekta.Appointment.Data.Modles
{
    public class AppointmentModel
    {
        /// <summary>
        /// adding comments
        /// </summary>
        [Key]
        public int  Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public Patient Patient { get; set; }

    }
}
