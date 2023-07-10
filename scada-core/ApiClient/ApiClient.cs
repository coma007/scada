using System.Configuration;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using scada_core.Exception;

namespace scada_core.ApiClient
{
    public class ApiClient
    {
        private HttpClient _httpClient;
        private HttpClientHandler _handler;
        private readonly string _apiKeyName;
        private readonly string _apiKeyValue;
        private readonly string _apiUrl;

        public ApiClient()
        {
            _apiKeyName = ConfigurationSettings.AppSettings["ApiKeyName"];
            _apiKeyValue = ConfigurationSettings.AppSettings["ApiKeyValue"];
            ConfigureHttpClient();
        }

        public async Task<JToken> MakeApiRequest(string endpointUrl)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(endpointUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                {
                    content = "{}";
                }
                return JToken.Parse(content);
            }

            _httpClient.Dispose();
            _handler.Dispose();
            throw new HttpErrorException("Request failed with status code: " + response.StatusCode + "\nRequest error message: " + response.Content);
        }
        
        public async Task<JToken> MakeApiRequest(string endpointUrl, HttpMethod httpMethod, HttpContent? requestBody = null)
        {
            HttpRequestMessage request = new HttpRequestMessage(httpMethod, endpointUrl);
            request.Content = requestBody;

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                {
                    content = "{}";
                }
                return JToken.Parse(content);
            }

            _httpClient.Dispose();
            _handler.Dispose();
            throw new HttpErrorException("Request failed with status code: " + response.StatusCode + "\nRequest error message: " + response.Content);
        }

        private void ConfigureHttpClient()
        {
            _handler = new HttpClientHandler();
            _handler.ServerCertificateCustomValidationCallback = ValidateCertificate;

            _httpClient = new HttpClient(_handler);
            _httpClient.DefaultRequestHeaders.Add(_apiKeyName, _apiKeyValue);
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static bool ValidateCertificate(HttpRequestMessage request, X509Certificate2 certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}