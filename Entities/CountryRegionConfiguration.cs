using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Entities {
    
    
    public class CountryRegionConfiguration : IEntityTypeConfiguration<CountryRegion> {
        
        private string _schema = "Person";
        
        public virtual void Configure(EntityTypeBuilder<CountryRegion> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<CountryRegion> builder, string schema) {
            builder.ToTable("CountryRegion", schema);
            builder.HasKey(x => x.CountryRegionCode);

            builder.Property(x => x.CountryRegionCode).HasColumnName(@"CountryRegionCode").HasColumnType("nvarchar").IsRequired().HasMaxLength(3);
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            //Foreign keys
        }
    }
}

