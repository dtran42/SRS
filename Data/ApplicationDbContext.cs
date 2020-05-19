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

        public DbSet<UserInfo> UserInfos { get; set; }
        //public DbSet<WorkOrder> WorkOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>(etb =>
            {
                etb.HasKey(e => e.Email);
                etb.ToTable("UserInfos");
            });
            //modelBuilder.Entity<WorkOrder>(etb =>
            //{
            //    etb.HasKey(e => e.Id);
            //    etb.ToTable("WorkOrders");
            //});

            base.OnModelCreating(modelBuilder);
        }
    }
}
