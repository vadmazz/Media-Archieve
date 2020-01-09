using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MediaArchieve.Shared;
using Newtonsoft.Json;

namespace MediaArchieve.Client.Model.ServerSide
{
    public class FoldersService
    {
        private readonly ArchieveClient _client = new ArchieveClient();
        /// <summary>
        /// Создает папку на сервере
        /// Возвращает true or false в зависимости от успешности выполнения
        /// </summary>
        public async Task CreateFolder(Folder folder)
        {
            var folderJson = JsonConvert.SerializeObject(folder,
                new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All});
            var response = await _client.Post(folderJson, ServerSettings.FolderUrl);
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException();
        }

        /// <summary>
        /// Обновляет папку на сервере
        /// Возвращает true or false в зависимости от успешности выполнения
        /// </summary>
        public async Task UpdateFolder(Folder folder)
        {
            var folderJson = JsonConvert.SerializeObject(folder,
                new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All});
            var response = await _client.Put(folderJson, ServerSettings.FolderUrl);
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException();
        }
        
        /// <summary>
        /// Удаляет папку с сервера
        /// Возвращает удаленный объект
        /// </summary>
        public async Task<Folder> DeleteFolder(Folder folder)
        {
            var response = await _client.Delete(folder.Id, ServerSettings.FolderUrl);
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException();
            var folderJson = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject(folderJson,
                new JsonSerializerSettings{TypeNameHandling = TypeNameHandling.All});
            
            return item as Folder;
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