Imports CdgPersistencia.ClasesBases

Public Class LocalesADM
    Inherits ADMbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "LocalesADM"

    Private __oApModelo As ApModelo


#End Region


#Region "CONSTRUCTORES"

    Public Sub New(ByVal ApModeloParam As ApModelo)
        MyBase.New(ApModeloParam.Get_conector())

        __oApModelo = ApModelo.Get_instancia()
        Set_tabla(New LocalesTBL())

    End Sub

#End Region


#Region "Miembros de ADMbase"

    Public Overrides Function lAgregar(ByVal oOTDbase As OTDbase) As List(Of Object)
        Throw New NotImplementedException()
    End Function

    Public Overrides Function lActualizar(ByVal oOTDbase As OTDbase) As List(Of Object)
        Throw New NotImplementedException()
    End Function

    Public Overrides Function lEliminar(ByVal oOTDbase As OTDbase) As List(Of Object)
        Throw New NotImplementedException()
    End Function

    Public Overrides Function lGet_elemento(ByVal oLocalParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lGet_elemento()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'intentamos recuperar los datos
        Try
            'establecemos la sentencia de filtrado de registros
            Dim cFiltroWhere As String = String.Format(_oTabla.Get_filtro_where_int(), LocalesTBL.ID.cNombre, oLocalParam.nId)

            'ejecutamos la busqueda filtrada
            lResultado = lGet_elementos(cFiltroWhere)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then

                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, CType(lResultado(1), List(Of LocalOTD))(0)})

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
                Dim lLocales As New List(Of LocalOTD)()
                Dim dtResultado As DataTable = CType(lResultado(1), DataTable)
                Dim oEmpAux As New EmpresaOTD()
                Dim oSisAux As New SistemaOTD()


                'recorremos las filas devueltas
                For Each dr As DataRow In dtResultado.Rows
                    Dim oEmpresa As New EmpresaOTD()
                    Dim oSistema As New SistemaOTD()

                    'recuperamos las instancias asociadas
                    oEmpresa.nId = Integer.Parse(dr(LocalesTBL.ID_EMPRESA.nIndice).ToString())
                    oSistema.nId = Integer.Parse(dr(LocalesTBL.ID_SISTEMA.nIndice).ToString())

                    'solo si son diferentes
                    If oEmpAux.nId = 0 And Not oEmpAux.nId = oEmpresa.nId Then
                        lResultado = __oApModelo.Empresas_ADM.lGet_elemento(oEmpresa)
                        If lResultado(0).Equals(1) Then oEmpresa = CType(lResultado(1), EmpresaOTD)

                    ElseIf oEmpAux.nId > 0 Then
                        oEmpresa = oEmpAux
                    End If

                    'solo si son diferentes
                    If oSisAux.nId = 0 And Not oSisAux.nId = oSistema.nId Then
                        lResultado = __oApModelo.Sistemas_ADM.lGet_elemento(oSistema)
                        If lResultado(0).Equals(1) Then oSistema = CType(lResultado(1), SistemaOTD)

                    ElseIf oSisAux.nId > 0 Then
                        oSistema = oSisAux
                    End If



                    'lo agregamos a la lista
                    lLocales.Add(New LocalOTD(Long.Parse(dr(LocalesTBL.ID.nIndice).ToString()) _
                                            , dr(LocalesTBL.DESCRIPCION.nIndice).ToString() _
                                            , oSistema _
                                            , Integer.Parse(dr(LocalesTBL.ID_EN_SISTEMA.nIndice).ToString()) _
                                            , Integer.Parse(dr(LocalesTBL.ID_TIPO_LOCAL.nIndice).ToString()) _
                                            , oEmpresa _
                                            , dr(LocalesTBL.NOMBRE_CLAVE.nIndice).ToString() _
                                           ))

                Next

                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, lLocales})

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

    Public Function lGet_locales_con_inventario() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lGet_locales_con_inventario()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'intentamos recuperar los datos
        Try
            'ensamblamos la consulta sql
            Dim cConsulta As String = _Select_sql.ToUpper().Replace("SELECT", "SELECT DISTINCT")
            cConsulta += String.Format(" INNER JOIN {0} ON {1}.{2} = {0}.{3} ORDER BY {1}.{5}" _
                                        , InventariosTBL.NOMBRE_TABLA _
                                        , LocalesTBL.NOMBRE_TABLA _
                                        , LocalesTBL.ID.cNombre _
                                        , InventariosTBL.ID_LOCAL.cNombre _
                                        , LocalesTBL.ID_EN_SISTEMA.cNombre _
                                        , LocalesTBL.DESCRIPCION.cNombre
                                        )

            'busqueda filtrada
            lResultado = _oConector.lEjecutar_consulta(cConsulta)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'instancias auxiliares
                Dim lLocales As New List(Of LocalOTD)()
                Dim dtResultado As DataTable = CType(lResultado(1), DataTable)
                Dim oEmpAux As New EmpresaOTD()
                Dim oSisAux As New SistemaOTD()

                'recorremos las filas devueltas
                For Each dr As DataRow In dtResultado.Rows
                    Dim oEmpresa As New EmpresaOTD()
                    Dim oSistema As New SistemaOTD()

                    'recuperamos las instancias asociadas
                    oEmpresa.nId = Integer.Parse(dr(LocalesTBL.ID_EMPRESA.nIndice).ToString())
                    oSistema.nId = Integer.Parse(dr(LocalesTBL.ID_SISTEMA.nIndice).ToString())

                    'solo si son diferentes
                    If oEmpAux.nId = 0 And Not oEmpAux.nId = oEmpresa.nId Then
                        lResultado = __oApModelo.Empresas_ADM.lGet_elemento(oEmpresa)
                        If lResultado(0).Equals(1) Then oEmpresa = CType(lResultado(1), EmpresaOTD)

                    ElseIf oEmpAux.nId > 0 Then
                        oEmpresa = oEmpAux
                    End If

                    'solo si son diferentes
                    If oSisAux.nId = 0 And Not oSisAux.nId = oSistema.nId Then
                        lResultado = __oApModelo.Sistemas_ADM.lGet_elemento(oSistema)
                        If lResultado(0).Equals(1) Then oSistema = CType(lResultado(1), SistemaOTD)

                    ElseIf oSisAux.nId > 0 Then
                        oSistema = oSisAux
                    End If

                    'lo agregamos a la lista
                    lLocales.Add(New LocalOTD(Long.Parse(dr(LocalesTBL.ID.nIndice).ToString()) _
                                            , dr(LocalesTBL.DESCRIPCION.nIndice).ToString() _
                                            , oSistema _
                                            , Integer.Parse(dr(LocalesTBL.ID_EN_SISTEMA.nIndice).ToString()) _
                                            , Integer.Parse(dr(LocalesTBL.ID_TIPO_LOCAL.nIndice).ToString()) _
                                            , oEmpresa _
                                            , dr(LocalesTBL.NOMBRE_CLAVE.nIndice).ToString() _
                                           ))

                Next

                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, lLocales})

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
