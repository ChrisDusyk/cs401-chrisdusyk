using System.Web.Mvc;

namespace AzureWebAppComponent.Controllers
{
	public class HomeController : Controller
	{
		[BasicAuth]
		public ActionResult Index()
		{
			return View();
		}

		[BasicAuth]
		public ActionResult About()
		{
			ViewBag.Message = "CS401";

			return View();
		}
	}
}