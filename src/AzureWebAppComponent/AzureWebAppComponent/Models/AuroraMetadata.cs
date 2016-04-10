using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AzureWebAppComponent.Models
{
	public class CustomerMetadata
	{
		[Display(Name = "Customer Name")]
		public string CustomerName { get; set; }

		[Display(Name = "Postal Code")]
		public string PostalCode { get; set; }
	}

	public class OrderMedetadata
	{
		[Display(Name = "Sold Date")]
		public DateTime CreatedDate { get; set; }

		[Display(Name = "Sold By")]
		public virtual Employee Employee { get; set; }

		[Display(Name = "Sold Products")]
		public virtual ICollection<OrderProduct> OrderProducts { get; set; }
	}

	public class EmployeeMetadata
	{
		[Display(Name = "Employee Name")]
		public string FirstName { get; set; }
	}
}