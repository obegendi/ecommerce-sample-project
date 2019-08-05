FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/EcommerceSample.Console/EcommerceSample.ConsoleApp.csproj", "src/EcommerceSample.Console/"]
COPY ["src/OrderManagement/OrderManagement.Core/OrderManagement.Core.csproj", "src/OrderManagement/OrderManagement.Core/"]
COPY ["src/CampaignManagement/CampaignManagement.Core/CampaignManagement.Core.csproj", "src/CampaignManagement/CampaignManagement.Core/"]
COPY ["src/SharedKernel/SharedKernel.csproj", "src/SharedKernel/"]
COPY ["src/ProductManagement/ProductManagement.Core/ProductManagement.Core.csproj", "src/ProductManagement/ProductManagement.Core/"]
COPY ["src/EcommerceSample.Data/EcommerceSample.Data.csproj", "src/EcommerceSample.Data/"]
COPY ["src/Helpers/CommandHandlersStrategies/CommandHandlers.csproj", "src/Helpers/CommandHandlersStrategies/"]
COPY ["src/EcommerceSample.TimeSimulator/EcommerceSample.TimeSimulator.csproj", "src/EcommerceSample.TimeSimulator/"]
COPY ["src/ApplicationLayer/ApplicationServices/ApplicationServices.csproj", "src/ApplicationLayer/ApplicationServices/"]
COPY ["src/Helpers/ValidationModule/ValidationModule.csproj", "src/Helpers/ValidationModule/"]
RUN dotnet restore "src/EcommerceSample.Console/EcommerceSample.ConsoleApp.csproj"
COPY . .
WORKDIR "/src/src/EcommerceSample.Console"
RUN dotnet build "EcommerceSample.ConsoleApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "EcommerceSample.ConsoleApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EcommerceSample.ConsoleApp.dll"]