#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /app
COPY . .
RUN dotnet restore "Benchmark/Benchmark.csproj"
WORKDIR "/app/Benchmark"
RUN dotnet build "Benchmark.csproj" -c Release -o /app/bin

RUN dotnet publish "Benchmark.csproj" -c Release -o /app/bin/publish

WORKDIR /app/bin/publish
ENTRYPOINT ["dotnet", "Benchmark.dll"]