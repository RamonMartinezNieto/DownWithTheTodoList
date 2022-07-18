#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DownWithTheTodoList.Ms.Blocks/DownWithTheTodoList.Ms.Blocks.csproj", "DownWithTheTodoList.Ms.Blocks/"]
RUN dotnet restore "DownWithTheTodoList.Ms.Blocks/DownWithTheTodoList.Ms.Blocks.csproj"
COPY . .
WORKDIR "/src/DownWithTheTodoList.Ms.Blocks"
RUN dotnet build "DownWithTheTodoList.Ms.Blocks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DownWithTheTodoList.Ms.Blocks.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DownWithTheTodoList.Ms.Blocks.dll"]