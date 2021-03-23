using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TESTWEBAPP.Models
{
	public class TInvoiceD
	{
        public Guid InvoiceDetailID { get; set; }
        public string InvoiceHID { get; set; }
        public string Name { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
    }
}
