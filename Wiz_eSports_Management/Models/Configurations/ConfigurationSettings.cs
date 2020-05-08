using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wiz_eSports_Management.Models.Configurations
{
    public class ConfigurationSettings
    {
        public int UnsuccessfullyAttempts { get; set; }
        public string URLApplication { get; set; }
        public int SendEmailLaterDays { get; set; }
        public int AuthorizationExpiryDays { get; set; }
        public string VersionNo { get; set; }
        public string VersionDate { get; set; }
    }
}
