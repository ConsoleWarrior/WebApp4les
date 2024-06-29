using System.Security.Cryptography;

namespace WebAppForDocker.RSATools
{
    public class RSAExtensions
    {
        public static RSA GetPrivateKey()
        {
            var key = File.ReadAllText(@"../WebAppForDocker/private_key.pem");
            var rsa = RSA.Create();

            rsa.ImportFromPem(key);

            return rsa;
        }

        public static RSA GetPublicKey()
        {
            var key = File.ReadAllText("../WebAppForDocker/public_key.pem");
            var rsa = RSA.Create();

            rsa.ImportFromPem(key);

            return rsa;
        }
    }
}
