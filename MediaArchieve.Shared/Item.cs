namespace MediaArchieve.Shared
{
    public class Item
    {
        public int Id { get; set; }
        public string IconSource { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public Folder Folder { get; set; }
        public int FolderId { get; set; }

        public Item()
        {
            
        }
        public Item(string iconSource, string name, string description)
        {
            IconSource = iconSource;
            Name = name;
            Description = description;
        }
        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void Update(Item item)
        {
            Name = item.Name;
            Description = item.Description;
        }
    }
}
