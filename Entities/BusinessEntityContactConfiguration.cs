using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Entities {
    
    
    public class BusinessEntityContactConfiguration : IEntityTypeConfiguration<BusinessEntityContact> {
        
        private string _schema = "Person";
        
        public virtual void Configure(EntityTypeBuilder<BusinessEntityContact> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<BusinessEntityContact> builder, string schema) {
            builder.ToTable("BusinessEntityContact", schema);
            builder.HasKey(x => new { x.BusinessEntityID, x.PersonID, x.ContactTypeID });

            builder.Property(x => x.BusinessEntityID).HasColumnName(@"BusinessEntityID").HasColumnType("int").IsRequired();
            builder.Property(x => x.PersonID).HasColumnName(@"PersonID").HasColumnType("int").IsRequired();
            builder.Property(x => x.ContactTypeID).HasColumnName(@"ContactTypeID").HasColumnType("int").IsRequired();
            builder.Property(x => x.Rowguid).HasColumnName(@"rowguid").HasColumnType("uniqueidentifier").IsRequired().ValueGeneratedOnAddOrUpdate();
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            //Foreign keys
            builder.HasOne(a => a.BusinessEntity).WithMany(b => b.BusinessEntityContacts).HasForeignKey(c => c.BusinessEntityID).OnDelete(DeleteBehavior.Restrict); // FK_BusinessEntityContact_BusinessEntity_BusinessEntityID
            builder.HasOne(a => a.Person).WithMany(b => b.BusinessEntityContacts).HasForeignKey(c => c.PersonID).OnDelete(DeleteBehavior.Restrict); // FK_BusinessEntityContact_Person_PersonID
            builder.HasOne(a => a.ContactType).WithMany(b => b.BusinessEntityContacts).HasForeignKey(c => c.ContactTypeID).OnDelete(DeleteBehavior.Restrict); // FK_BusinessEntityContact_ContactType_ContactTypeID
        }
    }
}

