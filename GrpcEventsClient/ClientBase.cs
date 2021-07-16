using System;
using Grpc.Net.Client;

namespace GrpcEventsClient
{
	public class ClientBase
	{

		protected readonly GrpcChannel _channel;
		protected readonly string _serviceBaseUrl;
		public ClientBase(string baseUrl)
		{
			_serviceBaseUrl = baseUrl;
			AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
			_channel = GrpcChannel.ForAddress(_serviceBaseUrl);
		}
	}
}