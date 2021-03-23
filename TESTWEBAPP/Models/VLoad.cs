using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TESTWEBAPP.Models
{
	public class VLoad
	{
		public string invNo { get; set; }
		//public Language langInfo { get; set; }		
		public List<Language> language { get; set; }
		//public MCurrency currInfo { get; set; }
		public List<MCurrency> mCurrency { get; set; }

		//public MTo toInfo { get; set; }
		public List<MTo> mTo { get; set; }
		//public MPo poInfo { get; set; }
		public List<MPo> mPo { get; set; }

		public TInvoiceH tInvoiceH { get; set; }
		public TInvoiceD detailInfo { get; set; }
		public List<TInvoiceD> tInvoiceD { get; set; }
	}
}
