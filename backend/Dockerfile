FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY DashkevichStudio.slnx ./
COPY src/DashkevichStudio.Domain/DashkevichStudio.Domain.csproj src/DashkevichStudio.Domain/
COPY src/DashkevichStudio.Application/DashkevichStudio.Application.csproj src/DashkevichStudio.Application/
COPY src/DashkevichStudio.Infrastructure/DashkevichStudio.Infrastructure.csproj src/DashkevichStudio.Infrastructure/
COPY src/DashkevichStudio.Api/DashkevichStudio.Api.csproj src/DashkevichStudio.Api/
RUN dotnet restore src/DashkevichStudio.Api/DashkevichStudio.Api.csproj

COPY src ./src
RUN dotnet publish src/DashkevichStudio.Api/DashkevichStudio.Api.csproj \
    --configuration Release \
    --no-restore \
    --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
RUN mkdir -p /app/uploads && chown -R app:app /app
COPY --from=build --chown=app:app /app/publish .
USER app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "DashkevichStudio.Api.dll"]
