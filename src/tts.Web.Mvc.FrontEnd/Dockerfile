FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
RUN ln -s ~/.dotnet/tools/libman /usr/local/bin/libman

WORKDIR /src
COPY ["src/tts.Web.Mvc/tts.Web.Mvc.csproj", "src/tts.Web.Mvc/"]
COPY ["src/tts.Web.Core/tts.Web.Core.csproj", "src/tts.Web.Core/"]
COPY ["src/tts.Application/tts.Application.csproj", "src/tts.Application/"]
COPY ["src/tts.Core/tts.Core.csproj", "src/tts.Core/"]
COPY ["src/tts.EntityFrameworkCore/tts.EntityFrameworkCore.csproj", "src/tts.EntityFrameworkCore/"]
WORKDIR "/src/src/tts.Web.Mvc"
RUN dotnet restore 

WORKDIR /src
COPY ["src/tts.Web.Mvc", "src/tts.Web.Mvc"]
COPY ["src/tts.Web.Core", "src/tts.Web.Core"]
COPY ["src/tts.Application", "src/tts.Application"]
COPY ["src/tts.Core", "src/tts.Core"]
COPY ["src/tts.EntityFrameworkCore", "src/tts.EntityFrameworkCore"]
WORKDIR "/src/src/tts.Web.Mvc"
RUN libman restore
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "tts.Web.Mvc.dll"]
