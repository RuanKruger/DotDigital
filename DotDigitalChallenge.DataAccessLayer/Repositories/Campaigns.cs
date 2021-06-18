using DotDigitalChallenge.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotDigitalChallenge.DataAccessLayer.Repositories
{
    public class Campaigns : ICampaigns
    {
        public string CreateCampaign()
        {
            return "GotTheJobOffer";
        }
    }
}
