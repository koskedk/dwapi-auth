using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Dwapi.Auth
{
    public static class SigninKeyExtension
    {
        public static void AddCertificateFromFile(this IIdentityServerBuilder builder, IConfiguration options)
        {
            var keyFilePath = options.GetSection("SigninKeyCredentials:KeyFilePath").Value;
            var keyFilePassword = options.GetSection("SigninKeyCredentials:KeyFilePassword").Value;

            if (File.Exists(keyFilePath))
            {
                //logger.LogDebug($"SigninCredentialExtension adding key from file {keyFilePath}");

                // You can simply add this line in the Startup.cs if you don't want an extension.
                // This is neater though ;)
                builder.AddSigningCredential(new X509Certificate2(keyFilePath, keyFilePassword));
            }
        }
    }
}
