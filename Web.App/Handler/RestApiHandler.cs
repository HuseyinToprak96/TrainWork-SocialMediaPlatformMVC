using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class RestApiHandler
{
    private readonly string _baseUrl;

    public RestApiHandler(string baseUrl)
    {
        _baseUrl = baseUrl;
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest data, string jwtToken)
    {
        using (var httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri(_baseUrl);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            var jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<TResponse>(responseContent);
            return responseObject;
        }
    }

    public async Task<TResponse> GetAsync<TResponse>(string url, string jwtToken)
    {
        using (var httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri(_baseUrl);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<TResponse>(responseContent);
            return responseObject;
        }
    }

    public async Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest data, string jwtToken)
    {
        using (var httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri(_baseUrl);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            var jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<TResponse>(responseContent);

            return responseObject;
        }
    }

    public async Task<bool> DeleteAsync(string url, string jwtToken)
    {
        using (var httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri(_baseUrl);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            var response = await httpClient.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }
    }
}
