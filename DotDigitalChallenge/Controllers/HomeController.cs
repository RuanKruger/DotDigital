﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotDigitalChallenge.Models;
using DotDigitalChallenge.DataAccessLayer.Repositories.Interfaces;
using DotDigitalChallenge.DataAccessLayer.Models.Campaign;

namespace DotDigitalChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContacts _Contacts;
        private readonly ICampaigns _Campaigns;

        public HomeController(
            ILogger<HomeController> logger,
            IContacts contacts,
            ICampaigns campaigns 
            )
        {
            _logger = logger;
            _Contacts = contacts;
            _Campaigns = campaigns;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateContacts(string filePath)
        {
            var bulkAdd = _Contacts.BulkCreate(filePath);

            // return Updated model with successfull upload
            return View("Index");
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
