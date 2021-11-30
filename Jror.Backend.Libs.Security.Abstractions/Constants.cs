using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Jror.Backend.Libs.Security.Abstractions
{
    public static class Constants
    {
        public const string PrivateKey =
            "C+9,8wJP.z@-%4D$yLc0Lp1Q+7!IVM-m4L/xkVLKBHi7X6-epWC$iD8.htLa@@ZJQgy-KruYalUPbWhZ!M**0CLQ3Ii$&*yvnrTRiOKhta#3ZXvhtH%QHR7*rlEt";

        public static byte[] BytesPrivateKey => Encoding.ASCII.GetBytes(PrivateKey);

        public static TokenValidationParameters TokenValidationParameters =>
            new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(BytesPrivateKey),
                ValidateIssuer = false,
                ValidateAudience = false
            };
    }
}