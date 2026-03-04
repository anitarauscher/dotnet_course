using RestSharp;
using Epam.Automation.src.Core;

namespace Epam.Automation.src.Api
{
    public class ApiClient
    {
        private readonly RestClient _client;

        public ApiClient(string baseUrl)
        {
            var options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(ConfigManager.ApiTimeoutInSeconds)
            };
            _client = new RestClient(options);
            Logger.Info($"API Client initialized with base URL: {baseUrl}");
        }

        public RestResponse Get(string endpoint)
        {
            Logger.Info($"Sending GET request to: {endpoint}");
            var request = new RestRequest(endpoint, Method.Get);
            var response = _client.Execute(request);
            Logger.Info($"Received response with status code: {response.StatusCode}");
            return response;
        }

        public RestResponse Post<T>(string endpoint, T body) where T : class
        {
            Logger.Info($"Sending POST request to: {endpoint}");
            var request = new RestRequest(endpoint, Method.Post);
            request.AddJsonBody(body);
            var response = _client.Execute(request);
            Logger.Info($"Received response with status code: {response.StatusCode}");
            return response;
        }

        public RestResponse Delete(string endpoint)
        {
            Logger.Info($"Sending DELETE request to: {endpoint}");
            var request = new RestRequest(endpoint, Method.Delete);
            var response = _client.Execute(request);
            Logger.Info($"Received response with status code: {response.StatusCode}");
            return response;
        }

        public RestResponse Put<T>(string endpoint, T body) where T : class
        {
            Logger.Info($"Sending PUT request to: {endpoint}");
            var request = new RestRequest(endpoint, Method.Put);
            request.AddJsonBody(body);
            var response = _client.Execute(request);
            Logger.Info($"Received response with status code: {response.StatusCode}");
            return response;
        }
    }
}