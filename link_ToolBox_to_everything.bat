:: I used AI to help me make a batch to add ToolBox to everything I want 
:: to have a namespace of helpful classes for doing mundane things like input validation

@echo off
setlocal enabledelayedexpansion

echo Checking for projects without ToolBox reference...

:: Find all .csproj files except ToolBox.csproj
for /r %%i in (*.csproj) do (
    set "FILENAME=%%~nxi"
    if /i not "!FILENAME!"=="ToolBox.csproj" (
        :: Check if file contains ToolBox reference
        findstr /i /c:"Include=\".*ToolBox.csproj\"" "%%i" >nul
        if errorlevel 1 (
            echo Adding ToolBox reference to %%~nxi
            dotnet add "%%i" reference "%~dp0ToolBox\ToolBox.csproj"
        ) else (
            echo %%~nxi already has ToolBox reference
        )
    )
)

echo.
echo Done! All projects should now have ToolBox reference.
pause