using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SRS.Models;

namespace SRS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //public DbSet<WorkOrder> WorkOrders { get; set; }
        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ApplicationUser>(etb =>
            //{
            //    etb.HasKey(e => e.Id);
            //    etb.ToTable("ApplicationUsers");
            //});
            //modelBuilder.Entity<WorkOrder>(etb =>
            //{
            //    etb.HasKey(e => e.Id);
            //    etb.ToTable("WorkOrders");
            //});

            base.OnModelCreating(modelBuilder);
        }
    }
}
