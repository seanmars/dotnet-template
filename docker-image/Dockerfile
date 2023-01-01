FROM microsoft/dotnet:{3.0}-sdk as publishImage
WORKDIR /app
COPY ./{source-code-folder}/. .
RUN dotnet restore
RUN dotnet publish -c Release -o Publish

FROM microsoft/dotnet:{3.0}-aspnetcore-runtime as execIamge
WORKDIR /www
ENV ASPNETCORE_ENVIRONMENT="{Production}"
ENV ASPNETCORE_URLS="{http://+:80}"
COPY --from=publishImage /app/Publish/. /www/.

ENTRYPOINT /bin/bash -c "dotnet {project-name}.dll"