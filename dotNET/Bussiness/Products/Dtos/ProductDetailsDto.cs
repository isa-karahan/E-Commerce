using DataAccess.Entities.Dtos;

namespace Bussiness.Products.Dtos
{
    public class ProductDetailsDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int UnitPrice { get; set; }
        public string Description { get; set; }
        public int BonusPercentage { get; set; }
        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }
}
