Imports CdgPersistencia.ClasesBases

Public Class UsuariosADM
    Inherits ADMbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "UsuariosADM"

    Private __oApModelo As ApModelo


#End Region


#Region "CONSTRUCTORES"

    Public Sub New(ByVal ApModeloParam As ApModelo)
        MyBase.New(ApModeloParam.Get_conector())

        __oApModelo = ApModelo.Get_instancia()
        Set_tabla(New UsuariosTBL())

    End Sub

#End Region

    
#Region "Miembros de ADMbase"

    Public Overrides Function lAgregar(ByVal oUsuarioParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lAgregar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'casteamos el parametro a su tipo
            Dim oUsuario As UsuarioOTD = CType(oUsuarioParam, UsuarioOTD)

            'establecemos la sentencia de actualizacion
            Dim cInsert As String = String.Format(_Insert_sql _
                                                  , oUsuario.nId _
                                                  , oUsuario.cDescripcion _
                                                  , "0x" + BitConverter.ToString(oUsuario.Contrasena).Replace("-", "") _
                                                  , oUsuario.NivelAcceso.nId _
                                                  , oUsuario.Habilitado _
                                                  , "True")

            'ejecutamos la sentencia
            lResultado = _oConector.lEjecutar_sentencia(cInsert)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'recorremos la lista de locales del usuario
                For Each oLocal As LocalOTD In oUsuario.Locales
                    'invocamos al metodo de insercion de asignacion locales
                    lResultado = ApModelo.Get_instancia().UsuariosLocalesADM.lAgregar(New UsuarioLocalOTD(0, oUsuario, oLocal))

                    'si NO se ejecuto correctamente
                    If Not lResultado(0).Equals(1) Then
                        'salimos del proceso
                        Exit For
                    End If
                Next
            End If

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    Public Overrides Function lActualizar(ByVal oUsuarioParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lActualizar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'casteamos el parametro a su tipo
            Dim oUsuario As UsuarioOTD = CType(oUsuarioParam, UsuarioOTD)

            'establecemos la sentencia de actualizacion
            Dim cUpdate As String = String.Format(_Update_sql _
                                                  , oUsuario.nId _
                                                  , oUsuario.cDescripcion _
                                                  , "0x" + BitConverter.ToString(oUsuario.Contrasena).Replace("-", "") _
                                                  , oUsuario.NivelAcceso.nId _
                                                  , oUsuario.Habilitado _
                                                  , "True")

            'ejecutamos la sentencia
            lResultado = _oConector.lEjecutar_sentencia(cUpdate & String.Format(_oTabla.Get_filtro_where_int() _
                                                                                , UsuariosTBL.ID.cNombre _
                                                                                , oUsuario.nId))

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'primero eliminamos todas las asignaciones anteriores
                lResultado = _oConector.lEjecutar_sentencia(String.Format("DELETE FROM {0} WHERE {1} = {2}" _
                                                            , UsuariosLocalesTBL.NOMBRE_TABLA _
                                                            , UsuariosLocalesTBL.ID_USUARIO.cNombre _
                                                            , oUsuario.nId))

                'si se ejecuto correctamente
                If lResultado(0).Equals(1) Then

                    'recorremos la lista de locales del usuario
                    For Each oLocal As LocalOTD In oUsuario.Locales
                        'invocamos al metodo de insercion de asignacion locales
                        lResultado = ApModelo.Get_instancia().UsuariosLocalesADM.lAgregar(New UsuarioLocalOTD(0, oUsuario, oLocal))

                        'si NO se ejecuto correctamente
                        If Not lResultado(0).Equals(1) Then
                            'salimos del proceso
                            Exit For
                        End If
                    Next

                End If
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

    Public Overrides Function lGet_elemento(ByVal oUsuarioParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lGet_elemento()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'intentamos recuperar los datos
        Try
            'establecemos la sentencia de filtrado de registros
            Dim cFiltroWhere As String = String.Format(_oTabla.Get_filtro_where_int(), UsuariosTBL.ID.cNombre, oUsuarioParam.nId)

            'ejecutamos la busqueda filtrada
            lResultado = lGet_elementos(cFiltroWhere)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'instancias auxiliares
                Dim oUsuario As UsuarioOTD = CType(lResultado(1), List(Of UsuarioOTD))(0)

                'formamos el filtro de seleccion de locales a los que esta asignado el usuario
                cFiltroWhere = " WHERE {0}.{1} IN (SELECT {2} FROM {3} WHERE {4} = {5})"
                cFiltroWhere = String.Format(cFiltroWhere _
                                            , LocalesTBL.NOMBRE_TABLA _
                                            , LocalesTBL.ID.cNombre _
                                            , UsuariosLocalesTBL.ID_LOCAL.cNombre _
                                            , UsuariosLocalesTBL.NOMBRE_TABLA _
                                            , UsuariosLocalesTBL.ID_USUARIO.cNombre _
                                            , oUsuario.Id)

                'recuperamos la lista de locales a los que esta asignado
                lResultado = ApModelo.Get_instancia.Locales_ADM.lGet_elementos(cFiltroWhere)

                'si se ejecuto correctamente
                If lResultado(0).Equals(1) Then
                    'tomamos la lista de locales y la pasamos a la instancia del usuario
                    oUsuario.Locales = lResultado(1)
                End If

                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, oUsuario})

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
                Dim lUsuarios As New List(Of UsuarioOTD)()
                Dim dtResultado As DataTable = CType(lResultado(1), DataTable)

                'recorremos las filas devueltas
                For Each dr As DataRow In dtResultado.Rows
                    Dim oNivel As New NivelAccesoOTD()
                    oNivel.nId = Long.Parse(dr(UsuariosTBL.NIVEL_ACCESO.nIndice).ToString())

                    'llamamos al metodo de recuperacion de instancia de nivel de acceso
                    lResultado = __oApModelo.NivelesAccesosADM().lGet_elemento(oNivel)

                    'si se ejecuto correctamente, tomamos la instancia devuelta
                    If lResultado(0).Equals(1) Then oNivel = CType(lResultado(1), NivelAccesoOTD)

                    'creamos la nueva instancia yla agregamos a la lista
                    lUsuarios.Add(New UsuarioOTD(Long.Parse(dr(UsuariosTBL.ID.nIndice).ToString()) _
                                                    , dr(UsuariosTBL.NOMBRE_USUARIO.nIndice).ToString() _
                                                    , dr(UsuariosTBL.CONTRASENA.nIndice) _
                                                    , oNivel _
                                                    , Boolean.Parse(dr(UsuariosTBL.HABILITADO.nIndice).ToString()) _
                                                ))

                Next

                'removemos las columnas que no queremos publicar
                If dtResultado.Columns.Contains(UsuariosTBL.MOSTRAR.cNombre) Then
                    dtResultado.Columns.Remove(UsuariosTBL.MOSTRAR.cNombre)
                End If
                If dtResultado.Columns.Contains(UsuariosTBL.CONTRASENA.cNombre) Then
                    dtResultado.Columns.Remove(UsuariosTBL.CONTRASENA.cNombre)
                End If

                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, lUsuarios, dtResultado})

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
    ''' Valida el inicio de sesion del Usuario parametro
    ''' </summary>
    ''' <param name="oUsuarioParam">Instancia de Usuario OTD a validar</param>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Public Function lValidar_usuario(ByVal oUsuarioParam As UsuarioOTD) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lValidar_usuario()"

        Dim PROCEDIMIENTO As String = "EXEC [dbo].[SP_VALIDAR_USUARIO] @id_usuario = {0}, @contrasena = {1}, @id_local = {2}"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'ensamblamos la consulta sql
            Dim cConsulta As String = String.Format(PROCEDIMIENTO _
                                                    , oUsuarioParam.nId _
                                                    , "0x" + BitConverter.ToString(oUsuarioParam.Contrasena).Replace("-", "") _
                                                    , oUsuarioParam.Locales(0).nId)

            'busqueda filtrada
            lResultado = _oConector.lEjecutar_consulta(cConsulta)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'instancias auxiliares
                Dim lUsuarios As New List(Of UsuarioOTD)()
                Dim dtResultado As DataTable = CType(lResultado(1), DataTable)

                'recorremos las filas devueltas
                For Each dr As DataRow In dtResultado.Rows
                    'si el usuario esta habilitado
                    If Boolean.Parse(dr(UsuariosTBL.HABILITADO.cNombre).ToString()) Then
                        'llamamos al metodo de recuperacion de datos del mismo y devolvemos como resultado del metodo
                        lResultado = lGet_elemento(oUsuarioParam)
                    Else
                        'sino, establecemos el resultado NEGATIVO del metodo
                        lResultado = New List(Of Object)(New Object() {-1, "Usuario o Contraseña no válidos!"})

                    End If

                    'salimos del bucle
                    Exit For

                Next

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
