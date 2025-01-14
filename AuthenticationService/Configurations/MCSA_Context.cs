
using MCSABackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace MCSABackend.Configurations
{
    public partial class MCSA_Context: DbContext
    {
        public MCSA_Context()
        {
        }

        public MCSA_Context(DbContextOptions<MCSA_Context> options)
            : base(options)
        {
        }
        public virtual DbSet<Countries> Country { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Countries>(entity =>
            {
                entity.ToTable("country", schema: "public");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
            });
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                string _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (_environment == "Development")
                {
                    IConfigurationRoot configuration = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile("appsettings.Development.json")
                        .Build();
                    var xx = configuration.GetConnectionString("PostgresDatabase");
                    optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgresDatabase"));

                }
                else if (_environment == "UAT")
                {
                    IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.UAT.json")
                    .Build();
                    optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgresDatabase"));
                }
            }
        }
    }
}
