#!/bin/sh

msbuild IntegrationTests/IntegrationTests.csproj
msbuild UnitTests/UnitTests.csproj

vstest.console.exe /logger:trx IntegrationTests/bin/debug/net6.0-windows10.0.22621.0/IntegrationTests.dll
vstest.console.exe /logger:trx UnitTests/bin/debug/net6.0-windows10.0.22621.0/UnitTests.dll
allure serve TestResults/

#rmdir /s /q %LOCALAPPDATA%\IsolatedStorage
rmdir -s -q %LOCALAPPDATA%/IsolatedStorage