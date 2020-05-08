using DataAccessLayer.DAO;
using Microsoft.Extensions.Configuration;

namespace BusinessLogicLayer.Services
{
    public class AuthenticateService
    {
        private readonly IConfiguration _configuration;
        private readonly AuthenticateDao _authenticateDao;

        public AuthenticateService(IConfiguration configuration)
        {
            _configuration = configuration;
            _authenticateDao = new AuthenticateDao(configuration);
        }

        public bool Authenticate(string userName, string password)
        {
            return _authenticateDao.Authenticate(userName, password);
        }
    }
}
