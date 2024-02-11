namespace dz3.Models
{
    public class Note
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public List<string> Tags { get; set; }
    }
}
