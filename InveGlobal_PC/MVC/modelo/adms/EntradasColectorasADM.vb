Imports CdgPersistencia.ClasesBases
Imports System.IO

Public Class EntradasColectorasADM
    Inherits ADMbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "EntradasColectorasADM"

    Private __oApModelo As ApModelo

    Private Const SP_TOMAR_ENTRADAS As String = "EXEC [dbo].[SP_TOMAR_ENTRADAS] " _
                                                & " @id_inventario = {0}, @colector = '{1}'" _
                                                & ", @modo_prueba = {2}"

    Private Const VISTA_DETALLES_SQL As String = "SELECT L.DESCRIPCION AS LOCACION" _
                                       & ", S.DESCRIPCION AS SOPORTE, E.NRO_SOPORTE AS NRO_SOP" _
                                       & ", E.NIVEL, E.METRO, E.SCANNING" _
                                       & ", P.DESCRIPCION AS ARTICULO" _
                                       & ", E.CANTIDAD, ISNULL(U.NOMBRE_USUARIO, 'DESCONOCIDO') AS NOMBRE_USUARIO " _
                                       & ", E.COLECTORA, E.ID_INVENTARIO, E.ID_LOCACION, E.ID_SOPORTE" _
                                       & ", E.ID_USUARIO" _
                                       & ", E.ID_LETRA_SOPORTE, E.NRO_CONTEO" _
                                       & " FROM dbo.ENTRADAS_COLECTORAS AS E" _
                                       & " INNER JOIN dbo.LOCACIONES AS L ON E.ID_LOCACION = L.ID_LOCACION" _
                                       & " INNER JOIN dbo.SOPORTES AS S ON E.ID_SOPORTE = S.ID_SOPORTE" _
                                       & " INNER JOIN dbo.LETRAS_SOPORTES AS LET ON E.ID_LETRA_SOPORTE = LET.ID_LETRA_SOPORTE" _
                                       & " INNER JOIN dbo.PRODUCTOS AS P ON E.SCANNING = P.SCANNING" _
                                       & " LEFT JOIN dbo.USUARIOS AS U ON E.ID_USUARIO = U.ID_USUARIO" _
                                       & " WHERE E.ID_INVENTARIO = {0}" _
                                       & " AND E.COLECTORA   = '{1}'"

#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase 
    ''' </summary>
    ''' <param name="ApModeloParam">Instancia del controlador de la aplicacion</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal ApModeloParam As ApModelo)
        MyBase.New(ApModeloParam.Get_conector())

        __oApModelo = ApModelo.Get_instancia()
        Set_tabla(New EntradasColectorasTBL())

    End Sub

#End Region


#Region "Miembros de ADMbase"

    Public Overrides Function lAgregar(ByVal oEntradaColectorParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lAgregar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'casteamos el parametro a su tipo
            Dim oEntradaColector As EntradaColectoraOTD = CType(oEntradaColectorParam, EntradaColectoraOTD)

            'establecemos la sentencia de insercion
            Dim cInsert As String = String.Format(_Insert_sql _
                                                 , oEntradaColector.nIdInventario _
                                                 , oEntradaColector.nIdLocacion _
                                                 , oEntradaColector.nNroConteo _
                                                 , oEntradaColector.nIdSoporte _
                                                 , oEntradaColector.nNroSoporte _
                                                 , oEntradaColector.nIdLetraSoporte _
                                                 , oEntradaColector.cScanning _
                                                 , oEntradaColector.nNivel _
                                                 , oEntradaColector.nMetro _
                                                 , oEntradaColector.nCantidad _
                                                 , oEntradaColector.cColectora _
                                                 , oEntradaColector.nIdUsuario _
                                                 )

            'ejecutamos la sentencia de insercion
            lResultado = _oConector.lEjecutar_sentencia(cInsert)

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado


    End Function

    Public Overrides Function lActualizar(ByVal oOTDbase As OTDbase) As List(Of Object)
        Throw New NotImplementedException()
    End Function

    Public Overrides Function lEliminar(ByVal oEntradaColectorParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lEliminar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'casteamos el parametro a su tipo
            Dim oEntradaColector As EntradaColectoraOTD = CType(oEntradaColectorParam, EntradaColectoraOTD)
            Dim cFiltroWhere As String = String.Empty

            'si el parametro tiene un identificador negativo para sus dos identificadores de ubicacion
            If oEntradaColector.nIdLocacion <= 0 And oEntradaColector.nIdSoporte <= 0 Then
                cFiltroWhere = String.Format(" WHERE {0} = {1} AND {2} = '{3}'" _
                                             , EntradasColectorasTBL.ID_INVENTARIO.cNombre _
                                             , oEntradaColector.nIdInventario _
                                             , EntradasColectorasTBL.COLECTORA.cNombre _
                                             , oEntradaColector.cColectora _
                                            )

                'barremos con todos los registros de este inventario
                lResultado = _oConector.lEjecutar_sentencia(_Delete_sql & cFiltroWhere)

            Else
                'sino, establecemos la sentencia de filtrado de registros
                cFiltroWhere = " WHERE {0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9} "
                cFiltroWhere += " AND {10} = {11} AND {12} = {13} AND {14} = {15} AND {16} = '{17}' "

                Dim cDelete As String = _Delete_sql & String.Format(cFiltroWhere _
                                                         , EntradasColectorasTBL.ID_INVENTARIO.cNombre _
                                                         , oEntradaColector.nIdInventario _
                                                         , EntradasColectorasTBL.ID_LOCACION.cNombre _
                                                         , oEntradaColector.nIdLocacion _
                                                         , EntradasColectorasTBL.NRO_CONTEO.cNombre _
                                                         , oEntradaColector.nNroConteo _
                                                         , EntradasColectorasTBL.ID_SOPORTE.cNombre _
                                                         , oEntradaColector.nIdSoporte _
                                                         , EntradasColectorasTBL.NRO_SOPORTE.cNombre _
                                                         , oEntradaColector.nNroSoporte _
                                                         , EntradasColectorasTBL.ID_LETRA_SOPORTE.cNombre _
                                                         , oEntradaColector.nIdLetraSoporte _
                                                         , EntradasColectorasTBL.NIVEL.cNombre _
                                                         , oEntradaColector.nNivel _
                                                         , EntradasColectorasTBL.METRO.cNombre _
                                                         , oEntradaColector.nMetro _
                                                         , EntradasColectorasTBL.SCANNING.cNombre _
                                                         , oEntradaColector.cScanning _
                                                         )

                'ejecutamos la sentencia de eliminacion
                lResultado = _oConector.lEjecutar_sentencia(cDelete)

            End If

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    Public Overrides Function lGet_elemento(ByVal oEntradaColectorParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lGet_elemento()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'casteamos el parametro a su tipo
            Dim oEntradaColector As EntradaColectoraOTD = CType(oEntradaColectorParam, EntradaColectoraOTD)

            'establecemos la sentencia de filtrado de registros
            Dim cFiltroWhere = " WHERE {0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9} "
            cFiltroWhere += " AND {10} = {11} AND {12} = {13} AND {14} = {15} AND {16} = '{17}' "

            cFiltroWhere = String.Format(cFiltroWhere _
                                         , EntradasColectorasTBL.ID_INVENTARIO.cNombre _
                                         , oEntradaColector.nIdInventario _
                                         , EntradasColectorasTBL.ID_LOCACION.cNombre _
                                         , oEntradaColector.nIdLocacion _
                                         , EntradasColectorasTBL.NRO_CONTEO.cNombre _
                                         , oEntradaColector.nNroConteo _
                                         , EntradasColectorasTBL.ID_SOPORTE.cNombre _
                                         , oEntradaColector.nIdSoporte _
                                         , EntradasColectorasTBL.NRO_SOPORTE.cNombre _
                                         , oEntradaColector.nNroSoporte _
                                         , EntradasColectorasTBL.ID_LETRA_SOPORTE.cNombre _
                                         , oEntradaColector.nIdLetraSoporte _
                                         , EntradasColectorasTBL.NIVEL.cNombre _
                                         , oEntradaColector.nNivel _
                                         , EntradasColectorasTBL.METRO.cNombre _
                                         , oEntradaColector.nMetro _
                                         , EntradasColectorasTBL.SCANNING.cNombre _
                                         , oEntradaColector.cScanning _
                                         )

            'ejecutamos la busqueda filtrada
            lResultado = lGet_elementos(cFiltroWhere)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, CType(lResultado(1), List(Of EntradaColectoraOTD))(0)})

            End If

        Catch ex As Exception

            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado


    End Function

    Public Overrides Function lGet_elementos(ByVal cFiltroWhere As String) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lGet_elementos()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'intentamos recuperar los datos
        Try
            'ensamblamos la consulta sql
            Dim cConsulta As String = _Select_sql & cFiltroWhere

            'busqueda filtrada
            lResultado = _oConector.lEjecutar_consulta(cConsulta)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'instancias auxiliares
                Dim lEntradas As New List(Of EntradaColectoraOTD)()
                Dim dtResultado As DataTable = CType(lResultado(1), DataTable)

                'recorremos las filas devueltas
                For Each dr As DataRow In dtResultado.Rows
                    'lo agregamos a la lista
                    lEntradas.Add(New EntradaColectoraOTD(Long.Parse(dr(EntradasColectorasTBL.ID_INVENTARIO.nIndice).ToString()) _
                                            , Integer.Parse(dr(EntradasColectorasTBL.ID_LOCACION.nIndice).ToString()) _
                                            , Integer.Parse(dr(EntradasColectorasTBL.NRO_CONTEO.nIndice).ToString()) _
                                            , Integer.Parse(dr(EntradasColectorasTBL.ID_SOPORTE.nIndice).ToString()) _
                                            , Integer.Parse(dr(EntradasColectorasTBL.NRO_SOPORTE.nIndice).ToString()) _
                                            , Integer.Parse(dr(EntradasColectorasTBL.ID_LETRA_SOPORTE.nIndice).ToString()) _
                                            , dr(EntradasColectorasTBL.SCANNING.nIndice).ToString() _
                                            , Integer.Parse(dr(EntradasColectorasTBL.NIVEL.nIndice).ToString()) _
                                            , Integer.Parse(dr(EntradasColectorasTBL.METRO.nIndice).ToString()) _
                                            , Double.Parse(dr(EntradasColectorasTBL.CANTIDAD.nIndice).ToString()) _
                                            , dr(EntradasColectorasTBL.COLECTORA.nIndice).ToString() _
                                            , Long.Parse(dr(EntradasColectorasTBL.ID_USUARIO.nIndice).ToString()) _
                                           ))

                Next

                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, lEntradas, dtResultado})

            End If

        Catch ex As Exception

            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

#End Region


#Region "ESPECIFICOS"

    ''' <summary>
    ''' Ejecuta la insercion de los registros desde el archivo CSV de entrada
    ''' </summary>
    ''' <param name="oInventario">Instancia del inventario actual</param>
    ''' <param name="cPathArchivoCsv">Path absoluto del archivo de entrada</param>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Public Function lCargar_entradas(ByVal oInventario As InventarioOTD, ByVal cPathArchivoCsv As String) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lCargar_entradas()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'si el archivo existe
            If File.Exists(cPathArchivoCsv) Then
                'auxiliares
                Dim cLineaEntrada As String
                Dim cColector As String
                Dim aValores As String()

                'mostramos el nombre del archivo
                MessageBox.Show("Nombre de Archivo " & cPathArchivoCsv)

                'instanciamos un lector de archivos
                Dim oStream As New StreamReader(cPathArchivoCsv)

                Try
                    'recuperamos la primera linea
                    cLineaEntrada = oStream.ReadLine()


                    'si no hay datos, probablemente el archivo este vacio
                    If cLineaEntrada = Nothing Then
                        Throw New IOException("El archivo no contiene datos!")
                    End If

                    cColector = "Desconocido"

                    Try
                        'intentamos recuperar el nombre del colector
                        cColector = cLineaEntrada.Split(":")(1)

                        'creamos una instancia de entrada auxiliar
                        Dim oEntrada As New EntradaColectoraOTD()
                        oEntrada.nIdInventario = oInventario.nId
                        oEntrada.cColectora = cColector

                        'eliminamos los registros anteriores
                        lResultado = lEliminar(oEntrada)

                    Catch ex As Exception
                        'en caso de error, nada =( no tenemos acceso al controlador desde aqui
                    End Try

                    'leemos todas las lineas del archivo mientras no sea fin del mismo
                    While Not oStream.EndOfStream
                        'tomamos la linea
                        cLineaEntrada = oStream.ReadLine()

                        'si no hay datos, probablemente el archivo este vacio
                        If cLineaEntrada = Nothing Then
                            Throw New IOException("El archivo no contiene datos!")
                        End If

                        'separamos los campos
                        aValores = cLineaEntrada.Split(";")

                        'si la coleccion de valores de entrada y la de campos de la tabla tienen la misma cantidad de campos
                        If (aValores.Length + 2) = _oTabla.CAMPOS.Length Then

                            'creamos la instancia de la entrada y la pasamos al metodo de insercion a la tabla correspondiente
                            lResultado = lAgregar(New EntradaColectoraOTD(oInventario.nId _
                                                                        , Integer.Parse(aValores(0)) _
                                                                        , Integer.Parse(aValores(1)) _
                                                                        , Integer.Parse(aValores(2)) _
                                                                        , Integer.Parse(aValores(3)) _
                                                                        , Integer.Parse(aValores(4)) _
                                                                        , aValores(7) _
                                                                        , Integer.Parse(aValores(5)) _
                                                                        , Integer.Parse(aValores(6)) _
                                                                        , Double.Parse(aValores(8)) _
                                                                        , cColector _
                                                                        , Long.Parse(aValores(9)) _
                                                                       ))
                            'si NO se ejecuto correctamente, salimos del bucle
                            If Not lResultado(0).Equals(1) Then Exit While

                        Else
                            'si no tiene la cantidad de campos requeridos
                            lResultado = New List(Of Object)(New Object() {-1 _
                                                                           , String.Format("El Archivo [{0}] no contiene todos los campos esperados!" _
                                                                                           , cPathArchivoCsv)})

                        End If

                    End While

                    'si NO hubo errores, devolvemos el nombre del colector como parte del resultado
                    If lResultado(0).Equals(1) Then lResultado(1) = cColector

                Catch ex As Exception
                    'en caso de error
                    lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

                Finally
                    'cerramos el archivo
                    If Not oStream Is Nothing Then oStream.Close()

                End Try

            Else
                'sino, resultado del metodo fallido
                lResultado = New List(Of Object)(New Object() {-1 _
                                                               , String.Format("No Existe el Archivo de Lecturas [{0}]" _
                                                                               , cPathArchivoCsv)})

            End If

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    ''' <summary>
    ''' Devuelve una tabla con los detalles de los datos de lecturas desde el colector
    ''' </summary>
    ''' <param name="oInventario">Instancia del Inventario actual</param>
    ''' <param name="cNombreColector">Nombre del Colector</param>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Public Function lGet_vista_datos(ByVal oInventario As InventarioOTD, ByVal cNombreColector As String) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lGet_vista_datos()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'intentamos recuperar los datos
        Try
            'ensamblamos la consulta sql
            Dim cConsulta As String = String.Format(VISTA_DETALLES_SQL, oInventario.nId, cNombreColector)

            'busqueda filtrada
            lResultado = _oConector.lEjecutar_consulta(cConsulta)

        Catch ex As Exception

            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    ''' <summary>
    ''' Ejecuta la transferencia delos datos de lecturas a la tabla de detalles de conteos
    ''' para hacer que pasen a formar parte del inventario
    ''' </summary>
    ''' <param name="oInventario">Instancia del inventario actual</param>
    ''' <param name="cNombreColector">Nombre del colector</param>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Public Function lTomar_entradas(ByVal oInventario As InventarioOTD _
                                    , ByVal cNombreColector As String _
                                    , ByVal nModoPrueba As Integer)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lTomar_entradas()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'ejecutamos la llamada al procedimiento
            lResultado = _oConector.lEjecutar_consulta(String.Format(SP_TOMAR_ENTRADAS _
                                                                    , oInventario.nId _
                                                                    , cNombreColector _
                                                                    , nModoPrueba))

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'tomamos la primera fila de la tabla devuelta
                Dim dr As DataRow = CType(lResultado(1), DataTable).Rows(0)

                'si el valor del primer campo NO es 0 (cero)
                If Not Integer.Parse(dr(0).ToString()).Equals(0) Then
                    'formamos el resultado devuelto desde la bd
                    lResultado(0) = Integer.Parse(dr(0).ToString())
                    lResultado(1) = dr(1).ToString()

                End If
            End If

        Catch ex As Exception

            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

#End Region


End Class
