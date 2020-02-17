using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CC.Web.Utility
{
    public class EncryptHelper
    {
        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="sourceData">源数据</param>
        /// <param name="secret">key</param>
        /// <returns></returns>
        public static string EncryptWithSHA256(string sourceData,string key)
        {
            key = string.IsNullOrEmpty(key) ? "" : key;
            var encoding = Encoding.UTF8;
            var keyBytes = encoding.GetBytes(key);
            var dataBytes = encoding.GetBytes(sourceData);
            using (var hmac256 = new HMACSHA256(keyBytes))
            {
                var hashData = hmac256.ComputeHash(dataBytes);
                return Convert.ToBase64String(hashData); 
            }
        }

        /// <summary>
        /// 原始64位 SHA256
        /// </summary>
        /// <param name="data">待加密数据</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string EncryptWithSHA256Original(string data, string key)
        {
            key = string.IsNullOrEmpty(key) ? "" : key;
            var encoding = Encoding.UTF8;
            byte[] keyBytes = encoding.GetBytes(key);
            byte[] dataBytes = encoding.GetBytes(data);
            using (var hmac256 = new HMACSHA256(keyBytes))
            {
                byte[] hashData = hmac256.ComputeHash(dataBytes);
                return BitConverter.ToString(hashData).Replace("-", "").ToLower();
            }
        }
    }
}
