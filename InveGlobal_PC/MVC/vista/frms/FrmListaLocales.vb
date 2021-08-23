Imports CdgPersistencia.ClasesBases

Public Class FrmListaLocales

    'atributos
    Public lLocalesSeleccionados As List(Of LocalOTD)

    ''' <summary>
    ''' Constructor de la Clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByRef lLocalesParam As List(Of LocalOTD))

        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()

        'tomamos la lista de locales parametros
        Me.lst_locales.DataSource = lLocalesParam


    End Sub


    ''' <summary>
    ''' Cuando se hace click en el boton [v]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_aceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_aceptar.Click

        'instanciamos la lista nueva para los locales seleccionados
        lLocalesSeleccionados = New List(Of LocalOTD)

        'recorremos los items seleccionados
        For Each oLocal As Object In Me.lst_locales.SelectedItems
            'y los vamos pasando a la lista de locales seleccionados
            lLocalesSeleccionados.Add(oLocal)

        Next

        'establecemos el resultado de este form de dialogo
        Me.DialogResult = Windows.Forms.DialogResult.OK

        'y lo cerramos
        Me.Close()

    End Sub

    ''' <summary>
    ''' Cuando se hace click en el boton [x]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_cancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click

        'establecemos el resultado de este form de dialogo
        Me.DialogResult = Windows.Forms.DialogResult.Cancel

        'y lo cerramos
        Me.Close()

    End Sub

    ''' <summary>
    ''' Cuando se hace click en la Lista de Locales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub lst_locales_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lst_locales.MouseClick

        'si se ha seleccionado al menos un item habilitamos el boton [v]
        Me.btn_aceptar.Enabled = Me.lst_locales.SelectedItems.Count > 0

    End Sub
End Class