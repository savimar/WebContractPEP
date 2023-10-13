using Npgsql;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WebContractPEP.Models.ClientModel;
using WebContractPEP.Models.ClientModel.PersonModel;

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
            Database.SetInitializer<ContractContext>(
                new ContractInitializer());  //ew DropCreateDatabaseAlways<ContractContext>()); ВЕРНУТЬ //ToDo

        }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractTemplate> Templates { get; set; }
        public DbSet<FillField> Fields { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Address>().HasOptional(a => a.Client).WithMany(c => c.Addresses).HasForeignKey(a => a.ClientId).WillCascadeOnDelete(true);
            modelBuilder.Entity<Client>().HasMany(c => c.BankDetails).WithOptional(b => b.Client).WillCascadeOnDelete(true);
            modelBuilder.Entity<ContractTemplate>().HasMany(c => c.Fields).WithOptional(b => b.ContractTemplate).WillCascadeOnDelete(false);
            modelBuilder.Entity<Person>().HasMany(c => c.Passports).WithOptional(b => (Person)b.Client).WillCascadeOnDelete(true); ;
            modelBuilder.Entity<Client>().HasMany(c => c.ContractTemplates).WithOptional(b => b.Client).WillCascadeOnDelete(false);
           


            modelBuilder.Entity<Client>().HasMany(c => c.Contracts)
                .WithMany(s => s.Concluding)
                .Map(t => t.MapLeftKey("ClientId")
                    .MapRightKey("ContractId")
                    .ToTable("ClientContract"));
        }



    }
    }

