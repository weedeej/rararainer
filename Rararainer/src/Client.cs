using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AdonisUI.Controls;
using RestSharp;

namespace Rararainer.API
{
    public partial class Servers
    {
        public List<String> servers { get;set; }
    }
    public partial class ErrorResponse
    {
        public String err { get;set; }
    }
    internal class Client
    {
        public RestClient client;
        private String baseUrl = "http://localhost:3000/api";
        public Client()
        {
            RestClientOptions restOptions = new RestClientOptions(baseUrl);
            this.client = new RestClient(restOptions);
            var resp = client.Get(new RestRequest("/test") { Method = Method.Get });
            Console.WriteLine(resp.Headers);
        }

        public List<String> getServers(String hwid)
        {
            try
            {
                Servers resp = this.client.Get<Servers>(new RestRequest("/servers/" + hwid) { Method = Method.Get });
                return resp.servers;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public MemoryWriter.WriterConfig getConfig(String server, String hwid)
        {
            RestResponse resp = this.client.Execute(new RestRequest("/config/" + server + "/" + hwid) { Method = Method.Get });
            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonSerializer.Deserialize<MemoryWriter.WriterConfig>(resp.Content);
            ErrorResponse err = JsonSerializer.Deserialize<ErrorResponse>(resp.Content);
            MessageBox.Show(err.err, "Error");
            return null;
        }
    }
}
