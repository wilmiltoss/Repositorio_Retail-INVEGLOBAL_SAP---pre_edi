using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace InveStockMBL.MODS
{
    #region Declaraciones Generales

    public class LeeIni
    {
        //----definimos los atributos publicos de la clase
        string cPathArchivo;

        //---------------------------------------------------------------------------------//
        //----              funcion para lectura del archivo de configuracion          ----//
        //---------------------------------------------------------------------------------//
        public string fLeer_Ini(string cSeccion, string cClave)
        {
            //---- una variable para almacenar la linea leida
            string cLinea;

            //---- otras variables a utilizar
            string cSeccionLeida= string.Empty;
            string cClaveLeida = string.Empty;
            string cValorParametro = string.Empty;

            //---- declaramos una variable de tipo StreamReader para el archivo
            StreamReader srArchivo;

            //---- si el archivo de configuracion existe
            if (File.Exists(cPathArchivo))
            {
                //---- redefinimos la variable de referencia al archivo de configuracion
                srArchivo = new StreamReader(cPathArchivo);

                //---- leemos toda la linea
                cLinea = srArchivo.ReadLine();

                //---- recorremos el archivo linea a linea mientras no sea EOF y no se haya encontrado aun el valor de la clave
                while (cLinea != null & cValorParametro.Length == 0)
                {
                    //---- eliminamos los espacios de la linea
                    cLinea = cLinea.Trim();

                    //---- si la linea NO empieza por ";" (comentario)
                    //---- y SI lo hace con "[" (clave de parametro)
                    if (cLinea.Substring(0, 1) == "[")
                    {
                        //si se encuentra el caracter de cierre de clave "]"
                        if (cLinea.IndexOf("]") > 0)
                        {
                            //---- tomamos el nombre de la clave
                            cSeccionLeida = cLinea.Substring(1, cLinea.IndexOf("]") - 1);

                            //---- si la seccion es la que se solicito
                            if (cSeccion == cSeccionLeida)
                            {
                                //---- leemos toda la linea
                                cLinea = srArchivo.ReadLine();

                                //---- recorremos el archivo linea a linea mientras no sea EOF,
                                //---- no se haya encontrado aun el valor de la clave
                                //---- y no sea otra seccion
                                while (cLinea != null & cValorParametro.Length == 0 & cLinea.Substring(0, 1) != "[")
                                {
                                    //---- eliminamos los espacios de la linea
                                    cLinea = cLinea.Trim();

                                    //---- si la linea NO empieza por ";" (comentario)
                                    if (cLinea.Substring(0, 1) != ";")
                                    {
                                        //---- si tiene el caracter de asignacion de valor "="
                                        if (cLinea.IndexOf("=") > 0)
                                        {
                                            //---- obtenemos el Nombre de la Clave
                                            cClaveLeida = cLinea.Substring(0, cLinea.IndexOf("="));

                                            //---- si la clave es la que se solicito
                                            if (cClave == cClaveLeida)
                                            {
                                                //---- recuperamos su valor sin espacios
                                                cValorParametro = cLinea.Substring(cLinea.IndexOf("=") + 1, cLinea.Length - (cLinea.IndexOf("=") + 1));
                                                cValorParametro = cValorParametro.Trim();
                                                if (cValorParametro.Length == 0)
                                                {
                                                    //---- mensaje de notificacion
                                                    MessageBox.Show(cClave + " no definido en INI"
                                                                    , "Archivo INI");

                                                }//---- Fin Valor de Clave Vacio

                                                //---- regresamos al programa principal
                                                return cValorParametro;

                                            }//---- Fin Caracter de Asignacion
                                        }//---- Fin Clave Solicitada
                                    }//---- Fin No Comentario

                                    //---- leemos la siguiente linea
                                    cLinea = srArchivo.ReadLine();

                                }//---- Fin Recorrer Archivo
                            }//---- Fin Seccion Solicitada
                        }//---- Fin Cierre Seccion
                    }//---- Fin Inicio Linea

                    //---- leemos la siguiente linea del archivo
                    cLinea = srArchivo.ReadLine();

                }//---- Fin Recorrer Archivo

                //---- Cerramos el Archivo
                srArchivo.Close();

            }//---- Fin Verifica Existe Archivo

            //---- si el valor de clave no fue encontrado o esta vacio
            if (cValorParametro.Length == 0)
            {
                //---- mensaje de notificacion
                MessageBox.Show(cClave + " no definido en INI"
                                , "Archivo INI");

            }//---- Fin Valor de Clave Vacio

            //---- regresamos al programa principal
            return cValorParametro;

        }//---- Fin fLeer_Ini

        //---- Contructor de clase
        public LeeIni(string cPathArchivoIni)
        {
            //---- pasamos el parametro como valor del atributo
            this.cPathArchivo = cPathArchivoIni;
        }

    }

    #endregion
}
