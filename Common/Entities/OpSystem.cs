
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    [Table("OperatingSystems")]
    public class OpSystem
    {
        [Key]
        [Column("OperatingSystemPK")]
        public int OpSystemPK { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string ProgrammingLanguage { get; set; }
        public string LatestVersion { get; set; }
        public string SupportedPlatforms { get; set; }
        public double MarketShare { get; set; }

    }
}
