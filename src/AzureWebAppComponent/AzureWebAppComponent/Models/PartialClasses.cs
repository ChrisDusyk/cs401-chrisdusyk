using System.ComponentModel.DataAnnotations;

namespace AzureWebAppComponent.Models
{
	public class PartialClasses
	{
		[MetadataType(typeof(CustomerMetadata))]
		public partial class Customer
		{
		}

		[MetadataType(typeof(OrderMedetadata))]
		public partial class Order
		{
		}

		[MetadataType(typeof(EmployeeMetadata))]
		public partial class Employee
		{
		}
	}
}