using System.Globalization;


namespace Facebook;

/// <summary>
/// Represents the authentication result of Facebook.
/// </summary>
public class FacebookOAuthResult
{
    /// <summary>
    /// Error that happens when using OAuth2 protocol.
    /// </summary>
    /// <remarks>
    /// https://developers.facebook.com/docs/oauth/errors/
    /// </remarks>
    public virtual string? Error { get; }

    /// <summary>
    /// Gets the short error reason for failed authentication if an error occurred.
    /// </summary>
    public virtual string? ErrorReason { get; }

    /// <summary>
    /// Gets the long error description for failed authentication if an error occurred.
    /// </summary>
    public virtual string? ErrorDescription { get; }

    /// <summary>
    /// Gets the <see cref="DateTime"/> when the access token will expire.
    /// </summary>
    public virtual DateTime Expires { get; }

    /// <summary>
    /// Gets the access token.
    /// </summary>
    public virtual string? AccessToken { get; }

    /// <summary>
    /// Gets the code used to exchange with Facebook to retrieve access token.
    /// </summary>
    public virtual string? Code { get; }

    /// <summary>
    /// Gets an opaque state used to maintain application state between the request and callback.
    /// </summary>
    public virtual string? State { get; }


    /// <summary>
    /// Initializes a new instance of the <see cref="FacebookOAuthResult"/> class.
    /// </summary>
    protected FacebookOAuthResult()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FacebookOAuthResult"/> class.
    /// </summary>
    /// <param name="parameters">
    /// The parameters.
    /// </param>
    /// <remarks>
    /// The values of parameters should not be url encoded.
    /// </remarks>
    internal FacebookOAuthResult(IDictionary<string, object> parameters)
    {
        if (parameters == null)
            throw new ArgumentNullException(nameof(parameters));

        if (parameters.TryGetValue("state", out var parameter))
        {
            State = parameter.ToString();
        }

        if (parameters.TryGetValue("error", out parameter))
        {
            Error = parameter.ToString();

            if (parameters.TryGetValue("error_reason", out parameter))
            {
                ErrorReason = parameter.ToString();
            }

            if (parameters.TryGetValue("error_description", out parameter))
            {
                ErrorDescription = parameter.ToString();
            }

            return;
        }

        if (parameters.TryGetValue("code", out parameter))
        {
            Code = parameter.ToString();
        }

        if (parameters.TryGetValue("access_token", out parameter))
        {
            AccessToken = parameter.ToString();
        }

        if (parameters.TryGetValue("expires_in", out parameter))
        {
            var expiresIn = Convert.ToDouble(parameter, CultureInfo.InvariantCulture);
            Expires = expiresIn > 0 ? DateTime.UtcNow.AddSeconds(expiresIn) : DateTime.MaxValue;
        }
    }


    /// <summary>
    /// Gets a value indicating whether access token or code was successfully retrieved.
    /// </summary>
    public virtual bool IsSuccess => string.IsNullOrEmpty(Error) &&
                                     (!string.IsNullOrEmpty(AccessToken) || !string.IsNullOrEmpty(Code));
}