﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Gwynwhyvaar.Utils.Shared.Extensions
{
    /// <summary>
    /// string extensions to handle various string manipulation tasks ..
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a new string containing the leftmost <paramref name="len"/> characters of this string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len">
        /// The number of characters to return. If greater than the length of the string, the entire string is returned.
        /// </param>
        /// <returns>A new string</returns>
        public static string Left(this string str, int len)
        {
            return str.Substring(0, Math.Min(len, str.Length));
        }

        /// <summary>
        /// Returns a new string containing the rightmost <paramref name="len"/> characters of this string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len">
        /// The number of characters to return. If greater than the length of the string, the entire string is returned.
        /// </param>
        /// <returns>A new string</returns>
        public static string Right(this string str, int len)
        {
            return str.Substring(Math.Max(str.Length - len, 0));
        }

        /// <summary>
        /// Converts a comma separated string of ints e.g. 1,2,3,4,5 to a generic List of ints
        /// </summary>
        /// <param name="str">A comma separated list of ints</param>
        /// <returns>List of ints in the given string</returns>
        public static List<int> ToIntArray(this string str)
        {
            var splitted = System.Text.RegularExpressions.Regex.Split(str, ",");
            return Array.ConvertAll<string, int>(splitted,
                delegate(string inputParameter) { return int.Parse(inputParameter); }).ToList();
        }

        /// <summary>
        /// encrypts a given string using DES (Data Encryption Standard) encryption algorithm
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(this string text, string key)
        {
            var rgbKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            var rgbIV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            var bytes = Encoding.UTF8.GetBytes(text);

            var provider = new DESCryptoServiceProvider();
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, provider.CreateEncryptor(rgbKey, rgbIV),
                CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        /// <summary>
        /// decrypts a given string using DES (Data Encryption Standard) encryption algorithm
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(this string text, string key)
        {
            var rgbKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            var rgbIV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            var bytes = Convert.FromBase64String(text);

            var provider = new DESCryptoServiceProvider();
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, provider.CreateDecryptor(rgbKey, rgbIV),
                CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }

        /// <summary>
        /// gets a .net object from a supplied json string ..
        /// </summary>
        /// <param name="json"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FromJson<T>(this string json)
        {
            if (string.IsNullOrEmpty(json)) return default;
            return JsonSerializer.Deserialize<T>(json);
        }

        /// <summary>
        /// gets a json string from a .net object ..
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="serializerOptions"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ToJson<T>(T obj, JsonSerializerOptions serializerOptions = null)
        {
            if (serializerOptions == null)
                return JsonSerializer.Serialize(obj);
            else
                return JsonSerializer.Serialize(obj, serializerOptions);
        }
    }
}