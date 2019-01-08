using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API_BoilerPlate.BRL.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace API_BoilerPlate.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> PostData<T>(string accessToken, string url, HttpContent httpContent)
        {
            //TODO: this doesnt look right here..... clear the defaultrequest headers instead of checking if null, 
            //as the token may have expired yet you're still sending it.

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using (HttpResponseMessage res = await _httpClient.PostAsync(url, httpContent))
            using (HttpContent content = res.Content)
            {
                string data = await content.ReadAsStringAsync();

                if ((data == null) || (data == ""))
                    throw new Exception("There was a problem communicating with the API " + url);

                var jtoken = JObject.Parse(data);

                return JsonConvert.DeserializeObject<T>(data);

                throw new Exception("Errr: " + data);
            }
        }

        public async Task<List<T>> GetData<T>(string accessToken, string url) where T : class
        {
            //TODO: this doesnt look right here..... clear the defaultrequest headers instead of checking if null, 
            //as the token may have expired yet you're still sending it.

            var emptyModel = new List<T>();

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using (HttpResponseMessage res = await _httpClient.GetAsync(url))
            using (HttpContent content = res.Content)
            {
                var data = await content.ReadAsStringAsync();

                if ((data == null) || (data == ""))
                    throw new Exception("There was a problem communicating with the API " + url);


                var resolvedData = JsonConvert.DeserializeObject<T>(data);

                if (resolvedData != null)
                {
                    return resolvedData as List<T>;
                }
                else
                {
                    return emptyModel;
                }

                throw new Exception(data);
            }
        }

        public async Task<T> GetDataSingle<T>(string accessToken, string url) where T : class
        {
            //TODO: this doesnt look right here..... clear the defaultrequest headers instead of checking if null, 
            //as the token may have expired yet you're still sending it.

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using (HttpResponseMessage res = await _httpClient.GetAsync(url))
            using (HttpContent content = res.Content)
            {
                var data = await content.ReadAsStringAsync();

                if ((data == null) || (data == ""))
                    throw new Exception("There was a problem communicating with the API " + url);

                var resolvedData = JsonConvert.DeserializeObject<T>(data);

                return resolvedData as T;
            }
        }
    }
}