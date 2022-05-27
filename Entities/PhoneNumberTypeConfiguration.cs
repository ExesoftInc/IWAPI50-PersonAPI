using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Entities {
    
    
    public class PhoneNumberTypeConfiguration : IEntityTypeConfiguration<PhoneNumberType> {
        
        private string _schema = "Person";
        
        public virtual void Configure(EntityTypeBuilder<PhoneNumberType> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<PhoneNumberType> builder, string schema) {
            builder.ToTable("PhoneNumberType", schema);
            builder.HasKey(x => x.PhoneNumberTypeID);

            builder.Property(x => x.PhoneNumberTypeID).HasColumnName(@"PhoneNumberTypeID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            //Foreign keys
        }
    }
}

