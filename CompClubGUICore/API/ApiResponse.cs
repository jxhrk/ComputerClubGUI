using Newtonsoft.Json.Linq;
using RestSharp;

namespace CompClubGUICore.API
{
    /// <summary>
    /// API Response
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Gets or sets the status code (HTTP status code)
        /// </summary>
        /// <value>The status code.</value>
        public int StatusCode { get; private set; }

        private JObject? ResponseJsonData;
        public string StringData;

        public ApiResponse(RestResponse restResponse)
        {
            StatusCode = (int)restResponse.StatusCode;
            ResponseJsonData = string.IsNullOrEmpty(restResponse.Content) ||
                                !restResponse.Content.StartsWith('{') ? null : JObject.Parse(restResponse.Content);

            StringData = !string.IsNullOrEmpty(restResponse.Content) ? restResponse.Content : "CONTENT IS EMPTY";
        }

        public T? GetValue<T>(string name)
        {
            if (ResponseJsonData == null) { return default; }
            JToken? value = ResponseJsonData.GetValue(name);
            if (value == null) { return default; }
            if (value is JArray or JObject)
            {
                return value.ToObject<T>();
            }
            return value.Value<T>();
        }
    }
}
