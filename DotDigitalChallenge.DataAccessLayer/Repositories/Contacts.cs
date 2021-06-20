using DotDigitalChallenge.Common;
using DotDigitalChallenge.DataAccessLayer.Models.Contact;
using DotDigitalChallenge.DataAccessLayer.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
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
        private readonly string GetImportStatusEndpoint;

        public Contacts(IConfiguration config)
        {
            _config = config;
            UserName = _config.GetValue<string>(Constants.AppSettings.Username);
            Password = _config.GetValue<string>(Constants.AppSettings.Password);
            BaseUrl = _config.GetValue<string>(Constants.AppSettings.BaseUrl);
            CreateBulkEndpoint = Constants.Contacts.CreateBulk;
            GetAllContactsEndpoint = Constants.Contacts.GetAll;
            GetImportStatusEndpoint = Constants.Contacts.GetImportStatus;
        }

        public ContactResponse BulkCreate(string csvFilePath)
        {
            try
            {
                var client2 = new RestClient($"{BaseUrl}{CreateBulkEndpoint}")
                {
                    Timeout = -1
                };
                var request = new RestRequest(Method.POST);
                var credentials2 = Encoding.ASCII.GetBytes($"{UserName}:{Password}");
                request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(credentials2)}");
                request.AddFile("fisier", csvFilePath);
                IRestResponse response = client2.Execute(request);

                if (response.IsSuccessful)
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<ContactResponse>(response.Content);
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        public List<Contact> GetAllContacts()
        {
            var client = new RestClient($"{BaseUrl}{GetAllContactsEndpoint}")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            var credentials = Encoding.ASCII.GetBytes($"{UserName}:{Password}");
            request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(credentials)}");
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Contact>>(response.Content);
            }

            return null;
        }

        public ContactResponse GetImportStatus(string id)
        {
            var client = new RestClient($"{BaseUrl}{GetImportStatusEndpoint}/{id}")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            var credentials = Encoding.ASCII.GetBytes($"{UserName}:{Password}");
            request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(credentials)}");
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<ContactResponse>(response.Content);
            }

            return null;
        }
    }
}
