using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace CdgUtiles.Log
{
    public class InfoALog
    {

        #region ATRIBUTOS DE LA CLASE

        public new const string NOMBRE_CLASE = "InfoALog";

        private string __cNombreArchivo;
        private new const string __cExtension = ".log";
        //private string __cDirLog;
        private string __cNombreAplicacion;

        private string __cPathCompleto;
        private StreamWriter __swArchivoLog;

        public int nArchivosViejos { get; set; }


        #endregion


        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="cNombreAplicaciónParam">Nombre de la aplicacion actual</param>
        public InfoALog(string cNombreAplicaciónParam)
            : this(cNombreAplicaciónParam, DateTime.Today.ToString("yyyyMMdd"))
        {

        }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="cNombreAplicaciónParam">Nombre de la aplicacion actual</param>
        /// <param name="cNombreArchivoParam">Nombre del archivo de log a escribir (automaticamente anade '.log')</param>
        public InfoALog(string cNombreAplicaciónParam, string cNombreArchivoParam)
        {
            nArchivosViejos = 30;

            this.__cNombreAplicacion = cNombreAplicaciónParam;
            this.__cNombreArchivo = cNombreArchivoParam;
            //this.__cDirLog = Environment.GetEnvironmentVariable("HOMEPATH");
            this.__cPathCompleto = directorio_logs + "\\" + this.__cNombreArchivo + __cExtension;

            //verificamos que existe el directorio destino
            if (!Directory.Exists(directorio_logs))
                Directory.CreateDirectory(directorio_logs);

            __Eliminar_Logs_Viejos();

        }

        /// <summary>
        /// Crea el archivo y lo deja listao para escribir en el
        /// </summary>
        /// <returns>True si el archivo fue creado</returns>
        private bool __bCrearArchivo()
        {
            //variable auxiliar
            bool bResultado = true;

            try
            {
                //intentamos crear el archivo destino
                this.__swArchivoLog = File.CreateText(this.__cPathCompleto);
                this.__swArchivoLog.Close();

            }
            catch
            {
                //en caso de error
                bResultado = false;
                throw;

            }

            //devolvemos el resultado del metodo
            return bResultado;

        }

        /// <summary>
        /// Valida que el archivo destino de log existe
        /// </summary>
        /// <returns>True si el archivo existe</returns>
        private bool __bValidar_Archivo()
        {
            //variable auxiliar
            bool bResultado = true;

            try
            {
                //si ya existe, devolvemos inmediatamente el resultado
                if (File.Exists(this.__cPathCompleto)) return bResultado;

                //sino, llamamos al metodo de creacion del mismo
                bResultado = this.__bCrearArchivo();

            }
            catch (Exception ex)
            {
                bResultado = false;
                throw;
            }

            //devolvemos el resultado 
            return bResultado;

        }

        /// <summary>
        /// Escribe una linea en el archivo de log
        /// </summary>
        /// <param name="cLinea">Cadena a escribir dentro del archivo</param>
        public void Escribir(string cLinea)
        {
            //si la linea esta vacia, salimos directamente
            if (cLinea.Trim().Equals(string.Empty)) return;

            //intentamos escribir en el archivo
            try
            {
                //si el archivo NO existe, salimos
                if (!this.__bValidar_Archivo()) return;

                //agregamos la marca de tiempo
                cLinea = DateTime.Now.ToString("HH:mm:ss") + " -> " + cLinea;

                this.__swArchivoLog = File.AppendText(this.__cPathCompleto);
                this.__swArchivoLog.WriteLine(cLinea);
                this.__swArchivoLog.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Devuelve el directorio de destino de logs de la aplicacion
        /// </summary>
        public string directorio_logs
        {
            get
            {
                //objeto auxiliar
                return Environment.GetEnvironmentVariable("HOMEPATH") + "\\" + this.__cNombreAplicacion;
            }
        }

        /// <summary>
        /// Metodo de eliminacion de archivos de LOGs viejos
        /// </summary>
        private void __Eliminar_Logs_Viejos()
        {

            // creamos una instancia de Info de Directorios
            DirectoryInfo oDirInfo = new DirectoryInfo(this.directorio_logs);

            // un diccionario para los archivos
            Dictionary<int, FileInfo> dicArchivos = new Dictionary<int, FileInfo>();

            try
            {
                // listamos los archivos de LOGS existentes en el Dir de destino actual
                foreach (FileInfo oInfo in oDirInfo.GetFiles("*" + __cExtension))
                {
                    if (!dicArchivos.ContainsKey(int.Parse(oInfo.Name.Substring(0, 8))))
                    {
                        dicArchivos.Add(
                            //int.Parse(oInfo.CreationTime.ToString("yyyyMMdd"))
                                    int.Parse(oInfo.Name.Substring(0, 8))
                                    , oInfo
                                    );
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // si el diccionario tiene mas elementos de los señalados para conservar
            if (dicArchivos.Count > nArchivosViejos)
            {
                // ordenamos en base a la fecha de los archivos
                List<KeyValuePair<int, FileInfo>> myList = dicArchivos.ToList();
                myList.Sort((firstPair, nextPair) =>
                {
                    return firstPair.Key.CompareTo(nextPair.Key);
                }
                );

                myList.Reverse();

                // eliminamos los archivos fuera del rango establecido
                while (myList.Count > nArchivosViejos)
                {
                    try
                    {
                        File.Delete(myList[myList.Count - 1].Value.FullName);
                        myList.RemoveAt(myList.Count - 1);
                    }
                    catch
                    {
                        break;
                    }
                }

            }

        }

    }
}
