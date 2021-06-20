using DotDigitalChallenge.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
