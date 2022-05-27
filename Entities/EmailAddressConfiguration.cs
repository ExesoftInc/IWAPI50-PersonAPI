using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Entities {
    
    
    public class EmailAddressConfiguration : IEntityTypeConfiguration<EmailAddress> {
        
        private string _schema = "Person";
        
        public virtual void Configure(EntityTypeBuilder<EmailAddress> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<EmailAddress> builder, string schema) {
            builder.ToTable("EmailAddress", schema);
            builder.HasKey(x => new { x.BusinessEntityID, x.EmailAddressID });

            builder.Property(x => x.BusinessEntityID).HasColumnName(@"BusinessEntityID").HasColumnType("int").IsRequired();
            builder.Property(x => x.EmailAddressID).HasColumnName(@"EmailAddressID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.EmailAddress_).HasColumnName(@"EmailAddress").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.Rowguid).HasColumnName(@"rowguid").HasColumnType("uniqueidentifier").IsRequired().ValueGeneratedOnAddOrUpdate();
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            //Foreign keys
            builder.HasOne(a => a.Person).WithMany(b => b.EmailAddresses).HasForeignKey(c => c.BusinessEntityID).OnDelete(DeleteBehavior.Restrict); // FK_EmailAddress_Person_BusinessEntityID
        }
    }
}

