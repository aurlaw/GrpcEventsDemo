using System;
using System.Threading;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcService.Protos;
using static GrpcService.Protos.Broadcast;


namespace GrpcEventsBroadcast
{
	public class BroadcastClientService : GrpcEventsComponents.ClientBase
	{
		public BroadcastClientService(string baseUrl) : base(baseUrl)
		{
		}

        public AsyncUnaryCall<Empty> AddMessageAsync(BroadcastData request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
		{
            var client = new BroadcastClient(_channel);
            return client.AddMessageAsync(request, headers, deadline, cancellationToken);
		}
        public AsyncUnaryCall<Empty> AddMessageAsync(BroadcastData request, CallOptions options)
		{
            var client = new BroadcastClient(_channel);
            return client.AddMessageAsync(request, options);
        }
    }
}

