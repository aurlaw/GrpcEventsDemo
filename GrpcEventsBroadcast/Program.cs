using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using GrpcService.Protos;

namespace GrpcEventsBroadcast
{
	class Program
	{
		static async Task Main(string[] args)
		{
			if(args.Length == 0)
			{
				Console.WriteLine("No arguments specified");
				return;
			}
			var endpointOption = args.FirstOrDefault(a => a.Contains("-endpoint"));
			if (endpointOption == null)
			{
				Console.WriteLine("Endpoint not specified");
				return;
			}
			var messages = args.Where(a => !a.Contains("-endpoint"));
			if(!messages.Any())
			{
				Console.WriteLine("No messages specified");
				return;
			}

			var endpointUrl = endpointOption.Substring(endpointOption.IndexOf('=') + 1);
			Console.WriteLine($"Using endpoint: {endpointUrl}");

			await Broadcast(endpointUrl, messages.ToArray());
		}

		static async Task Broadcast(string baseUrl, string[] messageArray)
		{
			Console.WriteLine($"Connecting to... {baseUrl}");
			var random = new Random();
			var message = string.Join(" ", messageArray);
			var broadcastMessage = new BroadcastData
			{
				Summary = message,
				TemperatureC = random.Next(-20, 52),
				DateTime = Timestamp.FromDateTime(DateTime.UtcNow)
			};
			Console.WriteLine($"Message: {message}");
			var client = new BroadcastClientService(baseUrl);
			_ = await client.AddMessageAsync(broadcastMessage);

		}
	}
}
