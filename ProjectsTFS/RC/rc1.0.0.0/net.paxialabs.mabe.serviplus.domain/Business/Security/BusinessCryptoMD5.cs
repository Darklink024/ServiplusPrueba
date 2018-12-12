using System;
using System.Security.Cryptography;
using System.Text;

namespace net.paxialabs.mabe.serviplus.domain.Business.Security
{
    /// <summary>
    /// Clase para el encryptado de cadenas usando el algoritmo MD5 de .Net
    /// Miguel Ordoñez
    /// Noviembre de 2016
    /// </summary>
    internal class BusinessCryptoMD5
    {
        /// <summary>
        /// Llave patron de la que se formara la encriptacion
        /// </summary>
        public String KeyCrypto { get; internal set; }

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="key">cadena de caracteres con la llave de encriptacion/desencriptacion</param>
        public BusinessCryptoMD5(string key)
        {
            this.KeyCrypto = key;
        }

        /// <summary>
        /// Metodo para encriptar
        /// </summary>
        /// <param name="TextForCrypto">Cadena a encriptar</param>
        /// <returns>Regresa una cadena con el texto encriptado</returns>
        public String CryptoString(string TextForCrypto)
        {
            byte[] keyArray;
            byte[] ArrayForCrypto = UTF8Encoding.UTF8.GetBytes(TextForCrypto);

            MD5CryptoServiceProvider HashMD5 = new MD5CryptoServiceProvider();

            keyArray = HashMD5.ComputeHash(UTF8Encoding.UTF8.GetBytes(this.KeyCrypto));
            HashMD5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(ArrayForCrypto, 0, ArrayForCrypto.Length);
            tdes.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// Metodo para desencriptar
        /// </summary>
        /// <param name="TextForDeCrypto">Cadena a desencriptar</param>
        /// <returns>Regresa una cadena con el texto desencriptado</returns>
        public String DeCryptoString(string TextForDeCrypto)
        {
            byte[] keyArray;
            byte[] ArrayForDeCrypto = Convert.FromBase64String(TextForDeCrypto);

            MD5CryptoServiceProvider HashMD5 = new MD5CryptoServiceProvider();

            keyArray = HashMD5.ComputeHash(UTF8Encoding.UTF8.GetBytes(this.KeyCrypto));

            HashMD5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(ArrayForDeCrypto, 0, ArrayForDeCrypto.Length);
            tdes.Clear();

            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
