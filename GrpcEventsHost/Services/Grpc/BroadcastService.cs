using System;
using System.Threading.Tasks;
using Easy.MessageHub;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcEventsHost.Models;
using GrpcService.Protos;
using Microsoft.Extensions.Logging;

namespace GrpcEventsHost.Services.Grpc
{
	public class BroadcastService : Broadcast.BroadcastBase
	{
		private readonly ILogger<BroadcastService> _logger;
		private readonly IMessageService _messageService;


		public BroadcastService(ILogger<BroadcastService> logger, IMessageService messageService)
		{
			_logger = logger;
			_messageService = messageService;
		}

		public override async Task<Empty> AddMessage(BroadcastData request, ServerCallContext context)
		{
			_logger.LogInformation("AddMessage invoked");
			var weatherResult = new WeatherResult
			{
				Updated = request.DateTime.ToDateTime(),
				Summary = request.Summary,
				Temp = request.TemperatureC
			};
			await _messageService.ProcessMessage(weatherResult);

			return new Empty();

		}
	}
}
