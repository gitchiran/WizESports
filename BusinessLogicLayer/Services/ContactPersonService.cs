using DataAccessLayer.DAO;
using DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;

namespace BusinessLogicLayer.Services
{
    public class ContactPersonService
    {
        private readonly IConfiguration _configuration;
        private readonly ContactPersonDao _contactPersonDao;

        public ContactPersonService(IConfiguration configuration)
        {
            _configuration = configuration;
            _contactPersonDao = new ContactPersonDao(configuration);
        }

        public ContactPerson GetContactPerson(int clanId)
        {
            return _contactPersonDao.GetContactPerson(clanId);
        }

        public int SaveContactPerson(ContactPerson contactPerson)
        {
            if(contactPerson != null)
            {
                return _contactPersonDao.SaveContactPerson(contactPerson);
            }
            else
            {
                return 0;
            }
            
        }

        public bool UpdateContactPerson(ContactPerson contactPerson)
        {
            if (contactPerson != null)
            {
                return _contactPersonDao.UpdateContactPerson(contactPerson);
            }
            else
            {
                return false;
            }
        }
    }
}
