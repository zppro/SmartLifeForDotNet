using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Security.Cryptography;
using System.Reflection;
using log4net;
using System.ComponentModel;

namespace SmartLife.Client.Sms
{
    public class BaseUtility
    {
        //记录日志
        public static readonly ILog LogError = LogManager.GetLogger("Sms");
        //默认密钥向量
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        //默认加解密密钥
        private static string cryptKey = "leblue13";

        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(cryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
               
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(cryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        /// <summary>
        /// 分割字符转成键值对
        /// </summary>
        /// <param name="strings">被分割字符</param>
        /// <param name="separators">分割符号</param>
        /// <returns></returns>
        public static Dictionary<string, string> StrToDictionary(string strings, char separators)
        {
            Dictionary<string, string> retdictionary = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(strings)) return retdictionary;

            foreach (var item in strings.Split(separators))
            {
                if (string.IsNullOrEmpty(item)) continue;
                int mark = item.IndexOf("=");
                retdictionary.Add(item.Substring(0, mark), item.Substring(mark + 1, item.Length - mark - 1));
            }

            return retdictionary;
        }

        //获取枚举的文本值和描述
        public static Dictionary<string, string> GetEnumDesc(Enum EnumType)
        {
            Dictionary<string, string> smsErrorDic = new Dictionary<string,string>();
            if (EnumType == null)  LogError.Error(new ArgumentNullException("EnumType"));
            if (!EnumType.GetType().IsEnum)  LogError.Error("参数类型不正确"); 

            FieldInfo[] fieldinfo = EnumType.GetType().GetFields();
            foreach (FieldInfo item in fieldinfo)
            {
                if (item.IsSpecialName) continue;
                Object[] obj = item.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (obj != null && obj.Length > 0)
                {
                    DescriptionAttribute da = (DescriptionAttribute)obj[0];
                    smsErrorDic.Add(item.GetRawConstantValue().ToString(), da.Description);
                }
            }

            if (smsErrorDic == null || smsErrorDic.Count <= 0) return new Dictionary<string, string>();

            return smsErrorDic;
        }

        //
        public static System.Collections.ArrayList StrSplitToArray(string content)
        {
            System.Collections.ArrayList list = new System.Collections.ArrayList();

            string[] arr = content.Split('/');
            if (arr.Length > 0)
            {
                list.AddRange(arr);
            }
            return list;
        }
    }
}
