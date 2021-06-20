using DotDigitalChallenge.DataAccessLayer.Models;
using DotDigitalChallenge.DataAccessLayer.Models.Contact;
using System.Collections.Generic;

namespace DotDigitalChallenge.Models
{
    public class CampaignViewModel
    {
        public List<Contact> Contacts { get; set; }

        public List<CampaignResponse> Campaigns { get; set; }

        public string CampaignCreateStatus { get; set; } = string.Empty;

        public string CampaignSendStatus { get; set; } = string.Empty;
    }
}
