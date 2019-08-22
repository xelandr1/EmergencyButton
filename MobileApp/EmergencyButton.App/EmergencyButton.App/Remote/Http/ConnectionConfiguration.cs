namespace EmergencyButton.App.Remote
{
    public class ConnectionConfiguration
    {
        public string Host { get; set; } = "93.157.254.10";
        public ushort Port { get; set; } = 8082;
        public string CertificateHash { get; set; }
        public string SecretKey { get; set; } = "0123456789123456";

        public int ConnectionTimeout { get; set; } = 1000;

    }
}