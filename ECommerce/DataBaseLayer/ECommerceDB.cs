using DataBaseLayer.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    public  class ECommerceDB : IdentityDbContext
    {
        public ECommerceDB(DbContextOptions options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RelatedWork>().Property(p => p.Photo).HasDefaultValue("");
          
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<RelatedWork> RelatedWorks { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Messages> Messages { get; set; }
    }
}
