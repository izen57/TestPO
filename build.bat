msbuild TestPO\TestPO.csproj
vstest.console.exe /logger:trx bin\debug\net6.0-windows10.0.22621.0\TestPO.dll
allure serve TestResults\