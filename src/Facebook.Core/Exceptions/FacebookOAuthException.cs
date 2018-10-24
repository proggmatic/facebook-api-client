using System;
using System.Runtime.Serialization;


namespace Facebook
{
    /// <summary>
    /// Represents errors that occur as a result of problems with the OAuth access token.
    /// </summary>
    [Serializable]
    public class FacebookOAuthException : FacebookApiException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FacebookOAuthException"/> class.
        /// </summary>
        public FacebookOAuthException()
        {
        }

        public FacebookOAuthException(FacebookError error) : base(error.Message)
        {
            this.ErrorType = error.Type;
            this.ErrorCode = error.Code;
            this.ErrorSubcode = error.ErrorSubcode;
            this.ErrorUserMsg = error.ErrorUserMsg;
            this.ErrorUserTitle = error.ErrorUserTitle;
            this.FbTraceId = error.FbTraceId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FacebookOAuthException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errorType">Type of the error.</param>
        /// <param name="errorCode">Code of the error.</param>
        /// <param name="errorSubcode">Subcode of the error.</param>
        public FacebookOAuthException(string message, string errorType = default, int errorCode = default, int errorSubcode = default)
            : base(message, errorType, errorCode, errorSubcode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FacebookOAuthException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public FacebookOAuthException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FacebookOAuthException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
        protected FacebookOAuthException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
