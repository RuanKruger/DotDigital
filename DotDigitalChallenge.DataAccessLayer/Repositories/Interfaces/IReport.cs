using DotDigitalChallenge.DataAccessLayer.Models.Report;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotDigitalChallenge.DataAccessLayer.Repositories.Interfaces
{
    public interface IReport
    {
        public ReportResponse GetCampaignSummaryById(string id);
    }
}
