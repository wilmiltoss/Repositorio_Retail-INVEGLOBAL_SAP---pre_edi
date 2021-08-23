Imports CdgPersistencia.ClasesBases

Public Class ExistenciaVwOTD
    Inherits OTDbase

    Public Shadows Const NOMBRE_CLASE As String = "ExistenciaVwOTD"


    Public nIdInventario As Long
    Public dFechaInventario As DateTime
    Public cIdSector As String
    Public cNombreSector As String

    Public cScanning As String
    Public nCosto As Double

    Public nCantidadTeorica As Double
    Public nConteo1 As Double
    Public nConteo2 As Double

    Public nDiferencia12 As Double
    Public nConteo3 As Double
    Public nCantidadAjuste As Double

    Public bAjustar As Boolean
    Public bAjustado As Boolean
    Public bPesable As Boolean

    Public cId As String

    'Public nIdSistema As Long
    'Public nIdEnSistema As Long
    'Public nIdLocacion As Long
    'Public nCanUbicacion As Double





#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Construtor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New(0, NOMBRE_CLASE)

        nIdInventario = 0L
        dFechaInventario = DateTime.Now
        cIdSector = String.Empty
        cNombreSector = String.Empty

        cScanning = String.Empty
        nCantidadTeorica = 0D
        nConteo1 = 0D
        nConteo2 = 0D

        nDiferencia12 = 0D
        nConteo3 = 0D
        nCantidadAjuste = 0D

        bAjustar = False
        bAjustado = False

        bPesables_Incluidos = False

        'nIdSistema = 0L
        'nIdEnSistema = 0L
        'nIdLocacion = 0L
        'nCanUbicacion = 0D

    End Sub

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="nIdInventarioParam">Identificador del inventario</param>
    ''' <param name="dFechaInventarioParam">Fecha del inventario</param>
    ''' <param name="cIdSectorParam">Identificador del sector del articulo</param>
    ''' <param name="cNombreSectorParam">Nombre del sector</param>
    ''' <param name="cScanningParam">Codigo de barras del articulo</param>
    ''' <param name="cDescripcionParam">Descripcion del articulo</param>
    ''' <param name="nCostoParam">Costo del articulo</param>
    ''' <param name="nCantidadTeoricaParam">Cantidad teorica del articulo</param>
    ''' <param name="nConteo1Param">Cantidad en primer conteo</param>
    ''' <param name="nConteo2Param">Cantidad en segundo conteo</param>
    ''' <param name="nConteo3Param">Cantidad en tercer conteo</param>
    ''' <param name="nCantidadAjusteParam">Cantidad de ajuste de diferencias</param>
    ''' <param name="bAjustarParam">Si se va a ajustar o no el registro</param>
    ''' <param name="bAjustadoParam">Si ya esta ajustado o no el registro</param>
    ''' <param name="bPesableParam">Si el producto es pesable o no</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal nIdInventarioParam As Long, ByVal dFechaInventarioParam As DateTime _
                    , ByVal cIdSectorParam As String, ByVal cNombreSectorParam As String _
                    , ByVal cScanningParam As String, ByVal cDescripcionParam As String _
                    , ByVal nCostoParam As Double, ByVal nCantidadTeoricaParam As String _
                    , ByVal nConteo1Param As Double, ByVal nConteo2Param As Double _
                    , ByVal nConteo3Param As Double, ByVal nCantidadAjusteParam As Double _
                    , ByVal bAjustarParam As Boolean, ByVal bAjustadoParam As Boolean _
                    , ByVal bPesableParam As Boolean
                    )
        MyBase.New(Long.Parse(cScanningParam), cDescripcionParam)

        nIdInventario = nIdInventarioParam
        dFechaInventario = dFechaInventarioParam
        cIdSector = cIdSectorParam
        cNombreSector = cNombreSectorParam

        cScanning = cScanningParam
        nCosto = nCostoParam

        nCantidadTeorica = nCantidadTeoricaParam
        nConteo1 = nConteo1Param
        nConteo2 = nConteo2Param

        nDiferencia12 = (nConteo2Param - nConteo1Param)
        nConteo3 = nConteo3Param
        nCantidadAjuste = nCantidadAjusteParam

        bAjustar = bAjustarParam
        bAjustado = bAjustadoParam
        bPesable = bPesableParam


    End Sub


#End Region


#Region "ESPECIFICOS"

    ''' <summary>
    ''' Devuelve una cadena de valores para los datos, separados por el caracter indicado
    ''' </summary>
    ''' <param name="cSeparador">Separador de valores</param>
    ''' <returns>Cadena de valores separados</returns>
    ''' <remarks></remarks>
    Public Function cGet_csv(ByVal cSeparador As Char) As String

        Return (nIdInventario.ToString() _
                    & cSeparador & dFechaInventario.ToString() _
                    & cSeparador & cIdSector.ToString() _
                    & cSeparador & cNombreSector _
                    & cSeparador & cScanning _
                    & cSeparador & cDescripcion _
                    & cSeparador & nCosto.ToString() _
                    & cSeparador & nCantidadTeorica.ToString() _
                    & cSeparador & nConteo1.ToString() _
                    & cSeparador & nConteo2.ToString() _
                    & cSeparador & nDiferencia12.ToString() _
                    & cSeparador & nConteo3.ToString() _
                    & cSeparador & nCantidadAjuste.ToString() _
                    & cSeparador & (nCosto * nCantidadAjuste).ToString() _
                    & cSeparador & bAjustar.ToString() _
                    & cSeparador & bAjustado.ToString() _
                    & cSeparador & bPesable.ToString() _
                    )

    End Function

    ''' <summary>
    ''' Devuelve una cadena de valores para el titulo, separados por el caracter indicado
    ''' </summary>
    ''' <param name="cSeparador">Separador de valores</param>
    ''' <returns>Cadena de valores separados</returns>
    ''' <remarks></remarks>
    Public Function cGet_titulos_csv(ByVal cSeparador As Char) As String

        Return ("ID_INVENTARIO" _
                    & cSeparador & "FECHA_INVENTARIO" _
                    & cSeparador & "ID_SECTOR" _
                    & cSeparador & "NOMBRE_SECTOR" _
                    & cSeparador & "SCANNING" _
                    & cSeparador & "DESCRIPCION" _
                    & cSeparador & "COSTO" _
                    & cSeparador & "TEORICO" _
                    & cSeparador & "CONTEO_1" _
                    & cSeparador & "CONTEO_2" _
                    & cSeparador & "DIFERENCIA_1_2" _
                    & cSeparador & "CONTEO_3" _
                    & cSeparador & "CANTIDAD_AJUSTE" _
                    & cSeparador & "VALORIZADO_AJUSTE" _
                    & cSeparador & "AJUSTAR" _
                    & cSeparador & "AJUSTADO" _
                    & cSeparador & "PESABLE" _
                    )

    End Function

    ''' <summary>
    ''' Devuelve la cantidad a tomar como conteo final
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function nGet_conteo_final() As Double

        If nConteo3 > 0 Then Return nConteo3
        If nConteo2 > 0 Then Return nConteo2
        Return nConteo1

    End Function

    ''' <summary>
    ''' Devuelve una cadena de valores para los datos para el Eme
    ''' , separados por el caracter indicado
    ''' </summary>
    ''' <returns>Cadena de valores separados</returns>
    ''' <remarks></remarks>
    Public Function cGet_string_ibm_vasto() As String

        'formato de cadena de salida IBM BASTO para cargar fotos del EmeRetail
        Dim cFormato As String = "{0} {1} 0000000000 0000000000 +0000000000 {2}"

        'valor para cantidad
        Dim cCantidad As String = "+0000000000"

        'si la cantidad es positiva y mayor a cero
        If nGet_conteo_final() > 0 Then

            cCantidad = Int(nGet_conteo_final).ToString()

            'si el producto es pesable, le incluimos lo que iran como decimales (2 posiciones)
            If bPesable Then cCantidad += "00"

            'agregamos el signo
            cCantidad = "+" & cCantidad.PadLeft(10, "0")

        End If

        Return (String.Format(cFormato, cIdSector.PadLeft(12, "0") _
                              , cScanning.Substring(0, (cScanning.Length - 1)).PadLeft(12, "0") _
                              , cCantidad
                                ))

    End Function



#End Region

End Class
