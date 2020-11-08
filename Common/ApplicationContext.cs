using Common.DataAccess;
using Common.Entities;

using Microsoft.EntityFrameworkCore;

namespace Common
{
    public class ApplicationContext : DbContext
    {
        public DbSet<OpSystem> OperatingSystems { get; set; }
        public DbSet<UniversityBuilding> UniversityBuildings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppSettingsManager.Settings.GetConnectingString());
        }
    }
}
