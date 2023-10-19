using Npgsql;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WebContractPEP.Models.ClientModel;
using WebContractPEP.Models.ClientModel.CompanyModel;
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
               new ContractInitializer()); //new DropCreateDatabaseAlways<ContractContext>()); //ВЕРНУТЬ //ToDo

        }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractTemplate> Templates { get; set; }
        public DbSet<FillField> Fields { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Company> Companies{ get; set; }
        public DbSet<IP> IPs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<IP>().ToTable("IP");
            modelBuilder.Entity<Company>().ToTable("Company");
            modelBuilder.Entity<Address>().HasOptional(a => a.Client).WithMany(c => c.Addresses).HasForeignKey(a => a.ClientId).WillCascadeOnDelete(true);
            modelBuilder.Entity<IP>().HasMany(c => c.BankDetails).WithOptional(b => b.IP).WillCascadeOnDelete(true);
            modelBuilder.Entity<Company>().HasMany(c => c.BankDetails).WithOptional(b => b.Company).WillCascadeOnDelete(true);
            modelBuilder.Entity<ContractTemplate>().HasMany(c => c.Fields).WithRequired(b => b.ContractTemplate).WillCascadeOnDelete(false);
            modelBuilder.Entity<Person>().HasMany(c => c.Passports).WithOptional(b => (Person)b.Client).WillCascadeOnDelete(true); ;
            modelBuilder.Entity<IP>().HasMany(c => c.ContractTemplates).WithOptional(b => b.IP).WillCascadeOnDelete(false);
            modelBuilder.Entity<Company>().HasMany(c => c.ContractTemplates).WithOptional(b => b.Company).WillCascadeOnDelete(false);
            modelBuilder.Entity<IP>().HasMany(c => c.Contracts).WithOptional(b => b.IP).WillCascadeOnDelete(false);
            modelBuilder.Entity<Company>().HasMany(c => c.Contracts).WithOptional(b => b.Company).WillCascadeOnDelete(false);
            modelBuilder.Entity<Person>().HasMany(c => c.Contracts).WithOptional(b => b.ClientPerson).WillCascadeOnDelete(false);
            //modelBuilder.Entity<IP>().HasOptional(c => c.Passport).WithRequired(b => (IP)b.Client).WillCascadeOnDelete(true); ;


            /*
                        modelBuilder.Entity<Client>().HasMany(c => c.Contracts)
                            .WithMany(s => s.Concluding)
                            .Map(t => t.MapLeftKey("ClientId")
                                .MapRightKey("ContractId")
                                .ToTable("ClientContract"));

            modelBuilder.Entity<Order>()
                .HasRequired(m => m.ShippingAddress)
                .WithMany(t => t.ShippingOrders)
                .HasForeignKey(m => m.ShippingAddressId)
                .WillCascadeOnDelete(false);

    modelBuilder.Entity<Order>()
                .HasRequired(m => m.BillingAddress)
                .WithMany(t => t.BillingOrders)
                .HasForeignKey(m => m.BillingAddressId)
                .WillCascadeOnDelete(false);
            */
        }



    }
    }

