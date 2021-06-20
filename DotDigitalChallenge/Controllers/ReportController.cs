using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotDigitalChallenge.DataAccessLayer.Models.Report;
using DotDigitalChallenge.DataAccessLayer.Repositories.Interfaces;
using DotDigitalChallenge.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotDigitalChallenge.Controllers
{
    public class ReportController : Controller
    {
        private IReport _Report;
        private ReportResponse Model = new ReportResponse();

        public ReportController(IReport report)
        {
            _Report = report;
        }
        public IActionResult Index()
        {
            return View(Model);
        }

        [HttpGet]
        public IActionResult GetCampaignReportById(string id)
        {
            var reportSummary = _Report.GetCampaignSummaryById(id);

            if (reportSummary != null)
            {
                Model = reportSummary;
                Model.CampaignSent = true;
            }

            return View("Index", Model);
        }
    }
}
