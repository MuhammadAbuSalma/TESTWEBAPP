using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TESTWEBAPP.Helper;
using TESTWEBAPP.Models;

namespace TESTWEBAPP.Controllers
{
	public class HomeController : Controller
	{
		InvoiceAPI _api = new InvoiceAPI();
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public async Task<IActionResult> Index(string headerId)
		{
			VLoad vload = new VLoad();
			if (!string.IsNullOrEmpty(headerId))
			{		
				HttpClient client = _api.Initial();

				//Language langinfo = new Language();
				List<Language> language = new List<Language>();
				//MCurrency currinfo = new MCurrency();
				List<MCurrency> mcurrency = new List<MCurrency>();
				//MTo toinfo = new MTo();
				List<MTo> mto = new List<MTo>();
				//MPo poinfo = new MPo();
				List<MPo> mpo = new List<MPo>();
				TInvoiceH tinvoiceh = new TInvoiceH();
				TInvoiceD detailinfo = new TInvoiceD();
				List<TInvoiceD> tinvoiced = new List<TInvoiceD>();

				HttpResponseMessage resTInvoiceH = await client.GetAsync($"api/TInvoiceH/GetTInvoiceHById?id={headerId}");
				var resultTInvoiceH = resTInvoiceH.Content.ReadAsStringAsync().Result;
				tinvoiceh = JsonConvert.DeserializeObject<TInvoiceH>(resultTInvoiceH);

				HttpResponseMessage resTInvoiceD = await client.GetAsync($"api/TInvoiceD/GetTInvoiceDById?id={headerId}");
				var resultTInvoiceD = resTInvoiceD.Content.ReadAsStringAsync().Result;
				tinvoiced = JsonConvert.DeserializeObject<List<TInvoiceD>>(resultTInvoiceD);

				language = new List<Language>
						{
							new Language { Text = "English (US)", Value = "US"},
							new Language { Text = "Indonesian (ID)", Value = "ID"}
						};

				HttpResponseMessage resCurr = await client.GetAsync("api/MCurrency/GetMCurrency");
				var resultCurr = resCurr.Content.ReadAsStringAsync().Result;
				mcurrency = JsonConvert.DeserializeObject<List<MCurrency>>(resultCurr);

				HttpResponseMessage resTo = await client.GetAsync("api/MTo/GetMTo");
				var resultTo = resTo.Content.ReadAsStringAsync().Result;
				mto = JsonConvert.DeserializeObject<List<MTo>>(resultTo);

				HttpResponseMessage resPo = await client.GetAsync("api/MPo/GetMPo");
				var resultPo = resPo.Content.ReadAsStringAsync().Result;
				mpo = JsonConvert.DeserializeObject<List<MPo>>(resultPo);
				foreach (var item in mpo)
				{
					item.Name = item.PONo + " - " + item.Name;
				}

				ViewBag.langInfo = tinvoiceh.Language;
				ViewBag.currInfo = tinvoiceh.Currency;
				ViewBag.toInfo = tinvoiceh.ToID;
				ViewBag.poInfo = tinvoiceh.Pono;
				vload.invNo = headerId;
				//vload.langInfo = langinfo;
				vload.language = language;
				//vload.currInfo = currinfo;
				vload.mCurrency = mcurrency;
				//vload.toInfo = toinfo;
				vload.mTo = mto;
				//vload.poInfo = poinfo;
				vload.mPo = mpo;
				vload.tInvoiceH = tinvoiceh;
				vload.detailInfo = detailinfo;
				vload.tInvoiceD = tinvoiced;
			}

			if (string.IsNullOrEmpty(headerId))
			{			
				HttpClient client = _api.Initial();
				string tot = await client.GetStringAsync("api/TInvoiceH/CountTInvoiceH");
				string invNo = "INV-" + (Convert.ToInt32(tot) + 1).ToString();

				//Language langinfo = new Language();
				List<Language> language = new List<Language>();
				//MCurrency currinfo = new MCurrency();
				List<MCurrency> mcurrency = new List<MCurrency>();
				//MTo toinfo = new MTo();
				List<MTo> mto = new List<MTo>();
				//MPo poinfo = new MPo();
				List<MPo> mpo = new List<MPo>();
				TInvoiceH tinvoiceh = new TInvoiceH();
				TInvoiceD detailinfo = new TInvoiceD();
				List<TInvoiceD> tinvoiced = new List<TInvoiceD>();

				language = new List<Language>
						{
							new Language { Text = "English (US)", Value = "US"},
							new Language { Text = "Indonesian (ID)", Value = "ID"}
						};

				HttpResponseMessage resCurr = await client.GetAsync("api/MCurrency/GetMCurrency");
				var resultCurr = resCurr.Content.ReadAsStringAsync().Result;
				mcurrency = JsonConvert.DeserializeObject<List<MCurrency>>(resultCurr);

				HttpResponseMessage resTo = await client.GetAsync("api/MTo/GetMTo");
				var resultTo = resTo.Content.ReadAsStringAsync().Result;
				mto = JsonConvert.DeserializeObject<List<MTo>>(resultTo);

				HttpResponseMessage resPo = await client.GetAsync("api/MPo/GetMPo");
				var resultPo = resPo.Content.ReadAsStringAsync().Result;
				mpo = JsonConvert.DeserializeObject<List<MPo>>(resultPo);
				foreach (var item in mpo)
				{
					item.Name = item.PONo + " - " + item.Name;
				}
				
				vload.invNo = invNo;
				//vload.langInfo = langinfo;
				vload.language = language;
				//vload.currInfo = currinfo;
				vload.mCurrency = mcurrency;
				//vload.toInfo = toinfo;
				vload.mTo = mto;
				//vload.poInfo = poinfo;
				vload.mPo = mpo;
				vload.tInvoiceH = tinvoiceh;
				vload.detailInfo = detailinfo;
				vload.tInvoiceD = tinvoiced;
			}
			return View(vload);
		}

		[HttpPost]
		public async Task<IActionResult> SaveInvoice(VLoad vm)
		{
			TInvoiceH tinvoiceh = new TInvoiceH();
			tinvoiceh = vm.tInvoiceH;
			HttpClient client = _api.Initial();
			StringContent content = new StringContent(JsonConvert.SerializeObject(tinvoiceh), Encoding.UTF8, "application/json");

			using (var response = await client.PostAsync("api/TInvoiceH/AddTInvoiceH", content))
			{
				var resultPostHeader = response.Content.ReadAsStringAsync().Result;
				tinvoiceh = JsonConvert.DeserializeObject<TInvoiceH>(resultPostHeader);
			}

			return (RedirectToAction("Index", new { id = vm.invNo }));
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
