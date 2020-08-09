using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Elekta.Appointment.Data.Modles
{
    public class Equipment
    {
        [Key]
        public int EquipmentId { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
