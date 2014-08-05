using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Fusion.Core
{
    public class RequestType
    {
        public RequestType(string url, bool authenticationRequired)
        {
            Url = url;
            AuthenticationRequired = authenticationRequired;
        }

        public string Url
        { get; private set; }

        public bool AuthenticationRequired
        { get; private set; }

        private readonly List<string> requiredParameters = new List<string>();
        public ReadOnlyCollection<string> RequiredParameters
        {
            get { return requiredParameters.AsReadOnly(); }
        }

        private readonly List<string> optionalParameters = new List<string>();
        public ReadOnlyCollection<string> OptionalParameters
        {
            get { return optionalParameters.AsReadOnly(); }
        }

        public RequestType AddRequiredParameter(string parameterName)
        {
            requiredParameters.Add(parameterName);
            return this;
        }

        public RequestType AddOptionalParameter(string parameterName)
        {
            optionalParameters.Add(parameterName);
            return this;
        }
    }
}