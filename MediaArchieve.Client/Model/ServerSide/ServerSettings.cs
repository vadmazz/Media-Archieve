namespace MediaArchieve.Client.Model.ServerSide
{
    public static class ServerSettings
    {
        public static string Url => "https://localhost:44392/api/";
        public static string FolderUrl => Url+"folders/";
        public static string ItemUrl => Url+"items/";
    }
}