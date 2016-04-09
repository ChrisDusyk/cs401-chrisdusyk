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

		[BasicAuth]
		public ActionResult Create()
		{
			OrderViewViewModel viewModel = new OrderViewViewModel();
			viewModel.CustomerList = db.Customers.ToList();
			viewModel.ProductList = db.Products.ToList();
			viewModel.Order = new Order();

			return View(viewModel);
		}

		[BasicAuth]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "OrderId,CreatedDate,CustomerId,SoldById")] Order order)
		{
			return View(order);
		}
	}
}