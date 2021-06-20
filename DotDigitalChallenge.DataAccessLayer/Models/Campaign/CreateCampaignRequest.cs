using Newtonsoft.Json;

namespace DotDigitalChallenge.DataAccessLayer.Models.Campaign
{
    public class CreateCampaignRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("fromName")]
        public string FromName { get; set; }

        [JsonProperty("htmlContent")]
        public string Content { get; set; }

        [JsonProperty("plainTextContent")]
        public string PlainTextContent { get; set; }
    }
}
