﻿using Npgsql;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebContractPEP.Models
{
    class NpgSqlConfiguration : DbConfiguration
    {
        public NpgSqlConfiguration()
        {
            var name = "Npgsql";

            SetProviderFactory(providerInvariantName: name,
                providerFactory: NpgsqlFactory.Instance);

            SetProviderServices(providerInvariantName: name,
                provider: NpgsqlServices.Instance);

            SetDefaultConnectionFactory(connectionFactory: new NpgsqlConnectionFactory());
        }
    }

    public class ContractContext : DbContext
    {
        public ContractContext() : base("Server=localhost;port=5432;Database=ContractDB;User Id=postgres;Password=root;")
        {
            Database.SetInitializer<ContractContext>(new DropCreateDatabaseAlways<ContractContext>());

        }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractTemplate> Templates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }
    }

}