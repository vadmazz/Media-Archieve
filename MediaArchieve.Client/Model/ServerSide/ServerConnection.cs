using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MediaArchieve.Client.Helpers;
using MediaArchieve.Shared;

namespace MediaArchieve.Client.Model.ServerSide
{
    public class ServerConnection
    {
        public event Action<object> ContextChanged;

        public ServerConnection()
        {
           CheckConnection();
           Thread thread = new Thread(Listen);
           thread.Start();
        }

        public void CheckConnection()
        {
            ArchieveClient a = new ArchieveClient();
            if (!a.CheckConnection())
                throw new InvalidConnectionException();
        }

        private void Listen()
        {
            var foldersList = new List<Folder>();
            var fs = new FoldersService();
            while (true)
            {
                Thread.Sleep(ServerSettings.TimeOutMilliseconds);
                var list = fs.GetAllFolders().Result.ToList();
                if (list != foldersList)
                    ContextChanged?.Invoke(null);
            }
        }
    }
}