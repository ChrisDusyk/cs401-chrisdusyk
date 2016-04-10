using System.Collections.Generic;
using AzureWebAppComponent.Models;

namespace CS401DataContract
{
	public class PackagedOrder
	{
		public Order Order { get; set; }

		public List<OrderProduct> OrderProducts { get; set; }
	}
}