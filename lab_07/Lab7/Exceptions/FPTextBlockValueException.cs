using System;
using System.Runtime.Serialization;

namespace Lab7.Exceptions
{
    class FPTextBlockValueException : Exception
    {
        public FPTextBlockValueException()
        {
        }

        public FPTextBlockValueException(string message) : base(message)
        {
        }

        public FPTextBlockValueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FPTextBlockValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
