using Newtonsoft.Json;
using RestSharp;
using IO.Swagger.Client;
using System.Diagnostics;

namespace CompClubGUICore.API
{
    /// <summary>
    /// API client is mainly responsible for making the HTTP call to the API backend.
    /// </summary>
    public partial class ApiClient
    {
        private static string ApiURL = "https://127.0.0.1:8000/";

        private static readonly RestClient RestClient = new RestClient(ApiURL);

        public static async Task<ApiResponse> CallPost(string path, object? postBody, bool auth = false, Dictionary<string, string>? pathParams = null)
        {
            return await CallApiAsync(path, Method.Post, Serialize(postBody), pathParams, "application/json", auth);
        }

        public static async Task<ApiResponse> CallDelete(string path, bool auth = true, Dictionary<string, string>? pathParams = null)
        {
            return await CallApiAsync(path, Method.Delete, null, pathParams, "application/json", auth);
        }


        public static async Task<ApiResponse> CallPut(string path, object postBody)
        {
            return await CallApiAsync(path, Method.Put, Serialize(postBody), null, "application/json", true);
        }

        public static async Task<ApiResponse> CallGet(string path)
        {
            return await CallApiAsync(path, Method.Get, null, null, "application/json", true);
        }

        private static async Task<ApiResponse> CallApiAsync(string path, Method method, object? postBody, Dictionary<string, string>? pathParams, string contentType, bool needAuth)
        {
#if DEBUG
            Trace.WriteLine($"{method.ToString().ToUpper()} {path}");
#endif

            var request = PrepareRequest(path, method, postBody, [], pathParams, contentType, needAuth);


            ApiResponse response = new(await Task.Run(RestResponse () =>
            {
                return RestClient.Execute(request);
            }));

#if DEBUG
            if (response.StatusCode != 200 &&
                response.StatusCode != 201)
            {
                Trace.WriteLine($"{response.StatusCode} RESPONSE STATUS\n{response.StringData}");
            }
#endif
            return response;
        }

        // Creates and sets up a RestRequest prior to a call.
        private static RestRequest PrepareRequest(
            string path, Method method, object? postBody,
            Dictionary<string, string> headerParams, Dictionary<string, string>? pathParams,
            string contentType, bool needAuth)
        {
            var request = new RestRequest(path, method);

            // add path parameter, if any
            if (pathParams != null)
            {
                foreach (var param in pathParams)
                    request.AddParameter(param.Key, param.Value, ParameterType.UrlSegment);
            }

            // add header parameter, if any
            foreach (var param in headerParams)
                request.AddHeader(param.Key, param.Value);

            if (needAuth)
            {
                request.AddHeader("Authorization", $"Bearer {AppInfo.SessionToken}");
            }

            if (postBody != null)
            {
                request.AddParameter(contentType, postBody, ParameterType.RequestBody);
            }

            return request;
        }

        /// <summary>
        /// Serialize an input (model) into JSON string
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>JSON string.</returns>
        private static string? Serialize(object? obj)
        {
            try
            {
                return obj != null ? JsonConvert.SerializeObject(obj) : null;
            }
            catch (Exception e)
            {
                throw new ApiException(500, e.Message);
            }
        }

    }
}
