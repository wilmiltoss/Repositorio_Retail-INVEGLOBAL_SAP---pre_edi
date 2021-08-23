Imports CdgPersistencia
Imports CdgPersistencia.ClasesBases

Public Class Config


#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "Config"

    Private Shared __oInstancia As Config

    Private __oConexion As SQLiteUtiles
    Private __dicParametros As Dictionary(Of String, String)

    'parametros de conexion a bd
    Public Const SERVIDOR_DB As String = "SERVIDOR_BD"
    Public Const CATALOGO_BD As String = "CATALOGO_BD"
    Public Const USUARIO_DB As String = "ID_USUARIO"
    Public Const CONTRASENA_BD As String = "FRASE_MAGICA"
    Public Const TIMEOUT_BD As String = "TIEMPO_ESPERA"

    'parametros de aplicacion local
    Public Const BAT_MONTAR_UNIDAD As String = "BAT_MONTAJE"
    Public Const UNIDAD_MONTADA As String = "UNIDAD_MONTADA"

    'bd sqlite
    Public Const ORIGEN_SQLITE As String = "ORIGEN_SQLITE"
    Public Const DESTINO_SQLITE As String = "DESTINO_SQLITE"
    Public Const NOMBRE_SQLITE As String = "NOMBRE_SQLITE"

    Public Const ORIGEN_LECTURAS As String = "ORIGEN_LECTURAS"
    Public Const DESTINO_LECTURAS As String = "DESTINO_LECTURAS"

    'otros parametros
    Public Const LINK_AYUDA As String = "LINK_AYUDA"


    'datos de la clase        
    Private Const __CADENA_CONEXION As String = "Data Source={0}; Version=3; Journal Mode=Off; Pooling=False;Max Pool Size=100;"
    Private Const __NOMBRE_TABLA As String = "config"
    Private Const __CAMPO_PARAMETROS As String = "parametro"
    Private Const __CAMPO_VALORES As String = "valor"

    Private Const __CONSULTA_PARAMETROS As String = "SELECT {0}, {1} FROM {2}"



#End Region


#Region "CONSTRUCTORES"

    Private Sub New()

        __Inicializar_componentes()

    End Sub


    Private Sub __Inicializar_componentes()

        'creamos una instancia de la conexion a la base de datos
        __oConexion = New SQLiteUtiles(CurDir() & "\config.sqlite")

        __dicParametros = New Dictionary(Of String, String)

        'ejecutamos la carga de los parametros de configuracion
        __cargar_parametros()


    End Sub



#End Region


#Region "METODOS DE LA CLASE"

    ''' <summary>
    ''' Ejectuca la carga delos parametros de configuracion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __cargar_parametros()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__cargar_parametros()"

        'abrimos la conexion al archivo
        Dim lResultado As List(Of Object) = __oConexion.lConectar()

        'si se conecto sin problemas
        If CType(lResultado(0), Integer) = 1 Then
            'ejecutamos la consulta de recuperacion de parametros
            lResultado = __oConexion.lEjecutar_consulta(String.Format(__CONSULTA_PARAMETROS _
                                                                        , __CAMPO_PARAMETROS _
                                                                        , __CAMPO_VALORES _
                                                                        , __NOMBRE_TABLA) _
                                                        )



            'si se ejecuto correctamente
            If CType(lResultado(0), Integer) = 1 Then
                Dim dtResultado As DataTable = CType(lResultado(1), DataTable)

                'recorremos las filas de la tabla devuelta
                For Each dr As DataRow In dtResultado.Rows

                    'cargamos el par al diccionario
                    __dicParametros.Add(dr(0).ToString().ToUpper(), dr(1).ToString())

                Next

            Else

                'sino, mensaje de notificacion
                MessageBox.Show(lResultado(1).ToString(), NOMBRE_METODO, MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1)

            End If

            'cerramos la conexion
            __oConexion.lDesconectar(NOMBRE_METODO)

        Else
            'sino, mensaje de notificacion
            MessageBox.Show(lResultado(1).ToString(), NOMBRE_METODO, MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1)

        End If

    End Sub



#End Region


#Region "GETTERS Y SETTERS"

    ''' <summary>
    ''' Devuelve una instancia de la clase
    ''' </summary>
    ''' <returns>Instancia de la clase</returns>
    ''' <remarks></remarks>
    Public Shared Function Get_instancia() As Config
        If __oInstancia Is Nothing Then
            __oInstancia = New Config()
        End If

        Return __oInstancia
    End Function



    ''' <summary>
    ''' Devuelve el valor del parametro de configuracion Solicitado
    ''' </summary>
    ''' <param name="cParametro">Nombre del Parametro</param>
    ''' <returns>Valor del Parametro, o en su defecto el nombre de la clase</returns>
    ''' <remarks></remarks>
    Public Function get_valor(ByVal cParametro As String) As String

        If __dicParametros.ContainsKey(cParametro) Then
            Return __dicParametros.Item(cParametro)
        Else
            Return NOMBRE_CLASE
        End If

    End Function



#End Region


End Class