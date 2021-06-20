using DotDigitalChallenge.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotDigitalChallenge.DataAccessLayer.Repositories.Interfaces
{
    public interface IContacts
    {
        public List<Contact> GetAllContacts();

        public Contact Create();

        public bool BulkCreate(string csvFilePath);
    }
}
