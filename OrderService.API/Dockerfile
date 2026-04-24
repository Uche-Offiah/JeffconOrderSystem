# BUild STAGE

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj first for better caching
COPY PaymentService.Worker/OrderService.API.csproj ./OrderService.API/

RUN dotnet restore ./OrderService.API/OrderService.API.csproj

COPY . .

WORKDIR /src/OrderService.API

RUN dotnet publish -c Release -o /app/out

# RUNTIME STAGE

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app

# Copy only published output
COPY --from=build /app/out .

# Run the worker
ENTRYPOINT ["dotnet", "OrderService.API.dll"]


# BUild stage

#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#WORKDIR /app
#
#COPY . ./
#RUN dotnet restore
#RUN dotnet publish -c Release -o out


# Runtime stage

#FROM mcr.microsoft.com/dotnet/runtime:8.0
#WORKDIR /app
#
#COPY --from=build /app/out .
#
#ENTRYPOINT ["dotnet", "OrderService.API.dll"]