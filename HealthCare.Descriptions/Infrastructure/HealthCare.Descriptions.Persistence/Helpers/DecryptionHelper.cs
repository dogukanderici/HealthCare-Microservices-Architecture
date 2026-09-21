using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Settings;
using HealthCare.Descriptions.Application.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Persistence.Helpers
{
    public class DecryptionHelper : IDecryptionHelper
    {
        private readonly string _secretKey;

        public DecryptionHelper(IOptions<CursorTokenSettings> options)
        {
            _secretKey = options.Value.SecretKey;
        }

        public CryptionResponse<T> DecryptToken<T>(string encryptedToken)
        {
            try
            {
                CryptionResponse<string> cryptionResponse = DecryptTokenForString(encryptedToken);
                string jsonString = cryptionResponse.TokenPayload;

                // T tipindeki veriye dönüştürülür.
                return CryptionResponse<T>.Success(JsonConvert.DeserializeObject<T>(jsonString));
            }
            catch (Exception ex)
            {
                return CryptionResponse<T>.Fail(ex.Message);
            }
        }

        public CryptionResponse<string> DecryptTokenForString(string encryptedToken)
        {
            try
            {
                // Network'te taşınan Base64 formatındaki veri işlem yapılabilmesi için byte'a çevrilir.
                byte[] fullCipher = Convert.FromBase64String(encryptedToken); // fullCipher = [16 byte IV] + [Şifreli Veri]

                // Secret Key (encryptionKey) byte array'e çevrilir.
                byte[] keyBytes = Encoding.UTF8.GetBytes(_secretKey.PadRight(32).Substring(0, 32));

                using Aes aes = Aes.Create();

                // IV için 16 byte'lık boş dizi oluşturulur.
                byte[] iv = new byte[16];

                // fullCipher'dan 0. index'ten iv'ye yazılacak 16 byte uzunluğundaki veriyi boş diziye kopyalar.
                Array.Copy(fullCipher, 0, iv, 0, iv.Length);

                aes.Key = keyBytes;
                aes.IV = iv;

                // Şifreyi çözebilmek için nesne oluşturulur.
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                // RAM'de tutabilmek için alan açılır. Fakat fullCipher'ın tamamı değil IV dışındaki veriyi verir. 
                using MemoryStream ms = new MemoryStream(fullCipher, iv.Length, fullCipher.Length - iv.Length);

                // Write'ın tersi bir akış kurulur bu sefer okuma işlemi yapılır.
                using CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                using StreamReader sr = new StreamReader(cs);

                // Tüm veri sonuna kadar okunur. (Hata artık burada fırlatılmayacak)
                string jsonString = sr.ReadToEnd();

                // T tipindeki veriye dönüştürülür.
                return CryptionResponse<string>.Success(jsonString);
            }
            catch (Exception ex)
            {
                return CryptionResponse<string>.Success(ex.Message);
            }
        }
    }
}