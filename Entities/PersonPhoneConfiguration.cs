using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Entities {
    
    
    public class PersonPhoneConfiguration : IEntityTypeConfiguration<PersonPhone> {
        
        private string _schema = "Person";
        
        public virtual void Configure(EntityTypeBuilder<PersonPhone> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<PersonPhone> builder, string schema) {
            builder.ToTable("PersonPhone", schema);
            builder.HasKey(x => new { x.BusinessEntityID, x.PhoneNumber, x.PhoneNumberTypeID });

            builder.Property(x => x.BusinessEntityID).HasColumnName(@"BusinessEntityID").HasColumnType("int").IsRequired();
            builder.Property(x => x.PhoneNumber).HasColumnName(@"PhoneNumber").HasColumnType("nvarchar").IsRequired().HasMaxLength(25);
            builder.Property(x => x.PhoneNumberTypeID).HasColumnName(@"PhoneNumberTypeID").HasColumnType("int").IsRequired();
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            //Foreign keys
            builder.HasOne(a => a.Person).WithMany(b => b.PersonPhones).HasForeignKey(c => c.BusinessEntityID).OnDelete(DeleteBehavior.Restrict); // FK_PersonPhone_Person_BusinessEntityID
            builder.HasOne(a => a.PhoneNumberType).WithMany(b => b.PersonPhones).HasForeignKey(c => c.PhoneNumberTypeID).OnDelete(DeleteBehavior.Restrict); // FK_PersonPhone_PhoneNumberType_PhoneNumberTypeID
        }
    }
}

