using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace STP.Scheduler.Infrastructure.Factories
{
  public static class HttpClientReponse<T>
  {
    /// <summary>
    /// Read the message from HttpResponseMessage and throw and ReplayDomainException if we get our internal Http error code: 460
    /// </summary>
    /// <param name="responseMessage"></param>
    /// <returns></returns>
    public static T ReadMessage(HttpResponseMessage responseMessage)
    {
      var responseData = responseMessage.Content.ReadAsStringAsync().Result;

      var httpCode = (int)responseMessage.StatusCode;

      if (httpCode >= 400)
      {
        throw new Exception(responseData);
      }

      //success
      return JsonConvert.DeserializeObject<T>(responseData);
    }
  }
}
