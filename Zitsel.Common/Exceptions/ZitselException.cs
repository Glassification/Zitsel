// <copyright file="ZitselException.cs" company="Thomas Beckett">
// Copyright (c) Thomas Beckett. All rights reserved.
// </copyright>

namespace Zitsel.Common.Exceptions
{
    using System;

    using Zitsel.Common.Enums;

    /// <summary>
    /// Represents an abstract base class for Concierge-specific exceptions.
    /// </summary>
    public abstract class ZitselException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZitselException"/> class with the specified message, severity, fatal flag, and optional inner exception.
        /// </summary>
        /// <param name="message">The error message that describes the exception.</param>
        /// <param name="severity">The severity level of the exception.</param>
        /// <param name="isFatal">A flag indicating whether the exception is fatal.</param>
        /// <param name="innerException">The inner exception that is the cause of this exception, or null if no inner exception is specified.</param>
        public ZitselException(string message, Severity severity, bool isFatal, Exception? innerException = null)
            : base(message, innerException)
        {
            this.Severity = severity;
            this.IsFatal = isFatal;
        }

        /// <summary>
        /// Gets a value indicating whether the exception is fatal.
        /// </summary>
        public bool IsFatal { get; init; }

        /// <summary>
        /// Gets the severity level of the exception.
        /// </summary>
        public Severity Severity { get; }
    }
}
