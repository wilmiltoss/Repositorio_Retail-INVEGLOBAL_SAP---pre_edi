Public Class FrmColectores

    Private __oApModelo As ApModelo
    Private __oApControlador As ApControlador

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="oApControlador">Referencia a la instancia del controlador de la aplicacion</param>
    ''' <param name="oApModelo">Referencia a la instancia del modelo de datos de la aplicacion</param>
    ''' <remarks></remarks>
    Public Sub New(ByRef oApControlador As ApControlador, ByRef oApModelo As ApModelo)

        ' Llamada necesaria para el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        __oApModelo = oApModelo
        __oApControlador = oApControlador

    End Sub

    Private Sub frm_colectores_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'asignamos el origen de datos del combo de colectores
        Me.cmb_colectores.DataSource = __oApModelo.ListaColectores()
        Me.cmb_colectores.DisplayMember = "ToString"

    End Sub

    ''' <summary>
    ''' Cuando se hace click en el boton [Aceptar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_aceptar_Click(sender As System.Object, e As System.EventArgs) Handles btn_aceptar.Click

        'si hay un item seleccionado
        If Not Me.cmb_colectores.SelectedItem Is Nothing Then
            'lo pasamos al modelo
            __oApControlador.Colector_OTD = CType(Me.cmb_colectores.SelectedItem, ColectorOTD)

            'establecemos el resultado del dialogo
            Me.DialogResult = DialogResult.OK
        Else
            'sino, nada
            Me.DialogResult = DialogResult.Abort
        End If

    End Sub

    ''' <summary>
    ''' Cuando se hace click en el boton [Cancelar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_cancelar_Click(sender As System.Object, e As System.EventArgs) Handles btn_cancelar.Click

        'establecemos el resultado
        Me.DialogResult = DialogResult.Abort

        'cerramos la ventana
        Me.Close()

    End Sub

End Class