# gRPC Event Demo

a .NET 5 demo featuring gRPC and  [Easy.MessageHub](https://github.com/NimaAra/Easy.MessageHub)

## Running

Terminal 1:

Runs the gRPC Server
```
cd GrpcEventsHost
dotnet run
```

Terminal 2:

Listens for events from the gRPC service using a client stream
```
cd GrpcEventsClient
dotnet run -endpoint=http://127.0.0.1:7575
```

Terminal 3:

Broadcasts messages to the gRPC server. 
```
cd GrpcEventsBroadcast
dotnet run -endpoint=http://127.0.0.1:7575 MESSAGE
```