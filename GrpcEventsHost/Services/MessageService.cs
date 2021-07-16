using System;
using System.Threading.Tasks;
using GrpcEventsHost.Models;
using Microsoft.Extensions.Logging;

namespace GrpcEventsHost.Services
{
	public class MessageService : IMessageService
	{
		private readonly ILogger<MessageService> _logger;
		private int _counter = 0;
		private static readonly string[] _summaries =
		 {
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		public MessageService(ILogger<MessageService> logger)
		{
			_logger = logger;
		}

		public Task Process()
		{
			var random = new Random();
			_counter++;
			var data = new WeatherResult
			{
				Updated = DateTime.UtcNow.AddDays(_counter),
				Temp = random.Next(-20, 52),
				Summary = _summaries[random.Next(_summaries.Length)]
			};
			_logger.LogInformation($"Process {data.Temp} at {data.Updated}");

			return Task.CompletedTask;
		}
	}
}
