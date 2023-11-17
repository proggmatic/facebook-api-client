﻿using System.Globalization;
using System.Text.Json.Serialization;


namespace Facebook;

/// <summary>
/// Represent errors that occur while calling a Facebook API.
/// </summary>
[Serializable]
public class FacebookApiException : Exception
{
    /// <summary>
    /// Gets or sets the type of the error.
    /// </summary>
    /// <value>The type of the error.</value>
    [JsonPropertyName("type")]
    public string? ErrorType { get; set; }

    /// <summary>
    /// Gets or sets the code of the error.
    /// </summary>
    /// <value>The code of the error.</value>
    [JsonPropertyName("code")]
    public int? ErrorCode { get; set; }

    /// <summary>
    /// Gets or sets the error subcode.
    /// </summary>
    /// <value>The code of the error subcode.</value>
    [JsonPropertyName("error_subcode")]
    public int? ErrorSubcode { get; set; }

    /// <summary>
    /// Gets or sets the error user title.
    /// </summary>
    [JsonPropertyName("error_user_title")]
    public string? ErrorUserTitle { get; set; }

    /// <summary>
    /// Gets or sets the error user message.
    /// </summary>
    [JsonPropertyName("error_user_msg")]
    public string? ErrorUserMsg { get; set; }

    [JsonPropertyName("fbtrace_id")]
    public string? FbTraceId { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="FacebookApiException"/> class.
    /// </summary>
    public FacebookApiException()
    {
    }

    public FacebookApiException(FacebookError error) : base(error.Message)
    {
        this.ErrorType = error.Type;
        this.ErrorCode = error.Code;
        this.ErrorSubcode = error.ErrorSubcode;
        this.ErrorUserMsg = error.ErrorUserMsg;
        this.ErrorUserTitle = error.ErrorUserTitle;
        this.FbTraceId = error.FbTraceId;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FacebookApiException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    public FacebookApiException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FacebookApiException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="errorType">Type of the error.</param>
    public FacebookApiException(string message, string? errorType)
        : this(string.Format(CultureInfo.InvariantCulture, "({0}) {1}", errorType ?? "Unknown", message))
    {
        ErrorType = errorType;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FacebookApiException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="errorType">Type of the error.</param>
    /// <param name="errorCode">Code of the error.</param>
    public FacebookApiException(string message, string? errorType, int errorCode)
        : this(string.Format(CultureInfo.InvariantCulture, "({0} - #{1}) {2}", errorType ?? "Unknown", errorCode, message))
    {
        ErrorType = errorType;
        ErrorCode = errorCode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FacebookApiException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="errorType">Type of the error.</param>
    /// <param name="errorCode">Code of the error.</param>
    /// <param name="errorSubcode">Subcode of the error.</param>
    public FacebookApiException(string message, string? errorType, int errorCode, int errorSubcode)
        : this(message, errorType, errorCode)
    {
        ErrorSubcode = errorSubcode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FacebookApiException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="innerException">The inner exception.</param>
    public FacebookApiException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}