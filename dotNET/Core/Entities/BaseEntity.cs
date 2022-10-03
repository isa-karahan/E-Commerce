using System.Text.Json.Serialization;

namespace Core.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
    }
}