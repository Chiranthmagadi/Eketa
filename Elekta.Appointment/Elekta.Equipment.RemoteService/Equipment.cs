using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elekta.Equipment.RemoteService
{
    public class EquipmentAvailable
    {
        public int EquipmentId { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime Date { get; set; }
    }
}
