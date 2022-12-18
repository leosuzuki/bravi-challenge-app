using BraviChallenge.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace BraviChallenge.Infra.Contexts
{
    public class DataContext : DbContext
    {
        public string DbPath { get; }

        public DataContext()
        {
            var folder = Environment.CurrentDirectory;
            //var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(folder, "challenge.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonContact> PersonContacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonContact>()
                .HasOne<Person>(pc => pc.Person)
                .WithMany(p => p.PersonContacts)
                .HasForeignKey(pc => pc.PersonId);
        }
    }
}
