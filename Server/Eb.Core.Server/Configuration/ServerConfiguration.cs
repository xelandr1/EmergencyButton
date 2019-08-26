using System;
using Eb.Core.Server.Core;

namespace Eb.Core.Server.Configuration
{
    public class ServerConfiguration
    {
        public ushort Port { get; set; } = 8082;
        public string CertificateHash { get; set; }
        public string SecretKey { get; set; } = "0123456789123456";

        public int MaxConcurrentCalls { get; set; } = 50;

        public Type AuthenticationValidator { get; set; } = typeof(LoginValidator);

        public string GetAddress()
        {
            return string.Format("https://localhost:{0}", Port);
        }
    }
}