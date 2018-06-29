function Filter-String{
[cmdletbinding()]
    param(
        [Parameter(Position=0,Mandatory=$true,ValueFromPipeline=$true)]
        [string[]]$message
    )
    process{
        foreach($msg in $message){
            if($nugetApiKey){
                $msg = $msg.Replace($nugetApiKey,'REMOVED-FROM-LOG')
            }

            $msg
        }
    }
}

function Write-Message{
    [cmdletbinding()]
    param(
        [Parameter(Position=0,Mandatory=$true,ValueFromPipeline=$true)]
        [string[]]$message
    )
    process{
        Filter-String -message $message | Write-Verbose
    }
}

function Get-Nuget{
    [cmdletbinding()]
    param(
        $toolsDir = ("$env:LOCALAPPDATA\Telegraph\tools\"),
        $nugetDownloadUrl = 'https://dist.nuget.org/win-x86-commandline/latest/nuget.exe'
    )
    process{
        $nugetDestPath = Join-Path -Path $toolsDir -ChildPath nuget.exe

        if(!(Test-Path $nugetDestPath)){
            $nugetDir = ([System.IO.Path]::GetDirectoryName($nugetDestPath))
            if(!(Test-Path $nugetDir)){
                New-Item -Path $nugetDir -ItemType Directory | Out-Null
            }

            'Downloading nuget.exe' | Write-Message
            (New-Object System.Net.WebClient).DownloadFile($nugetDownloadUrl, $nugetDestPath)

            # double check that is was written to disk
            if(!(Test-Path $nugetDestPath)){
                throw 'Unable to download nuget.'
            }
        }

        # return the path of the file
        $nugetDestPath
    }
}


function PublishNuGetPackage{
    [cmdletbinding(SupportsShouldProcess=$true)]
    param(
        [Parameter(Mandatory=$true,ValueFromPipeline=$true)]
        [string[]]$nugetPackage,

        [Parameter(Mandatory=$true)]
        $nugetApiKey,

        [Parameter()]
        $source = 'https://dist.nuget.org/win-x86-commandline/latest/nuget.exe'
    )
    process{
        foreach($pkg in $nugetPackage){
            $pkgPath = (get-item $pkg).FullName
            $cmdArgs = @('push',$pkgPath,$nugetApiKey,'-source',$source,'-NonInteractive')
			$logfilepath = "$([System.IO.Path]::GetTempFileName()).log"

            'Publishing nuget package [{0}]' -f $pkgPath | Write-Message

            $filteredCmd = Filter-String ('Publishing nuget package with the following args: [nuget.exe {0}]' -f ($cmdArgs -join ' '))
            if($PSCmdlet.ShouldProcess($env:COMPUTERNAME, $filteredCmd)){
                &(Get-Nuget) $cmdArgs *> $logfilepath
                if($LASTEXITCODE -ne 0){
                    throw ('nuget.exe failed with the following error code [{0}]' -f $LASTEXITCODE)
                }
            }
        }
    }
}

if($env:APPVEYOR_REPO_BRANCH -eq "release"){
	[string]$nugetApiKey = ($env:NuGetApiKey)

    PublishNuGetPackage -nugetPackage ".\Telegraph\bin\Release\Telegraph*.nupkg" -nugetApiKey $nugetApiKey
} 
else {
    # Untested commit so don't publish to Nuget
}