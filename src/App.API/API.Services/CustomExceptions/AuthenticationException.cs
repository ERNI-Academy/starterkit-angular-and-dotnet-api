
using System;
using System.Globalization;

namespace App.API.Services.CustomExceptions;
/// <summary>
/// The authentication exception.
/// </summary>
public class AuthenticationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationException"/> class.
    /// </summary>
    public AuthenticationException()
        : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationException"/> class.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    public AuthenticationException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationException"/> class.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    public AuthenticationException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}
