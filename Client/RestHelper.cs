using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using VodLib.models;
using VodLib.data;

namespace WPF_Client
{
    public class RestHelper
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
            return JsonSerializer.Deserialize<Client>(content,options);
        }

        public static async Task<Client> GetClientWithIDAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{baseUri}clients/{id}");

            if (!response.IsSuccessStatusCode)
                return new Client() { client_id = -404 };

            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Client>(content, options);
        }

        public static async Task<IEnumerable<string>> GetAllPaymentMethodsAsync()
        {
            HttpResponseMessage response = await client.GetAsync($"{baseUri}payments");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Getting Payments went wrong");

            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<string>>(content, options);
        }

        public static async Task<IEnumerable<string>> GetAllDeliveryMethodsAsync()
        {
            HttpResponseMessage response = await client.GetAsync($"{baseUri}deliveries");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Getting Deliveries went wrong");

            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<string>>(content, options);
        }


        public static async Task<bool> PatchClientAsync(Client myClient, int id)
        {
            string json = JsonSerializer.Serialize(myClient);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{baseUri}clients/patch/{id}", content);

            if (!response.IsSuccessStatusCode)
                return false;

            return true;

        }

        public static async Task<IEnumerable<Movie>> GetMoviesWithGenreAndTitleAsync(string title, string genre)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = await client.GetAsync($"{baseUri}movies/{genre}/{title}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Getting Movie with genre {genre} and title {title} went wrong");
            }

            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Movie>>(content, options);

        }

        public static async Task<bool> DeleteAccountAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{baseUri}delete/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            
            return true;

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

            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<bool> PostNewOrderAsync(Order myOrder, int id)
        {
            string json = JsonSerializer.Serialize(myOrder);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{baseUri}clients/{id}/orders", content);

            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }

        public static async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            HttpResponseMessage response = await client.GetAsync($"{baseUri}movies");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error getting all movies.");
            }

            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Movie>>(content, options);

        }

        

    }
}
