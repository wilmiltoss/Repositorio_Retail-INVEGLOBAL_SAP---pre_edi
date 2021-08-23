using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace CdgUtiles.Util
{
    /// <summary>
    /// Clase de administracion de conexiones FTP
    /// </summary>
    public class ClienteFTP
    {

        public const string NOMBRE_CLASE = "ClienteFTP";


        /// <summary>
        /// Ejecuta el envio del archivo parametro al servidor FTP
        /// </summary>
        /// <param name="cIpServidor">Direccion IP o Nombre del servidor FTP</param>
        /// <param name="cCarpetaDestino">Path de la carpeta Destino</param>
        /// <param name="cPathArchivo">Path absoluto del archivo a enviar</param>
        /// <returns>Lista de Resultados [int, object]</returns>
        public static List<object> lEnviar(string cIpServidor
                                        , string cCarpetaDestino
                                        , string cPathArchivo)
        {

            string NOMBRE_METODO = NOMBRE_CLASE + ".lEnviar()";

            //resultado por defecto
            var lResultado = new List<object>() { 0, NOMBRE_METODO + " No Ejecutado!" };

            //auxiliares
            FtpWebRequest request = null;
            FtpWebResponse response = null;

            try
            {
                //obtenemos el nombre del archivo
                var cNombreArchivo = Path.GetFileName(cPathArchivo);

                //creamos la URI para la conexion FTP
                var oURI = new Uri(string.Format("ftp://{0}/{1}/{2}", cIpServidor
                                                , cCarpetaDestino, cNombreArchivo
                                                ));

                // Get the object used to communicate with the server.
                request = (FtpWebRequest)WebRequest.Create(oURI);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                // This example assumes the FTP site uses anonymous logon.
                //request.Credentials = new NetworkCredential("anonymous", "anonymous");
                //request.UseBinary = true;
                request.UsePassive = false;
                //request.KeepAlive = false;

                // Copy the contents of the file to the request stream.
                byte[] fileContents = File.ReadAllBytes(cPathArchivo);
                //srStreamArchivo.Close();
                request.ContentLength = fileContents.Length;


                //
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();
                requestStream.Flush();

                response = (FtpWebResponse)request.GetResponse();

                //establecemos el resultado del metodo
                lResultado[0] = 1;
                lResultado[1] = "Envio de archivo completo. Estado: " + response.StatusDescription;

            }
            catch (Exception ex)
            {
                //en caso de error, establecemos el estado correspondiente
                lResultado[0] = -1;
                lResultado[1] = NOMBRE_METODO + ": " + ex.Message;
            }
            finally
            {
                //cerramos la solicitud y la respuesta recuperada
                request = null;
                if (response != null) response.Close();
            }


            //devolvemos el resultado
            return lResultado;

        }

        /// <summary>
        /// Ejecuta la recuperacion del archivo parametro desde el servidor FTP
        /// </summary>
        /// <param name="cIpServidor">Direccion IP o Nombre del servidor FTP</param>
        /// <param name="cPathOrigen">Path absoluto del Archivo de origen</param>
        /// <param name="cPathDestino">Path absoluto de destino del archivo</param>
        /// <returns>Lista de Resultados [int, object]</returns>
        public static List<object> lTraer(string cIpServidor
                                        , string cPathOrigen
                                        , string cPathDestino)
        {

            string NOMBRE_METODO = NOMBRE_CLASE + ".lTraer()";

            //resultado por defecto
            var lResultado = new List<object>() { 0, NOMBRE_METODO + " No Ejecutado!" };

            //auxiliares
            FtpWebRequest request = null;
            FtpWebResponse response = null;

            try
            {
                //creamos la URI para la conexion FTP
                var oURI = new Uri(string.Format(@"ftp://{0}/{1}", cIpServidor, cPathOrigen.Replace("\\", "/")));

                // Get the object used to communicate with the server.
                request = (FtpWebRequest)WebRequest.Create(oURI);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                // This example assumes the FTP site uses anonymous logon.
                //request.Credentials = new NetworkCredential("anonymous", "anonymous");
                //request.UseBinary = true;
                request.UsePassive = false;

                response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                //si el archvo local ya existe, lo eliminamos
                if (File.Exists(cPathDestino)) File.Delete(cPathDestino);

                //creamos el archivo local y lo escribimos
                using (var fsArchivo = File.Create(cPathDestino))
                {
                    for (int a = responseStream.ReadByte(); a != -1; a = responseStream.ReadByte())
                        fsArchivo.WriteByte((byte)a);
                }

                //establecemos el resultado del metodo
                lResultado[0] = 1;
                lResultado[1] = "Recepción de archivo completo. Estado: " + response.StatusDescription;

            }
            catch (Exception ex)
            {
                //en caso de error, establecemos el estado correspondiente
                lResultado[0] = -1;
                lResultado[1] = NOMBRE_METODO + ": " + ex.Message;
            }
            finally
            {
                //cerramos la solicitud y la respuesta recuperada
                request = null;
                if (response != null) response.Close();
            }

            //devolvemos el resultado
            return lResultado;

        }

    }
}