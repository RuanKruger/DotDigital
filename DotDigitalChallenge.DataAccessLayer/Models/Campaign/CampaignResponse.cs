using System;
using System.Collections.Generic;
using System.Text;

namespace DotDigitalChallenge.DataAccessLayer.Models
{
    public class CampaignResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string FromName { get; set; }
        public Fromaddress FromAddress { get; set; }
        public object HtmlContent { get; set; }
        public object PlainTextContent { get; set; }
        public string ReplyAction { get; set; }
        public object ReplyToAddress { get; set; }
        public bool IsSplitTest { get; set; }
        public string Status { get; set; }
    }

    public class Fromaddress
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }
}
