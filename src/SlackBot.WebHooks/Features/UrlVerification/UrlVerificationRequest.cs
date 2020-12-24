using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackBot.WebHooks.Features.UrlVerification
{
    public class UrlVerificationRequest
    {
        public string Challenge { get; set; }
        public string Token { get; set; }
    }
}
