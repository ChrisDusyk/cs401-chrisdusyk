using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlatformTestApplication.Models
{
	public class CustomerProductViewModel
	{
		public int CustomerProductID { get; set; }
		public int? ProductID { get; set; }
		public int? SoldByID { get; set; }
		public int? CustomerID { get; set; }
		public DateTime? SoldDate { get; set; }

		public Product Product { get; set; }
		public Employee Employee { get; set; }
		public Customer Customer { get; set; }
	}
}