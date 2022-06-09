﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using VodLib.models;
using VodLib.data;

namespace WPF_Client
{
    internal class RestHelper
    {
        static Uri baseUri = new Uri("https://localhost:7136/api/vod/");
        static HttpClient client = new HttpClient();

        public static JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public static async Task<Client> GetClientWithNameAsync(string firstname, string lastname)
        {
            HttpResponseMessage response = await client.GetAsync($"{baseUri}clients/{firstname}/{lastname}");


            if (!response.IsSuccessStatusCode)
                return new Client(){client_id = -404};              
            
            string content = await response.Content.ReadAsStringAsync();
            Client suspect = JsonSerializer.Deserialize<Client>(content,options);

            return suspect;

        }

        public static async Task<string> PostNewClientAsync(string firstname, string lastname)
        {
            Client C1 = new Client();
            C1.firstname = firstname;
            C1.lastname = lastname;

            string json = JsonSerializer.Serialize(C1);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{baseUri}clients", content);

            if (!response.IsSuccessStatusCode)
                return "-404";

            string responsedecoded = await response.Content.ReadAsStringAsync();
            

            return responsedecoded;
        }

    }
}
