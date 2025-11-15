@echo off
setlocal

echo.
echo Running AgileBoard Tests...
echo ================================
echo.

REM Check if dotnet is installed
where dotnet >nul 2>nul
if %errorlevel% neq 0 (
    echo ERROR: .NET SDK is not installed.
    echo Please install .NET 7.0 SDK from: https://dotnet.microsoft.com/download
    exit /b 1
)

REM Check .NET version
echo Checking .NET version...
dotnet --version
echo.

REM Restore dependencies
echo Restoring dependencies...
dotnet restore
if %errorlevel% neq 0 exit /b %errorlevel%
echo.

REM Build the solution
echo Building solution...
dotnet build --no-restore --configuration Release
if %errorlevel% neq 0 exit /b %errorlevel%
echo.

REM Run the tests
echo Running unit tests...
dotnet test tests/AgileBoard.Tests.Unit/AgileBoard.Tests.Unit.csproj --no-build --configuration Release --verbosity normal --logger "console;verbosity=detailed"

if %errorlevel% equ 0 (
    echo.
    echo ✓ All tests passed!
    exit /b 0
) else (
    echo.
    echo ✗ Tests failed!
    exit /b 1
)
