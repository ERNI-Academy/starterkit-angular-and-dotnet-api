
using System;
using System.Globalization;

namespace App.API.Services.CustomExceptions;
/// <summary>
/// The forbidden exception.
/// </summary>
public class ForbiddenException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ForbiddenException"/> class.
    /// </summary>
    public ForbiddenException()
        : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ForbiddenException"/> class.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    public ForbiddenException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ForbiddenException"/> class.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    public ForbiddenException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}
