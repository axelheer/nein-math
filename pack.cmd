@echo off

dotnet restore

dotnet pack src\NeinMath --configuration Release
