using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Meally.core.Entities.Order_Aggregate
{
    public enum OrderStatus
    {

        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "Order Received")]
        OrderReceived,

        [EnumMember(Value = "Order Failed")]
        OrderFailed
    }
}
