using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Net.Http.Headers;

namespace STP.Scheduler.Infrastructure.Factories
{
  public sealed class HttpClientFactory : IHttpClientFactory
  {
    private readonly ConcurrentDictionary<Uri, HttpClient> _httpClients;

    public HttpClientFactory()
    {
      _httpClients = new ConcurrentDictionary<Uri, HttpClient>();
    }

    public HttpClient Create(string baseAddress)
    {
      var client = _httpClients.GetOrAdd(new Uri(baseAddress),
          b => new HttpClient { BaseAddress = b });

      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      return client;
    }

    public void Dispose()
    {
      foreach (var httpClient in _httpClients.Values)
      {
        httpClient.Dispose();
      }
    }
  }
}
