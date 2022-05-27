using Microsoft.EntityFrameworkCore;
using System;

namespace PersonAPI.Entities {
    
    
    public interface IDbEntities : IDbEntityBase {
        
        DbSet<Address> Addresses {
            get;
            set;
        }
        
        DbSet<AddressType> AddressTypes {
            get;
            set;
        }
        
        DbSet<BusinessEntity> BusinessEntities {
            get;
            set;
        }
        
        DbSet<BusinessEntityAddress> BusinessEntityAddresses {
            get;
            set;
        }
        
        DbSet<BusinessEntityContact> BusinessEntityContacts {
            get;
            set;
        }
        
        DbSet<ContactType> ContactTypes {
            get;
            set;
        }
        
        DbSet<CountryRegion> CountryRegions {
            get;
            set;
        }
        
        DbSet<EmailAddress> EmailAddresses {
            get;
            set;
        }
        
        DbSet<Password> Passwords {
            get;
            set;
        }
        
        DbSet<Person> People {
            get;
            set;
        }
        
        DbSet<PersonPhone> PersonPhones {
            get;
            set;
        }
        
        DbSet<PhoneNumberType> PhoneNumberTypes {
            get;
            set;
        }
        
        DbSet<StateProvince> StateProvinces {
            get;
            set;
        }
    }
}

