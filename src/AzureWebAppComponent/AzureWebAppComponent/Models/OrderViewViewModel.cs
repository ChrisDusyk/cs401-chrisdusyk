using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureWebAppComponent.Models
{
	public class OrderViewViewModel
	{
		public List<Customer> CustomerList { get; set; }
		public List<Product> ProductList { get; set; }
		public Order Order { get; set; }
	}
}