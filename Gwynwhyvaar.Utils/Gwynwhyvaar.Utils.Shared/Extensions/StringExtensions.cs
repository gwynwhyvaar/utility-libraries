using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

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
            return JsonSerializer.Serialize(obj, serializerOptions);
        }

        /// <summary>
        ///  generic method  - converts a <T> object into an xml string
        /// </summary>
        /// <param name="o">object to convert to xml string</param>
        /// <param name="removeHeaderDeclaration">flag to determine if xml header tag should be added to output</param>
        /// <typeparam name="T">the type of the object to convert</typeparam>
        /// <returns>xml out put string of conversion</returns>
        public static string ToXml<T>(this T o, bool removeHeaderDeclaration = false)
        {
            var sw = new StringWriter();
            XmlTextWriter tw = null;
            try
            {
                var serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, o);
            }
            catch (Exception ex)
            {
                Trace.TraceInformation("Exception {0}", ex.ToString());
            }
            finally
            {
                sw.Close();
                if (tw != null) tw.Close();
            }

            if (!string.IsNullOrEmpty(sw.ToString()) && removeHeaderDeclaration)
                return sw.ToString().Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            return sw.ToString();
        }

        /// <summary>
        /// convert an xml string to an object
        /// </summary>
        /// <param name="xml"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FromXml<T>(this string xml)
        {
            StringReader strReader = null;
            XmlSerializer serializer;
            XmlTextReader xmlReader = null;
            object obj = null;
            try
            {
                strReader = new StringReader(xml);
                serializer = new XmlSerializer(typeof(T));
                xmlReader = new XmlTextReader(strReader);
                obj = serializer.Deserialize(xmlReader);
            }
            catch (Exception ex)
            {
                Trace.TraceInformation("Error {0} Converting Xml string to {1}", ex.Message, typeof(T).FullName);
            }
            finally
            {
                if (xmlReader != null) xmlReader.Close();
                if (strReader != null) strReader.Close();
            }

            if (obj is T) return (T)obj;
            try
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            catch (InvalidCastException)
            {
                return default;
            }
        }
    }
}