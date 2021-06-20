using DotDigitalChallenge.DataAccessLayer.Models;
using DotDigitalChallenge.DataAccessLayer.Models.Contact;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotDigitalChallenge.DataAccessLayer.Repositories.Interfaces
{
    public interface IContacts
    {
        public List<Contact> GetAllContacts();

        public ContactResponse BulkCreate(string csvFilePath);

        public ContactResponse GetImportStatus(string id);
    }
}
