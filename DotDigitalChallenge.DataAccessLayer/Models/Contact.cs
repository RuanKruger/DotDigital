using System;
using System.Collections.Generic;
using System.Text;

namespace DotDigitalChallenge.DataAccessLayer.Models
{
    public class Contact
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string OptInType { get; set; }

        public string EmailType { get; set; }

        public string DataFields { get; set; }

        public string Status { get; set; }
    }
}
