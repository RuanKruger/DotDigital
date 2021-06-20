using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotDigitalChallenge.DataAccessLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotDigitalChallenge.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContacts _Contacts;
        public ContactController(IContacts contacts)
        {
            _Contacts = contacts;
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
    }
}
