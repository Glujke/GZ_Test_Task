using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GZ_Test_WPF_Application.Api
{
    public class HttpApi : IDisposable
    {
        private readonly string url;
        private readonly HttpClient httpClient;
        private string requestUrl;
        public HttpApi(string url)
        {
            this.url = url;
            this.httpClient = new HttpClient();
            ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, errors) =>
            {
                return true;
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public IEnumerable<T> GetItems<T>() where T : class
        {
            requestUrl = $"{url}/{typeof(T).Name}";
            var response = httpClient.GetAsync(requestUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<IEnumerable<T>>().Result;
            } else
            {
                return null;
            }

        }
        public T GetItem<T>(int id) where T : class
        {
            requestUrl = $"{url}/{typeof(T).Name}";
            HttpResponseMessage response = httpClient.GetAsync($"{requestUrl}/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<T>().Result;
            }
            else
            {
                return null;
            }

        }

        public T AddItem<T>(T item) where T : class
        {
            requestUrl = $"{url}/{typeof(T).Name}";
            HttpResponseMessage response = httpClient.PostAsJsonAsync($"{requestUrl}/", item).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<T>().Result;
        }
        public void UpdateItem<T>(int id, T item)
        {
            requestUrl = $"{url}/{typeof(T).Name}";
            HttpResponseMessage response = httpClient.PutAsJsonAsync($"{requestUrl}/{id}", item).Result;
            response.EnsureSuccessStatusCode();
        }

        public void DeleteItem<T>(int id) where T : class
        {
            requestUrl = $"{url}/{typeof(T).Name}";
            HttpResponseMessage response = httpClient.DeleteAsync($"{requestUrl}/{id}").Result;
            response.EnsureSuccessStatusCode();
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}
