using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var directoryInfo = new DirectoryInfo(@"C:\dev\05\proj\dataprotection\src\ImmDataProtection\ImmDataProtection\shared\directory");

//builder.Services.AddDataProtection(opts=>opts.ApplicationDiscriminator="ImmDataProtection")
//    .PersistKeysToFileSystem(directoryInfo)
//    .ProtectKeysWithDpapiNG();

builder.Services.AddDataProtection(opts => opts.ApplicationDiscriminator = "ImmDataProtection")
    .PersistKeysToFileSystem(directoryInfo)
    .ProtectKeysWithCertificate(new X509Certificate2(@"C:\dev\05\proj\dataprotection\src\ImmDataProtection\ImmDataProtection\shared\ImmDataProtection.pfx", "P@ssword1234"));


//services.AddDataProtection(opts =>
//        opts.ApplicationDiscriminator = _configuration["DataProtection:DiscriminatorName"])
//    .SetApplicationName(_configuration["DataProtection:ApplicationName"])
//    .PersistKeysToFileSystem(new DirectoryInfo(_configuration["DataProtection:PersistedKeyPath"]))
//    .ProtectKeysWithCertificate( // certificate details
//        new X509Certificate2(
//            _configuration["DataProtection:CertificatePath"],
//            _configuration["DataProtection:CertificatePassword"])
//    );

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
