using System;
using System.Threading;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcService.Protos;
using static GrpcService.Protos.WeatherForecasts;

namespace GrpcEventsClient
{
	public class WeatherClientService : GrpcEventsComponents.ClientBase
	{ 
		public WeatherClientService(string baseUrl) : base(baseUrl)
		{

        }
        public AsyncServerStreamingCall<WeatherData> GetWeatherStream(Empty request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
        {
            var client = new WeatherForecastsClient(_channel);
            return client.GetWeatherStream(request, headers, deadline, cancellationToken);
        }

        public AsyncServerStreamingCall<WeatherData> GetWeatherStream(Empty request, CallOptions options)
        {
            var client = new WeatherForecastsClient(_channel);
            return client.GetWeatherStream(request, options);
        }
    }
}
