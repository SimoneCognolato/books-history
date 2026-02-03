FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["books-history.sln", "./"]
COPY ["books-history/books-history.csproj", "books-history/"]
COPY ["api.model/api.model.csproj", "api.model/"]
COPY ["data.migration/data.migration.csproj", "data.migration/"]
COPY ["data.model/data.model.csproj", "data.model/"]
COPY ["data.repository/data.repository.csproj", "data.repository/"]

RUN dotnet restore "books-history.sln"

COPY . .
WORKDIR "/src/books-history"
RUN dotnet publish "books-history.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "books-history.dll"]