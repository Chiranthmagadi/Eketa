using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elekta.Equipment.RemoteService
{
    public class Equipment
    {
        public int Id { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime Date { get; set; }
    }
}
