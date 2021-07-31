using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillSplitter.Models.Requests
{
    public class TogglePayment
    {
        public bool Confirmed { get; set; }
    }
}
