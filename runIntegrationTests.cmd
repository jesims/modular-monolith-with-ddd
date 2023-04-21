@ECHO OFF
SETLOCAL
SET CONTAINER_ID=
FOR /f %%i IN ('docker ps -q -f name^=myMeetings-integration-db') DO SET CONTAINER_ID=%%i

IF "%CONTAINER_ID%"=="" (
    ECHO "not found"
) ELSE (
	docker rm --force myMeetings-integration-db
)

REM docker run --rm --name myMeetings-integration-db -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=61cD4gE6!" -e "MSSQL_PID=Express" -p 1439:1433 -d mcr.microsoft.com/mssql/server:2017-latest-ubuntu
docker run --rm --name myMeetings-integration-db -e "POSTGRES_PASSWORD=sss_admin_password" -e "POSTGRES_USER=sss_admin" -e "POSTGRES_DB=postgres" -p 1439:5432 -d postgis/postgis:15-3.3
TIMEOUT 30
REM docker cp ./src/Database/CompanyName.MyMeetings.Database/Scripts/CreateDatabase_Linux.sql myMeetings-integration-db:/
REM docker exec -i myMeetings-integration-db sh -c "/opt/mssql-tools/bin/sqlcmd -d master -i /CreateDatabase_Linux.sql -U sss_admin -P sss_admin_password"
dotnet build src/ --configuration Release --no-restore
SET ASPNETCORE_MyMeetings_IntegrationTests_ConnectionString="Host=localhost;Port=1439;Database=postgres;Username=sss_admin;Password=sss_admin_password;"
REM SET ASPNETCORE_MyMeetings_IntegrationTests_ConnectionString=Host=localhost;Port=5432;Database=postgres;Username=sherpa;Password=sherpa;
REM dotnet "src/Database/DatabaseMigrator/bin/Release/netcoreapp3.1/DatabaseMigrator.dll" %ASPNETCORE_MyMeetings_IntegrationTests_ConnectionString% "src/Database/CompanyName.MyMeetings.Database/Scripts/Migrations"
REM flyway migrate
flyway -user=sss_admin -password=sss_admin_password -url=jdbc:postgresql://localhost:1439/postgres -locations=src/Database/sql migrate
echo "Migration Done"
dotnet test --configuration Release --no-build --verbosity normal src/Modules/Administration/Tests/IntegrationTests/CompanyName.MyMeetings.Modules.Administration.IntegrationTests.csproj
dotnet test --configuration Release --no-build --verbosity normal src/Modules/Payments/Tests/IntegrationTests/CompanyName.MyMeetings.Modules.Payments.IntegrationTests.csproj
dotnet test --configuration Release --no-build --verbosity normal src/Modules/UserAccess/Tests/IntegrationTests/CompanyNames.MyMeetings.Modules.UserAccess.IntegrationTests.csproj
dotnet test --configuration Release --no-build --verbosity normal src/Modules/Meetings/Tests/IntegrationTests/CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.csproj
dotnet test --configuration Release --no-build --verbosity normal src/Tests/IntegrationTests/CompanyName.MyMeetings.IntegrationTests.csproj
echo "Tests Done"