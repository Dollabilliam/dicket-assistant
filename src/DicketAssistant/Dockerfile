FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DicketAssistant/DicketAssistant.csproj", "DicketAssistant/"]
RUN dotnet restore "DicketAssistant/DicketAssistant.csproj"
COPY . .
WORKDIR "/src/DicketAssistant"
RUN dotnet build "DicketAssistant.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DicketAssistant.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DicketAssistant.dll"]
