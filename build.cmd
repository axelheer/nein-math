@echo off

dotnet --info

dotnet restore

if not defined build_options call init.cmd

dotnet build **\*\project.json %build_options:Release=Debug%
dotnet build **\*\project.json %build_options%

dotnet pack src\NeinMath %build_options%
