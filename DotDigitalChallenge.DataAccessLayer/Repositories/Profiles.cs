using DotDigitalChallenge.Common;
using DotDigitalChallenge.DataAccessLayer.Repositories.Interfaces;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using DotDigitalChallenge.DataAccessLayer.Models;

namespace DotDigitalChallenge.DataAccessLayer.Repositories
{
    public class Profiles : IProfile
{
        private readonly IConfiguration _config;
        private readonly string UserName;
        private readonly string Password;
        private readonly string BaseUrl;
        private readonly string Endpoint;

        public Profiles(IConfiguration config)
        {
            _config = config;
            UserName = _config.GetValue<string>(Constants.AppSettings.Username);
            Password = _config.GetValue<string>(Constants.AppSettings.Password);
            BaseUrl = _config.GetValue<string>(Constants.AppSettings.BaseUrl);
            Endpoint = Constants.Contacts.CreateBulk;
        }

        public Profile GetCurrentProfile()
        {
            var currentProfile = new Profile();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"{BaseUrl}");
                var credentials = Encoding.ASCII.GetBytes($"{UserName}:{Password}");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));
                var result = httpClient.GetAsync(Endpoint).Result;
            }

            return currentProfile;
        }
    }
}
