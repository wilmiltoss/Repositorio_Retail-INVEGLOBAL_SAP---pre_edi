Imports CdgPersistencia.ClasesBases

Public Class TmpMaestroArticulosADM
    Inherits ADMbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "TmpMaestroArticulosADM"

    Private __oApModelo As ApModelo

    'procedimentos almacenados intervinientes en esta tabla
    Public Const SP_CARGAR_MAESTRO_TMP As String = "EXEC [dbo].[SP_CARGAR_MAESTRO_TMP] @archivo_csv = '{0}'"

    Public Const SP_ELIMINAR_PESABLES_Y_DUPLICADOS As String = "EXEC [SP_ELIMINAR_PESABLES_Y_DUPLICADOS] @id_inventario= {0}"
    Public Const SP_ACTUALIZAR_ARTICULO_SECTOR As String = "EXEC [SP_ACTUALIZAR_ARTICULO_SECTOR] @id_inventario= {0}"
    Public Const SP_ELIMINAR_SECTORES_NO_CUBIERTOS As String = "EXEC [SP_ELIMINAR_SECTORES_NO_CUBIERTOS] @id_inventario= {0}"
    Public Const SP_ELIMINAR_ARTICULOS_INVENTARIO As String = "EXEC [SP_ELIMINAR_ARTICULOS_INVENTARIO] @id_inventario= {0}"

    Public Const SP_TOMAR_MAESTRO_ARTICULOS As String = "EXEC [SP_TOMAR_MAESTRO_ARTICULOS] @id_inventario= {0}"
    Public Const SP_CARGAR_INVENTARIO_PRODUCTOS As String = "EXEC [SP_CARGAR_INVENTARIO_PRODUCTOS] @id_inventario= {0}"
    Public Const SP_CARGAR_INVENTARIO_PRODUCTO_SECTOR As String = "EXEC [SP_CARGAR_INVENTARIO_PRODUCTO_SECTOR] @id_inventario= {0}"
    Public Const SP_CARGAR_EXISTENCIA_NUEVO_INVENTARIO As String = "EXEC [SP_CARGAR_EXISTENCIA_NUEVO_INVENTARIO] @id_inventario= {0}"

    Public Const SP_ACTUALIZAR_TEORICOS As String = "EXEC [dbo].[SP_ACTUALIZAR_TEORICOS] @id_inventario = {0}"


#End Region


#Region "CONSTRUCTORES"

    Public Sub New(ByVal ApModeloParam As ApModelo)
        MyBase.New(ApModeloParam.Get_conector())

        __oApModelo = ApModelo.Get_instancia()
        Set_tabla(New TmpMaestroArticulosTBL())

    End Sub



#End Region


#Region "Miembros de ADMbase"

    Public Overrides Function lAgregar(ByVal oTmpMaestroParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lAgregar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'casteamos el parametro a su tipo 
            Dim oTmpMaestro As TmpMaestroArticuloOTD = CType(oTmpMaestroParam, TmpMaestroArticuloOTD)

            'ensamblamos la sentencia del insercion
            Dim cInsertSql As String = String.Format(_Insert_sql, oTmpMaestro.nIdInventario _
                                                    , oTmpMaestro.nScanning _
                                                    , oTmpMaestro.cDescripcion _
                                                    , oTmpMaestro.nCosto.ToString().Replace(",", ".") _
                                                    , oTmpMaestro.cDetalle _
                                                    , oTmpMaestro.nTeorico.ToString().Replace(",", ".") _
                                                    , oTmpMaestro.cIdSector _
                                                    , oTmpMaestro.cPesable _
                                                    )

            'llamamos al procedimiento de ejecucion de sentencias sql
            lResultado = _oConector.lEjecutar_sentencia(cInsertSql)

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

    Public Overrides Function lEliminar(ByVal oTmpMaestroParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lEliminar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'casteamos el parametro a su tipo
            Dim oTmpMaestro As TmpMaestroArticuloOTD = CType(oTmpMaestroParam, TmpMaestroArticuloOTD)

            'establecemos la sentencia de filtrado de registros
            Dim cDeleteSql As String = String.Format(_oTabla.Get_delete_basico _
                                                       , String.Format(" {0}.{1} = {2} ", TmpMaestroArticulosTBL.NOMBRE_TABLA _
                                                                        , TmpMaestroArticulosTBL.ID_INVENTARIO.cNombre _
                                                                        , oTmpMaestro.nIdInventario) _
                                                        )

            'llamamos al procedimiento de ejecucion de sentencias sql
            lResultado = _oConector.lEjecutar_sentencia(cDeleteSql)

        Catch ex As Exception

            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado


    End Function

    Public Overrides Function lGet_elemento(ByVal oSectorParam As OTDbase) As List(Of Object)
        Throw New NotImplementedException()

    End Function

    Public Overrides Function lGet_elementos(ByVal cFiltroWhere As String) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lGet_elementos()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'llamamos al metodo de ejecucion de la consulta sql
            lResultado = _oConector.lEjecutar_consulta(_Select_sql & cFiltroWhere)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'tomamos la tabla devuelta
                Dim dtTabla As DataTable = CType(lResultado(1), DataTable)

                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, New List(Of TmpMaestroArticuloOTD), dtTabla})

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
    ''' Ejecuta la carga masiva del Archivo CSV a la tabla TMP_MAESTRO_ARTICULOS
    ''' Ojo, el archivo debe existir en el directorio donde el SP lo va a buscar
    ''' </summary>
    ''' <param name="cNombreArchivo">Nombre del Archivo a Localizar y Cargar</param>
    ''' <param name="oInventarioParam">Instancia del Inventario actual</param>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Public Function lCarga_masiva(ByVal cNombreArchivo As String, ByVal oInventarioParam As InventarioOTD) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lCarga_masiva()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'llamamos al procedimiento de ejecucion de sentencias sql
            : lResultado = _oConector.lEjecutar_sentencia(String.Format(SP_CARGAR_MAESTRO_TMP, cNombreArchivo))

            Application.DoEvents()

            'si se ejecuto correctamente, eliminamos los pesables y duplicados
            If lResultado(0).Equals(1) Then lResultado = _oConector.lEjecutar_sentencia(String.Format(SP_ELIMINAR_PESABLES_Y_DUPLICADOS, oInventarioParam.nId))

            Application.DoEvents()

            'si se ejecuto correctamente, actualizamos la asociacion articulo-sector
            If lResultado(0).Equals(1) Then lResultado = _oConector.lEjecutar_sentencia(String.Format(SP_ACTUALIZAR_ARTICULO_SECTOR, oInventarioParam.nId))

            Application.DoEvents()

            'si se ejecuto correctamente, eliminamos los sectores no cubiertos
            If lResultado(0).Equals(1) Then lResultado = _oConector.lEjecutar_sentencia(String.Format(SP_ELIMINAR_SECTORES_NO_CUBIERTOS, oInventarioParam.nId))

            Application.DoEvents()

            'si se ejecuto correctamente, eliminamos los articulos del inventario actual
            If lResultado(0).Equals(1) Then lResultado = _oConector.lEjecutar_sentencia(String.Format(SP_ELIMINAR_ARTICULOS_INVENTARIO, oInventarioParam.nId))

            Application.DoEvents()

            'si se ejecuto correctamente, actualizamos el maestrod e articulos tomando aquellos nuevos
            If lResultado(0).Equals(1) Then lResultado = _oConector.lEjecutar_sentencia(String.Format(SP_TOMAR_MAESTRO_ARTICULOS, oInventarioParam.nId))

            Application.DoEvents()

            'si se ejecuto correctamente, asociamos los productos al inventario
            If lResultado(0).Equals(1) Then lResultado = _oConector.lEjecutar_sentencia(String.Format(SP_CARGAR_INVENTARIO_PRODUCTOS, oInventarioParam.nId))

            Application.DoEvents()

            'si se ejecuto correctamente, asociamos los sectores-productos al inventario
            If lResultado(0).Equals(1) Then lResultado = _oConector.lEjecutar_sentencia(String.Format(SP_CARGAR_INVENTARIO_PRODUCTO_SECTOR, oInventarioParam.nId))

            Application.DoEvents()

            'si se ejecuto correctamente, creamos los registros de existencias del inventario
            If lResultado(0).Equals(1) Then lResultado = _oConector.lEjecutar_sentencia(String.Format(SP_CARGAR_EXISTENCIA_NUEVO_INVENTARIO, oInventarioParam.nId))

            Application.DoEvents()

            'si hubo problemas
            If Not lResultado(0).Equals(1) Then
                'en caso de error
                lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & lResultado(1).ToString()})

            End If

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado
    End Function

    ''' <summary>
    ''' Ejecuta la carga masiva del Archivo CSV a la tabla TMP_MAESTRO_ARTICULOS
    ''' Pero no reemplaza los registros de la tabla de Existencias, sólo actualiza el campo TEORICO
    ''' </summary>
    ''' <param name="cNombreArchivo">Nombre del Archivo a Localizar y Cargar</param>
    ''' <param name="oInventarioParam">Instancia del Inventario actual</param>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Public Function lActualizacion_masiva(ByVal cNombreArchivo As String, ByVal oInventarioParam As InventarioOTD) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lActualizacion_masiva()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'llamamos al procedimiento de ejecucion de sentencias sql
            lResultado = _oConector.lEjecutar_sentencia(String.Format(SP_CARGAR_MAESTRO_TMP, cNombreArchivo))

            Application.DoEvents()

            'si se ejecuto correctamente, eliminamos los pesables y duplicados
            If lResultado(0).Equals(1) Then lResultado = _oConector.lEjecutar_sentencia(String.Format(SP_ELIMINAR_PESABLES_Y_DUPLICADOS, oInventarioParam.nId))

            Application.DoEvents()

            'si se ejecuto correctamente, actualizamos la asociacion articulo-sector
            If lResultado(0).Equals(1) Then lResultado = _oConector.lEjecutar_sentencia(String.Format(SP_ACTUALIZAR_ARTICULO_SECTOR, oInventarioParam.nId))

            Application.DoEvents()

            'si se ejecuto correctamente, eliminamos los sectores no cubiertos
            If lResultado(0).Equals(1) Then lResultado = _oConector.lEjecutar_sentencia(String.Format(SP_ELIMINAR_SECTORES_NO_CUBIERTOS, oInventarioParam.nId))

            Application.DoEvents()

            'si se ejecuto correctamente, actualizamos las existencias teoricas del inventario actual
            If lResultado(0).Equals(1) Then lResultado = _oConector.lEjecutar_sentencia(String.Format(SP_ACTUALIZAR_TEORICOS, oInventarioParam.nId))

            Application.DoEvents()

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado
    End Function


#End Region



End Class
