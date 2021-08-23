Imports CdgPersistencia
Imports CdgPersistencia.ClasesBases

Public Class ApModeloSQLite


#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "ApModeloSQLite"

    Private Shared __oInstancia As ApModeloSQLite

    Private __oConector As SQLiteUtiles
    Private __cPathSQLite As String

    'administradores de operaciones sobre tablas
    Private __oConfiguracionesADM As ConfiguracionesSQLiteADM
    Private __oLocacionesADM As LocacionesSQLiteADM
    Private __oUsuariosADM As UsuariosSQLiteADM

    Private __oSoportesADM As SoportesSQLiteADM
    Private __oSectoresADM As SectoresSQLiteADM
    Private __oMaestroADM As MaestroSQLiteADM
    Private __oLecturasADM As LecturasSQLiteADM



#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub New()

        __Inicializar_componentes()

    End Sub

    ''' <summary>
    ''' Ejecuta la inicializacion de los componentes internos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __Inicializar_componentes()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".__Inicializar_componentes()"

        'recuperamos los datos necesarios para formar el path del archivo SQLite
        __cPathSQLite = Application.StartupPath & Config.Get_instancia().get_valor(Config.ORIGEN_SQLITE)
        __cPathSQLite += Config.Get_instancia().get_valor(Config.NOMBRE_SQLITE)

        'creamos una instancia de la conexion a la base de datos
        __oConector = New SQLiteUtiles(__cPathSQLite)

        'probamos abrir la conexion
        Dim lResultado As List(Of Object) = __oConector.lConectar()

        'si no se pudo establecer la conexion 
        If Not CType(lResultado(0), Integer) = 1 Then
            'mensaje de notificacion
            MessageBox.Show(lResultado(1).ToString(), NOMBRE_METODO, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)

        End If

    End Sub



#End Region


#Region "ESPECIFICOS"

    ''' <summary>
    ''' Ejecuta la eliminacion y creacion de las tablas mas grandes de la bd
    ''' </summary>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Public Function lResetear_tablas() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".lResetear_tablas()"

        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'intentamos volver a crear todas las tablas que asi lo requieran
            lResultado = Maestro_ADM().lCrear_tabla()

            'si se ejecuto correctamente, procedemos con la siguiente
            If lResultado(0).Equals(1) Then lResultado = Usuarios_ADM().lCrear_tabla()

            'al final, intentamos compactar la base de datos
            Get_conector().lEjecutar_sentencia("VACUUM")

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

#End Region


#Region "GETTERS Y SETTERS"

    ''' <summary>
    ''' Devuelve una instancia de la clase
    ''' </summary>
    ''' <returns>Devuelve una instancia de la clase</returns>
    ''' <remarks></remarks>
    Public Shared Function Get_instancia() As ApModeloSQLite

        If __oInstancia Is Nothing Then __oInstancia = New ApModeloSQLite()
        Return __oInstancia

    End Function

    ''' <summary>
    ''' Devuelve una instancia de la Utileria de Conexion a la BD
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_conector() As SQLiteUtiles
        Return __oConector
    End Function

    ''' <summary>
    ''' Devuelve el path absoluto al archivo SQLite
    ''' </summary>
    ''' <value></value>
    ''' <returns>Path absoluto al archivo</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Get_path_sqlite() As String
        Get
            Return __cPathSQLite
        End Get
    End Property


#End Region



#Region "ADMINISTRADORES"

    ''' <summary>
    ''' Devuelve una instancia del administrador de operaciones sobre la tabla CONFIGURACIONES
    ''' </summary>
    ''' <value>Nothing</value>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Configuraciones_ADM() As ConfiguracionesSQLiteADM
        Get
            If __oConfiguracionesADM Is Nothing Then __oConfiguracionesADM = New ConfiguracionesSQLiteADM(Get_instancia())
            Return __oConfiguracionesADM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una instancia del administrador de operaciones sobre la tabla LOCACIONES
    ''' </summary>
    ''' <value>Nothing</value>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Locaciones_ADM() As LocacionesSQLiteADM
        Get
            If __oLocacionesADM Is Nothing Then __oLocacionesADM = New LocacionesSQLiteADM(Get_instancia())
            Return __oLocacionesADM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una instancia del administrador de operaciones sobre la tabla SOPORTES
    ''' </summary>
    ''' <value>Nothing</value>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Soportes_ADM() As SoportesSQLiteADM
        Get
            If __oSoportesADM Is Nothing Then __oSoportesADM = New SoportesSQLiteADM(Get_instancia())
            Return __oSoportesADM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una instancia del administrador de operaciones sobre la tabla SECTORES
    ''' </summary>
    ''' <value>Nothing</value>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Sectores_ADM() As SectoresSQLiteADM
        Get
            If __oSectoresADM Is Nothing Then __oSectoresADM = New SectoresSQLiteADM(Get_instancia())
            Return __oSectoresADM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una instancia del administrador de operaciones sobre la tabla USUARIOS
    ''' </summary>
    ''' <value>Nothing</value>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Usuarios_ADM() As UsuariosSQLiteADM
        Get
            If __oUsuariosADM Is Nothing Then __oUsuariosADM = New UsuariosSQLiteADM(Get_instancia())
            Return __oUsuariosADM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una instancia del administrador de operaciones sobre la tabla MAESTRO de articulos
    ''' </summary>
    ''' <value>Nothing</value>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Maestro_ADM() As MaestroSQLiteADM
        Get
            If __oMaestroADM Is Nothing Then __oMaestroADM = New MaestroSQLiteADM(Get_instancia())
            Return __oMaestroADM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una instancia del administrador de operaciones sobre la tabla LECTURAS
    ''' </summary>
    ''' <value>Nothing</value>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Lecturas_ADM() As LecturasSQLiteADM
        Get
            If __oLecturasADM Is Nothing Then __oLecturasADM = New LecturasSQLiteADM(Get_instancia())
            Return __oLecturasADM
        End Get
    End Property

#End Region


End Class
