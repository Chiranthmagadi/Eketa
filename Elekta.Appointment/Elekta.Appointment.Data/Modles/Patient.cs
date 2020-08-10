using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Elekta.Appointment.Data.Modles
{
    public class Patient
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string PatientEmailId { get; set; }
    }
}
