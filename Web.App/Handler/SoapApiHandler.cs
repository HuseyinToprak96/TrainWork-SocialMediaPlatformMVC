using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Text;

namespace Web.App.Handler
{
    public class SoapApiHandler<T>
    {
        private readonly string _endpointUrl;
        private readonly string _jwtToken;

        public SoapApiHandler(string endpointUrl, string jwtToken)
        {
            _endpointUrl = endpointUrl;
            _jwtToken = jwtToken;
        }
        public T GetById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_endpointUrl}/{id}");
            return SendRequest<T>(request);
        }

        public IEnumerable<T> GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _endpointUrl);
            return SendRequest<IEnumerable<T>>(request);
        }

        public void Create(T item)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _endpointUrl);
            request.Content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            SendRequest<object>(request);
        }

        public void Update(int id, T item)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_endpointUrl}/{id}");
            request.Content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            SendRequest<object>(request);
        }

        public void Delete(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_endpointUrl}/{id}");
            SendRequest<object>(request);
        }

        private void SetAuthorizationHeader(HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
        }

        private T SendRequest<T>(HttpRequestMessage request)
        {
            using (var client = new HttpClient())
            {
                SetAuthorizationHeader(client);

                var response = client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                var responseContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(responseContent);
            }
        }
    }
}