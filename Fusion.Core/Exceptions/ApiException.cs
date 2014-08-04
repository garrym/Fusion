using System;

namespace Fusion.Core.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string message, params object[] args)
            : base(string.Format(message, args))
        {}
    }
}
