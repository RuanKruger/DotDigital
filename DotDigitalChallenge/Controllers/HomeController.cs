using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotDigitalChallenge.Models;
using DotDigitalChallenge.DataAccessLayer.Repositories.Interfaces;

namespace DotDigitalChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICampaigns _Campaigns;

        public HomeController(ILogger<HomeController> logger, ICampaigns campaigns)
        {
            _Campaigns = campaigns;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void CreateContacts()
        {
            //_Campaigns.GetCampaignById();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
