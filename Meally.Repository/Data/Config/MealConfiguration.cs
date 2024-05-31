using Meally.core.Entities;
using Meally.core.Entities.Order_Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.Repository.Data.Config
{
    public class MealConfiguration : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.Property(M => M.Name)
                   .IsRequired();

            builder.Property(M => M.Components)
                   .IsRequired();

            builder.Property(M => M.PictureUrl)
                   .IsRequired();

            builder.Property(M=>M.Type).IsRequired();

            builder.Property(M => M.CreationDate).IsRequired();


            builder.Property(M => M.Price)
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(M => M.Restaurant)
                   .WithMany()
                   .HasForeignKey(M => M.RestaurantId);
                   

            builder.HasOne(M => M.Category)
                   .WithMany()
                   .HasForeignKey(M => M.CategoryId);

           

            //builder.Navigation(M => M.Restaurant);
        }
    }
}
