Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class SectoresSQLiteADM
    Inherits ADMbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "SectoresSQLiteADM"

    Private __oApModelo As ApModeloSQLite


#End Region


#Region "CONSTRUCTORES"

    Public Sub New(ByVal ApModeloParam As ApModeloSQLite)
        MyBase.New(ApModeloParam.Get_conector())

        __oApModelo = ApModeloParam
        Set_tabla(New SectoresSQLiteTBL())

        _Insert_sql = _Insert_sql.ToUpper().Replace("INSERT", "INSERT OR REPLACE")


    End Sub

#End Region


#Region "Miembros de ADMbase"

    Public Overrides Function lAgregar(ByVal oSectorParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lAgregar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'casteamos el parametro a su tipo
            Dim oSector As SectorSQLiteOTD = CType(oSectorParam, SectorSQLiteOTD)

            'ensamblamos la sentencia de actualizacion
            Dim cInsert As String = String.Format(_Insert_sql, oSector.nId, oSector.cDescripcion)

            'ejecutamos la busqueda filtrada
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

    Public Overrides Function lEliminar(ByVal oOTDbase As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lEliminar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'ejecutamos la busqueda filtrada
            lResultado = _oConector.lEjecutar_sentencia(_Delete_sql)

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
        Throw New NotImplementedException()
    End Function

#End Region


#Region "ESPECIFICOS"

    ''' <summary>
    ''' Ejecuta la creacion de la tabla administrada
    ''' </summary>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Public Function lCrear_tabla() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lCrear_tabla()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'intentamos recuperar los datos
        Try
            'ejecutamos el comando de eliminacion de la tabla
            lResultado = _oConector.lEjecutar_sentencia(SectoresSQLiteTBL.SQL_DROP_TABLA)

            'si se ejecuto correctamente, creamos de vuelta la tabla
            If lResultado(0).Equals(1) Then lResultado = _oConector.lEjecutar_sentencia(SectoresSQLiteTBL.SQL_CREAR_TABLA)

        Catch ex As Exception

            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado


    End Function

    ''' <summary>
    ''' Ejecuta una insercion masiva de datos a la tabla SECTORES
    ''' <summary>
    ''' <param name="dtTablaParam">Instancia de DataTable origen</param>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Public Function lBulk_insert(ByVal dtTablaParam As DataTable) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lBulk_insert()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'creamos una instancia de la clase de insercion masiva
            Dim bulkCopy As New SQLiteBulkInsert(__oApModelo.Get_conector().Get_conexion(), _oTabla.Get_nombre_tabla())

            Dim aColumnas(dtTablaParam.Columns.Count - 1) As DataColumn
            Dim nC As Integer = 0

            'agregamos los parametros requeridos
            bulkCopy.AddParameter(SectoresSQLiteTBL.ID.cNombre, DbType.String)
            bulkCopy.AddParameter(SectoresSQLiteTBL.DESCRIPCION.cNombre, DbType.String)

            'recorremos las columnas de la tabla
            For Each dc As DataColumn In dtTablaParam.Columns
                'si la columna actual no esta contemplada
                If (Not dc.ColumnName.Equals(SectoresTBL.ID.cNombre)) _
                And (Not dc.ColumnName.Equals(SectoresTBL.DESCRIPCION.cNombre)) Then
                    aColumnas(nC) = dc
                Else
                    aColumnas(nC) = Nothing
                End If
                nC += 1
            Next

            'eliminamos las columnas sobrantes
            For nC = 0 To (aColumnas.Length - 1)
                If Not (aColumnas(nC) Is Nothing) Then dtTablaParam.Columns.Remove(aColumnas(nC))
            Next

            'renombramos las columnas
            dtTablaParam.Columns(SectoresTBL.ID.cNombre).ColumnName = SectoresSQLiteTBL.ID.cNombre
            dtTablaParam.Columns(SectoresTBL.DESCRIPCION.cNombre).ColumnName = SectoresSQLiteTBL.DESCRIPCION.cNombre

            'recorremos las filas del DataTable parametro
            For Each dr As DataRow In dtTablaParam.Rows
                'llamamos al metodo de insercion
                bulkCopy.Insert(dr.ItemArray)

            Next

            'llamamos al metodo de confirmacion de transaccion
            bulkCopy.Flush()

            'resultado del metodo
            lResultado = New List(Of Object)(New Object() {1, "Ok"})

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

#End Region

End Class
