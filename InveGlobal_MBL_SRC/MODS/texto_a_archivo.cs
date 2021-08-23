
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace InveStockMBL
{
    public class texto_a_archivo
    {
        /// <summary>
        /// ESTA CLASE ESCRIBE EN UN ARCHIVO PLANO LAS LINEAS PASADAS
        /// </summary>
        
        //---- variable para nombre y representacion del archivo
        string cNombreArchivo;

        //---- procedimiento que realizara el trabajo en si
        public bool pEscribirEnArchivo(string cLineaAEscribir)
        {
            //---- si el archivo no existe aun
            if (!File.Exists(this.cNombreArchivo))
            {
                //---- intentamos crearlo y escribir en el 
                try
                {
                    using (StreamWriter sw = File.CreateText(this.cNombreArchivo))
                    {
                        //---- escribimos la linea
                        sw.WriteLine(cLineaAEscribir);

                        //---- cerramos el archivo
                        sw.Close();

                        //---- devolvemos true como resultado de la funcion
                        return true;

                    }//---- FIN creacion y escritura en archivo
                }
                catch (Exception ex)
                {
                    //---- en caso de error, devolvemos falso
                    return false;

                }
            }
            else
            {
                //---- intentamos abrirlo y escribir en el
                try
                {
                    using (StreamWriter sw = File.AppendText(this.cNombreArchivo))
                    {
                        //---- escribimos la linea
                        sw.WriteLine(cLineaAEscribir);

                        //---- cerramos el archivo
                        sw.Close();

                        //---- devolvemos true como resultado de la funcion
                        return true;
                    }
                }
                catch (FileNotFoundException ex)
                {
                    //---- en caso de error, devolvemos falso
                    return false;

                }//---- FIN intento creacion archivo
            }//---- FIN evalua exitencia archivo
        }//---- FIN procedimiento pEscribirEnArchivo


        //---- procedimiento de eliminacion del archivo anterior
        public void pEliminarArchivo()
        {
            //---- si el archivo de salida existe
            if (File.Exists(this.cNombreArchivo)){
                    //---- lo eliminamos 
                    File.Delete(this.cNombreArchivo);

                    //---- confirmamos que haya sido asi
                    this.pEliminarArchivo();
            }
            else
            {
                //---- mensaje
                MessageBox.Show("La Carpeta de Datos esta Limpia", "Datos de Lectura");

            }//---- FIN si existe archivo

        }//---- FIN pEliminarArchivo

        //------------------------------------------------------------------------------------//
        //------------------------------------------------------------------------------------//

        //---- contructor de la clase
        public texto_a_archivo(string cPathNombreArchivo)
        {
            //---- pasamos el parametro como valor del atributo
            this.cNombreArchivo = cPathNombreArchivo;

        }//---- FIN del constructor
    }//---- FIN de la clase
}
