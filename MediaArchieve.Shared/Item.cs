namespace MediaArchieve.Shared
{
    public class Item
    {
        public int Id { get; set; }
        public string IconSource { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Preview Preview { get; set; }

        public Folder Folder { get; set; }
        public int FolderId { get; set; }
    }
}
