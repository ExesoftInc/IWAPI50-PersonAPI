using Microsoft.EntityFrameworkCore;
using PersonAPI.Services;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace PersonAPI.Entities {
    
    
    public class EntitiesContext : DbContext, IDbEntities {
        
        public EntitiesContext() {
            //empty constructor
        }
        
        public EntitiesContext(DbContextOptions<EntitiesContext> options) : 
                base(options) {
        }
        
        public virtual DbSet<Address> Addresses { get; set; }
        
        public virtual DbSet<AddressType> AddressTypes { get; set; }
        
        public virtual DbSet<BusinessEntity> BusinessEntities { get; set; }
        
        public virtual DbSet<BusinessEntityAddress> BusinessEntityAddresses { get; set; }
        
        public virtual DbSet<BusinessEntityContact> BusinessEntityContacts { get; set; }
        
        public virtual DbSet<ContactType> ContactTypes { get; set; }
        
        public virtual DbSet<CountryRegion> CountryRegions { get; set; }
        
        public virtual DbSet<EmailAddress> EmailAddresses { get; set; }
        
        public virtual DbSet<Password> Passwords { get; set; }
        
        public virtual DbSet<Person> People { get; set; }
        
        public virtual DbSet<PersonPhone> PersonPhones { get; set; }
        
        public virtual DbSet<PhoneNumberType> PhoneNumberTypes { get; set; }
        
        public virtual DbSet<StateProvince> StateProvinces { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.AddInterceptors(new DbInterceptor(new LoggerManager()));
        }
        
        public virtual async Task<int> SaveChangesAsync() {
           return await SaveChangesAsync(new CancellationToken());
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new AddressTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BusinessEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BusinessEntityAddressConfiguration());
            modelBuilder.ApplyConfiguration(new BusinessEntityContactConfiguration());
            modelBuilder.ApplyConfiguration(new ContactTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CountryRegionConfiguration());
            modelBuilder.ApplyConfiguration(new EmailAddressConfiguration());
            modelBuilder.ApplyConfiguration(new PasswordConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new PersonPhoneConfiguration());
            modelBuilder.ApplyConfiguration(new PhoneNumberTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StateProvinceConfiguration());
        }
    }
}

