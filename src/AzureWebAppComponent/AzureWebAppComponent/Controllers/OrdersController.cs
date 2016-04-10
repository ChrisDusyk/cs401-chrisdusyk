using AzureWebAppComponent.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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
				Order order = new Order();
				order.CustomerId = orderModel.CustomerID;
				order.CreatedDate = DateTime.Now;

				var currentUser = User.Identity.GetUserId();
				var employee = db.Employees.First(emp => emp.AspNetUserID == currentUser);
				order.SoldById = employee.EmployeeID;

				db.Orders.Add(order);
				db.SaveChanges();

				foreach(int productID in orderModel.ProductID)
				{
					OrderProduct orderProduct = new OrderProduct();
					orderProduct.OrderId = order.OrderId;
					orderProduct.ProductId = productID;

					db.OrderProducts.Add(orderProduct);
				}

				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			PopulateViewBag(orderModel.ProductID);

			return View(orderModel);
		}

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