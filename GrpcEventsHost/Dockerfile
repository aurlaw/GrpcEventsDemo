FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

ENV ASPNETCORE_URLS="https://+"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path="prod.pfx"

# Copy csproj and restore as distinct layers
# COPY *.csproj ./
# copy main project
COPY ["GrpcEventsHost/GrpcEventsHost.csproj", "src/GrpcEventsHost/"]

# copies also other dependencies
COPY ["Protos/*.proto", "src/Protos/"]
# COPY ["src/...csproj", "src/.."]


COPY ./ ./
WORKDIR "/src/GrpcEventsHost"
RUN dotnet restore "GrpcEventsHost.csproj"
RUN dotnet build "GrpcEventsHost.csproj" --no-restore -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "GrpcEventsHost.csproj" -c Release -o /app/publish



FROM mcr.microsoft.com/dotnet/aspnet:5.0.4-alpine3.13-amd64 AS final
# USER ContainerAdministrator
WORKDIR /app
COPY --from=publish /app/publish .
COPY GrpcEventsHost/prod.pfx .

ENTRYPOINT ["dotnet", "GrpcEventsHost.dll"]
