FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Job.Advertisement.Service.csproj", "./"]
RUN dotnet restore "Job.Advertisement.Service.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Job.Advertisement.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Job.Advertisement.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Job.Advertisement.Service.dll"] 

COPY swears.txt /app/


