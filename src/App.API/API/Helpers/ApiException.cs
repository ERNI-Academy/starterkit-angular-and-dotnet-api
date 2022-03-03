using System;
using System.Globalization;

namespace App.API.Helpers;

/// <summary>
/// Custom exception class for throwing application specific exceptions that can be caught and handled within the application
/// </summary>
public class ApiException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiException"/> class.
    /// </summary>
    public ApiException()
        : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiException"/> class.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    public ApiException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="args">The args.</param>
    public ApiException(string message, params object[] args) 
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}
