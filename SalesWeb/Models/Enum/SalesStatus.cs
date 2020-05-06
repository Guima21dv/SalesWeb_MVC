using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWeb.Models.Enum
{
    public enum SalesStatus : int
    {
        Pending = 0,
        Billed,
        Cancel
    }
}
