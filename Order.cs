using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement
{
	public class Order
	{
		public string Id { get; set; }
		public string Destination { get; set; }
		public Flight CarryFlight { get; set; }
	}
}
