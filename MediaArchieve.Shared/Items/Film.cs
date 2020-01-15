namespace MediaArchieve.Shared.Items
{
    /// <summary>
    /// Тип источника Фильм
    /// </summary>
    public class Film : Item
    {
        public Film()
        {
            IconSource = "Movie";
        }

        public Film(string name, string description) 
            : base(name, description)
        {
            IconSource = "Movie";
        }
    }
}
