using DotDigitalChallenge.Common;
using DotDigitalChallenge.DataAccessLayer.Models.Campaign;
using DotDigitalChallenge.DataAccessLayer.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
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

        public Campaigns(IConfiguration config)
        {
            _config = config;
            UserName = _config.GetValue<string>(Constants.AppSettings.Username);
            Password = _config.GetValue<string>(Constants.AppSettings.Password);
            BaseUrl = _config.GetValue<string>(Constants.AppSettings.BaseUrl);
            CreateEndpoint = Constants.Campaigns.Create;
            SendEndpoint = Constants.Campaigns.Send;
        }

        public System.Net.HttpStatusCode CreateCampaign(CreateCampaignRequest request)
        {
            // append requered unsubscribe portion TODO: add dynamic functionality
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
                SendDate = sendDate,
                SplitTestOptions = new SplitTestOptions() 
                {
                    TestMetric = "Clicks",
                    TestPercentage = 50,
                    TestPeriodHours = 10
                }
            };

            var requestToJson = Newtonsoft.Json.JsonConvert.SerializeObject(sendRequest);

            //var content = @"{   
            //                    campaignId: 1, 
            //                    contactIds: [1], 
            //                    sendDate: ""2020-01-01T09:40:18.527Z"", 
            //                    splitTestOptions: 
            //                        { 
            //                            TestMetric: ""Clicks"", 
            //                            TestPercentage: 50, 
            //                            TestPeriodHours: 10 
            //                        } 
            //                }";

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"{BaseUrl}");
                var credentials = Encoding.ASCII.GetBytes($"{UserName}:{Password}");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));
                var result = httpClient.PostAsync(SendEndpoint, new StringContent(requestToJson, Encoding.UTF8, "application/json")).Result;
                return result.StatusCode;
            }
        }
    }
}
