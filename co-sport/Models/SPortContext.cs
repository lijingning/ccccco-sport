using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace co_sport.Models
{
    public class SportContext : DbContext
    {
        public SportContext() : base("SportContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<SportTime> SportTimes { get; set; }
        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<User>().HasMany(o => o.Groups).WithMany(o => o.Users);
            modelBuilder.Entity<User>().HasMany(o => o.SportTimes).WithRequired(o => o.User);
        }
    }
}