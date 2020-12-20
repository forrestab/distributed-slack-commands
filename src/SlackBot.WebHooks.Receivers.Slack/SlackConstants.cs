namespace SlackBot.WebHooks.Receivers.Slack
{
    public static class SlackConstants
    {
        public static string ReceiverName => "slack";

        public static string EventBodyPropertyPath => "$['type']";

        public static string SignatureHeaderName => "X-Slack-Signature";

        public static string SignatureHeaderKey => "v0";

        public static int SecretKeyMinLength => 16;
    }
}
