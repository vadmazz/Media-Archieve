﻿using System.Collections.Generic;

namespace MediaArchieve.Shared
{
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Item> Items { get; set; }
    }
}
