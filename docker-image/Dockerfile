FROM mcr.microsoft.com/dotnet/aspnet:{Version} AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:{Version} AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["{Project Folder}/{Project Name}.csproj", "{Project Folder}/"]
RUN dotnet restore "{Project Folder}/{Project Name}.csproj"
COPY . .
WORKDIR "/src/{Project Folder}"
RUN dotnet build "{Project Name}.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "{Project Name}.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "{Project Name}.dll"]
