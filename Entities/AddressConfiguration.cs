using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Entities {
    
    
    public class AddressConfiguration : IEntityTypeConfiguration<Address> {
        
        private string _schema = "Person";
        
        public virtual void Configure(EntityTypeBuilder<Address> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<Address> builder, string schema) {
            builder.ToTable("Address", schema);
            builder.HasKey(x => x.AddressID);

            builder.Property(x => x.AddressID).HasColumnName(@"AddressID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.AddressLine1).HasColumnName(@"AddressLine1").HasColumnType("nvarchar").IsRequired().HasMaxLength(60);
            builder.Property(x => x.AddressLine2).HasColumnName(@"AddressLine2").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(60);
            builder.Property(x => x.City).HasColumnName(@"City").HasColumnType("nvarchar").IsRequired().HasMaxLength(30);
            builder.Property(x => x.StateProvinceID).HasColumnName(@"StateProvinceID").HasColumnType("int").IsRequired();
            builder.Property(x => x.PostalCode).HasColumnName(@"PostalCode").HasColumnType("nvarchar").IsRequired().HasMaxLength(15);
            builder.Property(x => x.Rowguid).HasColumnName(@"rowguid").HasColumnType("uniqueidentifier").IsRequired().ValueGeneratedOnAddOrUpdate();
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            //Foreign keys
            builder.HasOne(a => a.StateProvince).WithMany(b => b.Addresses).HasForeignKey(c => c.StateProvinceID).OnDelete(DeleteBehavior.Restrict); // FK_Address_StateProvince_StateProvinceID
        }
    }
}

