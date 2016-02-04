using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlatformTestApplication.Models
{
	[Table("CustomerProducts")]
	public partial class CustomerProduct
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CustomerProductID { get; set; }

		public int SalesPersonID { get; set; }

		public int CustomerID { get; set; }

		public DateTime SoldDate { get; set; }

		// Virtual collections
		[ForeignKey("SalesPersonID")]
		public virtual Employee SalesPerson { get; set; }

		//[ForeignKey("CustomerID")]
		public virtual Customer Customer { get; set; }
	}
}