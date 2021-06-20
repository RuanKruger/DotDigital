using System;
using System.Diagnostics;
using System.Linq;
using DotDigitalChallenge.DataAccessLayer.Models.Campaign;
using DotDigitalChallenge.DataAccessLayer.Repositories.Interfaces;
using DotDigitalChallenge.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotDigitalChallenge.Controllers
{
    public class CampaignController : Controller
    {
        private CampaignViewModel Model;
        private readonly ICampaigns _Campaigns;
        private readonly IContacts _Contacts;

        public CampaignController(ICampaigns campaigns, IContacts contacts)
        {
            _Campaigns = campaigns;
            _Contacts = contacts;

            var allContacts = _Contacts.GetAllContacts();
            var allCampaigns = _Campaigns.GetAllCampaigns();

            Model = new CampaignViewModel()
            {
                Contacts = allContacts,
                Campaigns = allCampaigns
            };
        }

        public IActionResult Index()
        {
            return View(Model);
        }

        [HttpPost]
        public IActionResult CreateCampaign(string campaignName, string campaignSubject, string campaignFromName, string htmlContent, string plainTextContent)
        {
            if (string.IsNullOrWhiteSpace(htmlContent))
            {
                return View("Index", Model);
            }

            var requestModel = new CreateCampaignRequest()
            {
                Name = campaignName,
                Subject = campaignSubject,
                FromName = campaignFromName,
                Content = htmlContent,
                PlainTextContent = plainTextContent
            };

            Model.CampaignCreateStatus = _Campaigns.CreateCampaign(requestModel).ToString();

            return View("Index", Model);
        }

        [HttpPost]
        public IActionResult SendCampaign(int campaignId, string contactIds, DateTime sendDate)
        {
            int[] conIds = contactIds.Split(',').Select(int.Parse).ToArray();

            Model.CampaignSendStatus = _Campaigns.SendCampaign(campaignId, conIds, sendDate).ToString();

            return View("Index", Model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
