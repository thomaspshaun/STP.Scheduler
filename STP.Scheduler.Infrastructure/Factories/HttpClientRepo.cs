using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace STP.Scheduler.Infrastructure.Factories
{
  public class HttpClientRepo : IHttpClientRepo
  {
    private IHttpClientFactory _httpClientFactory;

    public HttpClientRepo(IHttpClientFactory httpClientFactory)
    {
      _httpClientFactory = httpClientFactory;
    }

    public Res GetFromWebApi<Res>(string serviceUrl, string action, string controller) where Res : new()
    {
      var httpClient = _httpClientFactory.Create(serviceUrl);

      var stringContent = new StringContent(string.Empty);

      var response = httpClient.PostAsync($"{httpClient.BaseAddress}/{controller}/{action}",
                                            new StringContent(string.Empty, Encoding.UTF8, "application/json")).Result;

      return HttpClientReponse<Res>.ReadMessage(response);
    }

    public Res PostToWebApi<Req, Res>(Req req, string serviceUrl, string action, string controller) where Res : new()
    {
      var httpClient = _httpClientFactory.Create(serviceUrl);

      var response = httpClient.PostAsync(string.Format("{0}/{1}/{2}",
                            httpClient.BaseAddress,
                            controller,
                            action),
                            new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json")).Result;

      return HttpClientReponse<Res>.ReadMessage(response);
    }

    public void PostToWebApi<Req>(Req req, string serviceUrl, string action, string controller)
    {
      var httpClient = _httpClientFactory.Create(serviceUrl);

      var response = httpClient.PostAsync(string.Format("{0}/{1}/{2}",
                            httpClient.BaseAddress,
                            controller,
                            action),
                         new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json")).Result;

      var responseData = response.Content.ReadAsStringAsync().Result;

      var httpCode = (int)response.StatusCode;

      if (httpCode >= 400)
      {
        throw new Exception(responseData);
      }
    }

    public void PostToWebApi(string serviceUrl, string action, string controller)
    {
      var httpClient = _httpClientFactory.Create(serviceUrl);

      var response = httpClient.PostAsync(string.Format("{0}/{1}/{2}",
                            httpClient.BaseAddress,
                            controller,
                            action),
                          new StringContent(string.Empty, Encoding.UTF8, "application/json")).Result;

      var responseData = response.Content.ReadAsStringAsync().Result;

      var httpCode = (int)response.StatusCode;

      //if (httpCode >= 400)
      //{
      //  throw new Exception(responseData);
      //}
    }

  }
}
