using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Config
{
    public class KafkaAuthentication
    {
        public AuthTypes? Type { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? CertificateName { get; set; }

        public string? KerberosServiceName { get; set; }

        public string? KerberosPrincipal { get; set; }

        public string? KerberosKeytabName { get; set; }
    }
}
