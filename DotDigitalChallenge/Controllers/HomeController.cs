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
        private readonly IContacts _Contacts;

        public HomeController(ILogger<HomeController> logger, IContacts contacts)
        {
            _logger = logger;
            _Contacts = contacts;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void CreateContacts()
        {
            _Contacts.BulkCreate("c/asdasd");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
