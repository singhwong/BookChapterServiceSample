using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BookServiceClientApp.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BookServiceClientApp.Services
{
    public abstract class HttpClientService<T>:IDisposable where T:class
    {
        private HttpClient _httpClient;
        private readonly UrlService _urlService;
        private readonly ILogger<HttpClientService<T>> _logger;
        public HttpClientService(UrlService urlService,ILogger<HttpClientService<T>> logger)
        {
            _urlService = urlService ?? throw new ArgumentNullException(nameof(urlService));
            _logger = logger ??throw new ArgumentNullException(nameof(logger));
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(urlService.BaseAddress);
        }
        private bool _objectDisposed = false;

        public void Dispose()
        {
            _httpClient.Dispose();
            _objectDisposed = true;
        }
        private async Task<string> GetInternalAsync(string requestUri)
        {
            if (string.IsNullOrEmpty(requestUri))
            {
                throw new ArgumentNullException(nameof(requestUri));
            }
            if (_objectDisposed)
            {
                throw new ObjectDisposedException(nameof(_httpClient));
            }
            HttpResponseMessage resp = await _httpClient.GetAsync(requestUri);
            LogInformation($"status from GET {resp.StatusCode}");
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadAsStringAsync();
        }
        private void LogInformation(string message,[CallerMemberName]string callerName = null)
        {
            _logger.LogInformation($"{nameof(HttpClientService<T>)}.{callerName}: {message}");
        }
        public async virtual Task<T> GetAsync(string requestUri)
        {
            if (string.IsNullOrEmpty(requestUri))
            {
                throw new ArgumentNullException(requestUri);
            }
            string json = await GetInternalAsync(requestUri);
            return JsonConvert.DeserializeObject<T>(json);
        }
        public async virtual Task<IEnumerable<T>> GetAllAsync(string requestUri)
        {
            if (string.IsNullOrEmpty(requestUri))
            {
                throw new ArgumentNullException(nameof(requestUri));
            }
            string json = await GetInternalAsync(requestUri);
            Debug.WriteLine(json);
            return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
        }
        public async virtual Task<T> PostAsync(string requestUri,T item)
        {
            if (string.IsNullOrEmpty(requestUri))
            {
                throw new ArgumentNullException(nameof(requestUri));
            }
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (_objectDisposed)
            {
                throw new ObjectDisposedException(nameof(_httpClient));
            }
            string json = JsonConvert.SerializeObject(item);
            HttpContent content = new StringContent(json,Encoding.UTF8,"application/json");
            var resp = await _httpClient.PostAsync(requestUri,content);
            LogInformation($"status form POST {resp.StatusCode}");
            resp.EnsureSuccessStatusCode();
            LogInformation($"added resource at {resp.Headers.Location}");
            json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
        public async virtual Task PutAsync(string requestUri,T item)
        {
            if (string.IsNullOrEmpty(requestUri))
            {
                throw new ArgumentNullException(nameof(requestUri));
            }
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (_objectDisposed)
            {
                throw new ObjectDisposedException(nameof(_httpClient));
            }
            string json = JsonConvert.SerializeObject(item);
            HttpContent content = new StringContent(json,Encoding.UTF8,"application/json");
            var resp = await _httpClient.PutAsync(requestUri,content);
            LogInformation($"status form PUT {resp.StatusCode}");
            resp.EnsureSuccessStatusCode();
        }
        public async Task DeleteAsync(string requestUri)
        {
            if (string.IsNullOrEmpty(requestUri))
            {
                throw new ArgumentNullException(nameof(requestUri));
            }
            var resp = await _httpClient.DeleteAsync(requestUri);
            LogInformation($"status form DELETE {resp.StatusCode}");
            resp.EnsureSuccessStatusCode();
            Console.WriteLine("deleted!");
        }
        public async Task<XDocument> GetAllXmlAsync(string requestUri)
        {
            if (string.IsNullOrEmpty(requestUri))
            {
                throw new ArgumentNullException(nameof(requestUri));
            }
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));
            var resp = await _httpClient.GetAsync(requestUri);
            LogInformation($"status fron Get {resp.StatusCode}");
            resp.EnsureSuccessStatusCode();
            string xml = await resp.Content.ReadAsStringAsync();
            XDocument chapters = XDocument.Parse(xml);
            return chapters;
        }
    }
}
