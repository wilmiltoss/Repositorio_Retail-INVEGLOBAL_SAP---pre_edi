using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using OpenNETCF.Desktop.Communication;
using System.Devices;

namespace CdgUtiles.Util
{

    /// <summary>
    /// Clase de interaccion con colectores handheld via ActiveSync
    /// </summary>
    public static class WinMobile
    {
        public enum ROMS { IPSM, HONEYWELL };
        public static string[] aRoms = new string[2] { "\\IPSM", "\\HONEYWELL" };

        /// <summary>
        /// Transfiere un Archivo al Dispositivo Conectado al Equipo
        /// </summary>
        /// <param name="cArchivoOrigenParam">Path Absoluto del Archivo Origen</param>
        /// <param name="cDestinoArchivoParam">Path Absoluto del Archivo Destino</param>
        /// <param name="bReemplazarParam">Reemplazar en copia Previa</param>
        /// <param name="bEnviarArchivoParam">Si el Archivo se Envia='True', si se Trae='False'</param>
        /// <returns></returns>
        public static List<Object> lMover_Archivo_Dispositivo(String cArchivoOrigenParam,
                                                            String cDestinoArchivoParam,
                                                            Boolean bReemplazarParam,
                                                            Boolean bEnviarArchivoParam
                                                          )
        {
            //resultado por defecto del metodo
            List<Object> lResultado = new List<Object>() { 0, "Copia Archivo a Dispositivo No Ejecutado" };

            try
            {
                using (RemoteDeviceManager r = new RemoteDeviceManager())
                {
                    using (RemoteDevice oDispositivo = r.Devices.FirstConnectedDevice)
                    {
                        if (oDispositivo == null)
                            lResultado = new List<Object>() { 0, "El Dispositivo Movil No Esta Conectado a la PC!." };

                        else
                        {
                            //si la plataforma es PocketPC, sincronizamos primero
                            if (oDispositivo.Platform == "PocketPC")
                            {
                                // Start and stop a synchronization
                                oDispositivo.StartSync();
                                System.Threading.Thread.Sleep(2000);
                                oDispositivo.StopSync();
                            }

                            //evaluamos si se envia/trae, al/desde el Dispositivo
                            switch (bEnviarArchivoParam)
                            {
                                case true:

                                    //si el archivo local existe
                                    if (File.Exists(cArchivoOrigenParam))
                                    {
                                        //intentamos copiar al Dispositivo desde la PC
                                        try
                                        {
                                            //copiamos al PDA desde la PC en el Directorio de Destino
                                            RemoteFile.CopyFileToDevice(oDispositivo, cArchivoOrigenParam, cDestinoArchivoParam, bReemplazarParam);

                                            //establecemos el resultado del metodo
                                            lResultado = new List<Object>() { 1, "Ok" };
                                        }
                                        catch (Exception ex)
                                        {
                                            //en caso de error, lo devolvemos como resultado del metodo
                                            lResultado = new List<Object>(){-1, String.Format("Error Intentando Transferir Archivo a PDA : {0}",
                                                                                   ex.Message)
                                                                            };
                                        }
                                    }
                                    else
                                    {
                                        //si no existe, mensaje como resultado
                                        lResultado = new List<Object>() { -1, String.Format("No se encuentra el archivo Local: {0}", cArchivoOrigenParam) };
                                    }
                                    break;
                                default:
                                    //si el archivo Remoto existe
                                    if (RemoteFile.Exists(oDispositivo, cArchivoOrigenParam))
                                    {
                                        //intentamos copiar al Dispositivo desde la PC
                                        try
                                        {
                                            //copiamos al PDA desde la PC en el Directorio de Destino
                                            RemoteFile.CopyFileFromDevice(oDispositivo, cArchivoOrigenParam, cDestinoArchivoParam, bReemplazarParam);

                                            //establecemos el resultado del metodo
                                            lResultado = new List<Object>() { 1, "Ok" };

                                        }
                                        catch (Exception ex)
                                        {
                                            //en caso de error, lo devolvemos como resultado del metodo
                                            lResultado = new List<Object>(){-1, String.Format("Error Intentando Transferir Archivo desde PDA : {0}",
                                                                                   ex.Message)
                                                                    };

                                        }
                                    }
                                    else
                                    {
                                        //si no existe, mensaje como resultado
                                        lResultado = new List<Object>() { -1, String.Format("No se encuentra el archivo Remoto: {0}", cArchivoOrigenParam) };

                                    }

                                    break;
                            }
                        }
                    }
                }

                /*
                //variables a utilizar
                RAPI oAccesoRemoto = new RAPI();
                RAPI oMiRAPI = new RAPI();

                //intentamos ejecutar las acciones de sincronizacion de archivos
                try
                {
                    //si el dispositivo NO esta conectado al PC
                    if (!oMiRAPI.DevicePresent)
                    {
                        //establecemos el resultado del metodo, mensaje de notificacion
                        lResultado = new List<Object>() { 0, "El Dispositivo Movil No Esta Conectado a la PC!." };

                    }
                    else
                    {
                        //de otro modo, creamos una conexion Remote API
                        oMiRAPI.Connect();

                        //evaluamos si se envia/trae, al/desde el Dispositivo
                        switch (bEnviarArchivoParam)
                        {
                            case true:
                                //si el archivo local existe
                                if (File.Exists(cArchivoOrigenParam))
                                {
                                    //intentamos copiar al Dispositivo desde la PC
                                    try
                                    {
                                        //copiamos al PDA desde la PC en el Directorio de Destino
                                        oAccesoRemoto.CopyFileToDevice(cArchivoOrigenParam, cDestinoArchivoParam, bReemplazarParam);

                                        //establecemos el resultado del metodo
                                        lResultado = new List<Object>() { 1, "Ok" };
                                    }
                                    catch (Exception ex)
                                    {
                                        //en caso de error, lo devolvemos como resultado del metodo
                                        lResultado = new List<Object>(){-1, String.Format("Error Intentando Transferir Archivo a PDA : {0}",
                                                                               ex.Message)
                                                                        };
                                    }
                                }
                                else
                                {
                                    //si no existe, mensaje como resultado
                                    lResultado = new List<Object>() { -1, String.Format("No se encuentra el archivo Local: {0}", cArchivoOrigenParam) };
                                }

                                break;

                            default:
                                //si el archivo Remoto existe
                                if (oMiRAPI.DeviceFileExists(cArchivoOrigenParam))
                                {
                                    //intentamos copiar al Dispositivo desde la PC
                                    try
                                    {
                                        //copiamos al PDA desde la PC en el Directorio de Destino
                                        oAccesoRemoto.CopyFileFromDevice(cDestinoArchivoParam, cArchivoOrigenParam, bReemplazarParam);

                                        //establecemos el resultado del metodo
                                        lResultado = new List<Object>() { 1, "Ok" };

                                    }
                                    catch (Exception ex)
                                    {
                                        //en caso de error, lo devolvemos como resultado del metodo
                                        lResultado = new List<Object>(){-1, String.Format("Error Intentando Transferir Archivo desde PDA : {0}",
                                                                               ex.Message)
                                                                };

                                    }
                                }
                                else
                                {
                                    //si no existe, mensaje como resultado
                                    lResultado = new List<Object>() { -1, String.Format("No se encuentra el archivo Remoto: {0}", cArchivoOrigenParam) };

                                }

                                break;

                        }

                        //cerramos la conexion Remote API
                        oMiRAPI.Disconnect();

                    }

                }
                catch (RAPIException ex)
                {
                    //en caso de error, lo devolvemos como resultado del metodo
                    lResultado = new List<Object>() { -1, ex.Message };

                }*/

            }
            catch (Exception ex)
            {
                //en caso de error, lo devolvemos como resultado del metodo
                lResultado = new List<Object>() { -1, ex.Message };

            }

            //devolvemos el resultado del metodo
            return lResultado;

        }

        /// <summary>
        /// Devuelve el estado de conexion del equipo portatil a la PC
        /// </summary>
        public static bool Equipo_conectado
        {
            get
            {
                //variables a utilizar
                RAPI oMiRAPI = new RAPI();
                return oMiRAPI.DevicePresent;
            }
        }

        /// <summary>
        /// Devuelve el PATH al directorio de la ROM del Dipositivo 
        /// o en su defecto el directorio raiz
        /// </summary>
        public static string Get_rom
        {
            get
            {
                //si el equipo esta conectado
                if (Equipo_conectado)
                {
                    //variables a utilizar
                    RAPI oAccesoRemoto = new RAPI();
                    oAccesoRemoto.Connect();

                    if (oAccesoRemoto.DeviceFileExists(aRoms[(int)ROMS.IPSM])) return aRoms[(int)ROMS.IPSM];
                    else if (oAccesoRemoto.DeviceFileExists(aRoms[(int)ROMS.HONEYWELL])) return aRoms[(int)ROMS.HONEYWELL];
                    else return "\\";
                }

                //sino, nada
                return null;
            }
        }

        /// <summary>
        /// Intenta eliminar el archivo parametro en el dispositivo
        /// </summary>
        /// <param name="cPathArchivo">Path absoluto al archivo a eliminar</param>
        /// <returns>Arreglo de resultado [int, string]</returns>
        public static object[] aEliminarArchivo(string cPathArchivo)
        {
            
            //resultado por defecto
            var aResultado = new object[2] { 1, "Archivo eliminado" };

            try
            {
                //creamos una instancia de
                //variables a utilizar
                RAPI oAccesoRemoto = new RAPI();

                //si el dispositivo NO esta conectado al PC
                if (!oAccesoRemoto.DevicePresent)
                {
                    //establecemos el resultado del metodo, mensaje de notificacion
                    aResultado[0] = -1;
                    aResultado[1] = "El Dispositivo Móvil No Esta Conectado a la PC!..";

                }
                else
                {
                    //de otro modo, creamos una conexion Remote API
                    oAccesoRemoto.Connect();

                    try
                    {
                        //si el archivo Remoto existe
                        if (oAccesoRemoto.DeviceFileExists(cPathArchivo))
                            //intentamos eliminarlo
                            oAccesoRemoto.DeleteDeviceFile(cPathArchivo);
                    }
                    catch (Exception ex)
                    {
                        //en caso de error
                        aResultado[0] = -1;
                        aResultado[1] = string.Format("No se puedo eliminar el archivo {0}", cPathArchivo);
                        aResultado[1] = aResultado[1].ToString() + "\n Error: " + ex.Message;
                    }
                    finally
                    {
                        //cerramos la conexion Remote API
                        oAccesoRemoto.Disconnect();
                    }
                }
            }
            catch (Exception ex)
            {
                //en caso de error
                aResultado[0] = -1;
                aResultado[1] = ex.Message;
            }


            //devolvemos el resultado
            return aResultado;

        }

    }
}