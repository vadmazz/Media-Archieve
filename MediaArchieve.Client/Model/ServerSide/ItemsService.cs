using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MediaArchieve.Shared;
using Newtonsoft.Json;

namespace MediaArchieve.Client.Model.ServerSide
{
    public class ItemsService
    {
        private readonly ArchieveClient _client = new ArchieveClient();
        /// <summary>
        /// Создает источник на сервере
        /// </summary>
        public async Task CreateItem(Item item, int folderId)
        {
            var itemJson = JsonConvert.SerializeObject(item,
                new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All});
            var response = await _client.Post(itemJson, ServerSettings.ItemUrl+folderId);
            
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException();
        }
        
        /// <summary>
        /// Возвращает коллекцию всех источников
        /// </summary>
        public async Task<IEnumerable<Item>> GetAllItems(int folderId)
        {
            var response = await _client.Get(ServerSettings.ItemUrl + folderId);
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException();
            var itemJson = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject(itemJson,
                new JsonSerializerSettings{TypeNameHandling = TypeNameHandling.All});

            return item as IEnumerable<Item>;
        }
        
        /// <summary>
        /// Обновляет источник на сервере
        /// </summary>
        public async Task UpdateItem(Item item)
        {
            var itemJson = JsonConvert.SerializeObject(item,
                new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All});
            var response = await _client.Put(itemJson, ServerSettings.ItemUrl+item.FolderId+"/"+item.Id);
            if (!response.IsSuccessStatusCode)
                throw new 
                    Exception(response.ToString());
        }
        
        /// <summary>
        /// Удаляет источник с сервера
        /// Возвращает удаленный объект
        /// </summary>
        public async Task<Item> DeleteItem(Item item)
        {
            var response = await _client.Delete(ServerSettings.ItemUrl+item.FolderId + "/" + item.Id);
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException();
            var itemJson = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject(itemJson,
                new JsonSerializerSettings{TypeNameHandling = TypeNameHandling.All});
            
            return item as Item;
        }
    }
}