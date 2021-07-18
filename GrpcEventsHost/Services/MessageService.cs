using System;
using System.Threading;
using System.Threading.Tasks;
using Easy.MessageHub;
using GrpcEventsHost.Models;
using Microsoft.Extensions.Logging;

namespace GrpcEventsHost.Services
{
	public class MessageService : IMessageService
	{
		private readonly ILogger<MessageService> _logger;
		private readonly IMessageHub _hub;
		private int _counter = 0;
		private static readonly string[] _summaries =
		 {
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		public MessageService(ILogger<MessageService> logger, IMessageHub hub)
		{
			_logger = logger;
			_hub = hub;
		}

		public async Task Process(CancellationToken cancellationToken)
		{
			var random = new Random();
			while(_counter < 100 && !cancellationToken.IsCancellationRequested)
			{
				_counter++;
				var data = new WeatherResult
				{
					Updated = DateTime.UtcNow.AddDays(_counter),
					Temp = random.Next(-20, 52),
					Summary = _summaries[random.Next(_summaries.Length)]
				};
				_logger.LogInformation($"Process {data.Temp} at {data.Updated}");
				// publish message
				_hub.Publish(data);

				await Task.Delay(500);
			}
		}

		public Task ProcessMessage(WeatherResult weatherResult)
		{
			_logger.LogInformation($"Publish {weatherResult.Temp} at {weatherResult.Updated}");
			_hub.Publish(weatherResult);

			return Task.CompletedTask;
		}
	}
}
