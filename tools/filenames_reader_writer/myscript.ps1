$basePath = (Get-Location).Path
$outputFile = "OrdnerUndDateienUebersicht.txt"

# Funktion, um kompakte Übersicht zu erstellen
function Get-DirectoryStructure {
    param ([string]$path)

    # Rekursiv durchgehen, aber 'obj' und 'bin' Ordner sowie bestimmte Dateien ignorieren
    Get-ChildItem -Path $path -Recurse -Directory | Where-Object {
        $_.FullName -notlike "*\obj*" -and $_.FullName -notlike "*\bin*" -and $_.FullName -notlike "*\abppkg*"
    } | ForEach-Object {
        # Ordnerpfade
        $folderPath = $_.FullName.Substring($basePath.Length + 1)
        Write-Output "`n\$folderPath"
        
        # Dateien im aktuellen Ordner anzeigen, 'obj', 'bin', 'FodyWeavers', und abppkg Dateien ignorieren
        Get-ChildItem -Path $_.FullName -File | Where-Object {
            $_.Name -notlike "FodyWeavers*" `
            -and $_.Name -notlike "*.abppkg" `
            -and $_.Name -notlike "*.abppkg.analyze.json"
        } | ForEach-Object {
            Write-Output "    Datei: $($_.Name)"
        }
    }
}

# Übersicht erstellen und in eine Textdatei speichern
Get-DirectoryStructure $basePath | Out-File -FilePath $outputFile -Encoding UTF8

Write-Output "Die Übersicht wurde in der Datei $outputFile gespeichert."
