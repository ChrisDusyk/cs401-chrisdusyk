using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlatformTestApplication.Models
{
	[Table("Employees")]
	public class Employee
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int EmployeeID { get; set; }

		[StringLength(50)]
		public string FirstName { get; set; }

		[StringLength(50)]
		public string LastName { get; set; }

		[StringLength(100)]
		public string Email { get; set; }

		public virtual ICollection<CustomerProduct> SoldProducts { get; set; }

		/// <summary>
		/// FK back to the AspNetUsers table for identity.
		/// </summary>
		[StringLength(128)]
		public string AspNetUserID { get; set; }
	}
}