namespace Movies_API.Data.Models
{
    public class PublishCollection
    {
        public Publish Publish { get; set; }
        public int PublishId { get; set; }
        public Collection Collection { get; set; }
        public int CollectionId { get; set; }
    }
}
