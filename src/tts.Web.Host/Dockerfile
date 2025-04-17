FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/tts.Web.Host/tts.Web.Host.csproj", "src/tts.Web.Host/"]
COPY ["src/tts.Web.Core/tts.Web.Core.csproj", "src/tts.Web.Core/"]
COPY ["src/tts.Application/tts.Application.csproj", "src/tts.Application/"]
COPY ["src/tts.Core/tts.Core.csproj", "src/tts.Core/"]
COPY ["src/tts.EntityFrameworkCore/tts.EntityFrameworkCore.csproj", "src/tts.EntityFrameworkCore/"]
WORKDIR "/src/src/tts.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/tts.Web.Host", "src/tts.Web.Host"]
COPY ["src/tts.Web.Core", "src/tts.Web.Core"]
COPY ["src/tts.Application", "src/tts.Application"]
COPY ["src/tts.Core", "src/tts.Core"]
COPY ["src/tts.EntityFrameworkCore", "src/tts.EntityFrameworkCore"]
WORKDIR "/src/src/tts.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "tts.Web.Host.dll"]
