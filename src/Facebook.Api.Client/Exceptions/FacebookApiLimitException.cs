using System.Runtime.Serialization;


namespace Facebook;

/// <summary>
/// Represents errors that occur as a result of problems with the OAuth access token.
/// </summary>
[Serializable]
public class FacebookApiLimitException : FacebookApiException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FacebookApiLimitException"/> class. 
    /// </summary>
    public FacebookApiLimitException()
    {
    }

    public FacebookApiLimitException(FacebookError error) : base(error.Message)
    {
        this.ErrorType = error.Type;
        this.ErrorCode = error.Code;
        this.ErrorSubcode = error.ErrorSubcode;
        this.ErrorUserMsg = error.ErrorUserMsg;
        this.ErrorUserTitle = error.ErrorUserTitle;
        this.FbTraceId = error.FbTraceId;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FacebookApiLimitException"/> class. 
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="errorType">The error type.</param>
    public FacebookApiLimitException(string message, string? errorType = default)
        : base(message, errorType)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FacebookApiLimitException"/> class. 
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="innerException">
    /// The inner exception.
    /// </param>
    public FacebookApiLimitException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}