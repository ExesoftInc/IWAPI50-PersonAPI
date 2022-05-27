using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Entities {
    
    
    public class PersonConfiguration : IEntityTypeConfiguration<Person> {
        
        private string _schema = "Person";
        
        public virtual void Configure(EntityTypeBuilder<Person> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<Person> builder, string schema) {
            builder.ToTable("Person", schema);
            builder.HasKey(x => x.BusinessEntityID);

            builder.Property(x => x.BusinessEntityID).HasColumnName(@"BusinessEntityID").HasColumnType("int").IsRequired();
            builder.Property(x => x.PersonType).HasColumnName(@"PersonType").HasColumnType("nchar").IsRequired().IsFixedLength().HasMaxLength(2);
            builder.Property(x => x.NameStyle).HasColumnName(@"NameStyle").HasColumnType("bit").IsRequired();
            builder.Property(x => x.Title).HasColumnName(@"Title").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(8);
            builder.Property(x => x.FirstName).HasColumnName(@"FirstName").HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            builder.Property(x => x.MiddleName).HasColumnName(@"MiddleName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.LastName).HasColumnName(@"LastName").HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            builder.Property(x => x.Suffix).HasColumnName(@"Suffix").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(10);
            builder.Property(x => x.EmailPromotion).HasColumnName(@"EmailPromotion").HasColumnType("int").IsRequired();
            builder.Property(x => x.AdditionalContactInfo).HasColumnName(@"AdditionalContactInfo").HasColumnType("xml").IsRequired(false);
            builder.Property(x => x.Demographics).HasColumnName(@"Demographics").HasColumnType("xml").IsRequired(false);
            builder.Property(x => x.Rowguid).HasColumnName(@"rowguid").HasColumnType("uniqueidentifier").IsRequired().ValueGeneratedOnAddOrUpdate();
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            //Foreign keys
            builder.HasOne(a => a.BusinessEntity).WithOne(b => b.Person).HasForeignKey<Person>(c => c.BusinessEntityID).OnDelete(DeleteBehavior.Restrict); // FK_Person_BusinessEntity_BusinessEntityID
        }
    }
}

