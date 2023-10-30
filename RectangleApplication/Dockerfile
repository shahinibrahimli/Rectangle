FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

ARG SONAR_PROJECT_KEY=SustainCert_sc-platform-univerification
ARG SONAR_OGRANIZAION_KEY=sustaincert
ARG SONAR_HOST_URL=https://sonarcloud.io
ARG SONAR_TOKEN

WORKDIR /app

# Install Sonar Scanner, Coverlet and Java (required for Sonar Scanner)
RUN apt-get update && apt-get install -y openjdk-11-jdk
RUN dotnet tool install --global dotnet-sonarscanner && \
    dotnet tool install --global dotnet-coverage
ENV PATH="$PATH:/root/.dotnet/tools"


# Start Sonar Scanner
RUN dotnet sonarscanner begin \
    /k:"$SONAR_PROJECT_KEY" \
    /o:"$SONAR_OGRANIZAION_KEY" \
    /d:sonar.host.url="$SONAR_HOST_URL" \
    /d:sonar.login="$SONAR_TOKEN" \
    /d:sonar.cs.vscoveragexml.reportsPaths=coverage.xml

COPY *.sln .
COPY */*.csproj /app/
RUN for from in /app/*.csproj; do to=$(echo "$from" | sed 's/\/\([^/]*\)\.csproj$/\/\1&/') \
    && mkdir -p "$(dirname "$to")" && mv "$from" "$to"; done

RUN dotnet restore
COPY . .
COPY SC.UVD.API/appsettings.dev.json SC.UVD.API/appsettings.json
RUN dotnet build -c Release

RUN dotnet-coverage collect 'dotnet test' -f xml -o 'coverage.xml'
RUN dotnet sonarscanner end /d:sonar.login="$SONAR_TOKEN"

FROM build as migration

ENV PATH="${PATH}:/root/.dotnet/tools"

WORKDIR /app
COPY  . /app
RUN dotnet tool install --global dotnet-ef ; dotnet add SC.UVD.API package Microsoft.EntityFrameworkCore.Design -v 7.0.0

ENTRYPOINT dotnet ef database update --startup-project SC.UVD.API --project SC.UVD.Persistence


FROM build AS publish
ARG API_VERSION=1.0.0
ARG BUILD_NUMBER=0
ARG COMMIT_HASH=''
RUN echo Building version $API_VERSION.$BUILD_NUMBER+$COMMIT_HASH
RUN dotnet publish -c Release -o /publish --no-restore /p:Version=$API_VERSION.$BUILD_NUMBER /p:SourceRevisionId=$COMMIT_HASH

FROM base AS final
WORKDIR /app
COPY --from=publish /publish .
ENTRYPOINT ["dotnet", "SC.UVD.API.dll"]
