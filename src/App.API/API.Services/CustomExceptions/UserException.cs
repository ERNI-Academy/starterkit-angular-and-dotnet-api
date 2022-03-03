
using System;
using System.Globalization;

namespace App.API.Services.CustomExceptions;
/// <summary>
/// The user exception.
/// </summary>
public class UserException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserException"/> class.
    /// </summary>
    public UserException()
        : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserException"/> class.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    public UserException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserException"/> class.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    public UserException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}
