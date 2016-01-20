param(
	[string]$NUnitRunner, [string]$inputfiles
)

#$NUnitRunner = ".\packages\NUnit.Console.3.0.0\tools\nunit3-console.exe";

#$inputfiles = "SauceLab\bin\Release\SauceLab.dll";

# get the directory of this script file
$currentDirectory = [IO.Path]::GetDirectoryName($MyInvocation.MyCommand.Path)

# get the full path and file name of the App.config file in the same directory as this script
$appConfigFile = [IO.Path]::Combine($currentDirectory, 'SauceLab.dll.config')

# initialize the xml object
$appConfig = New-Object XML

# load the config file as an xml object
$appConfig.Load($appConfigFile)
Write-Host "Starting Tests on Browsers....."
Write-Host "  Sauce Config File = " $appConfigFile;

# get browser lists from config
$browserlist = $appConfig.SelectNodes('//add[@key="BrowserList"]/@value')[0].'#text';
Write-Host "  Browser List = " $browserlist;

$browserlist.Split('|') | ForEach { 
		$browser = "$_";
		Write-Host "    Starting Tests for Browser = " $browser
		$key = "//add[@key='$browser']/@value"
	
		# get the Browser Configuration List
		$browserConfigList = $appConfig.SelectNodes($key)[0].'#text';
		Write-Host "      Browser Configuration = " $browserConfigList

		$browserConfigList.Split('|') | foreach { 
				$browserConfig = "$_";
				$config = $browserConfig.Split(',');
				
				#read Version & platform-os
				$version = "$($config[0])"; # Read Version
				$os = "$($config[1])"; # Read OS
				
				#Update configurtaion in AppConfig
				$appConfig.SelectNodes('//add[@key="TestBrowser"]/@value')[0].'#text' = "$browser";
				$appConfig.SelectNodes('//add[@key="TestVersion"]/@value')[0].'#text' = "$version";
				$appConfig.SelectNodes('//add[@key="TestOS"]/@value')[0].'#text' = "$os";
				
				#save the updated config file
				$appConfig.Save($appConfigFile)
				Write-Host "      Invoking Sauce for Configuration = Browser:$browser, Version:$version, Platform:$os";
				#& "$NUnitRunner" "SauceLab\bin\Release\SauceLab.dll";
				& "$NUnitRunner" "$inputfiles"
		}
	}
	Write-Host "End of Tests on Browsers....."
