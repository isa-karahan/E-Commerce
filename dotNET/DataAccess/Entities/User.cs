using Core.Entities;

namespace DataAccess.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Added { get; set; } = DateTime.Now;
    }
}
