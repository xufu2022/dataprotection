$date_now = Get-Date
$extended_date = $date_now.AddYears(3)
$cert = New-SelfSignedCertificate -certstorelocation cert:\localmachine\my -dnsname ImmDataProtection -notafter $extended_date
$pwd = ConvertTo-SecureString -String 'P@ssword1234' -Force -AsPlainText
$path = 'cert:\localMachine\my\' + $cert.thumbprint
Export-PfxCertificate -cert $path -FilePath ImmDataProtection.pfx -Password $pwd

Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass