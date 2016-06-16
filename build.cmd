@echo off

dotnet --info

dotnet restore

dotnet build src\*\project.json --configuration Debug
dotnet build src\*\project.json --configuration Release
dotnet build test\*\project.json --configuration Debug
dotnet build test\*\project.json --configuration Release

dotnet pack src\NeinMath --configuration Release
