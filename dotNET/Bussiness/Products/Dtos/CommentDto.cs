namespace DataAccess.Entities.Dtos
{
    public class CommentDto
    {
        public string UserName { get; set; } = string.Empty;
        public long Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Added { get; set; }
    }
}
