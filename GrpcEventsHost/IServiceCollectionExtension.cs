using Easy.MessageHub;
using Microsoft.Extensions.DependencyInjection;

namespace GrpcEventsHost
{
	public static class IServiceCollectionExtension
	{
		public static IServiceCollection AddMessageBus(this IServiceCollection services)
		{
			var bus = new MessageHub();
			services.AddSingleton<IMessageHub>(bus);

			return services;
		}

	}
}
