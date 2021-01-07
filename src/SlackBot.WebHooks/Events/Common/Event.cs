using Newtonsoft.Json;

namespace SlackBot.WebHooks.Events.Common
{
    public class Event
    {
        [JsonProperty("api_app_id")]
        public string ApiAppId { get; set; }
        [JsonProperty("team_id")]
        public string TeamId { get; set; }
        public string Token { get; set; }
    }
}
