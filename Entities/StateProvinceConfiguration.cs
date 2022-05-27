using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Entities {
    
    
    public class StateProvinceConfiguration : IEntityTypeConfiguration<StateProvince> {
        
        private string _schema = "Person";
        
        public virtual void Configure(EntityTypeBuilder<StateProvince> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<StateProvince> builder, string schema) {
            builder.ToTable("StateProvince", schema);
            builder.HasKey(x => x.StateProvinceID);

            builder.Property(x => x.StateProvinceID).HasColumnName(@"StateProvinceID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.StateProvinceCode).HasColumnName(@"StateProvinceCode").HasColumnType("nchar").IsRequired().IsFixedLength().HasMaxLength(3);
            builder.Property(x => x.CountryRegionCode).HasColumnName(@"CountryRegionCode").HasColumnType("nvarchar").IsRequired().HasMaxLength(3);
            builder.Property(x => x.IsOnlyStateProvinceFlag).HasColumnName(@"IsOnlyStateProvinceFlag").HasColumnType("bit").IsRequired();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            builder.Property(x => x.TerritoryID).HasColumnName(@"TerritoryID").HasColumnType("int").IsRequired();
            builder.Property(x => x.Rowguid).HasColumnName(@"rowguid").HasColumnType("uniqueidentifier").IsRequired().ValueGeneratedOnAddOrUpdate();
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            //Foreign keys
            builder.HasOne(a => a.CountryRegion).WithMany(b => b.StateProvinces).HasForeignKey(c => c.CountryRegionCode).OnDelete(DeleteBehavior.Restrict); // FK_StateProvince_CountryRegion_CountryRegionCode
        }
    }
}

