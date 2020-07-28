using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace InventoryManagement
{
	class Program
	{
		private static int days = 2;

		private static List<Flight> _flights = new List<Flight>();
		private static List<Order> _orders = new List<Order>();
		private const int MaxLoad = 20;
		public static void Main(string[] args)
		{

			Console.WriteLine("Commands: \"Flight\" to show availabe flight." + Environment.NewLine +
				"\"Order\" to load order and show iteraries." + Environment.NewLine +
				"\"Exit\" to exit.");
			var input = Console.ReadLine();

			while (input.Trim().ToLowerInvariant() != "exit")
			{
				switch (input.Trim().ToLowerInvariant())
				{
					case "flight":
						LoadSchedules();
						foreach (var f in _flights)
						{
							Console.WriteLine(string.Format("Flight: {0}, departure: {1}, arrival: {2}, day: {3}",
								f.Id, f.OriginAirport, f.DestinationAirport, f.Day));
						}
						break;
					case "order":
						LoadOrders();
						ScheduleOrders();
						break;
					default:
						break;

				}

				Console.WriteLine("Commands: \"Flight\" to show availabe flight." + Environment.NewLine +
				"\"Order\" to load order and show iteraries." + Environment.NewLine +
				"\"Exit\" to exit.");
				input = Console.ReadLine();
			}
		}

		public static void LoadSchedules()
		{
			//clear list
			_flights.Clear();

			var flightId = 1;
			for (var i = 0; i < days; i++)
			{
				var flight1 = new Flight()
				{
					Id = flightId++,
					OriginAirport = "YUL",
					OriginCity = "Montreal",
					DestinationAirport = "YYZ",
					DestinationCity = "Toronto",
					Day = i + 1,
					Load = 0
				};
				var flight2 = new Flight()
				{
					Id = flightId++,
					OriginAirport = "YUL",
					OriginCity = "Montreal",
					DestinationAirport = "YYC",
					DestinationCity = "Calgary",
					Day = i + 1,
					Load = 0
				};
				var flight3 = new Flight()
				{
					Id = flightId++,
					OriginAirport = "YUL",
					OriginCity = "Montreal",
					DestinationAirport = "YVR",
					DestinationCity = "Vancouver",
					Day = i + 1,
					Load = 0
				};

				_flights.Add(flight1);
				_flights.Add(flight2);
				_flights.Add(flight3);
			}
		}


		public static void LoadOrders()
		{
			//clear list
			_orders.Clear();

			//var path = Path.Combine(Directory.GetCurrentDirectory(), "\\coding-assigment-orders.json");
			var path = @"D:\personal\code\air tek\project\InventoryManagement\coding-assigment-orders.json";
			// read file into a string and deserialize JSON to a type
			Dictionary<string, Order> orders= JsonConvert.DeserializeObject<Dictionary<string, Order>>(
				File.ReadAllText(path));

			foreach (var order in orders)
			{
				order.Value.Id = order.Key;
				_orders.Add(order.Value);
			}
		}


		public static void ScheduleOrders()
		{
			foreach (var order in _orders)
			{
				var availableFlight = _flights.FirstOrDefault(x => x.DestinationAirport == order.Destination && x.Load < MaxLoad);

				if (availableFlight != null) {
					order.CarryFlight = availableFlight;
					availableFlight.Load++;
				}
			}

			foreach(var order in _orders)
			{
				if (order.CarryFlight != null)
					Console.WriteLine(string.Format("order: {0}, flightNumber: {1}, departure: {2}, arrival: {3}, day: {4}",
						order.Id, order.CarryFlight.Id, order.CarryFlight.OriginAirport, order.CarryFlight.DestinationAirport,
						order.CarryFlight.Day));
				else
					Console.WriteLine(string.Format("order: {0}, flightNumber: not scheduled", order.Id));
			}
		}
	}
}
