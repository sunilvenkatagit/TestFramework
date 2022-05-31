using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace TestAutomationFramework.Extensions
{
    public static class ResponseExtension
    {
        public static int GetStatusCode(this RestResponse response)
        {
            return (int)response.StatusCode;
        }
        public static string GetStatusMsg(this RestResponse response)
        {
            return response.StatusCode.ToString();
        }
        public static dynamic Extract(this RestResponse response)
        {
            dynamic deserializedResponse = JsonConvert.DeserializeObject(response.Content);
            return deserializedResponse;
        }
        public static T ExtractAs<T>(this RestResponse response) where T : new()
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
        public static string Path(this RestResponse response, string jsonPath)
        {
            var jObject = JObject.Parse(response.Content);
            var value = (string)jObject.SelectToken(jsonPath);
            return value;
        }
    }
}
