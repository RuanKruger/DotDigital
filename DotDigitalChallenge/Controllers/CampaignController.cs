using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotDigitalChallenge.DataAccessLayer.Models.Campaign;
using DotDigitalChallenge.DataAccessLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotDigitalChallenge.Controllers
{
    public class CampaignController : Controller
    {

        private readonly ICampaigns _Campaigns;

        public CampaignController(ICampaigns campaigns)
        {
            _Campaigns = campaigns;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCampaign(string campaignName, string campaignSubject, string campaignFromName, string htmlContent, string plainTextContent)
        {
            if (string.IsNullOrWhiteSpace(htmlContent))
            {
                return View("Index");
            }

            var requestModel = new CreateCampaignRequest()
            {
                Name = campaignName,
                Subject = campaignSubject,
                FromName = campaignFromName,
                Content = htmlContent,
                PlainTextContent = plainTextContent
            };

            var createCampaign = _Campaigns.CreateCampaign(requestModel);

            // update model with status

            // return Updated model with successfull upload
            return View("Index");
        }

        [HttpPost]
        public IActionResult SendCampaign(int campaignId, string contactIds, DateTime sendDate)
        {
            int[] conIds = contactIds.Split(',').Select(int.Parse).ToArray();

            var sendCampaign = _Campaigns.SendCampaign(campaignId, conIds, sendDate);

            // return Updated model with successfull upload
            return View("Index");
        }
    }
}
