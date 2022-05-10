// ----------------------------------------------------------------------------------
// <copyright company="Exesoft Inc.">
//	This code was generated by Instant Web API code automation software (https://www.InstantWebAPI.com)
//	Copyright Exesoft Inc. © 2019.  All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Entities {
    
    
    public class BusinessEntityAddressConfiguration : IEntityTypeConfiguration<BusinessEntityAddress> {
        
        private string _schema = "Person";
        
        public virtual void Configure(EntityTypeBuilder<BusinessEntityAddress> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<BusinessEntityAddress> builder, string schema) {
            builder.ToTable("BusinessEntityAddress", schema);
            builder.HasKey(x => new { x.BusinessEntityID, x.AddressID, x.AddressTypeID });

            builder.Property(x => x.BusinessEntityID).HasColumnName(@"BusinessEntityID").HasColumnType("int").IsRequired();
            builder.Property(x => x.AddressID).HasColumnName(@"AddressID").HasColumnType("int").IsRequired();
            builder.Property(x => x.AddressTypeID).HasColumnName(@"AddressTypeID").HasColumnType("int").IsRequired();
            builder.Property(x => x.Rowguid).HasColumnName(@"rowguid").HasColumnType("uniqueidentifier").IsRequired().ValueGeneratedOnAddOrUpdate();
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            //Foreign keys
            builder.HasOne(a => a.BusinessEntity).WithMany(b => b.BusinessEntityAddresses).HasForeignKey(c => c.BusinessEntityID).OnDelete(DeleteBehavior.Restrict); // FK_BusinessEntityAddress_BusinessEntity_BusinessEntityID
            builder.HasOne(a => a.Address).WithMany(b => b.BusinessEntityAddresses).HasForeignKey(c => c.AddressID).OnDelete(DeleteBehavior.Restrict); // FK_BusinessEntityAddress_Address_AddressID
            builder.HasOne(a => a.AddressType).WithMany(b => b.BusinessEntityAddresses).HasForeignKey(c => c.AddressTypeID).OnDelete(DeleteBehavior.Restrict); // FK_BusinessEntityAddress_AddressType_AddressTypeID
        }
    }
}
