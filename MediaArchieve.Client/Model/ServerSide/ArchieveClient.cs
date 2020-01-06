using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MediaArchieve.Client.Model.ServerSide
{
    public class ArchieveClient
    {
        private HttpClient _client = new HttpClient();

        public async Task<HttpResponseMessage> Post(string obj, string url)
        {
            var content = new StringContent(obj, Encoding.UTF8, "application/json");
            return await _client.PostAsync(url, content);
        }

        public async Task<HttpResponseMessage> Get(string url)
        {
            return await _client.GetAsync(url);
        }
    }
}