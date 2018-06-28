if($env:APPVEYOR_REPO_BRANCH -eq "release"){
    PublishNuGetPackage -nugetPackage Telegraph -nugetApiKey ApiKeyGoesHere
} 
else {
    # Untested commit so don't publish to Nuget yet
}


function Get-Nuget{
    [cmdletbinding()]
    param(
        $toolsDir = ("$env:LOCALAPPDATA\Ligershark\tools\"),
        $nugetDownloadUrl = 'http://nuget.org/nuget.exe'
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
        $source = 'https://www.nuget.org/api/v2/package'
    )
    process{
        foreach($pkg in $nugetPackage){
            $pkgPath = (get-item $pkg).FullName
            $cmdArgs = @('push',$pkgPath,$nugetApiKey,'-source',$source,'-NonInteractive')

            'Publishing nuget package [{0}]' -f $pkgPath | Write-Message

            $filteredCmd = Filter-String ('Publishing nuget package with the following args: [nuget.exe {0}]' -f ($cmdArgs -join ' '))
            if($PSCmdlet.ShouldProcess($env:COMPUTERNAME, $filteredCmd)){
                &(Get-Nuget) $cmdArgs
                if($LASTEXITCODE -ne 0){
                    throw ('nuget.exe failed with the following error code [{0}]' -f $LASTEXITCODE)
                }
            }
        }
    }
}