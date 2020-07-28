using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement
{
	public class Flight
	{

		public int Id { get; set; }

		public string OriginAirport
		{
			get
			{
				return _originAirport;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException();
				_originAirport = value;
			}
		}
		private string _originAirport;

		public string DestinationAirport { get; set; }

		public string OriginCity { get; set; }

		public string DestinationCity { get; set; }

		public int Load { get; set; }

		public int Day { get; set; }
	}
}
