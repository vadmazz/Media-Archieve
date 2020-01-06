using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MediaArchieve.Shared;
using Newtonsoft.Json;

namespace MediaArchieve.Client.Model.ServerSide
{
    public class ItemsService : ArchieveClient
    {
        /// <summary>
        /// Возвращает true or false в зависимости от того создался ли источник
        /// </summary>
        public async Task<bool> CreateItem(Item item)
        {
            var itemJson = JsonConvert.SerializeObject(item,
                new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All});
            var response = await Post(itemJson, ServerSettings.ItemUrl);
            
            return response.IsSuccessStatusCode;
        }
        
        /// <summary>
        /// Возвращает коллекцию всех источников
        /// </summary>
        public async Task<IEnumerable<Item>> GetAllItems()
        {
            var response = await Get(ServerSettings.ItemUrl);
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException();
            var itemJson = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject(itemJson,
                new JsonSerializerSettings{TypeNameHandling = TypeNameHandling.All});

            return item as IEnumerable<Item>;
        }
        
        //todo:UPDATE, DELETE 
    }
}