using System;

namespace Fusion.Core.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message, params string[] args) : base(string.Format(message, args))
        {
        }
    }
}