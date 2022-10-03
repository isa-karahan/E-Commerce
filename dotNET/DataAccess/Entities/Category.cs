using Core.Entities;

namespace DataAccess.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int BonusPercentage { get; set; }
    }
}
