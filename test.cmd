@echo off

set opencover=%UserProfile%\.nuget\packages\OpenCover\4.6.519\tools\OpenCover.Console.exe
set reportgenerator=%UserProfile%\.nuget\packages\ReportGenerator\2.5.2\tools\ReportGenerator.exe

if not exist TestResults mkdir TestResults || goto :eof

"%opencover%" -target:dotnet.exe -targetargs:"test test\NeinMath.Tests\NeinMath.Tests.csproj --configuration Release --framework net461" -searchdirs:test\NeinMath.Tests\bin\Release\net461 -output:TestResults\NeinMath.netframework.report.xml -register:user -filter:+[NeinMath]* -returntargetcode -oldstyle || goto :eof
"%opencover%" -target:dotnet.exe -targetargs:"test test\NeinMath.Tests\NeinMath.Tests.csproj --configuration Release --framework netcoreapp1.1" -searchdirs:test\NeinMath.Tests\bin\Release\netcoreapp1.1 -output:TestResults\NeinMath.netcoreapp.report.xml -register:user -filter:+[NeinMath]* -returntargetcode -oldstyle || goto :eof

"%reportgenerator%" -reports:TestResults\*.report.xml -targetdir:TestResults\report -reporttypes:Badges;Html || goto :eof
