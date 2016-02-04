using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlatformTestApplication.Models
{
	[Table("Products")]
	public partial class Product
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ProductID { get; set; }

		[StringLength(5)]
		public string ProductCode { get; set; }

		[StringLength(200)]
		public string ProductName { get; set; }
	}
}