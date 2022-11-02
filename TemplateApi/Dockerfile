#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ARG DATABASE_CONNECTION_STRING

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TemplateApi/TemplateApi.csproj", "TemplateApi/"]
COPY ["TemplateApi.CrossCutting/TemplateApi.CrossCutting.csproj", "TemplateApi.CrossCutting/"]
COPY ["TemplateApi.Domain/TemplateApi.Domain.csproj", "TemplateApi.Domain/"]
COPY ["TemplateApi.Infra/TemplateApi.Infra.csproj", "TemplateApi.Infra/"]
RUN dotnet restore "TemplateApi/TemplateApi.csproj"
COPY . .
WORKDIR "/src/TemplateApi"
RUN dotnet build "TemplateApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TemplateApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TemplateApi.dll"]