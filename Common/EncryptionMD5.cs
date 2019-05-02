using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Common
{
    /// <summary>
    /// MD5
    /// 单向加密
    /// </summary>
    public static class EncryptionMD5
    {
        /// <summary>
        /// 获得一个字符串的加密密文
        /// 此密文为单向加密，即不可逆(解密)密文
        /// </summary>
        /// <param name="plainText">待加密明文</param>
        /// <returns>已加密密文</returns>
        public static string EncryptString(string plainText)
        {
            return EncryptStringMD5(plainText);
        }
        /// <summary>
        /// 获得一个字符串的加密密文
        /// 此密文为单向加密，即不可逆(解密)密文
        /// </summary>
        /// <param name="plainText">待加密明文</param>
        /// <returns>已加密密文</returns>
        public static string EncryptStringMD5(string plainText)
        {
            string encryptText = "";
            if (string.IsNullOrEmpty(plainText)) return encryptText;
            encryptText = FormsAuthentication.HashPasswordForStoringInConfigFile(plainText, "md5");
            return encryptText;
        }
        /// <summary>
        /// 判断明文与密文是否相符
        /// </summary>
        /// <param name="plainText">待检查的明文</param>
        /// <param name="encryptText">待检查的密文</param>
        /// <returns>bool</returns>
        public static bool EqualEncryptString(string plainText, string encryptText)
        {
            return EqualEncryptStringMD5(plainText, encryptText);
        }
        /// <summary>
        /// 判断明文与密文是否相符
        /// </summary>
        /// <param name="plainText">待检查的明文</param>
        /// <param name="encryptText">待检查的密文</param>
        /// <returns>bool</returns>
        public static bool EqualEncryptStringMD5(string plainText, string encryptText)
        {
            bool result = false;
            if (string.IsNullOrEmpty(plainText) || string.IsNullOrEmpty(encryptText))
                return result;
            result = EncryptStringMD5(plainText).Equals(encryptText);
            return result;
        }
    }
}
