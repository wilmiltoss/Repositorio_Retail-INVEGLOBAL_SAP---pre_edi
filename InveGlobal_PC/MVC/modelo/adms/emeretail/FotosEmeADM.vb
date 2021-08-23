Imports CdgPersistencia.ClasesBases
Imports System.Data.OracleClient

Public Class FotosEmeADM
    Inherits ADMbase


#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "FotosEmeADM"

    Private __oApModelo As ApModelo

    Private Const SP_OBTENER_MAESTRO As String = "SP_OBTENER_MAESTRO_INVENTARIO"

    


#End Region


#Region "CONSTRUCTORES"

    Public Sub New(ByVal ApModeloParam As ApModelo)
        MyBase.New(ApModelo.Get_instancia().Get_conexion_oracle())

        __oApModelo = ApModelo.Get_instancia()
        '''Set_tabla(New TBLbase())

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

    Public Overrides Function lGet_elemento(ByVal oOTDbase As OTDbase) As List(Of Object)
        Throw New NotImplementedException()
    End Function

    Public Overrides Function lGet_elementos(ByVal cFiltroWhere As String) As List(Of Object)
        Throw New NotImplementedException()
    End Function

    

#End Region


#Region "ESPECIFICOS"


#End Region

    ''' <summary>
    ''' Ejecuta el procedimiento de recuperacion de cantidades teoricas por articulo
    ''' </summary>
    ''' <param name="oLocalParam">Instancia de LocalOTD</param>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Public Function lGet_foto_inventario(ByVal oLocalParam As LocalOTD) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lGet_foto_inventario()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'auxiliares
        Dim drFila As DataRow = Nothing


        'intentamos recuperar los datos
        Try
            'creamos el arreglo de parametros
            Dim aParametros(4) As OracleParameter

            aParametros(0) = New OracleClient.OracleParameter("PARAM_DESTINATION_ID", OracleType.Number)
            aParametros(1) = New OracleClient.OracleParameter("PARAM_CAT_CODE", OracleType.VarChar)
            aParametros(2) = New OracleClient.OracleParameter("PARAM_REC_QUANTITY", OracleType.Number)
            aParametros(3) = New OracleClient.OracleParameter("TMP_CURSOR", OracleType.Cursor)

            aParametros(0).Value = oLocalParam.nIdEnSistema
            aParametros(1).Value = String.Empty
            aParametros(2).Value = 0
            aParametros(3).Direction = ParameterDirection.Output


            'llamamos al metodo correspondiente
            lResultado = _oConector.lEjecutar_procedimiento(SP_OBTENER_MAESTRO, aParametros)

            'si se ejecuto correctamente4
            If lResultado(0).Equals(1) Then
                'instancias auxiliares
                Dim lItems As New List(Of FotoEmeOTD)()
                Dim dtResultado As DataTable = CType(lResultado(1), DataTable)


                'recorremos las filas devueltas
                For Each dr As DataRow In dtResultado.Rows
                    drFila = dr
                    'lo agregamos a la lista
                    lItems.Add(New FotoEmeOTD(Long.Parse(dr(0).ToString()) _
                                            , dr(1).ToString() _
                                            , Double.Parse(dr(2).ToString()) _
                                            , dr(3).ToString() _
                                            , Double.Parse(dr(4).ToString()) _
                                            , dr(5).ToString() _
                                            , Char.Parse(dr(6).ToString()) _
                                           ))

                Next

                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, lItems})

            End If

        Catch ex As Exception

            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message, drFila})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function


End Class
