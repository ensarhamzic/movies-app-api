namespace Movies_API.Data.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
