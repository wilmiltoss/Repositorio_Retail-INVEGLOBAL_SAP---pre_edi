Imports CdgPersistencia.ClasesBases
Imports System.Globalization

Public Class InventariosADM
    Inherits ADMbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "InventariosADM"

    Private __oApModelo As ApModelo

    Private Const INSERTAR As String = "EXEC [dbo].[SP_INSERTAR_DATOS_INVENTARIO] @fecha_inventario='{0:dd/MM/yyyy}',@id_local={1},@comentarios='{2}',@id_usuario={3}"
    Private Const UPDATE As String = "UPDATE [DATOS_INVENTARIO] SET [NRO_CONTEO_APLICADO] = 1,[CERRADO] = '{0}' WHERE [ID_INVENTARIO] = {1}"


#End Region


#Region "CONSTRUCTORES"

    Public Sub New(ByVal ApModeloParam As ApModelo)
        MyBase.New(ApModeloParam.Get_conector())

        __oApModelo = ApModelo.Get_instancia()
        Set_tabla(New InventariosTBL())

    End Sub

#End Region


#Region "Miembros de ADMbase"

    Public Overrides Function lAgregar(ByVal oInventarioParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lAgregar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'intentamos recuperar los datos
        Try
            'casteamos el parametro a su tipo
            Dim oInventario As InventarioOTD = CType(oInventarioParam, InventarioOTD)

            'iniciamos una transaccion
            lResultado = _oConector.lIniciar_transaccion()

            'establecemos la sentencia a ejecutar
            Dim cInsert As String = String.Format(INSERTAR, oInventario.dFechaInventario _
                                                       , oInventario.Local_OTD.nId, oInventario.cDescripcion _
                                                       , oInventario.Usuario_OTD.nId)

            'ejecutamos la sentencia
            lResultado = _oConector.lEjecutar_escalar(cInsert)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'tomamos el identificador creado para el inventario
                oInventario.nId = lResultado(1)

                'llamamos al metodo de insercion de datos de configuracion de inventario
                '__oApModelo.Configuraciones_ADM().lAgregar(oInventario.Configuracion_OTD)

            End If

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'recorremos la lista de sectores cubiertos
                For Each oSector As SectorOTD In oInventario.lSectores
                    'llamamos al metodo se insercion de inventario_sectores
                    lResultado = __oApModelo.InventariosSectores_ADM().lAgregar(New InventarioSectorOTD(oInventario.nId _
                                                                                                        , oSector.cIdSector _
                                                                                                        ))

                    'si NO se ejecuto correctamente, salimos del bucle
                    If Not lResultado(0).Equals(1) Then Exit For

                Next
            End If

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'Confirmamos la transaccion
                lResultado = _oConector.lConfirmar_transaccion()
            Else
                'revertimos la transaccion
                _oConector.lRevertir_transaccion()
            End If

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    Public Overrides Function lActualizar(ByVal oInventarioParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lActualizar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'intentamos recuperar los datos
        Try
            'casteamos el parametro a su tipo
            Dim oInventario As InventarioOTD = CType(oInventarioParam, InventarioOTD)

            'iniciamos una transaccion
            lResultado = _oConector.lIniciar_transaccion()

            'establecemos la sentencia a ejecutar
            Dim cUpdate As String = String.Format(UPDATE, oInventario.bCerrado.ToString(), oInventario.nId)

            'ejecutamos la sentencia
            lResultado = _oConector.lEjecutar_sentencia(cUpdate)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'Confirmamos la transaccion
                lResultado = _oConector.lConfirmar_transaccion()
            Else
                'revertimos la transaccion
                _oConector.lRevertir_transaccion()
            End If

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    Public Overrides Function lEliminar(ByVal oOTDbase As OTDbase) As List(Of Object)
        Throw New NotImplementedException()
    End Function

    Public Overrides Function lGet_elemento(ByVal oInventarioParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lGet_elemento()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'intentamos recuperar los datos
        Try
            'establecemos la sentencia de filtrado de registros
            Dim cFiltroWhere As String = String.Format(_oTabla.Get_filtro_where_int(), InventariosTBL.ID.cNombre, oInventarioParam.nId)

            'ejecutamos la busqueda filtrada
            lResultado = lGet_elementos(cFiltroWhere)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, CType(lResultado(1), List(Of InventarioOTD))(0)})

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
            Dim cConsulta As String = String.Format(_Select_sql & cFiltroWhere)

            'busqueda filtrada
            lResultado = _oConector.lEjecutar_consulta(cConsulta)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'instancias auxiliares
                Dim oLocAux As New LocalOTD()
                Dim oUsuAux As New UsuarioOTD()
                Dim oSisAux As New SistemaOTD()


                Dim lInventarios As New List(Of InventarioOTD)()
                Dim dtResultado As DataTable = CType(lResultado(1), DataTable)

                'recorremos las filas devueltas
                For Each dr As DataRow In dtResultado.Rows

                    Dim oLocalOTD As New LocalOTD()
                    Dim oUsuarioOTD As New UsuarioOTD()
                    Dim oSistemaOTD As New SistemaOTD()
                    Dim oConfiguracionOTD As New ConfiguracionOTD()

                    'intentamos recuperar las instancias asociadas
                    oLocalOTD.nId = Long.Parse(dr(InventariosTBL.ID_LOCAL.nIndice).ToString())
                    oUsuarioOTD.nId = Long.Parse(dr(InventariosTBL.ID_USUARIO.nIndice).ToString())
                    oSistemaOTD.nId = Long.Parse(dr(InventariosTBL.ID_SISTEMA.nIndice).ToString())
                    oConfiguracionOTD.nId = Long.Parse(dr(InventariosTBL.ID.nIndice).ToString())

                    'solo si son diferentes
                    If oLocAux.nId = 0 And Not oLocAux.nId = oLocalOTD.nId Then
                        lResultado = __oApModelo.Locales_ADM.lGet_elemento(oLocalOTD)
                        If lResultado(0).Equals(1) Then oLocalOTD = CType(lResultado(1), LocalOTD)
                    ElseIf oLocAux.nId > 0 Then
                        oLocalOTD = oLocAux
                    End If

                    'solo si son diferentes
                    If oUsuAux.nId = 0 And Not oUsuAux.nId = oUsuarioOTD.nId Then
                        lResultado = __oApModelo.Usuarios_ADM.lGet_elemento(oUsuarioOTD)
                        If lResultado(0).Equals(1) Then oUsuarioOTD = CType(lResultado(1), UsuarioOTD)
                    ElseIf oLocAux.nId > 0 Then
                        oUsuarioOTD = oUsuAux
                    End If

                    'solo si son diferentes
                    If oSisAux.nId = 0 And Not oSisAux.nId = oSistemaOTD.nId Then
                        lResultado = __oApModelo.Usuarios_ADM.lGet_elemento(oSistemaOTD)
                        If lResultado(0).Equals(1) Then oSistemaOTD = CType(lResultado(1), SistemaOTD)
                    ElseIf oLocAux.nId > 0 Then
                        oSistemaOTD = oSisAux
                    End If

                    'recuperamos la instancia de configuracion
                    lResultado = __oApModelo.Configuraciones_ADM.lGet_elemento(oConfiguracionOTD)
                    If lResultado(0).Equals(1) Then oConfiguracionOTD = CType(lResultado(1), ConfiguracionOTD)

                    'creamos una instancia de inventario
                    Dim oInve As New InventarioOTD(Long.Parse(dr(InventariosTBL.ID.nIndice).ToString()) _
                                                    , dr(InventariosTBL.ID.nIndice).ToString() _
                                                    , DateTime.Parse(dr(InventariosTBL.FECHA_INVENTARIO.nIndice).ToString()) _
                                                    , oLocalOTD _
                                                    , oSistemaOTD _
                                                    , oUsuarioOTD _
                                           )

                    oInve.Configuracion_OTD = oConfiguracionOTD

                    'lo agregamos a la lista de resultados
                    lInventarios.Add(oInve)

                Next

                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, lInventarios})

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



#End Region

    Public Function lGet_ultimo_inventario(ByVal oLocalParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lGet_ultimo_inventario()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'intentamos recuperar los datos
        Try
            'ensamblamos la consulta sql
            Dim cConsulta As String = String.Format("SELECT TOP 1 {0}", _oTabla.Lista_de_campos)
            cConsulta += String.Format(" FROM {0} WHERE {0}.{1} = {2} ORDER BY {0}.{3} DESC" _
                                       , InventariosTBL.NOMBRE_TABLA, InventariosTBL.ID_LOCAL.cNombre _
                                       , oLocalParam.nId, InventariosTBL.FECHA.cNombre)

            'busqueda filtrada
            lResultado = _oConector.lEjecutar_consulta(cConsulta)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'instancias auxiliares
                Dim oLocalOTD As New LocalOTD()
                Dim oUsuarioOTD As New UsuarioOTD()
                Dim oInventario As New InventarioOTD()
                Dim oSistemaOTD As New SistemaOTD()
                Dim oConfiguracionOTD As New ConfiguracionOTD()

                Dim dtResultado As DataTable = CType(lResultado(1), DataTable)

                'recorremos las filas devueltas
                ', DateTime.ParseExact(dr(InventariosTBL.FECHA_INVENTARIO.nIndice).ToString(), "yyyyMMdd", CultureInfo.InvariantCulture) _
                For Each dr As DataRow In dtResultado.Rows
                    'intentamos recuperar las instancias asociadas
                    oLocalOTD.nId = Long.Parse(dr(InventariosTBL.ID_LOCAL.nIndice).ToString())
                    oUsuarioOTD.nId = Long.Parse(dr(InventariosTBL.ID_USUARIO.nIndice).ToString())
                    oSistemaOTD.nId = Long.Parse(dr(InventariosTBL.ID_SISTEMA.nIndice).ToString())
                    oConfiguracionOTD.nId = Long.Parse(dr(InventariosTBL.ID.nIndice).ToString())

                    lResultado = __oApModelo.Locales_ADM.lGet_elemento(oLocalOTD)
                    'si se ejecuto correctamente
                    If lResultado(0).Equals(1) Then oLocalOTD = CType(lResultado(1), LocalOTD)

                    lResultado = __oApModelo.Usuarios_ADM.lGet_elemento(oUsuarioOTD)
                    'si se ejecuto correctamente
                    If lResultado(0).Equals(1) Then oUsuarioOTD = CType(lResultado(1), UsuarioOTD)

                    lResultado = __oApModelo.Sistemas_ADM.lGet_elemento(oSistemaOTD)
                    'si se ejecuto correctamente
                    If lResultado(0).Equals(1) Then oSistemaOTD = CType(lResultado(1), SistemaOTD)

                    'recuperamos la instancia de configuracion
                    lResultado = __oApModelo.Configuraciones_ADM.lGet_elemento(oConfiguracionOTD)
                    If lResultado(0).Equals(1) Then oConfiguracionOTD = CType(lResultado(1), ConfiguracionOTD)

                    'lo agregamos a la lista
                    oInventario = New InventarioOTD(Long.Parse(dr(InventariosTBL.ID.nIndice).ToString()) _
                                                    , dr(InventariosTBL.COMENTARIOS.nIndice).ToString() _
                                                    , DateTime.Parse(dr(InventariosTBL.FECHA_INVENTARIO.nIndice).ToString()) _
                                                    , oLocalOTD _
                                                    , oSistemaOTD _
                                                    , oUsuarioOTD _
                                           )

                    oInventario.Configuracion_OTD = oConfiguracionOTD
                    oInventario.bCerrado = Boolean.Parse(dr(InventariosTBL.CERRADO.nIndice).ToString())

                    Exit For

                Next

                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, oInventario})

            End If

        Catch ex As Exception

            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function


End Class

