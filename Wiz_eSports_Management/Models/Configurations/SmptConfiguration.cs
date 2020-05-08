using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wiz_eSports_Management.Models.Configurations
{
    public class SmptConfiguration
    {
        public string Server { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public int Port { get; set; }
    }
}
