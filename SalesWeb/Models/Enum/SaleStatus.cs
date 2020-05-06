using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWeb.Models.Enum
{
    public enum SaleStatus : int
    {
        Pending = 0,
        Billed,
        Canceled
    }
}
