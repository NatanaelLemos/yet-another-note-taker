using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Http
{
    public class RestClient : IRestClient
    {
        private static Dictionary<string, string> LoginData = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "client_secret", "notetaker"  },
                { "scope", "YetAnotherNoteTaker" },
                { "client_id", "notetaker" }
            };

        public async Task<T> Get<T>(string url, string authToken = "")
        {
            using var client = GetClient(authToken);

            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<HateoasDto<T>>(json).Value;
        }

        public async Task<TOut> Post<TOut>(string url, object dto, string authToken = "")
        {
            using var client = GetClient(authToken);
            using var json = CreateJson(dto);

            var response = await client.PostAsync(url, json);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(jsonResponse);
            }

            return JsonConvert.DeserializeObject<HateoasDto<TOut>>(jsonResponse).Value;
        }

        public async Task<TOut> Put<TOut>(string url, object dto, string authToken = "")
        {
            using var client = GetClient(authToken);
            using var json = CreateJson(dto);

            var response = await client.PutAsync(url, json);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<HateoasDto<TOut>>(jsonResponse).Value;
        }

        public async Task Delete(string url, string authToken = "")
        {
            using var client = GetClient(authToken);
            await client.DeleteAsync(url);
        }

        public async Task<string> Authenticate(string url, string email, string password)
        {
            var loginData = new Dictionary<string, string>(LoginData);
            loginData.Add("username", email);
            loginData.Add("password", password);

            using var client = GetClient("");
            using var body = CreateFormUrlEncoded(loginData);

            var response = await client.PostAsync(url, body);
            var json = await response.Content.ReadAsStringAsync();
            var token = (JObject)JsonConvert.DeserializeObject(json);
            return token.Value<string>("access_token");
        }

        private HttpClient GetClient(string authToken)
        {
            var client = new HttpClient();

            if (!string.IsNullOrWhiteSpace(authToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authToken);
            }

            return client;
        }

        private HttpContent CreateJson<T>(T dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private HttpContent CreateFormUrlEncoded(Dictionary<string, string> parameters)
        {
            var content = new FormUrlEncodedContent(parameters);
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            return content;
        }
    }
}
