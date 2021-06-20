using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DotDigitalChallenge.DataAccessLayer.Repositories.Interfaces;
using DotDigitalChallenge.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotDigitalChallenge.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContacts _Contacts;
        private ContactViewModel Model = new ContactViewModel();
        public ContactController(IContacts contacts)
        {
            _Contacts = contacts;
        }

        public IActionResult Index()
        {
            return View(Model);
        }

        [HttpPost]
        public IActionResult CreateContacts(string filePath)
        {
            var bulkAdd = _Contacts.BulkCreate(filePath);

            if (bulkAdd != null)
            {
                Model.UploadQueryId = bulkAdd.Id;
                Model.Status = bulkAdd.Status;
            }
            else
            {
                Model.Status = "Bad Request";
            }

            return View("Index", Model);
        }

        [HttpGet]
        public IActionResult GetContactUploadStatus(string id)
        {
            var bulkAdd = _Contacts.GetImportStatus(id);

            if (bulkAdd != null)
            {
                Model.UploadQueryId = bulkAdd.Id;
                Model.Status = bulkAdd.Status;
            }
            else
            {
                Model.Status = "Bad Request";
            }

            return View("Index", Model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
