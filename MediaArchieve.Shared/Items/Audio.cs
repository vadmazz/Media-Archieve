namespace MediaArchieve.Shared.Items
{
    /// <summary>
    /// Тип источника Аудио
    /// </summary>
    public class Audio : Item
    {
        public Audio()
        {
            IconSource = "Audio";
        }

        public Audio(string name, string description) 
            : base(name, description)
        {
            IconSource = "Audio";
        }
    }
}
