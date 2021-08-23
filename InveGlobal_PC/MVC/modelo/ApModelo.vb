Imports CdgPersistencia
Imports CdgPersistencia.ClasesBases
Imports System.IO
Imports OpenNETCF.Data.Text

Public Class ApModelo


#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "ApModelo"

    Private Shared __oInstancia As ApModelo

    Private __oConector As ConectorBase
    Private __oConectorOracle As ConectorBase



    'administradores de operaciones sobre tablas
    Private __UsuariosADM As UsuariosADM
    Private __LocalesADM As LocalesADM
    Private __InventariosADM As InventariosADM
    Private __EmpresasADM As EmpresasADM

    Private __SectoresADM As SectoresADM
    Private __SistemasADM As SistemasADM
    Private __InventariosSectoresADM As InventariosSectoresADM
    Private __ConfiguracionesADM As ConfiguracionesADM

    Private __TmpMaestroArticulosADM As TmpMaestroArticulosADM
    Private __LocacionesADM As LocacionesADM
    Private __SoportesADM As SoportesADM
    Private __ProductosADM As ProductosADM

    Private __DetallesConteosTmpADM As DetallesConteosTmpADM
    Private __VwDetallesConteosADM As VwDetallesConteosADM
    Private __CargasManualesADM As CargasManualesADM
    Private __EntradaColectorasADM As EntradasColectorasADM

    Private __ExistenciasVwADM As ExistenciasVwADM
    Private __ExistenciasVwADM_SAP As ExistenciasVwADM_SAP

    Private __NivelesAccesosADM As NivelesAccesosADM
    Private __UsuariosLocalesADM As UsuariosLocalesADM

    'desde EMERETAIL
    Private __oFotosEmeADM As FotosEmeADM

    Private __lColectores As List(Of ColectorOTD)
    Public NOMBRE_ARCHIVO_COLECTORES As String = Environment.GetEnvironmentVariable("HOMEPATH") + "\\misColectores.csv"


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

        'tomamos una instancia del administrador de configuraciones
        Dim oConfig As Config = Config.Get_instancia()

        'una instancia para evaluar el tiempo de espera
        Dim nTiempoEspera As Integer = Integer.TryParse(oConfig.get_valor(Config.TIMEOUT_BD), 300)

        'creamos una instancia de la conexion a la base de datos
        __oConector = New SQLServerUtiles(oConfig.get_valor(Config.SERVIDOR_DB), oConfig.get_valor(Config.CATALOGO_BD) _
                                        , oConfig.get_valor(Config.USUARIO_DB), oConfig.get_valor(Config.CONTRASENA_BD) _
                                        , nTiempoEspera)

        'probamos abrir la conexion
        Dim lResultado As List(Of Object) = __oConector.lConectar()

        If CType(lResultado(0), Integer) = 1 Then
            Me.lCargar_archivo_colectores()
        End If

        'si no se pudo establecer la conexion 
        If Not CType(lResultado(0), Integer) = 1 Then
            'mensaje de notificacion
            MessageBox.Show(lResultado(1).ToString(), NOMBRE_METODO, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)

        End If

    End Sub



#End Region



#Region "METODOS"

    ''' <summary>
    ''' Ejecutala lectura del archivo de datos de Colectores en el equipo local
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function lCargar_archivo_colectores() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".lCargar_archivo_colectores()"

        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Dim cMensaje As String = "Ok"

        Try

            'si el archivo de colectores no existe, lo creamos
            If Not File.Exists(NOMBRE_ARCHIVO_COLECTORES) Then
                Dim oNuevo As FileStream = File.Create(NOMBRE_ARCHIVO_COLECTORES)
                oNuevo.Close()
                oNuevo.Dispose()

                'establecemos el mensaje de notificacion
                cMensaje = "El archivo " + NOMBRE_ARCHIVO_COLECTORES + " fue creado ahora porque no se encontro una version previa..."

            End If

            ' creamos un data set para cargar el contenido del archivo de colectores
            Using dsMaestro As DataSet = New DataSet()

                '"conectamos" al archivo
                Dim oAdaptadorCSV As TextDataAdapter = New TextDataAdapter(NOMBRE_ARCHIVO_COLECTORES, True, ";")
                Dim nIdx As Integer = 0

                'cargamos los datos al DS
                oAdaptadorCSV.Fill(dsMaestro, "colectores")

                'liberamos el archivo
                oAdaptadorCSV = Nothing

                __lColectores = New List(Of ColectorOTD)

                For Each drFila As DataRow In dsMaestro.Tables("colectores").Rows
                    __lColectores.Add(New ColectorOTD(nIdx, drFila(0).ToString(), drFila(1).ToString()))
                    nIdx += 1
                Next

                lResultado(0) = 1
                lResultado(1) = cMensaje


            End Using


        Catch ex As Exception
            ' en caso de excepciones
            lResultado(0) = -1
            lResultado(1) = NOMBRE_METODO + ": " + ex.Message

        End Try

        ' devolvemos el resultado del metodo
        Return lResultado

    End Function


#End Region



#Region "GETTERS Y SETTERS"

    ''' <summary>
    ''' Devuelve una instancia de la clase
    ''' </summary>
    ''' <returns>Devuelve una instancia de la clase</returns>
    ''' <remarks></remarks>
    Public Shared Function Get_instancia() As ApModelo

        If __oInstancia Is Nothing Then __oInstancia = New ApModelo()
        Return __oInstancia

    End Function

    ''' <summary>
    ''' Devuelve una instancia de la utileria de Conexion a la Base de Datos
    ''' </summary>
    ''' <returns>instancia de la utileria de Conexion</returns>
    ''' <remarks></remarks>
    Public Function Get_conector() As ConectorBase
        Return __oConector
    End Function

    ''' <summary>
    ''' Devuelve o establece la instancia de conexion a la BD Oracle
    ''' </summary>
    ''' <value>Instancia de Oracle Utiles</value>
    ''' <returns>Instancia de Oracle Utiles</returns>
    ''' <remarks></remarks>
    Public Property Get_conexion_oracle() As ConectorBase
        Get
            Return __oConectorOracle
        End Get
        Set(ByVal value As ConectorBase)
            __oConectorOracle = value
        End Set
    End Property


    ''' <summary>
    ''' Devuelve una instancia del administrador de operaciones sobre la tabla USUARIOS
    ''' </summary>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Usuarios_ADM() As UsuariosADM
        Get
            If __UsuariosADM Is Nothing Then __UsuariosADM = New UsuariosADM(Get_instancia())
            Return __UsuariosADM

        End Get

    End Property

    ''' <summary>
    ''' Devuelve una instancia del administrador de operaciones sobre la tabla LOCALES
    ''' </summary>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Locales_ADM() As LocalesADM
        Get
            If __LocalesADM Is Nothing Then __LocalesADM = New LocalesADM(Get_instancia())
            Return __LocalesADM

        End Get

    End Property

    ''' <summary>
    ''' Devuelve una instancia del administrador de operaciones sobre la tabla DATOS_INVENTARIOS
    ''' </summary>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Inventarios_ADM() As InventariosADM
        Get
            If __InventariosADM Is Nothing Then __InventariosADM = New InventariosADM(Get_instancia())
            Return __InventariosADM

        End Get

    End Property

    ''' <summary>
    ''' Devuelve una instancia del administrador de operaciones sobre la tabla EMPRESAS
    ''' </summary>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Empresas_ADM() As EmpresasADM
        Get
            If __EmpresasADM Is Nothing Then __EmpresasADM = New EmpresasADM(Get_instancia())
            Return __EmpresasADM

        End Get

    End Property

    ''' <summary>
    ''' Devuelve una instancia del administrador de operaciones sobre la tabla SISTEMAS
    ''' </summary>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Sistemas_ADM() As SistemasADM
        Get
            If __SistemasADM Is Nothing Then __SistemasADM = New SistemasADM(Get_instancia())
            Return __SistemasADM

        End Get

    End Property

    ''' <summary>
    ''' Devuelve una instancia del administrador de operaciones sobre la tabla SISTEMAS
    ''' </summary>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Sectores_ADM() As SectoresADM
        Get
            If __SectoresADM Is Nothing Then __SectoresADM = New SectoresADM(Get_instancia())
            Return __SectoresADM

        End Get

    End Property

    ''' <summary>
    ''' Devuelve una instancia del administrador de operaciones sobre la tabla INVENTARIO_SECTORES
    ''' </summary>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property InventariosSectores_ADM() As InventariosSectoresADM
        Get
            If __InventariosSectoresADM Is Nothing Then __InventariosSectoresADM = New InventariosSectoresADM(Get_instancia())
            Return __InventariosSectoresADM

        End Get

    End Property

    ''' <summary>
    ''' Devuelve una instancia del administrador de operaciones sobre la tabla CONFIGURACIONES
    ''' </summary>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Configuraciones_ADM() As ConfiguracionesADM
        Get
            If __ConfiguracionesADM Is Nothing Then __ConfiguracionesADM = New ConfiguracionesADM(Get_instancia())
            Return __ConfiguracionesADM

        End Get

    End Property

    ''' <summary>
    ''' Devuelve una instancia del administrador de 
    ''' obtencion de Foto de Inventario desde
    ''' Foto de Inventario desde EmeRetail
    ''' </summary>
    ''' <returns>Instancia del administrador</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FotosEme_ADM() As FotosEmeADM
        Get
            If __oFotosEmeADM Is Nothing Then __oFotosEmeADM = New FotosEmeADM(Me)
            Return __oFotosEmeADM

        End Get
    End Property

    ''' <summary>
    ''' Devuelve una instancia del administrador de
    ''' operaciones sobre la tabla TMP_MAESTRO_ARTICULOS
    ''' </summary>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TmpMaestro_ADM() As TmpMaestroArticulosADM
        Get
            If __TmpMaestroArticulosADM Is Nothing Then __TmpMaestroArticulosADM = New TmpMaestroArticulosADM(Me)
            Return __TmpMaestroArticulosADM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una instancia del administrador de operaciones sobre la tabla LOCACIONES
    ''' </summary>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Locaciones_ADM() As LocacionesADM
        Get
            If __LocacionesADM Is Nothing Then __LocacionesADM = New LocacionesADM(Me)
            Return __LocacionesADM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una instancia del administrador de operaciones sobre la tabla SOPORTES
    ''' </summary>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Soportes_ADM() As SoportesADM
        Get
            If __SoportesADM Is Nothing Then __SoportesADM = New SoportesADM(Me)
            Return __SoportesADM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una instancia del administrador de operaciones sobre la tabla PRODUCTOS
    ''' </summary>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Productos_ADM() As ProductosADM
        Get
            If __ProductosADM Is Nothing Then __ProductosADM = New ProductosADM(Me)
            Return __ProductosADM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una instancia del administrador de 
    ''' operaciones sobre la tabla DETALLES_CONTEOS TEMPORALES
    ''' </summary>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DetallesConteosTmp_ADM() As DetallesConteosTmpADM
        Get
            If __DetallesConteosTmpADM Is Nothing Then __DetallesConteosTmpADM = New DetallesConteosTmpADM(Me)
            Return __DetallesConteosTmpADM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve o establece la instancia del administrador de operaciones 
    ''' sobre la Vista temporal de DETALLES de CONTEOS
    ''' </summary>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public Property VwDetallesConteos_ADM() As VwDetallesConteosADM
        Get
            Return __VwDetallesConteosADM
        End Get
        Set(ByVal value As VwDetallesConteosADM)
            __VwDetallesConteosADM = value
        End Set
    End Property

    ''' <summary>
    ''' Devuelve o establece la instancia del administrador de operaciones 
    ''' sobre la tabla CARGA_MANUAL
    ''' </summary>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CargasManuales_ADM() As CargasManualesADM
        Get
            If __CargasManualesADM Is Nothing Then __CargasManualesADM = New CargasManualesADM(Me)
            Return __CargasManualesADM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve o establece la instancia del administrador de operaciones 
    ''' sobre la tabla ENTRADAS_COLECTORAS
    ''' </summary>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property EntradasColectorasADM() As EntradasColectorasADM
        Get
            If __EntradaColectorasADM Is Nothing Then __EntradaColectorasADM = New EntradasColectorasADM(Me)
            Return __EntradaColectorasADM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve o establece la instancia del administrador de operaciones 
    ''' sobre la VISTA VW_EXISTENCIAS_TOTALES
    ''' </summary>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ExistenciasVwADM() As ExistenciasVwADM
        Get
            If __ExistenciasVwADM Is Nothing Then __ExistenciasVwADM = New ExistenciasVwADM(Me)
            Return __ExistenciasVwADM
        End Get
    End Property


    ''' <summary>
    ''' Devuelve o establece la instancia del administrador de operaciones 
    ''' sobre la VISTA VW_EXISTENCIAS_TOTALES_SAP
    ''' </summary>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ExistenciasVwADM_SAP() As ExistenciasVwADM_SAP
        Get
            If __ExistenciasVwADM_SAP Is Nothing Then __ExistenciasVwADM_SAP = New ExistenciasVwADM_SAP(Me)
            Return __ExistenciasVwADM_SAP
        End Get
    End Property

    ''' <summary>
    ''' Devuelve o establece la instancia del administrador de operaciones 
    ''' sobre la tabla NIVELES_ACCESO
    ''' </summary>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property NivelesAccesosADM() As NivelesAccesosADM
        Get
            If __NivelesAccesosADM Is Nothing Then __NivelesAccesosADM = New NivelesAccesosADM(Me)
            Return __NivelesAccesosADM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve o establece la instancia del administrador de operaciones 
    ''' sobre la tabla USUARIOS_LOCALES
    ''' </summary>
    ''' <returns>Instancia del administrador de operaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UsuariosLocalesADM() As UsuariosLocalesADM
        Get
            If __UsuariosLocalesADM Is Nothing Then __UsuariosLocalesADM = New UsuariosLocalesADM(Me)
            Return __UsuariosLocalesADM
        End Get
    End Property

    Public ReadOnly Property ListaColectores() As List(Of ColectorOTD)
        Get
            If __lColectores Is Nothing Then __lColectores = New List(Of ColectorOTD)
            Return __lColectores
        End Get
    End Property

#End Region



End Class
