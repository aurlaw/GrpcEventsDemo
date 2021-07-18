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
	public class WeatherService : WeatherForecasts.WeatherForecastsBase
    {
        private readonly ILogger<WeatherService> _logger;
        private readonly IMessageHub _hub;


		public WeatherService(ILogger<WeatherService> logger, IMessageHub hub)
		{
			_logger = logger;
			_hub = hub;
		}

		public override async Task GetWeatherStream(Empty _, IServerStreamWriter<WeatherData> responseStream, ServerCallContext context)
		{
            _logger.LogInformation("GetWeatherStream invoked");

            var streamToken = _hub.Subscribe<WeatherResult>(result => OnWeatherData(result, responseStream));
			try
			{
                await context.CancellationToken.WaitUntilCancelled().ConfigureAwait(false);
			}
            catch (Exception ex)
			{
                _logger.LogError(ex, ex.Message);
			}
            finally
			{
                _hub.Unsubscribe(streamToken);
			}
        }

        private async Task OnWeatherData(WeatherResult weatherResult, IServerStreamWriter<WeatherData> responseStream)
		{
            var forecast = new WeatherData
            {
                DateTime = Timestamp.FromDateTime(weatherResult.Updated),
                TemperatureC = weatherResult.Temp,
                TemperatureF = weatherResult.TempF,
                Summary = weatherResult.Summary
            };
            await responseStream.WriteAsync(forecast);
        }
    }
}
/*
 * 
 var streamToken = _hub.Subscribe<SnapshotBroadcastWorkItem.Data>(data => onNewData(data, responseStream));
 * 
try
    {
        await context.CancellationToken.WaitUntilCancelled().ConfigureAwait(false);
    }
    catch (Exception e)
    {
        // log me, maybe?
    }
    finally
    {
        // cleanup
        _hub.Unsubscribe(streamToken);
    } * 
 * 
private async Task onNewData(SnapshotBroadcastWorkItem.Data data, IServerStreamWriter<StartStreamResponse> responseStream)
{
    await responseStream.WriteAsync(data).ConfigureAwait(false);
}
 
 */