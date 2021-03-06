param (
	[Parameter(Mandatory=$true)]
	[ValidatePattern("\d\.\d\.\d\.\d")]
	[string]
	$ReleaseVersionNumber
)

$ErrorActionPreference = "Stop"

$PSScriptFilePath = (Get-Item $MyInvocation.MyCommand.Path).FullName
$SolutionRoot = Split-Path -Path $PSScriptFilePath -Parent
$NugetPath = Join-Path $SolutionRoot 'Solution Items\nuget.exe'

# Build the NuGet package
$ProjectPath = Join-Path -Path $SolutionRoot -ChildPath "Ninefold.API\Ninefold.API.csproj"
& $NugetPath pack $ProjectPath -Prop Configuration=Release -OutputDirectory $SolutionRoot
if (-not $?)
{
	throw "The NuGet process returned an error code."
}

# Upload the NuGet package
$NuPkgPath = Join-Path -Path $SolutionRoot -ChildPath "Ninefold.$ReleaseVersionNumber.nupkg"
& $NugetPath push $NuPkgPath
if (-not $?)
{
	throw "The NuGet process returned an error code."
} 