using System;
using System.Runtime.Serialization;

namespace Lab7.Exceptions
{
    public class FPTextBlockParseException : Exception
    {
        public FPTextBlockParseException()
        {
        }

        public FPTextBlockParseException(string message) : base(message)
        {
        }

        public FPTextBlockParseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FPTextBlockParseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
