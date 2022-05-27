using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Entities {
    
    
    public class PasswordConfiguration : IEntityTypeConfiguration<Password> {
        
        private string _schema = "Person";
        
        public virtual void Configure(EntityTypeBuilder<Password> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<Password> builder, string schema) {
            builder.ToTable("Password", schema);
            builder.HasKey(x => x.BusinessEntityID);

            builder.Property(x => x.BusinessEntityID).HasColumnName(@"BusinessEntityID").HasColumnType("int").IsRequired();
            builder.Property(x => x.PasswordHash).HasColumnName(@"PasswordHash").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(128);
            builder.Property(x => x.PasswordSalt).HasColumnName(@"PasswordSalt").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(10);
            builder.Property(x => x.Rowguid).HasColumnName(@"rowguid").HasColumnType("uniqueidentifier").IsRequired().ValueGeneratedOnAddOrUpdate();
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            //Foreign keys
            builder.HasOne(a => a.Person).WithOne(b => b.Password).HasForeignKey<Password>(c => c.BusinessEntityID).OnDelete(DeleteBehavior.Restrict); // FK_Password_Person_BusinessEntityID
        }
    }
}

