using DotDigitalChallenge.Common;
using DotDigitalChallenge.DataAccessLayer.Models;
using DotDigitalChallenge.DataAccessLayer.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace DotDigitalChallenge.DataAccessLayer.Repositories
{
    public class Contacts : IContacts
    {
        private readonly IConfiguration _config;
        private readonly string UserName;
        private readonly string Password;
        private readonly string BaseUrl;
        private readonly string CreateBulkEndpoint;
        private readonly string GetAllContactsEndpoint;

        public Contacts(IConfiguration config)
        {
            _config = config;
            UserName = _config.GetValue<string>(Constants.AppSettings.Username);
            Password = _config.GetValue<string>(Constants.AppSettings.Password);
            BaseUrl = _config.GetValue<string>(Constants.AppSettings.BaseUrl);
            CreateBulkEndpoint = Constants.Contacts.CreateBulk;
            GetAllContactsEndpoint = Constants.Contacts.GetAll;
        }

        public bool BulkCreate(string csvFilePath)
        {
            using (var multipartFormDataContent = new MultipartFormDataContent())
            using (var fileContent = new ByteArrayContent(File.ReadAllBytes(csvFilePath)))
            using (var httpClient = new HttpClient())
            {
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("text/csv");
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "FileName.csv",
                    DispositionType = DispositionTypeNames.Attachment,
                    Name = "fileData"
                };
                multipartFormDataContent.Add(fileContent);
                httpClient.BaseAddress = new Uri($"{BaseUrl}");
                var credentials = Encoding.ASCII.GetBytes($"{UserName}:{Password}");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));
                var result = httpClient.PostAsync(CreateBulkEndpoint, multipartFormDataContent).Result;

                if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    return true;
                }
            }

            return false;
        }

        public List<Contact> GetAllContacts()
        {
            var client = new RestClient($"{BaseUrl}{GetAllContactsEndpoint}")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            var credentials2 = Encoding.ASCII.GetBytes($"{UserName}:{Password}");
            request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(credentials2)}");
            IRestResponse response = client.Execute(request);

            var allContacts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Contact>>(response.Content);

            return allContacts;
        }

        public Contact Create()
        {
            throw new NotImplementedException();
        }
    }
}
