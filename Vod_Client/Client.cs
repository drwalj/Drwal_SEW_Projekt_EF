using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Vod_Client
{
    internal class Client
    {
        static async Task Main(string[] args)
        {
            Console.Title = "VoD Console";
            Console.BackgroundColor = ConsoleColor.Blue;

            Uri uri = new Uri("https://localhost:7136/api/VoD");

            HttpClient client = new HttpClient();
            client.BaseAddress = uri;

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

        }
    }
}