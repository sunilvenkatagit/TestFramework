using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace TestAutomationFramework.Utils
{
    public class ApiLogger
    {
        #region Request Logging
        public static void LogRequestToReport(RestRequest restRequest, IDictionary<string, string> requestHeaders, IDictionary<string, string> requestParameters, object requestBody, DataFormat dataFormat = DataFormat.Json)
        {
            throw new NotImplementedException("Logging to a report is not implemented yet!!!");
        }
        public static void LogRequestToConsole(RestRequest restRequest, IDictionary<string, string> requestHeaders, IDictionary<string, string> requestParameters, object requestBody, DataFormat dataFormat = DataFormat.Json)
        {
            PrepareApiRequestLogger(requestHeaders, requestParameters, requestBody, dataFormat, out string headersToReport, out string parametersToReport, out string bodyToReport);

            Console.WriteLine(">>>>>>>>>>>>>>>>>>> API REQUEST DETAILS <<<<<<<<<<<<<<<<<<<\n");
            Console.WriteLine($"Request Uri: {restRequest.Resource}");
            Console.WriteLine($"Request Method: {restRequest.Method}");
            Console.WriteLine($"Request Headers:\n{headersToReport}");
            Console.WriteLine($"Request Parameters:\n{parametersToReport}");
            Console.WriteLine($"Request Body:\n{bodyToReport}\n");
        }
        #endregion Request Logging

        #region Response Logging
        public static void LogApiResposeToReport(RestResponse response)
        {
            throw new NotImplementedException("Logging to a report is not implemented yet!!!");
        }
        public static void LogApiResposeToConsole(RestResponse response)
        {
            PrepareApiResponseLogger(response, out string headersToReport, out string bodyToReport);

            Console.WriteLine(">>>>>>>>>>>>>>>>>>> API RESPONSE DETAILS <<<<<<<<<<<<<<<<<<<\n");
            Console.WriteLine($"Response Status Code: {(int)response.StatusCode}");
            Console.WriteLine($"Response Status Description: {response.StatusDescription}");
            Console.WriteLine($"Response Headers:\n{headersToReport}");
            Console.WriteLine($"Response Body:\n{bodyToReport}");
        }
        #endregion Response Logging

        #region Helper Methods
        private static void PrepareApiRequestLogger(IDictionary<string, string> requestHeaders, IDictionary<string, string> requestParameters, object requestBody, DataFormat dataFormat, out string headersToReport, out string parametersToReport, out string bodyToReport)
        {
            bodyToReport = null;

            // Request Headers
            if (requestHeaders != null && requestHeaders.Count > 0)
                headersToReport = string.Join("\n", requestHeaders.Select(ele => "  . " + ele.Key + " = " + ele.Value));
            else
                headersToReport = "     --- None ---";

            // Request Parameters
            if (requestParameters != null && requestParameters.Count > 0)
                parametersToReport = string.Join("\n", requestParameters.Select(ele => "  . " + ele.Key + " = " + ele.Value));
            else
                parametersToReport = "     --- None ---";

            // Request Body
            if (requestBody != null && !requestBody.GetType().Equals(typeof(string))) // body object is a POCO
            {
                if (dataFormat == DataFormat.Xml)
                    bodyToReport = new DotNetXmlSerializer().Serialize(requestBody);
                else
                    bodyToReport = JsonConvert.SerializeObject(requestBody, Formatting.Indented);
            }
            else if (requestBody != null && requestBody.GetType().Equals(typeof(string)))// body object is a string
            {
                try
                {
                    if (requestBody.ToString().TrimStart().StartsWith("<") || dataFormat == DataFormat.Xml) // XML body
                    {
                        var parsedXml = XDocument.Parse(requestBody.ToString());
                        string xmlFormattedString = parsedXml.ToString();
                        bodyToReport = xmlFormattedString;
                    }
                    else if (requestBody.ToString().TrimStart().StartsWith("{") || requestBody.ToString().TrimStart().StartsWith("[")) // Json body
                    {
                        dynamic parsedJson = JsonConvert.DeserializeObject(requestBody.ToString());
                        string jsonFormattedString = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
                        bodyToReport = jsonFormattedString;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"Failed to convert the below Response Content to a pretty format string. Printing as received.");
                }
            }
            else
            {
                bodyToReport = "     --- None ---";
            }
        }
        private static void PrepareApiResponseLogger(RestResponse response, out string headersToReport, out string bodyToReport)
        {
            // Response Headers
            if (response.Headers.Count > 0)
                headersToReport = $"{string.Join("\n", response.Headers.Select(ele => "  . " + ele.Name + " = " + ele.Value)) }";
            else
                headersToReport = "     --- None ---";

            // Response Body
            bodyToReport = ConvertStringResponseToPrettyPrint(response.Content);
        }
        private static string ConvertStringResponseToPrettyPrint(string responseContent)
        {
            // Convert json or xml response into a pretty format string

            if (!string.IsNullOrEmpty(responseContent))
            {
                try
                {
                    if (responseContent.TrimStart().StartsWith("<"))
                    {
                        var parsedXml = XDocument.Parse(responseContent);
                        string xmlFormattedString = parsedXml.ToString();
                        return xmlFormattedString;
                    }
                    else if (responseContent.TrimStart().StartsWith("{") || responseContent.TrimStart().StartsWith("["))
                    {
                        dynamic parsedJson = JsonConvert.DeserializeObject(responseContent);
                        string jsonFormattedString = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
                        return jsonFormattedString;
                    }
                }
                catch (Exception)
                {
                    return $"\nEXCEPTION: Failed to convert the below Response Body to a pretty format string. Printing as received.\n {responseContent}";
                }
                return responseContent;
            }
            else
                return $"     --- No Response Body ---";
        }
        #endregion Helper Methods
    }
}
