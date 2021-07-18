using System;
using System.Threading;
using System.Threading.Tasks;
using GrpcEventsHost.Models;

namespace GrpcEventsHost.Services
{
	public interface IMessageService
	{
		Task Process(CancellationToken cancellationToken);
		Task ProcessMessage(WeatherResult weatherResult);

	}
}
