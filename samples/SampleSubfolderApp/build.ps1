<#
  Example Prism.Avalonia app with the DLLs in a subfolder and .exe in the root.
  Idea inspired by dnSpyEx (https://github.com/dnSpyEx/dnSpy/)
#>
param(
	[ValidateSet("all","netframework","net-x86","net-x64")]
	[string]$buildtfm = 'all',
	[switch]$NoMsbuild
	)

$ErrorActionPreference = 'Stop'

$netframework_tfm = 'net48'
$net_tfm = 'net6.0-windows'
$configuration = 'Debug'
$net_baseoutput = "output\$configuration\net6.0"
$apphostpatcher_dir = "AppPatcher"

function Build-Net
{
	param([string]$arch)

	Write-Host "Building .NET $arch binaries"

  $appName = "SampleSubfolderApp.exe"

	$rid = "win-$arch"
	$outdir = "$net_baseoutput\$net_tfm\$rid"
	$publishDir = "$outdir\publish"

	if ($NoMsbuild)
  {
		dotnet publish -v:m -c $configuration -f $net_tfm -r $rid --self-contained
		if ($LASTEXITCODE) { exit $LASTEXITCODE }
	}
	else
  {
		msbuild -v:m -m -restore -t:Publish -p:Configuration=$configuration -p:TargetFramework=$net_tfm -p:RuntimeIdentifier=$rid -p:SelfContained=True
		if ($LASTEXITCODE) { exit $LASTEXITCODE }
	}

	# move all files to a bin sub dir but keep the exe apphosts
	$tmpbin = 'tmpbin'
	Rename-Item $publishDir $tmpbin
	New-Item -ItemType Directory $publishDir > $null
	Move-Item $outdir\$tmpbin $publishDir
	Rename-Item $publishDir\$tmpbin bin

	foreach ($exe in $appName, "${appName}.Console.exe")
  {
		Move-Item $publishDir\bin\$exe $publishDir
		& $apphostpatcher_dir\bin\$configuration\$netframework_tfm\AppHostPatcher.exe $publishDir\$exe -d bin
		if ($LASTEXITCODE) { exit $LASTEXITCODE }
	}
}


$buildNet	 = $buildtfm -eq 'all' -or $buildtfm -eq 'netframework'
$buildNetX86 = $buildtfm -eq 'all' -or $buildtfm -eq 'net-x86'
$buildNetX64 = $buildtfm -eq 'all' -or $buildtfm -eq 'net-x64'

if ($buildNetX86 -or $buildNetX64)
{
	if ($NoMsbuild)
  {
		dotnet build -v:m -c $configuration -f $netframework_tfm $apphostpatcher_dir\AppHostPatcher.csproj
		if ($LASTEXITCODE) { exit $LASTEXITCODE }
	}
	else
  {
		msbuild -v:m -m -restore -t:Build -p:Configuration=$configuration -p:TargetFramework=$netframework_tfm $apphostpatcher_dir\AppHostPatcher.csproj
		if ($LASTEXITCODE) { exit $LASTEXITCODE }
	}
}

if ($buildNetX86) {
	Build-Net x86
}

if ($buildNetX64) {
	Build-Net x64
}