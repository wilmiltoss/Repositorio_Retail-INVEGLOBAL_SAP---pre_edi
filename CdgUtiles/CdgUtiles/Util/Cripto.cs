using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace CdgUtiles.Util
{

    /// <summary>
    /// Clase de utilería de encrptamiento de datos
    /// </summary>
    public static class Cripto
    {

        /// <summary>
        /// Encripta con el algoritmo SHA1 el string pasado
        /// </summary>
        /// <param name="cCadena">Cadena a encriptar</param>
        /// <returns>Cadena de bytes</returns>
        public static byte[] Get_SHA1(string cCadena)
        {
            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha1.ComputeHash(encoding.GetBytes(cCadena));

            //devovlemos la cadena resultante
            return stream;
        }



    }

}
