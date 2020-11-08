using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    [Table("UniversityBuildings")]
    public class UniversityBuilding
    {
        [Key]
        public int UniversityBuildingPK { get; set; }
        public string BuildingName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
