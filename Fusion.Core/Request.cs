using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Fusion.Core.Authentication;
using Fusion.Core.Enums;
using Fusion.Core.Extensions;

namespace Fusion.Core
{
    public class Request
    {
        private readonly IDictionary<string, string> parameters = new Dictionary<string, string>();

        public Request(RequestType requestType, IAuthenticationKey authenticationKey = null)
        {
            RequestType = requestType;
            AuthenticationKey = authenticationKey;
        }

        private IAuthenticationKey AuthenticationKey { get; set; }
        public RequestType RequestType { get; private set; }

        public string Url
        {
            get
            {
                var builder = new UriBuilder { Scheme = "http", Host = "api.eve-online.com", Path = RequestType.Url };

                var requestParams = new NameValueCollection();
                if (RequestType.AuthenticationRequired)
                {
                    var keyType = AuthenticationKey.GetType();
                    if (keyType == typeof(AuthenticationKey))
                    {
                        var key = (AuthenticationKey)AuthenticationKey;
                        requestParams.Add("userID", key.UserId.ToString());
                        requestParams.Add("apiKey", key.Key);
                    }
                    else if (keyType == typeof(NewAuthenticationKey))
                    {
                        var key = (NewAuthenticationKey)AuthenticationKey;
                        requestParams.Add("keyID", key.KeyID.ToString());
                        requestParams.Add("vCode", key.VCode);
                    }
                }

                foreach (var param in parameters)
                    requestParams.Add(param.Key, param.Value);

                builder.Query = Helpers.ToQueryString(requestParams);

                return builder.Uri.ToString();
            }
        }

        public void AddParameter(RequestParameter parameter, object value)
        {
            if (value is Array)
                parameters.Add(parameter.GetStringValue(), string.Join(",", (long[])value));
            else
                parameters.Add(parameter.GetStringValue(), value.ToString());
        }

        public bool IsValid()
        {
            var valid = true;
            foreach (var requiredParameter in RequestType.RequiredParameters)
            {
                if (!parameters.ContainsKey(requiredParameter))
                    valid = false;
            }
            return valid;
        }
    }
}