using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddProduct
{
     class Cryptology
    {
         public static string Encryption(string text,int key)
        //Statik metotlar, sınıfın bir örneği oluşturulmadan doğrudan sınıf üzerinden çağrılabilir. 
        {
            char[] x = text.ToCharArray();//Bu satır, text parametresinden gelen metni karakter dizisine dönüştürür
                                          //ve x değişkenine atar.
                                          //Her bir karakter, dizinin bir elemanı olarak saklanır.
            
            string encryptedText = null;//Bu satır, encryptedText adında bir değişken tanımlar
                                        //ve değerini null olarak başlatır.
                                        //Bu değişken, şifrelenmiş metni tutacaktır.

            foreach (char item in x )
                //Bu satır, x karakter dizisindeki her bir karakter için bir döngü başlatır.
                //Döngü her bir karakteri item değişkenine atar
                //ve döngü gövdesindeki kodları çalıştırır.
            {
                encryptedText += Convert.ToChar(item + key);//Bu satır, şifreleme işlemini gerçekleştirir.
                                                            //item karakterinin değerine key değeri eklenir
                                                            //ve bu toplam Convert.ToChar() metodu ile karaktere dönüştürülür.
                                                            //Dönüştürülen karakter, encryptedText değişkenine eklenir.
                                                            //Bu işlem, metindeki her bir karakter için tekrarlanır.

            }
            return encryptedText;//Bu satır, Encryption metodunun sonunda,
                                 //şifrelenmiş metin olan encryptedText değişkeninin değerini geri döndürür.
        }
        public static string Decryption(string text,int key )
        {
            char[] x = text.ToCharArray();
            string decryptedText = null;

            foreach (char item in x)
            {
                decryptedText += Convert.ToChar(item-key);//Bu satır, şifre çözme işlemini gerçekleştirir.
                                                          //item karakterinin değerinden key değeri çıkarılır
                                                          //ve bu fark Convert.ToChar() metodu ile karaktere dönüştürülür.
                                                          //Dönüştürülen karakter, decryptedText değişkenine eklenir.
                                                          //Bu işlem, metindeki her bir karakter için tekrarlanır.
            }
            return decryptedText;
        }
        /*
         Özetle, Decryption metodu, şifrelenmiş metni ve şifrelemede kullanılan 
        key değerini alarak metnin şifresini çözer ve şifresiz metni geri döndürür.*/
    }
}
