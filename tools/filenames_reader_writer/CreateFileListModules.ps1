$baseFolder = Read-Host -Prompt "Bitte geben Sie den absoluten Pfad ein"
$outputFolder = $PSScriptRoot

# Funktion, um nur die Dateien zu finden, die auf *Module.cs und *.csproj enden
function GetModuleAndCsprojFiles {
    param ([string]$folder)

    Get-ChildItem -Path $folder -Recurse -File | Where-Object {
        $_.Name -match "Module\.cs$" -or $_.Extension -eq ".csproj"
    }
}

# Hauptfunktion, um die Textdateien zu erstellen
function CreateModuleFileList {
    param ([string]$folder)

    $folder = $folder.Trim('"')
    Get-ChildItem -Path $folder -Directory | Where-Object {
        $_.FullName -notlike "*\obj*" -and $_.FullName -notlike "*\bin*" -and $_.FullName -notlike "*\abppkg*"
    } | ForEach-Object {
        $folderName = $_.Name
        $outputFilePath = Join-Path -Path $outputFolder -ChildPath ("$folderName.txt")

        $moduleFiles = GetModuleAndCsprojFiles -folder $_.FullName
        if ($moduleFiles) {
            $outputContent = ""
            foreach ($file in $moduleFiles) {
                $relativePath = $file.FullName.Replace($baseFolder, '').Trim('\')
                $outputContent += "$relativePath`n
`n" + (Get-Content $file.FullName -Raw) + "`n`n"
            }
            Set-Content -Path $outputFilePath -Value $outputContent
        }
    }
}

# Skript ausführen

CreateModuleFileList -folder $baseFolder

Write-Host "Die Dateien wurden erfolgreich erstellt."