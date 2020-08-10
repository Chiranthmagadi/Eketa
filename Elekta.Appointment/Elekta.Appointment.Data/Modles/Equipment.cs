using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Elekta.Appointment.Data.Modles
{
    public class Equipment
    {
        public int Id { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime Date { get; set; }
    }
}
