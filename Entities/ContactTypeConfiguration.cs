using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Entities {
    
    
    public class ContactTypeConfiguration : IEntityTypeConfiguration<ContactType> {
        
        private string _schema = "Person";
        
        public virtual void Configure(EntityTypeBuilder<ContactType> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<ContactType> builder, string schema) {
            builder.ToTable("ContactType", schema);
            builder.HasKey(x => x.ContactTypeID);

            builder.Property(x => x.ContactTypeID).HasColumnName(@"ContactTypeID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            //Foreign keys
        }
    }
}

