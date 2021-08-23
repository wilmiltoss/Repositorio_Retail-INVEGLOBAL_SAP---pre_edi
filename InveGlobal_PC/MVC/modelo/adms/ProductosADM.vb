Imports CdgPersistencia.ClasesBases

Public Class ProductosADM
    Inherits ADMbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "ProductosADM"

    Private __oApModelo As ApModelo


#End Region


#Region "CONSTRUCTORES"

    Public Sub New(ByVal ApModeloParam As ApModelo)
        MyBase.New(ApModeloParam.Get_conector())

        __oApModelo = ApModeloParam
        Set_tabla(New ProductosTBL())

    End Sub

#End Region


#Region "Miembros de ADMbase"

    Public Overrides Function lAgregar(ByVal oLocacionParam As OTDbase) As List(Of Object)
        Throw New NotImplementedException()
    End Function

    Public Overrides Function lActualizar(ByVal oOTDbase As OTDbase) As List(Of Object)
        Throw New NotImplementedException()
    End Function

    Public Overrides Function lEliminar(ByVal oOTDbase As OTDbase) As List(Of Object)
        Throw New NotImplementedException()
    End Function

    Public Overrides Function lGet_elemento(ByVal oProductoParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lGet_elemento()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'casteamos el parametro a su tipo
            Dim oProducto As ProductoOTD = CType(oProductoParam, ProductoOTD)

            'sentencia de filtrado
            Dim cFiltroWhere As String = String.Format(_oTabla.Get_filtro_where_int, ProductosTBL.ID.cNombre, oProducto.nId)

            'si el identificador del parametro NO es valido
            If oProducto.nId <= 0 Then
                'evaluamos el scanning
                If Not oProducto.cScanning.Trim().Equals(String.Empty) Then
                    'tomamos el scanning como condicion de filtrado
                    cFiltroWhere = String.Format(" WHERE {0} = '{1}'", ProductosTBL.SCANNING.cNombre, oProducto.cScanning.Trim())
                End If
            End If

            'llamammos al metodo de recuperacion de elementos
            lResultado = lGet_elementos(cFiltroWhere)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'tomamos la lista devuelta
                Dim lProductos As List(Of ProductoOTD) = CType(lResultado(1), List(Of ProductoOTD))

                'solo devolvemos el primero
                lResultado = New List(Of Object)(New Object() {1, lProductos(0)})

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

        Try
            'ejecutamos la busqueda filtrada
            lResultado = _oConector.lEjecutar_consulta(_Select_sql & cFiltroWhere)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'tomamos la tabla devuelta
                Dim dtTabla As DataTable = CType(lResultado(1), DataTable)
                Dim lProductos As New List(Of ProductoOTD)()

                'recorremos las filas de la tabla
                For Each dr As DataRow In dtTabla.Rows
                    'creamos una nueva instancia y la agregamos a la lista
                    lProductos.Add(New ProductoOTD(Long.Parse(dr(ProductosTBL.ID.nIndice).ToString()) _
                                                    , dr(ProductosTBL.DESCRIPCION.nIndice).ToString() _
                                                    , dr(ProductosTBL.SCANNING.nIndice).ToString() _
                                                    , dr(ProductosTBL.DETALLE.nIndice).ToString() _
                                                    , Boolean.Parse(dr(ProductosTBL.PESABLE.nIndice).ToString()) _
                                                    ) _
                                    )
                Next

                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, lProductos})

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
    ''' Ejecuta una consulta especifica y devuelve el maestro de articulos del
    ''' inventario
    ''' </summary>
    ''' <param name="oInventarioParam">Instancia de Invnetario cuyo maestro recuperar</param>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Public Function lGet_maestro_inventario(ByVal oInventarioParam As InventarioOTD) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lGet_maestro_inventario()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'consulta a ejecutar
            Dim cConsultaSql As String = "SELECT {0} FROM {1} " _
                                            & " INNER JOIN {2} ON {1}.{3} = {2}.{4} " _
                                            & " INNER JOIN {5} ON {2}.{6} = {5}.{7} " _
                                            & " WHERE {2}.{8} = {9} "

            Dim cCampos As String = String.Format("{0}, {1}.{2}, SUBSTRING({1}.{3}, 1, 3) AS {3} " _
                                                  , _oTabla.Lista_de_campos_tabla() _
                                                  , InventarioProductoSectoresTBL.NOMBRE_TABLA _
                                                  , InventarioProductoSectoresTBL.MONTO.cNombre _
                                                  , InventarioProductoSectoresTBL.ID_SECTOR.cNombre _
                                                )

            'formateamos la consulta
            cConsultaSql = String.Format(cConsultaSql, cCampos, _oTabla.Get_nombre_tabla() _
                                         , InventarioProductosTBL.NOMBRE_TABLA, ProductosTBL.ID.cNombre, InventarioProductosTBL.ID_PRODUCTO.cNombre _
                                         , InventarioProductoSectoresTBL.NOMBRE_TABLA, InventarioProductosTBL.ID.cNombre, InventarioProductoSectoresTBL.ID.cNombre _
                                         , InventarioProductosTBL.ID_INVENTARIO.cNombre _
                                         , oInventarioParam.nId _
                                         )

            'ejecutamos la busqueda filtrada
            lResultado = _oConector.lEjecutar_consulta(cConsultaSql)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'tomamos la tabla devuelta
                Dim dtTabla As DataTable = CType(lResultado(1), DataTable)

                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, dtTabla})

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
