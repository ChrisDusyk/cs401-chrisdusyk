using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureWebAppComponent.Models
{
	public class OrderViewViewModel
	{
		public int CustomerID { get; set; }
		public int ProductID { get; set; }
		public Order Order { get; set; }
	}
}