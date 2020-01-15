namespace MediaArchieve.Shared.Items
{
    /// <summary>
    /// Тип источника Клип
    /// </summary>
    public class Clip : Item
    {
        public Clip()
        {
            IconSource = "Videocam";
        }

        public Clip(string name, string description) 
            : base(name, description)
        {
            IconSource = "Videocam";
        }
    }
}
