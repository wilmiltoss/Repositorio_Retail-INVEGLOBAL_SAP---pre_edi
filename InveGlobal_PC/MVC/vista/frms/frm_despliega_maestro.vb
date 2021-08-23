Imports System.Windows.Forms

''' <summary>
''' FORMULARIO DE DESPLIEGUE DE DATOS DEL MASTRO DE ARTICULOS
''' </summary>
''' <remarks></remarks>
Public Class frm_despliega_maestro

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Tomar Maestro]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        'pedimos confirmacion del usuario y solo si la Respuesta es "SI" procedemos
        If MessageBox.Show("Se reemplazarán los Datos Actualmente Cargados!" _
                            & Chr(13) & "¿Esta Seguro de Continuar?", "Maestro de Articulos", _
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            'establecemos el Resultado del Dialogo a "OK"
            Me.DialogResult = System.Windows.Forms.DialogResult.OK

            'cerramos este formulario
            Me.Close()

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Cancelar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        'establecemos el Resultado del Dialogo a "CANCELADO"
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel

        'cerramos este formulario
        Me.Close()

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA DE TAMANO EL FORMULARIO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_despliega_maestro_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        'cambiamos el tamano del DataGrid
        Me.grd_datos_maestro.Height = Me.Height - (Me.grd_datos_maestro.Top + 45 + Me.TableLayoutPanel1.Size.Height)
        Me.grd_datos_maestro.Width = Me.Width - (Me.grd_datos_maestro.Left + 20)

        'movemos el label de Fecha de Toma
        Me.lbl_fecha_toma.Top = Me.grd_datos_maestro.Height + Me.grd_datos_maestro.Location.Y + 5

        'movemos el label de Registros Afectados
        Me.lbl_registros.Top = Me.grd_datos_maestro.Height + Me.grd_datos_maestro.Location.Y + 5

    End Sub

    ''' <summary>
    ''' CUANDO SE CARGA EL FORMULARIO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_despliega_maestro_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'evento a LOG
        'principal.pInfo_a_Log("Se carga el Formulario : " & Me.Name)

    End Sub

End Class
