using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlatformTestApplication.Models
{
	public class CustomerViewModel
	{
		public int CustomerID { get; set; }
		public string CustomerName { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string Province { get; set; }
		public string PostalCode { get; set; }

		public IEnumerable<CustomerProductViewModel> CustomerProducts { get; set; }
	}
}