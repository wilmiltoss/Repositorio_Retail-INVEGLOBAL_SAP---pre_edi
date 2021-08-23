using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CdgUtiles.Util
{
    /// <summary>
    /// Clase de utilería para codificacion de cadenas de caracteres
    /// </summary>
    public static class Codificaciones
    {
        /// <summary>
        /// Codifica la cadena parametro a base64
        /// </summary>
        /// <param name="aCadenaBytes">Cadena de bytes a codificar</param>
        /// <returns>Cadena Base64</returns>
        public static string Get_base64_encode(byte[] aCadenaBytes)
        {
            //encbuff = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(aCadenaBytes);
        }

        /// <summary>
        /// decodifica la cadena parametro base64 a utf8
        /// </summary>
        /// <param name="str">Cadena Base64 a decodificar</param>
        /// <returns>Cadena ASCII</returns>
        public static string Get_base64_decode(string str)
        {
            byte[] decbuff = Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(decbuff);// System.Text.Encoding.UTF8.GetString(decbuff);
        }

    }
}
