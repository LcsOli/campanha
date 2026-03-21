using System.Net;
using System.Text;
using Campaign.API.Configuration.Exceptions;

namespace Campaign.API.Services.SecretKey
{
    public static class SecretKeyService
    {
        public static string Get()
        {
            var secretkey = Environment.GetEnvironmentVariable("COMPAIGN_SECRET_KEY");

            if (string.IsNullOrEmpty(secretkey))
                throw new CompaignException(HttpStatusCode.InternalServerError, "Variável de ambiente COMPAIGN_SECRET_KEY não encontrada.");

            return secretkey;
        }

        public static byte[] GetBytes()
        {
            return Encoding.UTF8.GetBytes(Get());
        }
    }
}
