using Microsoft.EntityFrameworkCore;

namespace Elekta.Equipment.RemoteService
{
    public class EquipmentAvailabilityContext : DbContext
    {
        public EquipmentAvailabilityContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EquipmentAvailable> Equipments { get; set; }
    }
}
