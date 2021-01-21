using SlackBot.Services.GetWeather.Infrastructure.System;
using System.Collections.Generic;
using System.Linq;

namespace SlackBot.Services.GetWeather.Infrastructure.NServiceBus
{
    public class ConnectionFactory
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string VirtualHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            IDictionary<string, string> values = new Dictionary<string, string>();

            values.AddIf(NotNullOrEmpty, "host", this.Host);
            values.AddIf(NotNullOrEmpty, "port", this.Port.ToString());
            values.AddIf(NotNullOrEmpty, "virtualhost", this.VirtualHost);
            values.AddIf(NotNullOrEmpty, "username", this.UserName);
            values.AddIf(NotNullOrEmpty, "password", this.Password);

            return string.Join(";", values.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        }

        private bool NotNullOrEmpty(string value) => !string.IsNullOrEmpty(value);
    }
}
