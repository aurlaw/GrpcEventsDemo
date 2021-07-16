using System;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcService.Protos;
using Microsoft.Extensions.Logging;

namespace GrpcEventsHost.Services.Grpc
{
	public class WeatherService : WeatherForecasts.WeatherForecastsBase
    {
        private readonly ILogger<WeatherService> _logger;
        private readonly IMessageService _messageService;

		public WeatherService(ILogger<WeatherService> logger, IMessageService messageService)
		{
			_logger = logger;
			_messageService = messageService;
		}

		public override async Task GetWeatherStream(Empty _, IServerStreamWriter<WeatherData> responseStream, ServerCallContext context)
		{
            _logger.LogInformation("GetWeatherStream invoked");
            while(!context.CancellationToken.IsCancellationRequested)
			{
                var forecast = new WeatherData();
                _logger.LogInformation("Sending weather response");
                await responseStream.WriteAsync(forecast);
                await Task.Delay(500); //simulate latency

            }
            if (context.CancellationToken.IsCancellationRequested)
			{
                _logger.LogInformation("The client cancelled request");
            }
        }
    }
}
/*
     public class WeatherService : WeatherForecasts.WeatherForecastsBase
    {
        
        public override async Task GetWeatherStream(Empty _, IServerStreamWriter<WeatherData> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("GetWeatherStream invoked");
            var max = 20;
            var i = 0;
            var now = DateTime.UtcNow;
            var random = new Random();
            while(!context.CancellationToken.IsCancellationRequested && i < max)
            {
                var forecast = new WeatherData
                {
                    DateTime = Timestamp.FromDateTime(now.AddDays(i++)),
                    TemperatureC = random.Next(-20, 50),
                    Summary = _summaries[random.Next(_summaries.Length)]
                };
                forecast.TemperatureF = (forecast.TemperatureC * 9/5) + 32;

                _logger.LogInformation("Sending weather response");
                await responseStream.WriteAsync(forecast);

                await Task.Delay(500); //simulate latency
            }
            if(context.CancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("The client cancelled request");
            }   
        }
        
    }
 
 */