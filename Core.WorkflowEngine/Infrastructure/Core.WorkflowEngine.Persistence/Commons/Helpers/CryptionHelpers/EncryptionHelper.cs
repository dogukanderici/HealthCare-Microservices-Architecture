using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Core.WorkflowEngine.Persistence.Commons.Helpers.CryptionHelpers
{
    public class EncryptionHelper : IEncryptionHelper
    {
        private readonly string _secretKey;

        public EncryptionHelper(IOptions<CryptionOption> option)
        {
            _secretKey = option.Value.SecretKey;
        }

        public string EncryptToken<T>(T payload)
        {
            // Şifrelenecek veri string'e çevrilir.
            string jsonString = JsonConvert.SerializeObject(payload);

            // Secret Key değeri byte Array'e çevrilir.
            byte[] secretKeyByte = Encoding.UTF8.GetBytes(_secretKey.PadRight(32).Substring(0, 32));

            using Aes aes = Aes.Create();

            aes.Key = secretKeyByte;
            aes.GenerateIV(); // Her şiferleme için benzersiz Initialization Vector üretir.

            // Şifrelemeyi yapacak nesne secret key ve IV ile oluşturulur.
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            // Crypto sınıfı şifrelediği veriyi yazacak bir akış ister. Havada tutamaz.
            // Şirelenmiş veri RAM'e yazılır.
            using MemoryStream ms = new MemoryStream();

            // Şifrenin çözülebilmesi için verinin başına plain olarak yazılır.
            // DecryptionHelper ilk 16 byte'tan bunun IV olduğunu anlayacak.
            ms.Write(aes.IV, 0, aes.IV.Length);

            using CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (StreamWriter sw = new StreamWriter(cs))
            {
                sw.Write(jsonString);
            }
            ;

            // Verinin network'e taşınabilmesi için base64 formatına çevrilir.
            return Convert.ToBase64String(ms.ToArray());
        }
    }
}