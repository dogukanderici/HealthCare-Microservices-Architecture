using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace HealthCare.Descriptions.Application.Common.Helpers
{
    public static class DecryptionHelper
    {
        public static T DecryptToken<T>(string encryptedToken, string encryptionKey)
        {
            // Network'te taşınan Base63 formatındaki veri işlem yapılabilmesi için byte'a çevrilir.
            byte[] fullCipher = Convert.FromBase64String(encryptionKey); // fullCipher = [16 byte IV] + [Şifreli Veri]

            // Secret Key byte array'e çevrilir.
            byte[] keyBytes = Encoding.UTF8.GetBytes(encryptedToken.PadRight(32).Substring(0, 32));

            using Aes aes = Aes.Create();

            // IV için 16 byet'lık boş dizi oluşturulur.
            byte[] iv = new byte[16];

            // fullCipher'dan 0. index'ten iv'ye yazılacak 16 byte uzunluğundaki veriyi boş diziye kopyalar.
            Array.Copy(fullCipher, 0, iv, 0, iv.Length);

            aes.Key = keyBytes;
            aes.IV = iv;

            // Şifreyi çözebilmek için nesne oluşturulur.
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            // RAM'de tutabilmek için alan açılır. Fakat fullCipher'ın tammamı değil IV dışındaki veriyi verir. 
            using MemoryStream ms = new MemoryStream(fullCipher, iv.Length, fullCipher.Length - iv.Length);

            // Write'ın tersi bir akış kurulur bu sefer okuma işlemi yapılır.
            using CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using StreamReader sr = new StreamReader(cs);

            // Tüm veri sonuna kadar okunur.
            string jsonString = sr.ReadToEnd();

            // T tipindeki veriye dönüştürülür.
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}