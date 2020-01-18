namespace MediaArchieve.Client.Model.ServerSide
{
    public static class ServerSettings
    {
        public static string Url { get; set; } = "https://localhost:5001/api/";
        public static string Host { get; set; } = "127.0.0.1";
        public static int Port { get; set; } = 5001;
        public static string FolderUrl { get; set; } = Url+"folders/";
        public static string ItemUrl { get; set; } = Url+"items/";
        public static int TimeOutMilliseconds { get; set; } = 5000;
    }
    
}