If (Test-Path "/artifacts"){
    Write-Output "Remvoing old artifacts"
	Remove-Item "/artifacts"
} else {
    Write-Output "The artifact folder does not exist. Continuing"
}

dotnet restore
dotnet pack ./Akeneo/Akeneo.csproj -c Release -o ../artifacts