function GetChangerPath() {
    $fileName = 'KeyblangChanger.exe'
    $path = Join-Path $PSScriptRoot $fileName

    if ([System.IO.File]::Exists($path)) {
        return $path
    }

    Add-Type -AssemblyName System.Windows.Forms
    $FileBrowser = New-Object System.Windows.Forms.OpenFileDialog -Property @{
        InitialDirectory = "$PSScriptRoot"
        Filter = "$fileName|$fileName"
        RestoreDirectory = $true
    }
    $null = $FileBrowser.ShowDialog()

    if ([System.IO.File]::Exists($FileBrowser.FileName) -and $FileBrowser.FileName.EndsWith($fileName)) {
        return $FileBrowser.FileName
    }
    Exit 0
}

function CheckElevation() {
    Write-Host " Elevation... " -NoNewline
    If (-NOT ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator))
    {
        Write-Host " Restarting in admin mode..."
        # Relaunch as an elevated process:
        Start-Process powershell.exe "-File",('"{0}"' -f $PSCommandPath) -Verb RunAs -WorkingDirectory $(get-location).path
        exit 0
    } else {
        Write-Host "OK"
    }
}

function ClearExistingTasks() {
    $tasks = Get-ScheduledTask Keyblangchanger_*

    if ($tasks.Count -ne 0) {
        $taskNames = ($tasks | % { $_.TaskName })
        $decision = $Host.UI.PromptForChoice("", "This will remove existing tasks and create new ones. Is it okay? Tasks that will be removed: `n$taskNames", ('&Yes', '&No'), 1)
        if ($decision -ne 0) {
            Exit 0
        }

        Unregister-ScheduledTask -TaskName $taskNames -Confirm:$false
        Write-Host "Removed existing $($taskNames)"
    }
}

function CreateTask($lang, $appPath) {
    $action = New-ScheduledTaskAction -Execute  $appPath -Argument "set $lang"
    $user = $(whoami)
    $principal = New-ScheduledTaskPrincipal -RunLevel Highest -UserId $user
    $settings = New-ScheduledTaskSettingsSet
    $task = New-ScheduledTask -Action $action -Principal $principal -Settings $settings -Description "Changes current window language to $lang. (Runs as admin)"
    $name = "Keyblangchanger_$lang"
    Register-ScheduledTask $name -InputObject $task
}

CheckElevation
ClearExistingTasks
$appPath = GetChangerPath
Write-Host "Using $appPath"
$langs = Read-Host "Enter space-separated keybaoard layout codes"

foreach ($lang in $langs.Split(" "))
{
    CreateTask $lang $appPath
}

Write-Host -NoNewLine 'Press any key to continue...';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');