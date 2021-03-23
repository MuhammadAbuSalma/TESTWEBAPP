using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TESTWEBAPP.Helper
{
	public class InvoiceAPI
	{
		public HttpClient Initial()
		{
			var client = new HttpClient();
			client.BaseAddress = new Uri("https://localhost:44329");
			return client;
		}
	}
}
