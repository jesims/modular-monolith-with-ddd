#
# Run Integration Tests
#

param(
    [switch]$build
)

#
# Build flag: If it's passed in as a parameter, it will force the tests to run 
# after a full compile. Without the flag on, it will just run as per normal.
# 
$buildflag = "--no-build"
if ($build) {
    $buildflag = ""
}

$containername="mymeetings-integration-db"
$testuser="sss_admin"
$testpassword="sss_admin_password"
$dbname="postgres"
$timerseconds=15
$result = docker ps -q -f name=$containername
Write-Output $result
if ($result) {
    docker rm --force $containername
}

Write-Output "▶️ Set up docker container"

docker run --rm --name $containername -e "POSTGRES_PASSWORD=$testpassword" -e "POSTGRES_USER=$testuser" -e "POSTGRES_DB=$dbname" -p 1439:5432 -d postgis/postgis:15-3.3
$env:ASPNETCORE_MyMeetings_IntegrationTests_ConnectionString="Host=localhost;Port=1439;Database=$dbname;Username=$testuser;Password=$testpassword;"

Write-Output "⏱️ Pausing for $timerseconds seconds to allow container to be spun up..."
Start-Sleep -s $timerseconds

Write-Output "▶️ Run Database Migrations"
evolve migrate postgresql -c "Host=localhost;Port=1439;Database=$dbname;Username=$testuser;Password=$testpassword;" -l "src/Database/sql"
Write-Output "✅ Migration Done"

Write-Output "▶️ Run Tests"
dotnet test --configuration Release $buildflag --verbosity normal src/Modules/Administration/Tests/IntegrationTests/CompanyName.MyMeetings.Modules.Administration.IntegrationTests.csproj
# dotnet test --configuration Release --no-build --verbosity normal src/Modules/Payments/Tests/IntegrationTests/CompanyName.MyMeetings.Modules.Payments.IntegrationTests.csproj
dotnet test --configuration Release $buildflag --verbosity normal src/Modules/UserAccess/Tests/IntegrationTests/CompanyNames.MyMeetings.Modules.UserAccess.IntegrationTests.csproj
dotnet test --configuration Release $buildflag --verbosity normal src/Modules/Meetings/Tests/IntegrationTests/CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.csproj
dotnet test --configuration Release $buildflag --verbosity normal src/Tests/IntegrationTests/CompanyName.MyMeetings.IntegrationTests.csproj
Write-Output "✅ Tests Completed"
