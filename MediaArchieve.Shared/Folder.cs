using System.Collections.Generic;

namespace MediaArchieve.Shared
{
    public class Folder 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Item> Items { get; set; }

        public void Update(Folder folder)
        {
            Name = folder.Name;
            Items = folder.Items;
        }
    }
}
