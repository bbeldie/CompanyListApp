using Company.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Dal.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<CompanyModel>
    {
        public void Configure(EntityTypeBuilder<CompanyModel> builder)
        {
            builder.ToTable("company", "company");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.Property(e => e.Exchange).HasColumnName("exchange");
            builder.Property(e => e.Ticker).HasColumnName("ticker");
            builder.Property(e => e.Isin).HasColumnName("isin");
            builder.Property(e => e.Website).HasColumnName("website");
        }
    }
}
