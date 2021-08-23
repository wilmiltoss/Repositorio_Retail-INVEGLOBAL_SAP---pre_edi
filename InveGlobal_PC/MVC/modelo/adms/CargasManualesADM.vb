Imports CdgPersistencia.ClasesBases

Public Class CargasManualesADM
    Inherits ADMbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "CargasManualesADM"

    Private __oApModelo As ApModelo

    Private Const SP_TOMAR_CONTEOS As String = "EXEC [dbo].[SP_TOMAR_CARGAS_MANUALES] " _
                                                & "@id_inventario= {0}, @colector= '{1}'" _
                                                & ", @modo_prueba = 0"


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
        Set_tabla(New CargasManualesTBL())

    End Sub

#End Region


#Region "Miembros de ADMbase"

    Public Overrides Function lAgregar(ByVal oCargaManualParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lAgregar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'casteamos el parametro a su tipo
            Dim oCargaManual As CargaManualOTD = CType(oCargaManualParam, CargaManualOTD)

            'establecemos la sentencia de insercion
            Dim cInsert As String = String.Format(_Insert_sql _
                                         , oCargaManual.cColector _
                                         , oCargaManual.nIdInventario _
                                         , oCargaManual.nIdLocacion _
                                         , oCargaManual.nNroConteo _
                                         , oCargaManual.nIdSoporte _
                                         , oCargaManual.nNroSoporte _
                                         , oCargaManual.nIdLetraSoporte _
                                         , oCargaManual.nNivel _
                                         , oCargaManual.nMetro _
                                         , oCargaManual.cScanning _
                                         , oCargaManual.nCantidad _
                                         , oCargaManual.nIdUsuario _
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

    Public Overrides Function lActualizar(ByVal oCargaManualParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lActualizar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'casteamos el parametro a su tipo
            Dim oCargaManual As CargaManualOTD = CType(oCargaManualParam, CargaManualOTD)

            'establecemos la sentencia de actualizacion
            Dim cUpdate As String = String.Format(_Update_sql _
                                         , oCargaManual.cColector _
                                         , oCargaManual.nIdInventario _
                                         , oCargaManual.nIdLocacion _
                                         , oCargaManual.nNroConteo _
                                         , oCargaManual.nIdSoporte _
                                         , oCargaManual.nNroSoporte _
                                         , oCargaManual.nIdLetraSoporte _
                                         , oCargaManual.nNivel _
                                         , oCargaManual.nMetro _
                                         , oCargaManual.cScanning _
                                         , oCargaManual.nCantidad _
                                         , oCargaManual.nIdUsuario _
                                         )

            'establecemos la sentencia de filtrado de registros
            Dim cFiltroWhere As String = " WHERE {0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9} "
            cFiltroWhere += " AND {10} = {11} AND {12} = {13} AND {14} = {15} AND {16} = '{17}' "

            'le agregamos la sentencia de siltrado
            cUpdate += String.Format(cFiltroWhere _
                                                     , CargasManualesTBL.ID_INVENTARIO.cNombre _
                                                     , oCargaManual.nIdInventario _
                                                     , CargasManualesTBL.ID_LOCACION.cNombre _
                                                     , oCargaManual.nIdLocacion _
                                                     , CargasManualesTBL.NRO_CONTEO.cNombre _
                                                     , oCargaManual.nNroConteo _
                                                     , CargasManualesTBL.ID_SOPORTE.cNombre _
                                                     , oCargaManual.nIdSoporte _
                                                     , CargasManualesTBL.NRO_SOPORTE.cNombre _
                                                     , oCargaManual.nNroSoporte _
                                                     , CargasManualesTBL.ID_LETRA_SOPORTE.cNombre _
                                                     , oCargaManual.nIdLetraSoporte _
                                                     , CargasManualesTBL.NIVEL.cNombre _
                                                     , oCargaManual.nNivel _
                                                     , CargasManualesTBL.METRO.cNombre _
                                                     , oCargaManual.nMetro _
                                                     , CargasManualesTBL.SCANNING.cNombre _
                                                     , oCargaManual.cScanning _
                                                     )


            'ejecutamos la sentencia de actualizacion
            lResultado = _oConector.lEjecutar_sentencia(cUpdate)

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    Public Overrides Function lEliminar(ByVal oCargaManualParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lEliminar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'casteamos el parametro a su tipo
            Dim oCargaManual As CargaManualOTD = CType(oCargaManualParam, CargaManualOTD)
            Dim cFiltroWhere As String = String.Empty

            'si el parametro tiene un identificador negativo para sus dos identificadores de ubicacion
            If oCargaManual.nIdLocacion < 0 And oCargaManual.nIdSoporte < 0 Then
                cFiltroWhere = String.Format(" WHERE {0} = {1}" _
                                             , CargasManualesTBL.ID_INVENTARIO.cNombre _
                                             , oCargaManual.nIdInventario _
                                                )

                'barremos con todos los registros de este inventario
                lResultado = _oConector.lEjecutar_sentencia(_Delete_sql & cFiltroWhere)

            Else
                'sino, establecemos la sentencia de filtrado de registros
                cFiltroWhere = " WHERE {0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9} "
                cFiltroWhere += " AND {10} = {11} AND {12} = {13} AND {14} = {15} AND {16} = '{17}' "

                Dim cDelete As String = _Delete_sql & String.Format(cFiltroWhere _
                                                         , CargasManualesTBL.ID_INVENTARIO.cNombre _
                                                         , oCargaManual.nIdInventario _
                                                         , CargasManualesTBL.ID_LOCACION.cNombre _
                                                         , oCargaManual.nIdLocacion _
                                                         , CargasManualesTBL.NRO_CONTEO.cNombre _
                                                         , oCargaManual.nNroConteo _
                                                         , CargasManualesTBL.ID_SOPORTE.cNombre _
                                                         , oCargaManual.nIdSoporte _
                                                         , CargasManualesTBL.NRO_SOPORTE.cNombre _
                                                         , oCargaManual.nNroSoporte _
                                                         , CargasManualesTBL.ID_LETRA_SOPORTE.cNombre _
                                                         , oCargaManual.nIdLetraSoporte _
                                                         , CargasManualesTBL.NIVEL.cNombre _
                                                         , oCargaManual.nNivel _
                                                         , CargasManualesTBL.METRO.cNombre _
                                                         , oCargaManual.nMetro _
                                                         , CargasManualesTBL.SCANNING.cNombre _
                                                         , oCargaManual.cScanning _
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

    Public Overrides Function lGet_elemento(ByVal oCargaManualParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lGet_elemento()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'casteamos el parametro a su tipo
            Dim oCargaManual As CargaManualOTD = CType(oCargaManualParam, CargaManualOTD)

            'establecemos la sentencia de filtrado de registros
            Dim cFiltroWhere = " WHERE {0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9} "
            cFiltroWhere += " AND {10} = {11} AND {12} = {13} AND {14} = {15} AND {16} = '{17}' "

            cFiltroWhere = String.Format(cFiltroWhere _
                                         , CargasManualesTBL.ID_INVENTARIO.cNombre _
                                         , oCargaManual.nIdInventario _
                                         , CargasManualesTBL.ID_LOCACION.cNombre _
                                         , oCargaManual.nIdLocacion _
                                         , CargasManualesTBL.NRO_CONTEO.cNombre _
                                         , oCargaManual.nNroConteo _
                                         , CargasManualesTBL.ID_SOPORTE.cNombre _
                                         , oCargaManual.nIdSoporte _
                                         , CargasManualesTBL.NRO_SOPORTE.cNombre _
                                         , oCargaManual.nNroSoporte _
                                         , CargasManualesTBL.ID_LETRA_SOPORTE.cNombre _
                                         , oCargaManual.nIdLetraSoporte _
                                         , CargasManualesTBL.NIVEL.cNombre _
                                         , oCargaManual.nNivel _
                                         , CargasManualesTBL.METRO.cNombre _
                                         , oCargaManual.nMetro _
                                         , CargasManualesTBL.SCANNING.cNombre _
                                         , oCargaManual.cScanning _
                                         )

            'ejecutamos la busqueda filtrada
            lResultado = lGet_elementos(cFiltroWhere)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, CType(lResultado(1), List(Of CargaManualOTD))(0)})

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
                Dim lCargas As New List(Of CargaManualOTD)()
                Dim dtResultado As DataTable = CType(lResultado(1), DataTable)

                'recorremos las filas devueltas
                For Each dr As DataRow In dtResultado.Rows
                    'lo agregamos a la lista
                    lCargas.Add(New CargaManualOTD(dr(CargasManualesTBL.COLECTOR.nIndice).ToString() _
                                            , Long.Parse(dr(CargasManualesTBL.ID_INVENTARIO.nIndice).ToString()) _
                                            , Integer.Parse(dr(CargasManualesTBL.ID_LOCACION.nIndice).ToString()) _
                                            , Integer.Parse(dr(CargasManualesTBL.NRO_CONTEO.nIndice).ToString()) _
                                            , Integer.Parse(dr(CargasManualesTBL.ID_SOPORTE.nIndice).ToString()) _
                                            , Integer.Parse(dr(CargasManualesTBL.NRO_SOPORTE.nIndice).ToString()) _
                                            , Integer.Parse(dr(CargasManualesTBL.ID_LETRA_SOPORTE.nIndice).ToString()) _
                                            , Integer.Parse(dr(CargasManualesTBL.NIVEL.nIndice).ToString()) _
                                            , Integer.Parse(dr(CargasManualesTBL.METRO.nIndice).ToString()) _
                                            , dr(CargasManualesTBL.SCANNING.nIndice).ToString() _
                                            , Double.Parse(dr(CargasManualesTBL.CANTIDAD.nIndice).ToString()) _
                                            , Long.Parse(dr(CargasManualesTBL.ID_USUARIO.nIndice).ToString())
                                           ))

                Next

                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, lCargas, dtResultado})

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
    ''' Ejecuta el procedimiento de transferencia de los datos de conteos manuales 
    ''' a la tabla de detalles de conteos
    ''' </summary>
    ''' <param name="oInventarioParam">Instancia del Inventario actual</param>
    ''' <param name="cNombreEquipo">Nombre del Equipo desde el que se estan cargando los datos</param>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Public Function lTomar_conteos(ByVal oInventarioParam As InventarioOTD, ByVal cNombreEquipo As String) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lTomar_conteos()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'ejecutamos la llamada al procedimiento
            lResultado = _oConector.lEjecutar_consulta(String.Format(SP_TOMAR_CONTEOS, oInventarioParam.nId, cNombreEquipo))

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
