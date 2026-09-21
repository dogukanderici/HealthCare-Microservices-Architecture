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
    public class EncryptionHelper : IEncryptionHelper
    {
        private readonly string _secretKey;
        public EncryptionHelper(IOptions<CursorTokenSettings> options)
        {
            _secretKey = options.Value.SecretKey;
        }

        public string EncryptToken<T>(T payload)
        {
            // Gelen payload string'e çevrilir.
            string jsonString = JsonConvert.SerializeObject(payload);

            // Secret Key değeri byte array'e çevirilir.
            // AES-256 için 32 byte uzunluğunda key gerekir. Bu key 32 karakterden kısaysa PadRight() ile sona boşluk eklenir, uzun ise Substring() ile 32 karakteri alınır.
            byte[] keyBytes = Encoding.UTF8.GetBytes(_secretKey.PadRight(32).Substring(0, 32));

            using Aes aes = Aes.Create();
            aes.Key = keyBytes;
            aes.GenerateIV(); // Her şifreleme için 16 byte'lık beznersiz IV (Initialization Vector) üretir.

            // Secret Key ve IV ile şifreleme işlemini yapacak nesen oluşturulur.
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            // Geçici olarak tutmak için RAM'de yer açılır.
            // Crypto sınıfı şifrelediği veriyi yazabileceği bir hedef ister havada tutamaz.
            using MemoryStream ms = new MemoryStream();

            // Şifrenin geri çözülebilmesi için verinin başına plain olarak yazılır.
            // DecryptionHelper ilk 16 beyte'tan bunun IV olduğunu anlayacak.
            // using block'ları biitnce GC temizliği yapar ve arkada iz bırakmaz.
            // RAM'e yazmak disk veya ağa yazmaktan daha ucuz olduğundan RAM'e yazılır. (Amaç kalıcı olarak tutmak değil.)
            ms.Write(aes.IV, 0, aes.IV.Length);

            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (StreamWriter sw = new StreamWriter(cs))
            {
                sw.Write(jsonString);
            }

            // Verinin network'te taşınabilmesi için Base64 formatına çevrilir.
            return Convert.ToBase64String(ms.ToArray());
        }
    }
}