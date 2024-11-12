$outputFolder = $PSScriptRoot
$groupedFilePrefix = "Tiknas_Grouped"
$maxFileSizeMB = 9.9

# Funktion, um Dateien in Gruppen zusammenzuführen
function GroupFiles {
    $groupIndex = 1
    $groupFilePath = Join-Path -Path $outputFolder -ChildPath ("$groupedFilePrefix$groupIndex.txt")
    $currentSizeMB = 0

    # Sammle nur die Textdateien im aktuellen Ordner (wo das Skript liegt)
    Get-ChildItem -Path $outputFolder -Filter "*.txt" | Where-Object { $_.Name -notlike "$groupedFilePrefix*" } | ForEach-Object {
        $fileSizeMB = [math]::Round((Get-Item $_.FullName).Length / 1MB, 2)
        if (($currentSizeMB + $fileSizeMB) -le $maxFileSizeMB) {
            Add-Content -Path $groupFilePath -Value (Get-Content $_.FullName -Raw)
            $currentSizeMB += $fileSizeMB
            Remove-Item -Path $_.FullName
        } else {
            $groupIndex++
            $groupFilePath = Join-Path -Path $outputFolder -ChildPath ("$groupedFilePrefix$groupIndex.txt")
            $currentSizeMB = $fileSizeMB
            Set-Content -Path $groupFilePath -Value (Get-Content $_.FullName -Raw)
            Remove-Item -Path $_.FullName
        }
    }
}

# Skript ausführen
GroupFiles

Write-Host "Die Textdateien wurden erfolgreich gruppiert."