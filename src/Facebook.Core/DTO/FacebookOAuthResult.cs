using System;
using System.Collections.Generic;
using System.Globalization;


namespace Facebook
{
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
        public virtual string Error { get; }

        /// <summary>
        /// Gets the short error reason for failed authentication if an error occurred.
        /// </summary>
        public virtual string ErrorReason { get; }

        /// <summary>
        /// Gets the long error description for failed authentication if an error occurred.
        /// </summary>
        public virtual string ErrorDescription { get; }

        /// <summary>
        /// Gets the <see cref="DateTime"/> when the access token will expire.
        /// </summary>
        public virtual DateTime Expires { get; }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        public virtual string AccessToken { get; }

        /// <summary>
        /// Gets the code used to exchange with Facebook to retrieve access token.
        /// </summary>
        public virtual string Code { get; }

        /// <summary>
        /// Gets an opaque state used to maintain application state between the request and callback.
        /// </summary>
        public virtual string State { get; }


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

            if (parameters.ContainsKey("state"))
            {
                State = parameters["state"].ToString();
            }

            if (parameters.ContainsKey("error"))
            {
                Error = parameters["error"].ToString();

                if (parameters.ContainsKey("error_reason"))
                {
                    ErrorReason = parameters["error_reason"].ToString();
                }

                if (parameters.ContainsKey("error_description"))
                {
                    ErrorDescription = parameters["error_description"].ToString();
                }

                return;
            }

            if (parameters.ContainsKey("code"))
            {
                Code = parameters["code"].ToString();
            }

            if (parameters.ContainsKey("access_token"))
            {
                AccessToken = parameters["access_token"].ToString();
            }

            if (parameters.ContainsKey("expires_in"))
            {
                var expiresIn = Convert.ToDouble(parameters["expires_in"], CultureInfo.InvariantCulture);
                Expires = expiresIn > 0 ? DateTime.UtcNow.AddSeconds(expiresIn) : DateTime.MaxValue;
            }
        }



        /// <summary>
        /// Gets a value indicating whether access token or code was successfully retrieved.
        /// </summary>
        public virtual bool IsSuccess => string.IsNullOrEmpty(Error) &&
                                         (!string.IsNullOrEmpty(AccessToken) || !string.IsNullOrEmpty(Code));
    }
}