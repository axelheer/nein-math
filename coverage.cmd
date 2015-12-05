@echo off

set artifacts=%~dp0artifacts
set coverage=%~dp0coverage
set test=%~dp0test

set dnx=%UserProfile%\.dnx\runtimes\dnx-clr-win-x64.1.0.0-rc1-update1\bin\dnx.exe
set opencover=%UserProfile%\.dnx\packages\OpenCover\4.6.166\tools\OpenCover.Console.exe
set reportgenerator=%UserProfile%\.dnx\packages\ReportGenerator\2.3.5\tools\ReportGenerator.exe

if not exist coverage mkdir coverage

"%opencover%" -target:"%dnx%" -targetargs:"--lib \"%artifacts%\bin\NeinMath\Release\net40\" --project \"%test%\NeinMath.Tests\" test" -output:"%coverage%\NeinMath.xml" -register:user -filter:+[NeinMath]*

"%reportgenerator%" -reports:"%coverage%\NeinMath.xml" -targetdir:"%coverage%\report"
