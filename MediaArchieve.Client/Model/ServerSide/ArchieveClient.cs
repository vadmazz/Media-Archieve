using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MediaArchieve.Client.Model.ServerSide
{
    public class ArchieveClient
    {
        private HttpClient _client = new HttpClient();

        public async Task<HttpResponseMessage> Post(string json, string url)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _client.PostAsync(url, content);
        }

        public async Task<HttpResponseMessage> Get(string url) =>
            await _client.GetAsync(url);
        

        public async Task<HttpResponseMessage> Put(string json, string url)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _client.PutAsync(url, content);
        } 
        
        public async Task<HttpResponseMessage> Delete(int id, string url) =>
            await _client.DeleteAsync(url+id);
    }
}