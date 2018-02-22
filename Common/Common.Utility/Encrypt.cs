using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common.Utility
{
    public class Encrypt
    {
        private static string ASEPrivateKey = "00000";
        private static string ASEPublicKey = "11111";

        public static string AESEnCrypt(string string2EnCrypt)
        {
            //預設用公司電話當私鑰
            return AESEnCrypt(string2EnCrypt, ASEPrivateKey);
        }

        public static string AESEnCrypt(string string2EnCrypt, string privateKey)
        {
            RijndaelManaged AES;
            MD5CryptoServiceProvider MD5;

            AES = new RijndaelManaged();
            MD5 = new MD5CryptoServiceProvider();
            byte[] plainTextData = Encoding.Unicode.GetBytes(string2EnCrypt);
            byte[] keyData = MD5.ComputeHash(Encoding.Unicode.GetBytes(privateKey));
            //公鑰
            byte[] IVData = MD5.ComputeHash(Encoding.Unicode.GetBytes(ASEPublicKey));
            ICryptoTransform transform = AES.CreateEncryptor(keyData, IVData);
            byte[] outputData = transform.TransformFinalBlock(plainTextData, 0, plainTextData.Length);
            return Convert.ToBase64String(outputData);
        }

        public static string AESDecrypt(string string2DeCrypt)
        {
            //處理傳遞參數會發生 "+" 會被取代掉 *kent
            string2DeCrypt = string2DeCrypt.Replace(" ", "+");
            return AESDecrypt(Convert.FromBase64String(string2DeCrypt), ASEPrivateKey);
        }

        public static string AESDecrypt(string string2DeCrypt, string privateKey)
        {
            return AESDecrypt(Convert.FromBase64String(string2DeCrypt), privateKey);
        }

        private static string AESDecrypt(byte[] dnCrypt, string privateKey)
        {
            RijndaelManaged AES;
            MD5CryptoServiceProvider MD5;

            AES = new RijndaelManaged();
            MD5 = new MD5CryptoServiceProvider();
            byte[] keyData = MD5.ComputeHash(Encoding.Unicode.GetBytes(privateKey));
            byte[] IVData = MD5.ComputeHash(Encoding.Unicode.GetBytes(ASEPublicKey));
            ICryptoTransform transform = AES.CreateDecryptor(keyData, IVData);
            byte[] outputData = transform.TransformFinalBlock(dnCrypt, 0, dnCrypt.Length);
            return Encoding.Unicode.GetString(outputData);
        }

        public static string HttpEncoding(string string2EnCrypt)
        {
            return HttpUtility.HtmlEncode(string2EnCrypt);
        }

        public static string HttpDecoding(string string2EnCrypt)
        {
            return HttpUtility.HtmlDecode(string2EnCrypt);
        }

        public static string HttpEncodeDecode(string string2EnCrypt)
        {
            string2EnCrypt = HttpUtility.HtmlEncode(string2EnCrypt);
            return HttpUtility.HtmlDecode(string2EnCrypt);
        }

    }
}
