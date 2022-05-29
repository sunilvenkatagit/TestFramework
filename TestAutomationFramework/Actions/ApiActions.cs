using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Serializers;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.Collections.Generic;
using TestAutomationFramework.Utils;

namespace TestAutomationFramework.Actions
{
    public class ApiActions
    {
        #region Properties
        private string RequestUrl { get; set; }
        private IDictionary<string, string> RequestHeaders { get; set; }
        private IDictionary<string, string> RequestParameters { get; set; }
        private object RequestBody_JsonObject { get; set; }
        private string RequestBody_JsonString { get; set; }
        private object RequestBody_XmlObject { get; set; }
        private string RequestBody_XmlString { get; set; }
        private RestClient RestClient { get; set; }
        private RestRequest RestRequest { get; set; }
        #endregion Properties

        #region Public Methods
        /// <summary>
        /// The Request URL (complete api endpoint) to make the request against. <br />
        /// <b>Example:</b> <i>https://example.com/v1/api/examples/totalCount</i>
        /// </summary>
        /// <returns>ApiActions</returns>
        public ApiActions AddRequestUrl(string requestUrl)
        {
            RequestUrl = requestUrl;
            return this;
        }

        /// <summary>
        /// Adds a header to the api request.
        /// </summary>        
        /// <returns>ApiActions</returns>
        public ApiActions AddHeader(string headerName, string headerValue)
        {
            if (RequestHeaders == null)
                RequestHeaders = new Dictionary<string, string>();

            RequestHeaders.Add(headerName, headerValue);
            return this;
        }

        /// <summary>
        /// Adds multiple headers to the api request, using the Dictionary key-value pairs provided <br />
        /// where <i><b>key</b></i> will be used as header name, and <i><b>value</b></i> as header value
        /// </summary>        
        /// <returns>ApiActions</returns>
        public ApiActions AddHeaders(IDictionary<string, string> requestHeaders)
        {
            RequestHeaders = requestHeaders;
            return this;
        }

        /// <summary>
        /// Adds a parameter to the api request.
        /// </summary>        
        /// <returns>ApiActions</returns>
        public ApiActions AddParameter(string parameterName, string parameterValue)
        {
            if (RequestParameters == null)
                RequestParameters = new Dictionary<string, string>();

            RequestParameters.Add(parameterName, parameterValue);
            return this;
        }

        /// <summary>
        /// Adds multiple parameters to the api request, using the Dictionary key-value pairs provided <br />
        /// where <i><b>key</b></i> will be used as parameter name, and <i><b>value</b></i> as parameter value
        /// </summary>        
        /// <returns>ApiActions</returns>
        public ApiActions AddParameters(IDictionary<string, string> requestParameters)
        {
            RequestParameters = requestParameters;
            return this;
        }

        /// <summary>
        /// Adds a C# model as request body to the api request. <br />
        /// The argument <paramref name="jsonObject"/> can only be a C# Model class.
        /// </summary>
        /// <returns>ApiActions</returns>
        public ApiActions AddJsonObjectBody(object jsonObject)
        {
            RequestBody_JsonObject = jsonObject;
            return this;
        }

        /// <summary>
        /// Adds a json formatted string as request body to the api request. <br />
        /// The argument <paramref name="jsonString"/> can only be a json formatted string.
        /// </summary>
        /// <returns>ApiActions</returns>
        public ApiActions AddJsonStringBody(string jsonString)
        {
            RequestBody_JsonString = jsonString;
            return this;
        }

        /// <summary>
        /// Adds a C# model as request body to the api request. <br />
        /// The argument <paramref name="xmlObject"/> can only be a C# Model class.
        /// </summary>
        /// <returns>ApiActions</returns>
        public ApiActions AddXmlObjectBody(object xmlObject)
        {
            RequestBody_XmlObject = xmlObject;
            return this;
        }

        /// <summary>
        /// Adds a xml formatted string as request body to the api request. <br />
        /// The argument <paramref name="xmlString"/> can only be an xml formatted string.
        /// </summary>
        /// <returns>ApiActions</returns>
        public ApiActions AddXmlStringBody(string xmlString)
        {
            RequestBody_XmlString = xmlString;
            return this;
        }

        /// <summary>
        /// Send a GET request to the specified Url. <br /> <br />
        /// The arguments <paramref name="logApiRequestToReport"/> and <paramref name="logApiResponseToReport"/> are optional. <br />
        /// By default request and reponse details will be printed to the console. <br />
        /// Specify 'true' to print the api call log to the ExtentReports. <br />
        /// Specify 'null' to avoid any printing. <br />
        /// </summary>
        /// <returns>A raw api response value.</returns>
        public RestResponse ExecuteGetMethod(bool? logApiRequestToReport = false, bool? logApiResponseToReport = false)
        {
            var apiResponse = SendApiRequest(Method.Get, logApiRequestToReport, logApiResponseToReport);
            return apiResponse;
        }

        /// <summary>
        /// Send a POST request to the specified Url. <br /> <br />
        /// The arguments <paramref name="logApiRequestToReport"/> and <paramref name="logApiResponseToReport"/> are optional. <br />
        /// By default request and reponse details will be printed to the console. <br />
        /// Specify 'true' to print the api call log to the ExtentReports. <br />
        /// Specify 'null' to avoid any printing. <br />
        /// </summary>
        /// <returns>A raw api response value.</returns>
        public RestResponse ExecutePostMethod(bool? logApiRequestToReport = false, bool? logApiResponseToReport = false)
        {
            var apiResponse = SendApiRequest(Method.Post, logApiRequestToReport, logApiResponseToReport);
            return apiResponse;
        }

        /// <summary>
        /// Send a PUT request to the specified Url. <br /> <br />
        /// The arguments <paramref name="logApiRequestToReport"/> and <paramref name="logApiResponseToReport"/> are optional. <br />
        /// By default request and reponse details will be printed to the console. <br />
        /// Specify 'true' to print the api call log to the ExtentReports. <br />
        /// Specify 'null' to avoid any printing. <br />
        /// </summary>
        /// <returns>A raw api response value.</returns>
        public RestResponse ExecutePutMethod(bool? logApiRequestToReport = false, bool? logApiResponseToReport = false)
        {
            var apiResponse = SendApiRequest(Method.Put, logApiRequestToReport, logApiResponseToReport);
            return apiResponse;
        }

        /// <summary>
        /// Send a PATCH request to the specified Url. <br /> <br />
        /// The arguments <paramref name="logApiRequestToReport"/> and <paramref name="logApiResponseToReport"/> are optional. <br />
        /// By default request and reponse details will be printed to the console. <br />
        /// Specify 'true' to print the api call log to the ExtentReports. <br />
        /// Specify 'null' to avoid any printing. <br />
        /// </summary>
        /// <returns>A raw api response value.</returns>
        public RestResponse ExecutePatchMethod(bool? logApiRequestToReport = false, bool? logApiResponseToReport = false)
        {
            var apiResponse = SendApiRequest(Method.Patch, logApiRequestToReport, logApiResponseToReport);
            return apiResponse;
        }

        /// <summary>
        /// Send a DELETE request to the specified Url. <br /> <br />
        /// The arguments <paramref name="logApiRequestToReport"/> and <paramref name="logApiResponseToReport"/> are optional. <br />
        /// By default request and reponse details will be printed to the console. <br />
        /// Specify 'true' to print the api call log to the ExtentReports. <br />
        /// Specify 'null' to avoid any printing. <br />
        /// </summary>
        /// <returns>A raw api response value.</returns>
        public RestResponse ExecuteDeleteMethod(bool? logApiRequestToReport = false, bool? logApiResponseToReport = false)
        {
            var apiResponse = SendApiRequest(Method.Delete, logApiRequestToReport, logApiResponseToReport);
            return apiResponse;
        }

        #region Type Methods
        /// <summary>
        /// Send a GET request to the specified Url. <br /> <br />
        /// The arguments <paramref name="logApiRequestToReport"/> and <paramref name="logApiResponseToReport"/> are optional. <br />
        /// By default request and reponse details will be printed to the console. <br />
        /// Specify 'true' to print the api call log to the ExtentReports. <br />
        /// Specify 'null' to avoid any printing. <br />
        /// </summary>
        /// <returns>A T representation of the api response value.</returns>
        public RestResponse<T> ExecuteGetMethod<T>(bool? logApiRequestToReport = false, bool? logApiResponseToReport = false) where T : new()
        {
            var apiResponseT = SendApiRequest<T>(Method.Get, logApiRequestToReport, logApiResponseToReport);
            return apiResponseT;
        }

        /// <summary>
        /// Send a POST request to the specified Url. <br /> <br />
        /// The arguments <paramref name="logApiRequestToReport"/> and <paramref name="logApiResponseToReport"/> are optional. <br />
        /// By default request and reponse details will be printed to the console. <br />
        /// Specify 'true' to print the api call log to the ExtentReports. <br />
        /// Specify 'null' to avoid any printing. <br />
        /// </summary>
        /// <returns>A T representation of the api response value.</returns>
        public RestResponse<T> ExecutePostMethod<T>(bool? logApiRequestToReport = false, bool? logApiResponseToReport = false) where T : new()
        {
            var apiResponseT = SendApiRequest<T>(Method.Post, logApiRequestToReport, logApiResponseToReport);
            return apiResponseT;
        }

        /// <summary>
        /// Send a PUT request to the specified Url. <br /> <br />
        /// The arguments <paramref name="logApiRequestToReport"/> and <paramref name="logApiResponseToReport"/> are optional. <br />
        /// By default request and reponse details will be printed to the console. <br />
        /// Specify 'true' to print the api call log to the ExtentReports. <br />
        /// Specify 'null' to avoid any printing. <br />
        /// </summary>
        /// <returns>A T representation of the api response value.</returns>
        public RestResponse<T> ExecutePutMethod<T>(bool? logApiRequestToReport = false, bool? logApiResponseToReport = false) where T : new()
        {
            var apiResponseT = SendApiRequest<T>(Method.Put, logApiRequestToReport, logApiResponseToReport);
            return apiResponseT;
        }

        /// <summary>
        /// Send a PATCH request to the specified Url. <br /> <br />
        /// The arguments <paramref name="logApiRequestToReport"/> and <paramref name="logApiResponseToReport"/> are optional. <br />
        /// By default request and reponse details will be printed to the console. <br />
        /// Specify 'true' to print the api call log to the ExtentReports. <br />
        /// Specify 'null' to avoid any printing. <br />
        /// </summary>
        /// <returns>A T representation of the api response value.</returns>
        public RestResponse<T> ExecutePatchMethod<T>(bool? logApiRequestToReport = false, bool? logApiResponseToReport = false) where T : new()
        {
            var apiResponseT = SendApiRequest<T>(Method.Patch, logApiRequestToReport, logApiResponseToReport);
            return apiResponseT;
        }

        /// <summary>
        /// Send a POST request to the specified Url. <br /> <br />
        /// The arguments <paramref name="logApiRequestToReport"/> and <paramref name="logApiResponseToReport"/> are optional. <br />
        /// By default request and reponse details will be printed to the console. <br />
        /// Specify 'true' to print the api call log to the ExtentReports. <br />
        /// Specify 'null' to avoid any printing. <br />
        /// </summary>
        /// <returns>A T representation of the api response value.</returns>
        public RestResponse<T> ExecuteDeleteMethod<T>(bool? logApiRequestToReport = false, bool? logApiResponseToReport = false) where T : new()
        {
            var apiResponseT = SendApiRequest<T>(Method.Delete, logApiRequestToReport, logApiResponseToReport);
            return apiResponseT;
        }
        #endregion Type Methods

        #region - Oauth2.0 Authentication
        /// <summary>
        /// Authenticates to the specified url with the provided parameters. <br />
        /// <b>Note:</b> Generally parameters would be <i><b>scope, grant_type, client_id, client_secret</b></i>
        /// </summary>
        /// <returns>An access token</returns>
        public string GetOAuthToken(string requestUrl, Dictionary<string, string> parameters)
        {
            string oauthToken;

            AddRequestUrl(requestUrl);
            AddParameters(parameters);
            var apiResponse = ExecutePostMethod(null, null);

            try
            {
                dynamic deserializedResponse = JsonConvert.DeserializeObject(apiResponse.Content);
                oauthToken = deserializedResponse.access_token;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Exception occured during deserialization.\nError message: {ex.Message}");
            }

            if (string.IsNullOrEmpty(oauthToken))
                throw new ArgumentException("Access token is empty or null. Please check the request details!");

            return oauthToken;
        }

        /// <summary>
        /// Authenticates to the specified url with the provided parameters. <br />
        /// <b>Note:</b> Generally parameters would be <i><b>scope, grant_type, client_id, client_secret</b></i>
        /// </summary>
        /// <returns>A T representation of the whole OAuth response value</returns>
        public RestResponse<T> GetOAuthToken<T>(string requestUrl, Dictionary<string, string> parameters) where T : new()
        {
            AddRequestUrl(requestUrl);
            AddParameters(parameters);
            var apiResponse = ExecutePostMethod<T>(null, null);

            return apiResponse;
        }
        #endregion - Oauth2.0 Authentication

        #endregion Public Methods

        #region Helper Methods
        private RestRequest PrepareApiRequest(Method apiActionMethod, bool? logApiRequestToReport)
        {
            // New RestClient
            RestClient = new RestClient();
            RestClient.UseNewtonsoftJson(SerializerSettings);

            // Add Request URL & action method
            RestRequest = new RestRequest
            {
                Resource = RequestUrl,
                Method = apiActionMethod
            };

            // Add RequestHeaders
            if (RequestHeaders != null)
                foreach (string headerName in RequestHeaders.Keys)
                    RestRequest.AddHeader(headerName, RequestHeaders[headerName]); // {headerName, headerValue}                

            // Add RequestParameters
            if (RequestParameters != null)
                foreach (string paramName in RequestParameters.Keys)
                    RestRequest.AddParameter(paramName, RequestParameters[paramName]); // {paramName, paramValue}  

            // Add RequestBody from a C# model
            if (RequestBody_JsonObject != null)
                RestRequest.AddJsonBody(RequestBody_JsonObject);
            else if (RequestBody_XmlObject != null)
                RestRequest.AddXmlBody(RequestBody_XmlObject);

            // Add RequestBody from a string
            if (RequestBody_JsonString != null)
            {
                RestRequest.AddStringBody(RequestBody_JsonString, ContentType.Json);
                RequestBody_JsonObject = RequestBody_JsonString;
            }
            else if (RequestBody_XmlString != null)
            {
                RestRequest.AddStringBody(RequestBody_XmlString, ContentType.Xml);
                RequestBody_XmlObject = RequestBody_XmlString;
            }

            // Request Logging
            if (logApiRequestToReport == true)
                ApiLogger.LogRequestToReport(RestRequest, RequestHeaders, RequestParameters, RequestBody_JsonObject ?? RequestBody_XmlObject, RequestBody_XmlObject != null ? DataFormat.Xml : DataFormat.Json);
            else if (logApiRequestToReport == false)
                ApiLogger.LogRequestToConsole(RestRequest, RequestHeaders, RequestParameters, RequestBody_JsonObject ?? RequestBody_XmlObject, RequestBody_XmlObject != null ? DataFormat.Xml : DataFormat.Json);

            return RestRequest;
        }
        private RestResponse SendApiRequest(Method apiActionMethod, bool? logApiRequestToReport, bool? logApiResponseToReport)
        {
            var apiRequest = PrepareApiRequest(apiActionMethod, logApiRequestToReport);
            var apiResponse = RestClient.ExecuteAsync(apiRequest).GetAwaiter().GetResult();

            // Response logging
            if (logApiResponseToReport == true)
                ApiLogger.LogApiResposeToReport(apiResponse);
            else if (logApiResponseToReport == false)
                ApiLogger.LogApiResposeToConsole(apiResponse);

            return apiResponse;
        }
        private RestResponse<T> SendApiRequest<T>(Method apiActionMethod, bool? logApiRequestToReport, bool? logApiResponseToReport) where T : new()
        {
            var apiRequest = PrepareApiRequest(apiActionMethod, logApiRequestToReport);
            var apiResponseT = RestClient.ExecuteAsync<T>(apiRequest).GetAwaiter().GetResult();

            // Response logging
            if (logApiResponseToReport == true)
                ApiLogger.LogApiResposeToReport(apiResponseT);
            else if (logApiResponseToReport == false)
                ApiLogger.LogApiResposeToConsole(apiResponseT);

            return apiResponseT;
        }
        private static JsonSerializerSettings SerializerSettings
        {
            get
            {
                var defaultContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };

                JsonSerializerSettings jsonSerializerSettings = new()
                {
                    ContractResolver = defaultContractResolver,
                };

                return jsonSerializerSettings;
            }
        }
        #endregion Helper Methods
    }
}
