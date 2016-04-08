using AzureWebAppComponent.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AzureWebAppComponent.Controllers
{
	public class OrdersController : Controller
	{
		private CS401_DBEntities db = new CS401_DBEntities();

		[BasicAuth]
		public async Task<ActionResult> Index()
		{
			var orders = db.Orders.ToList();

			return View(await db.Orders.ToListAsync());
		}
	}
}