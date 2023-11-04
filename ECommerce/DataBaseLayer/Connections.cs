using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    internal static  class Connections
    {
        public const string TestEnvConnectionString = "Data Source=SQL5063.site4now.net;Initial Catalog=db_a9cd62_esslamfathydb;User Id=db_a9cd62_esslamfathydb_admin;Password=";
        public const string DevEnvConnectionString = "Data Source=DESKTOP-4B7SDHS;Initial Catalog=ECommerce;Integrated Security=True;TrustServerCertificate=True";
        public const string LiveEnvConnectionString = "";
    }
}
