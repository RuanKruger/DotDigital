using DotDigitalChallenge.DataAccessLayer.Models.Campaign;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotDigitalChallenge.DataAccessLayer.Repositories.Interfaces
{
    public interface ICampaigns
    {
        public System.Net.HttpStatusCode CreateCampaign(CreateCampaignRequest request);
        public System.Net.HttpStatusCode SendCampaign(int campaignId, int[] contactIds, DateTime sendDate);
    }
}
