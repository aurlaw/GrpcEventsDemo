using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;

namespace GrpcEventsClient
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
			if(endpointOption == null)
			{
				Console.WriteLine("Endpoint not specified");
				return;
			}
			var endpointUrl = endpointOption.Substring(endpointOption.IndexOf('=')+1);
			Console.WriteLine($"Using endpoint: {endpointUrl}");
			//Console.WriteLine(string.Join('|', args));
			await GetWeatherAsync(endpointUrl);
		}


		static async Task GetWeatherAsync(string baseUrl)
		{
			Console.WriteLine($"Connecting to... {baseUrl}");
			Console.WriteLine("Get Weather Async");
            var client = new WeatherClientService(baseUrl);
            var cancelToken = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            using var streamingCall = client.GetWeatherStream(new Google.Protobuf.WellKnownTypes.Empty(), cancellationToken: cancelToken.Token);
			try
			{
                await foreach(var weather in streamingCall.ResponseStream.ReadAllAsync(cancellationToken: cancelToken.Token))
				{
					//Console.WriteLine("Weather received");
					Console.WriteLine($"{weather.DateTime.ToDateTime():s} | {weather.Summary} | {weather.TemperatureC} C | {weather.TemperatureF} F");
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
			{
                Console.WriteLine("Weather Stream cancelled from client");
			}

        }

    }
}
