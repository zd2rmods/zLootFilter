Set-StrictMode -Version latest
$ErrorActionPreference = "Stop"

# Clean
dotnet clean
Remove-Item .\mod -Recurse -ErrorAction Ignore
Remove-Item .\temp -Recurse -ErrorAction Ignore
Remove-Item .\release -Recurse -ErrorAction Ignore

# Crete mod directory
New-Item -ItemType Directory -Path .\mod

# Create temp directory
New-Item -ItemType Directory -Path .\temp

# Build console app
dotnet restore
dotnet build --no-restore --no-incremental -c Release
dotnet publish --no-restore --no-build -c Release -o .\temp\app

# Run console application
.\temp\app\zLootFilterConsoleApp.exe .\original .\mod

# Pack MPQ file
New-Item -ItemType Directory -Path .\release
New-Item -ItemType Directory -Path .\release\zLootFilter
.\tools\mpqtool.exe new .\mod\ .\release\zLootFilter\zLootFilter.mpq

# Create release zip file
Compress-Archive -Path .\release\zLootFilter -DestinationPath .\release\zLootFilter.zip
