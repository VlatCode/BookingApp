using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBookingSystem.HelpersClassLib
{
    public class AppSettings
    {
        // Connection string to connect to the SQL database
        // In the appsettings.json, copy & paste the connection string from SQL Studio local server
        public string ConnectionString { get; set; }
        // SecretKey is used for JWT
        public string SecretKey { get; set; }
    }
}
