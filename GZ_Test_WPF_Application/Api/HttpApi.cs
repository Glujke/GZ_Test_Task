using GZ_Test_WPF_Application.Models;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GZ_Test_WPF_Application.Api
{
    public class HttpApi<T> : IDisposable where T : class
    {
        private readonly string url;
        private readonly HttpClient httpClient;
        private readonly string requestUrl;
        public HttpApi(string url)
        {
            this.url = url;
            this.httpClient = new HttpClient();
            requestUrl = $"{url}/{typeof(T).Name}";
            ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, errors) =>
            {
                return true;
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ObservableCollection<T> GetItems()
        {
            var response = httpClient.GetAsync(requestUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<ObservableCollection<T>>().Result;
            } else
            {
                return null;
            }

        }
        public T GetItem(int id)
        {
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

        public T AddItem(T item)
        {
            HttpResponseMessage response = httpClient.PostAsJsonAsync($"{requestUrl}/", item).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<T>().Result;
        }
        public void UpdateItem(int id, T item)
        {
            HttpResponseMessage response = httpClient.PutAsJsonAsync($"{requestUrl}/{id}", item).Result;
            response.EnsureSuccessStatusCode();
        }

        public void DeleteItem(int id)
        {
            HttpResponseMessage response = httpClient.DeleteAsync($"{requestUrl}/{id}").Result;
            response.EnsureSuccessStatusCode();
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}
