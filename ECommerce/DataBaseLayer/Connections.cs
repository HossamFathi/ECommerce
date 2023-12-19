using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    internal static  class Connections
    {
        public const string TestEnvConnectionString = "Data Source=CompanyMoasassah.mssql.somee.com;Initial Catalog=CompanyMoasassah;User Id=hosamfathi_SQLLogin_1;Password=39qpifsv7a;persist security info=False;TrustServerCertificate=True";
        public const string DevEnvConnectionString = "Data Source=DESKTOP-4B7SDHS;Initial Catalog=ECommerce;Integrated Security=True;TrustServerCertificate=True";
        public const string LiveEnvConnectionString = "";
    }
}
