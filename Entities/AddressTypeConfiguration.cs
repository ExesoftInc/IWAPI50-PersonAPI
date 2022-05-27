using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Entities {
    
    
    public class AddressTypeConfiguration : IEntityTypeConfiguration<AddressType> {
        
        private string _schema = "Person";
        
        public virtual void Configure(EntityTypeBuilder<AddressType> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<AddressType> builder, string schema) {
            builder.ToTable("AddressType", schema);
            builder.HasKey(x => x.AddressTypeID);

            builder.Property(x => x.AddressTypeID).HasColumnName(@"AddressTypeID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            builder.Property(x => x.Rowguid).HasColumnName(@"rowguid").HasColumnType("uniqueidentifier").IsRequired().ValueGeneratedOnAddOrUpdate();
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            //Foreign keys
        }
    }
}

