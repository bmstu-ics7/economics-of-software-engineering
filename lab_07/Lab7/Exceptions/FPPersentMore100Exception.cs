using System;
using System.Runtime.Serialization;

namespace Lab7.Exceptions
{
    class FPPersentMore100Exception : Exception
    {
        public FPPersentMore100Exception()
        {
        }

        public FPPersentMore100Exception(string message) : base(message)
        {
        }

        public FPPersentMore100Exception(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FPPersentMore100Exception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
