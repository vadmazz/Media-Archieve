using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
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
        
        public async Task<HttpResponseMessage> Delete(string url) =>
            await _client.DeleteAsync(url);

        public bool CheckConnection()
        {
            IPAddress IP;
            if (IPAddress.TryParse(ServerSettings.Host,out IP)){
                Socket s = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.Tcp);
                try
                {   
                    s.Connect(ServerSettings.Host, ServerSettings.Port);
                }
                catch(Exception)
                {
                    return false;
                }
            }
            return true;
        }
    }
}