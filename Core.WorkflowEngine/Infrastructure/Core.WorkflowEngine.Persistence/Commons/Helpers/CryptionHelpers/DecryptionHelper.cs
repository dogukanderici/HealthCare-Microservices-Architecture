using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data.Entity.Core.Mapping;
using System.Security.Cryptography;
using System.Text;

namespace Core.WorkflowEngine.Persistence.Commons.Helpers.CryptionHelpers
{
    public class DecryptionHelper : IDecryptionHelper
    {
        private readonly string _secretKey;

        public DecryptionHelper(IOptions<CryptionOption> option)
        {
            _secretKey = option.Value.SecretKey;
        }

        public CryptionResponse<T> DecryptToken<T>(string encryptedToken)
        {
            CryptionResponse<string> cryptionResponse = DecryptTokenForString(encryptedToken);

            if (!cryptionResponse.IsSuccess)
                return CryptionResponse<T>.Fail(cryptionResponse.Message);

            return CryptionResponse<T>.Success(JsonConvert.DeserializeObject<T>(cryptionResponse.TokenPayload));
        }

        public CryptionResponse<string> DecryptTokenForString(string encryptedToken)
        {
            try
            {
                // Network'e taşınan base64 formatındaki veri işlem yapılabilmesi için byte array'e çevrilir.
                byte[] fullCipher = Convert.FromBase64String(encryptedToken); // fullcipher => 16 byte IV + şifreli veri

                // Secret Key byte arra'e çevrilir.
                byte[] secretKeyByte = Encoding.UTF8.GetBytes(_secretKey.PadRight(32).Substring(0, 32));

                using Aes aes = Aes.Create();

                // IV için 16 byte'lık boş byte array oluşturulur.
                byte[] iv = new byte[16];

                // fullcipher'ın 0. index'inden iv'ye yazılacak 16 byte uzunluğundeki veriyi diziye kopyalar.
                Array.Copy(fullCipher, 0, iv, 0, iv.Length);

                aes.Key = secretKeyByte;
                aes.IV = iv;

                // Şifreyi çözebilmek için nesne oluşturulur.
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                // Şifreleme işleminin yapılabilmesi için bir akışa (stream) yazılması gerekir.
                // RAM'de veriyi tutabilmek için alan açılır. RAM'de IV dışındaki veri tutulur.
                using MemoryStream ms = new MemoryStream(fullCipher, iv.Length, fullCipher.Length - iv.Length);
                using CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                using StreamReader sr = new StreamReader(cs);

                // Tüm veri sonuna kadar okunur.
                string jsonString = sr.ReadToEnd();

                return CryptionResponse<string>.Success(jsonString);
            }
            catch (Exception ex)
            {
                return CryptionResponse<string>.Fail(ex.Message);
            }
        }
    }
}