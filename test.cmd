@echo off

set test=%~dp0test
set results=%~dp0TestResults

set opencover=%UserProfile%\.nuget\packages\OpenCover\4.6.519\tools\OpenCover.Console.exe
set reportgenerator=%UserProfile%\.nuget\packages\ReportGenerator\2.4.5\tools\ReportGenerator.exe

if not exist %results% mkdir %results%

"%opencover%" -target:dotnet.exe -targetargs:"test \"%test%\NeinMath.Tests\"" -output:"%results%\NeinMath.xml" -register:user -filter:+[NeinMath]*

"%reportgenerator%" -reports:"%results%\NeinMath.xml" -targetdir:"%results%\report"
