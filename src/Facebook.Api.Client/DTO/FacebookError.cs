using System.Text.Json.Serialization;


namespace Facebook
{
    internal class FacebookGenericError
    {
        [JsonPropertyName("error")]
        public FacebookError Error { get; set; }
    }

    public class FacebookError
    {
        /// <summary>
        /// A human-readable description of the error.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the type of the error.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the code of the error.
        /// </summary>
        [JsonPropertyName("code")]
        public int? Code { get; set; }

        /// <summary>
        /// Additional information about the error.
        /// </summary>
        [JsonPropertyName("error_subcode")]
        public int? ErrorSubcode { get; set; }

        /// <summary>
        /// The title of the dialog, if shown. The language of the message is based on the locale of the API request.
        /// </summary>
        [JsonPropertyName("error_user_title")]
        public string ErrorUserTitle { get; set; }

        /// <summary>
        /// The message to display to the user. The language of the message is based on the locale of the API request.
        /// </summary>
        [JsonPropertyName("error_user_msg")]
        public string ErrorUserMsg { get; set; }

        /// <summary>
        /// Internal support identifier. When reporting a bug related to a Graph API call, include the fbtrace_id to help us find log data for debugging.
        /// </summary>
        [JsonPropertyName("fbtrace_id")]
        public string FbTraceId { get; set; }
    }
}