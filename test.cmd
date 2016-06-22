@echo off

set opencover=%UserProfile%\.nuget\packages\OpenCover\4.6.519\tools\OpenCover.Console.exe
set reportgenerator=%UserProfile%\.nuget\packages\ReportGenerator\2.4.5\tools\ReportGenerator.exe

if not exist TestResults mkdir TestResults

"%opencover%" -target:dotnet.exe -targetargs:"test test\NeinMath.Tests -xml TestResults\NeinMath.result.xml" -output:TestResults\NeinMath.report.xml -register:user -filter:+[NeinMath]*

"%reportgenerator%" -reports:TestResults\NeinMath.report.xml -targetdir:TestResults\report -reporttypes:Badges;Html
