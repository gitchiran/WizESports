using DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class ContactPersonDao: BaseDao
    {
        private readonly IConfiguration _configuration;

        public ContactPersonDao(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public ContactPerson GetContactPerson(int clanId)
        {
            try
            {
                var clan = db.Clan.FirstOrDefault(c => c.Id == clanId);
                if (clan != null)
                {
                    return db.ContactPerson.FirstOrDefault(c => c.Id == clan.ContactPerson);
                }

                return null;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public int SaveContactPerson(ContactPerson contactPerson)
        {
            try
            {
                int contactId = 0;
                if(contactPerson != null)
                {
                    db.ContactPerson.Add(contactPerson);
                    db.SaveChanges();
                    contactId = contactPerson.Id;
                }
                

                return contactId;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool UpdateContactPerson(ContactPerson contactPerson)
        {
            try
            {
                int isUpdated = 0;
                db.ContactPerson.Update(contactPerson);
                isUpdated = db.SaveChanges();

                return isUpdated > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
