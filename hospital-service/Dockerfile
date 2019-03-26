FROM microsoft/dotnet:2.2-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-stretch AS build
WORKDIR /src
COPY ["hospital-service/hospital-service.csproj", "hospital-service/"]
RUN dotnet restore "hospital-service/hospital-service.csproj"
COPY . .
WORKDIR "/src/hospital-service"
RUN dotnet build "hospital-service.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "hospital-service.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "hospital-service.dll"]