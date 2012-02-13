using System.Collections.Generic;

namespace Fusion.Core
{
    public class RequestType
    {
        public RequestType(string url, bool authenticationRequired)
        {
            Url = url;
            AuthenticationRequired = authenticationRequired;
            RequiredParameters = new HashSet<string>();
            OptionalParameters = new HashSet<string>();
        }

        public string Url { get; private set; }
        public bool AuthenticationRequired { get; private set; }
        public HashSet<string> RequiredParameters { get; private set; }
        public HashSet<string> OptionalParameters { get; private set; }

        public RequestType AddRequiredParameter(string parameterName)
        {
            RequiredParameters.Add(parameterName);
            return this;
        }

        public RequestType AddOptionalParameter(string parameterName)
        {
            OptionalParameters.Add(parameterName);
            return this;
        }
    }
}