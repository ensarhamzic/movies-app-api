namespace Movies_API.Data.Models
{
    public class Publish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

        public ICollection<Collection> Collections;
    }
}
