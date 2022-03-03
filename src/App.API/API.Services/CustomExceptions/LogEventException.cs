
using System;
using System.Globalization;

namespace App.API.Services.CustomExceptions;
/// <summary>
/// The LogEvent exception.
/// </summary>
public class LogEventException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LogEventException"/> class.
    /// </summary>
    public LogEventException()
        : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LogEventException"/> class.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    public LogEventException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LogEventException"/> class.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    public LogEventException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}
