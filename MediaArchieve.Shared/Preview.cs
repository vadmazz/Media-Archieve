using System.ComponentModel.DataAnnotations.Schema;

namespace MediaArchieve.Shared
{
    public class Preview
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
    }
}
