using OrderServiceBus.Models;
using System.Collections.Generic;

namespace CS401DataContract
{
	public class PackagedOrder
	{
		public Order Order { get; set; }

		public List<OrderProduct> OrderProducts { get; set; }
	}
}