$baseFolder = Read-Host -Prompt "Bitte geben Sie den absoluten Pfad ein"
$outputFolder = $PSScriptRoot

# Funktion, um alle bearbeitbaren Dateien zu finden
function Get-EditableFiles {
    param ([string]$folder)

    Get-ChildItem -Path $folder -Recurse -File -Exclude *.dll, *.exe, *.pdb, FodyWeavers.xml, *.abppkg.analyze.json, *.abppkg.json | Where-Object {
        $_.Extension -in @('.cs', '.razor', '.txt', '.config', '.xml', '.json') -and $_.FullName -notlike "*\bin\*" -and $_.FullName -notlike "*\obj\*"
    }
}

# Hauptfunktion, um die Textdateien zu erstellen
function CreateFileList {
    param ([string]$folder)

    $folder = $folder.Trim('"')
    Get-ChildItem -Path $folder -Directory | Where-Object {
        $_.FullName -notlike "*\obj*" -and $_.FullName -notlike "*\bin*" -and $_.FullName -notlike "*\abppkg*"
    } | ForEach-Object {
        $folderName = $_.Name
        $outputFilePath = Join-Path -Path $outputFolder -ChildPath ("$folderName.txt")

        $editableFiles = Get-EditableFiles -folder $_.FullName
        if ($editableFiles) {
            $outputContent = ""
            foreach ($file in $editableFiles) {
                $relativePath = $file.FullName.Replace($baseFolder, '').Trim('\')
                $outputContent += "$relativePath`n
`n" + (Get-Content $file.FullName -Raw) + "`n`n"
            }
            Set-Content -Path $outputFilePath -Value $outputContent
        }
    }
}

# Skript ausführen

CreateFileList -folder $baseFolder

Write-Host "Die Dateien wurden erfolgreich erstellt."