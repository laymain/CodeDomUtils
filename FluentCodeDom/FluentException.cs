using System;
using System.Collections.Generic;
using System.Text;

namespace FluentCodeDom
{
    /// <summary>
    /// A class for fluent erros. Has a failing object method that you know which object failed.
    /// This is reqired because in fluent configuration stack trace is useless.
    /// </summary>
    public class FluentException : Exception 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FluentException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="failingObject">The failing object. Can be null.</param>
        public FluentException(string message, object failingObject)
            : base(message)
        {
            FailingFluentObject = failingObject;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="failingObject">The failing object. Can be null.</param>
        public FluentException(string message, Exception innerException, object failingObject)
            : base(message, innerException)
        {
            FailingFluentObject = failingObject;
        }

        /// <summary>
        /// The object in which the error was produced. Can be null
        /// </summary>
        public object FailingFluentObject { get; protected set; }

        public static FluentException NewFormat(object failingFluentObject, string error, params object[] args)
        {
            return new FluentException(string.Format(error, args), failingFluentObject);
        }
    }
}
