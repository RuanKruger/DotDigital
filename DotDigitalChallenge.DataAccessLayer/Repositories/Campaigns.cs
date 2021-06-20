using DotDigitalChallenge.Common;
using DotDigitalChallenge.DataAccessLayer.Models;
using DotDigitalChallenge.DataAccessLayer.Models.Campaign;
using DotDigitalChallenge.DataAccessLayer.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DotDigitalChallenge.DataAccessLayer.Repositories
{
    public class Campaigns : ICampaigns
    {
        private readonly IConfiguration _config;
        private readonly string UserName;
        private readonly string Password;
        private readonly string BaseUrl;
        private readonly string CreateEndpoint;
        private readonly string SendEndpoint;
        private readonly string GetAllEndpoint;

        public Campaigns(IConfiguration config)
        {
            _config = config;
            UserName = _config.GetValue<string>(Constants.AppSettings.Username);
            Password = _config.GetValue<string>(Constants.AppSettings.Password);
            BaseUrl = _config.GetValue<string>(Constants.AppSettings.BaseUrl);
            CreateEndpoint = Constants.Campaigns.Create;
            SendEndpoint = Constants.Campaigns.Send;
            GetAllEndpoint = Constants.Campaigns.GetAll;
        }

        public System.Net.HttpStatusCode CreateCampaign(CreateCampaignRequest request)
        {
            // append required unsubscribe portion TODO: add dynamic functionality
            request.Content += $"<a href=\"http://$UNSUB$\" style=\"color: black;\"> Unsubscribe from this newsletter</a>";
            request.PlainTextContent += $"<a href=\"http://$UNSUB$\" style=\"color: black;\"> Unsubscribe from this newsletter</a>";

            var requestToJson = Newtonsoft.Json.JsonConvert.SerializeObject(request);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(BaseUrl);
                var credentials = Encoding.ASCII.GetBytes($"{UserName}:{Password}");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));
                var result = httpClient.PostAsync(CreateEndpoint, new StringContent(requestToJson, Encoding.UTF8, "application/json")).Result;

                return result.StatusCode;
            }
        }

        public System.Net.HttpStatusCode SendCampaign(int campaignId, int[] contactIds, DateTime sendDate)
        {
            var sendRequest = new SendCampaignRequest()
            {
                CampaignId = campaignId,
                ContactIds = contactIds,
                SendDate = sendDate
            };

            var requestToJson = Newtonsoft.Json.JsonConvert.SerializeObject(sendRequest);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"{BaseUrl}");
                var credentials = Encoding.ASCII.GetBytes($"{UserName}:{Password}");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));
                var result = httpClient.PostAsync(SendEndpoint, new StringContent(requestToJson, Encoding.UTF8, "application/json")).Result;
                return result.StatusCode;
            }
        }

        public List<CampaignResponse> GetAllCampaigns()
        {
            var client = new RestClient($"{BaseUrl}{GetAllEndpoint}")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            var credentials2 = Encoding.ASCII.GetBytes($"{UserName}:{Password}");
            request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(credentials2)}");
            IRestResponse response = client.Execute(request);

            var allContacts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CampaignResponse>>(response.Content);

            return allContacts;
        }
    }
}
