using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.DAO
{
    public class BaseDao
    {
        public readonly WizContext db;
        private readonly IConfiguration _configuration;
        public BaseDao(IConfiguration configuration)
        {
            _configuration = configuration;
            db = new WizContext(configuration);
        }
    }
}
