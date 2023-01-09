#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
COPY ["Benchmark/Benchmark.csproj", "Benchmark/"]
COPY ["Data/Repositories.csproj", "Data/"]
COPY ["Exceptions/Exceptions.csproj", "Exceptions/"]
COPY ["Model/Model.csproj", "Model/"]
COPY ["Logic/Logic.csproj", "Logic/"]
COPY ["RepositoriesImplementations/RepositoriesImplementations.csproj", "RepositoriesImplementations/"]
COPY . .
RUN dotnet restore "Benchmark/Benchmark.csproj"
WORKDIR /app/Benchmark
RUN dotnet build "Benchmark.csproj" -c Release -o /app/bin

RUN dotnet publish "Benchmark.csproj" -c Release -o /app/bin/publish /p:UseAppHost=false

WORKDIR /app/bin/publish
ENTRYPOINT ["dotnet", "Benchmark.dll"]