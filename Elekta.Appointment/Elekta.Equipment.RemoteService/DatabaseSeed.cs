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

        private List<Equipment> AddEquipments()
        {
            var equipments = new List<Equipment>
            {
                new Equipment
                {
                    Id = 1,
                    IsAvailable = true,
                    Date = Convert.ToDateTime("2020-01-02T08,00:00.000Z")
                },
                new Equipment
                {
                    Id = 2,
                    IsAvailable = true,
                    Date = Convert.ToDateTime("2020-01-02T09,00:00.000Z")
                },
                new Equipment
                {
                    Id = 3,
                    IsAvailable = true,
                    Date = Convert.ToDateTime("2020-01-02T08,00:00.000Z")
                },
                new Equipment
                {
                    Id = 4,
                    IsAvailable = true,
                    Date = Convert.ToDateTime("2020-01-02T09,00:00.000Z")
                }
            };

            return equipments;
        }

    }
}
