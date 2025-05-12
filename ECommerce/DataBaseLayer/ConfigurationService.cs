using DataBaseLayer;
using Microsoft.AspNetCore.Identity;
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

            string connectionString = Connections.LiveEnvConnectionString;

            services.AddIdentity<IdentityUser, IdentityRole>(option =>
            {
                // config user password validation 
                option.Password.RequireDigit = true; // password must have at least digit
                option.Password.RequireLowercase = true;// password must have at least one lower case alhpabitic
                option.Password.RequireUppercase = false;  //password not must contain upercase 
                option.Password.RequiredLength = 8;// password must have at least 8 charcter 
                option.Password.RequireNonAlphanumeric = false; // password not must contain non alpha numeric charchter 
                option.User.RequireUniqueEmail = true; // requeired Uniqe Email 


            }).AddEntityFrameworkStores<ECommerceDB>() // add schema to DB
           .AddDefaultTokenProviders(); // add token provider that use to confirmation (email - phone) and two factor Login
            services.AddDbContext<ECommerceDB>(
            options => options.UseSqlServer(connectionString)) ;
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
           
        }
    }

}
