using DotDigitalChallenge.Common;
using DotDigitalChallenge.DataAccessLayer.Models.Report;
using DotDigitalChallenge.DataAccessLayer.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Text;

namespace DotDigitalChallenge.DataAccessLayer.Repositories
{
    public class Report : IReport
    {
        private readonly IConfiguration _config;
        private readonly string UserName;
        private readonly string Password;
        private readonly string BaseUrl;
        private readonly string GetSummaryEndpoint;

        public Report(IConfiguration config)
        {
            _config = config;
            UserName = _config.GetValue<string>(Constants.AppSettings.Username);
            Password = _config.GetValue<string>(Constants.AppSettings.Password);
            BaseUrl = _config.GetValue<string>(Constants.AppSettings.BaseUrl);
            GetSummaryEndpoint = Constants.Report.GetSummary;
        }

        public ReportResponse GetCampaignSummaryById(string id)
        {
            var client = new RestClient(string.Format($"{BaseUrl}{GetSummaryEndpoint}", id))
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            var credentials = Encoding.ASCII.GetBytes($"{UserName}:{Password}");
            request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(credentials)}");
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<ReportResponse>(response.Content);
            }

            return null;
        }
    }
}
