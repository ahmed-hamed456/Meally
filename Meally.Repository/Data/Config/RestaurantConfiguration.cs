using Meally.core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.Repository.Data.Config
{
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.Property(R => R.Name)
                .IsRequired();

            builder.Property(R => R.Description)
                .IsRequired();

            builder.Property(R => R.Address)
                .IsRequired();

            builder.Property(R => R.PictureUrl)
               .IsRequired();

            builder.Property(R => R.Rate)
                .HasColumnType("decimal(18,2)");

            builder.Property(R => R.CreationDate).IsRequired();

         
        }
    }
}
