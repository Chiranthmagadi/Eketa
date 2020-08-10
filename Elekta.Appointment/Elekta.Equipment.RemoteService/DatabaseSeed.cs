using System;
using System.Collections.Generic;

namespace Elekta.Equipment.RemoteService
{
    /// <summary>
    /// You can ignore this class. Because we're using an in memory database we wanted to seed the db with data to enable you to test your application
    /// </summary>
    public class DatabaseSeed
    {
        private readonly EquipmentAvailabilityContext _context;

        public DatabaseSeed(EquipmentAvailabilityContext context)
        {
            _context = context;
        }

        public void SeedDatabase()
        {
            var equipments = AddEquipments();
            _context.Equipments.AddRange(equipments);
             _context.SaveChanges();
       }

        private List<EquipmentAvailable> AddEquipments()
        {
            var equipments = new List<EquipmentAvailable>
            {
                new EquipmentAvailable
                {
                    EquipmentId = 1,
                    IsAvailable = true,
                    Date = new DateTime(2020,11,10,08,00,00)
                },
                new EquipmentAvailable
                {
                    EquipmentId = 1,
                    IsAvailable = false,
                    Date = new DateTime(2020,11,10,09,00,00)
                },
                new EquipmentAvailable
                {
                    EquipmentId = 2,
                    IsAvailable = false,
                    Date = new DateTime(2020,11,10,08,00,00)
                },
                new EquipmentAvailable
                {
                    EquipmentId = 2,
                    IsAvailable = true,
                    Date = new DateTime(2020,11,10,09,00,00)
                }
            };

            return equipments;
        }

    }
}
