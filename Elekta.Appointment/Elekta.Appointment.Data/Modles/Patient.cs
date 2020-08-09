using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Elekta.Appointment.Data.Modles
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string PatientName { get; set; }
        [Required]
        [EmailAddress]
        public string PatientEmailId { get; set; }
    }
}
