using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MediaArchieve.Shared;
using Newtonsoft.Json;

namespace MediaArchieve.Client.Model.ServerSide
{
    public class FoldersService
    {
        private ArchieveClient _client = new ArchieveClient();
        /// <summary>
        /// Создает папку на сервере
        /// Возвращает true or false в зависимости от успешности выполнения
        /// </summary>
        public async Task<bool> CreateFolder(Folder folder)
        {
            var folderJson = JsonConvert.SerializeObject(folder,
                new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All});
            var response = await _client.Post(folderJson, ServerSettings.FolderUrl);
            
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Возвращает коллекцию всех папок
        /// </summary>
        public async Task<IEnumerable<Folder>> GetAllFolders()
        {
            var response = await _client.Get(ServerSettings.FolderUrl);
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException();
            var folderJson = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject(folderJson,
                new JsonSerializerSettings{TypeNameHandling = TypeNameHandling.All});

            return item as IEnumerable<Folder>;
        }
    }
}