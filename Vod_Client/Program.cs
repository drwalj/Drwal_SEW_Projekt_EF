using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Vod_Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            Uri uri = new Uri("https://localhost:7256/api/restaurant/%22);

            HttpClient client = new HttpClient();
            client.BaseAddress = uri;

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

        }
    }
}