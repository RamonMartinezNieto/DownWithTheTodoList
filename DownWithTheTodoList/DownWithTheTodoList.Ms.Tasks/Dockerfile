#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DownWithTheTodoList.Ms.Tasks/DownWithTheTodoList.Ms.Tasks.csproj", "DownWithTheTodoList.Ms.Tasks/"]
RUN dotnet restore "DownWithTheTodoList.Ms.Tasks/DownWithTheTodoList.Ms.Tasks.csproj"
COPY . .
WORKDIR "/src/DownWithTheTodoList.Ms.Tasks"
RUN dotnet build "DownWithTheTodoList.Ms.Tasks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DownWithTheTodoList.Ms.Tasks.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DownWithTheTodoList.Ms.Tasks.dll"]