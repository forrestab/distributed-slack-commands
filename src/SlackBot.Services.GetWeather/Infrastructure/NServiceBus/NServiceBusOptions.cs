namespace SlackBot.Services.GetWeather.Infrastructure.NServiceBus
{
    public class NServiceBusOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string VirtualHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EndpointName { get; set; }
    }
}
