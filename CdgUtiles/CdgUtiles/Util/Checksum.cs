using System;


namespace CdgUtiles.Util
{
    /// <summary>
    /// Utileria de funciones de calculos de verificaciones
    /// </summary>
    public static class Checksum
    {
        /// <summary>
        /// Devuelve el DV para codigos EAN8
        /// </summary>
        /// <param name="data">Cadena de digitos a calcular</param>
        /// <returns>Digito verificardo correspondiente</returns>
        /// <see cref="http://www.softmatic.com/barcode/ean-check-digit-c-sharp.html"/>
        public static int nEan8(String data)
        {
            // Test string for correct length
            if (data.Length != 7 && data.Length != 8)
                return -1;

            // Test string for being numeric
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] < 0x30 || data[i] > 0x39)
                    return -1;
            }

            int sum = 0;

            for (int i = 6; i >= 0; i--)
            {
                int digit = data[i] - 0x30;
                if ((i & 0x01) == 1)
                    sum += digit;
                else
                    sum += digit * 3;
            }
            int mod = sum % 10;
            return mod == 0 ? 0 : 10 - mod;
        }

        /// <summary>
        /// Devuelve el DV para codigos EAN13
        /// </summary>
        /// <param name="data">Cadena de digitos a calcular</param>
        /// <returns>Digito verificardo correspondiente</returns>
        /// <see cref="http://www.softmatic.com/barcode/ean-check-digit-c-sharp.html"/>
        public static int nEan13(String data)
        {
            if (data == null || data.Length != 12)
                throw new ArgumentException("Code length should be 12, i.e. excluding the checksum digit");

            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                int v;
                if (!int.TryParse(data[i].ToString(), out v))
                    throw new ArgumentException("Invalid character encountered in specified code.");
                sum += (i % 2 == 0 ? v : v * 3);
            }
            int check = 10 - (sum % 10);
            return check % 10;
        }

    }
}
