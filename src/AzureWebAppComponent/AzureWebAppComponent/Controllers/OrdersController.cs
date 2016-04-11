using AzureWebAppComponent.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceBus;
using CS401DataContract;
using Newtonsoft.Json;

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
			PopulateViewBag();

			return View();
		}

		[BasicAuth]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind] OrderViewViewModel orderModel)
		{
			if (ModelState.IsValid)
			{
				// Unpack data from form and collect the necessary missing data
				Order order = new Order();
				order.CustomerId = orderModel.CustomerID;
				order.CreatedDate = DateTime.Now;

				// Get the employee object attached to the logged in user
				var currentUser = User.Identity.GetUserId();
				var employee = db.Employees.First(emp => emp.AspNetUserID == currentUser);
				order.SoldById = employee.EmployeeID;

				List<OrderProduct> orderProducts = new List<OrderProduct>();

				foreach(int productID in orderModel.ProductID)
				{
					OrderProduct orderProduct = new OrderProduct();
					orderProduct.OrderId = -1;
					orderProduct.ProductId = productID;

					orderProducts.Add(orderProduct);
				}

				// Create the PackagedOrder, which will be sent through the Service Bus to the API
				PackagedOrder newOrder = new PackagedOrder();
				newOrder.Order = order;
				newOrder.OrderProducts = orderProducts;

				// Handle timezone information. Client will send it in their local time. DateTimeZoneHandling.RoundtripKind maintains the timezone information in the DateTime 
				// so it can be easily converted to the client's local time later on.
				JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
				{
					NullValueHandling = NullValueHandling.Include,
					DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind
				};

				// Serialize the PackagedOrder to JSON to send in the Service Bus
				string jsonOrder = JsonConvert.SerializeObject(newOrder, jsonSettings);

				// Create the message to be sent from the PackagedOrder
				var message = new BrokeredMessage(jsonOrder);

				// Asynchronously send the message to the Service Bus
				await QueueConnector.Client.SendAsync(message);

				return RedirectToAction("Index");
			}

			PopulateViewBag(orderModel.ProductID);

			return View(orderModel);
		}

		/// <summary>
		/// Add DropDownList data to the ViewBag so it can be presented on the View.
		/// </summary>
		/// <param name="products">If the same View needs to be displayed, this array represents the products that the user had selected before.</param>
		private void PopulateViewBag(int[] products = null)
		{
			var customerSelection = db.Customers
				.AsQueryable()
				.Select(cust => new
				{
					CustomerID = cust.CustomerID,
					CustomerName = cust.CustomerName
				})
				.ToList();

			ViewBag.CustomerSelection = new SelectList(customerSelection, "CustomerID", "CustomerName");

			if (products == null)
			{
				var productSelection = db.Products
				.AsQueryable()
				.Select(prod => new
				{
					ProductID = prod.ProductID,
					ProductName = prod.ProductName
				})
				.ToList();

				ViewBag.ProductSelection = new MultiSelectList(productSelection, "ProductID", "ProductName");
			}
			else
			{
				var productSelection = db.Products
				.AsQueryable()
				.Select(prod => new
				{
					ProductID = prod.ProductID,
					ProductName = prod.ProductName,
					Selected = (products.Contains(prod.ProductID) ? true : false)
				})
				.ToList();

				ViewBag.ProductSelection = new MultiSelectList(productSelection, "ProductID", "ProductName");
			}
		}
	}
}