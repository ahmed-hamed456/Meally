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
    public class SubscriptionConfigration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.Property(S => S.PackgeName)
                .IsRequired();

            builder.Property(S => S.TotalPrice)
                .IsRequired();

            builder.Property(S => S.NumberOfDays)
                .IsRequired();
        }
    }
}
