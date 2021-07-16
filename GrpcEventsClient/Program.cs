using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;

namespace GrpcEventsClient
{
	class Program
	{
		static async Task Main(string[] args)
		{
			await GetWeatherAsync("http://127.0.0.1:7575");
		}


        static async Task GetWeatherAsync(string baseUrl)
		{
			Console.WriteLine("Get Weather Async");
            var client = new WeatherClientService(baseUrl);
            var cancelToken = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            using var streamingCall = client.GetWeatherStream(new Google.Protobuf.WellKnownTypes.Empty(), cancellationToken: cancelToken.Token);
			try
			{
                await foreach(var weather in streamingCall.ResponseStream.ReadAllAsync(cancellationToken: cancelToken.Token))
				{
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
