using System;
using System.Collections.Generic;
using System.Text;

namespace MediaArchieve.Shared.Items
{
    /// <summary>
    /// Тип источника Фотография
    /// </summary>
    public class Photo : Item
    {
        public Photo()
        {
            IconSource = "Image";
        }

        public Photo(string name, string description) 
            : base(name, description)
        {
            IconSource = "Image";
        }
    }
}
