namespace MediaArchieve.Shared.Items
{
    /// <summary>
    /// Тип источника Документ
    /// </summary>
    public class Document : Item
    {
        public Document()
        {
            IconSource = "FileDocumentBoxOutline";
        }

        public Document(string name, string description) 
            : base(name, description)
        {
            IconSource = "FileDocumentBoxOutline";
        }
    }
}
