using DataBaseLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    public static class ServiceCollectionExtensions
    {

        public static void RegisterYourLibrary(this IServiceCollection services)
        {

            string connectionString = Connections.TestEnvConnectionString;
            services.AddDbContext<ECommerceDB>(
            options => options.UseSqlServer(connectionString));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
           
        }
    }

}
