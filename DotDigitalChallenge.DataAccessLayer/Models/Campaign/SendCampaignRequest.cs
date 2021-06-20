using Newtonsoft.Json;
using System;

namespace DotDigitalChallenge.DataAccessLayer.Models.Campaign
{
    public class SendCampaignRequest
    {
        [JsonProperty("campaignId")]
        public int CampaignId { get; set; }

        [JsonProperty("contactIds")]
        public int[] ContactIds { get; set; }

        [JsonProperty("sendDate")]
        public DateTime SendDate { get; set; }

        //[JsonProperty("splitTestOptions")]
        //public SplitTestOptions SplitTestOptions { get; set; }
    }

    
    public class SplitTestOptions
    {
        [JsonProperty("TestMetric")]
        public string TestMetric { get; set; }

        [JsonProperty("TestPercentage")]
        public int TestPercentage { get; set; }

        [JsonProperty("TestPeriodHours")]
        public int TestPeriodHours { get; set; }
    }
}
