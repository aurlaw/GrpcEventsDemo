# gRPC Event Demo

a .NET 5 demo featuring gRPC and  [Easy.MessageHub](https://github.com/NimaAra/Easy.MessageHub)

## Running

Terminal 1:


Runs the gRPC Server
```
cd GrpcEventsHost
dotnet run
```

or using Docker

Generate SSL
```
cd GrpcEventsHost
./gen-prod.https.sh localhost grpc
```

Build and run image
```
cd ..

docker build -t grpc-server -f GrpcEventsHost/Dockerfile .

docker run --rm -it -p 5051:443 -e ASPNETCORE_HTTPS_PORT=5051 -e ASPNETCORE_URLS="https://*:443" -e ASPNETCORE_Kestrel__Certificates__Default__Password="grpc" -e ASPNETCORE_Kestrel__Certificates__Default__Path="prod.pfx" grpc-server

```


Terminal 2:

Listens for events from the gRPC service using a client stream
```
cd GrpcEventsClient
dotnet run -endpoint=http://127.0.0.1:7575
```
or if using docker service
```
dotnet run -endpoint=https://localhost:5051
```

Terminal 3:

Broadcasts messages to the gRPC server. 
```
cd GrpcEventsBroadcast
dotnet run -endpoint=http://127.0.0.1:7575 MESSAGE
```
or if using docker service
```
dotnet run -endpoint=https://localhost:5051 MESSAGE
```
