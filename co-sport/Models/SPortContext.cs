using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace co_sport.Models
{
    public class SportContext:DbContext
    {
        public SportContext() : base("SportContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<SportTimeTable> SportTimeTables { get; set; }
        public DbSet<Time> Times { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<User>().HasMany(o => o.Groups).WithMany(o => o.Users);
            modelBuilder.Entity<User>().HasRequired(o => o.SportTimeTable).WithRequiredDependent(o => o.User);
            modelBuilder.Entity<SportTimeTable>().HasMany(o => o.Times);
        }
    }
}