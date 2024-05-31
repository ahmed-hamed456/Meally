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
    public class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShappingAddress, ShappingAddress => ShappingAddress.WithOwner());

            builder.Property(O => O.Status)
                .HasConversion(
                        OStatus => OStatus.ToString(),
                        OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus)
                );

            builder.HasOne(S => S.Subscription)
                   .WithMany(S => S.Orders)
                   .HasForeignKey(S => S.SubscriptionId);
        }
    }
}
