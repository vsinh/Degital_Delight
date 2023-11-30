namespace DegitalDelight.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime Date { get; set; }
        public int Upvote { get; set; }
        public int Downvote { get; set; }
        public bool IsDeleted { get; set; } = false;
        public User? User { get; set; }
        public Product? Product { get; set; }
        public Comment? Reply { get; set; }
    }
}
