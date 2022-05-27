using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Entities {
    
    
    public class BusinessEntityConfiguration : IEntityTypeConfiguration<BusinessEntity> {
        
        private string _schema = "Person";
        
        public virtual void Configure(EntityTypeBuilder<BusinessEntity> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<BusinessEntity> builder, string schema) {
            builder.ToTable("BusinessEntity", schema);
            builder.HasKey(x => x.BusinessEntityID);

            builder.Property(x => x.BusinessEntityID).HasColumnName(@"BusinessEntityID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Rowguid).HasColumnName(@"rowguid").HasColumnType("uniqueidentifier").IsRequired().ValueGeneratedOnAddOrUpdate();
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            //Foreign keys
        }
    }
}

