using System.Collections.Generic;

namespace OrderServiceBus.Models
{
	public class PackagedOrder
	{
		public Order Order { get; set; }

		public List<OrderProduct> OrderProducts { get; set; }
	}
}