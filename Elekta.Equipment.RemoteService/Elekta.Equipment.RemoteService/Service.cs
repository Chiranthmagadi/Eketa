﻿using System;
using System.Linq;

namespace Elekta.Equipment.RemoteService
{
    public class Service : IService
    {
        private readonly EquipmentAvailabilityContext _context;

        public Service(EquipmentAvailabilityContext context)
        {
            _context = context;
        }

        public bool? IsEquipmentAvailable(DateTime date)
        {
            var equipment = _context.Equipments.FirstOrDefault(c => c.Date == date);
            return equipment?.IsAvailable;
        }
    }

    public interface IService
    {
        bool? IsEquipmentAvailable(DateTime date);
    }
}
