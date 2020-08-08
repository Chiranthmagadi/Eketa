using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Elekta.Appointment.Database.Models
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
