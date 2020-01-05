﻿namespace MediaArchieve.Shared.Items
{
    /// <summary>
    /// Тип источника Документ
    /// </summary>
    public class Document : Item
    {
        public Document()
        {
            IconSource = "C:\\Users\\vadim\\Downloads\\doc.png";
        }

        public Document(string name, string description) 
            : base(name, description)
        {
            IconSource = "C:\\Users\\vadim\\Downloads\\doc.png";
        }
    }
}
