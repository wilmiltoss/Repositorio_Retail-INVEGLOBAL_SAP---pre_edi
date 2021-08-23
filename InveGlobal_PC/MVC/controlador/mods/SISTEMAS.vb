Module SISTEMAS

#Region "VARIABLES_PRIVADAS"



    Public oSistemaPrincipal As New SistemaOTD()

    Private __oSistemaTemporal As New SistemaOTD()

    Private cIDLocalEnSistemaPrincipal As String = String.Empty

    Private cIDSistemaSecundario As String = "-1"
    Private cIDLocalEnSistemaSecundario As String = "-1"

    'auxiliares
    Private strSentenciaSQL As String = String.Empty

#End Region

    ''' <summary>
    ''' PREPARA EL MODULO DEL SISTEMA CUYO NOMBRE ES PASADO COMO PARAMETRO
    ''' </summary>
    ''' <param name="oSistemaParam">Instancia del Sistema a Preparar</param>
    ''' <returns>True|False segun el resultado</returns>
    ''' <remarks></remarks>
    Public Function bPreparar_sistema(ByVal oSistemaParam As SistemaOTD) As Boolean

        'luego el ID del Sistema
        oSistemaPrincipal = oSistemaParam

        'evaluamos cual Modulo de Sistema es el que se preparara
        Select Case oSistemaParam.cDescripcion.ToUpper()
            Case "EMERETAIL"
                'llamamos a la funcion de preparacion del Modulo EMERETAIL y devolvemos el resultado de la misma
                Return SISTEMAS.bolPreparar_EME()

            Case "SICS"
                'llamamos a la funcion de preparacion del Modulo EMERETAIL y devolvemos el resultado de la misma
                Return SISTEMAS.bolPreparar_SICS()

            Case Else
                'si el nombre del sistema no esta previsto, evento a LOG
                'principal.pInfo_a_Log("Error intentando Preparar Modulo del Sistema no Previsto : " & strNombreSistema)

                'devolvemos el resultado de la funcion
                Return False

        End Select

    End Function

    ''' <summary>
    ''' ASIGAN LOS VALROES DE LAS VARIABLES LAS SECUNDARIAS A LAS PRINCIPALES Y DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub oficializar_sistema()
        'asignamos el nombre del sistema
        oSistemaPrincipal = __oSistemaTemporal

        cIDLocalEnSistemaPrincipal = cIDLocalEnSistemaSecundario

        'reseteamos el contenido de las variables del sistema secundario
        __oSistemaTemporal = New SistemaOTD()



        cIDLocalEnSistemaSecundario = "-1"

    End Sub

    ''' <summary>
    ''' PREPARA EL MODULO 'EMERETAIL' PARA SU USO Y DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <returns>Devuelve 'True' si se Procedio Correctamente</returns>
    ''' <remarks></remarks>
    Private Function bolPreparar_EME() As Boolean

        'cargamos los Parametros del Sistema de Gestion, si no se pudieron cargar, devolvemos el resultado de la funcion
        If Not EMERETAIL.bCargar_Parametros_Sistema(oSistemaPrincipal.nId.ToString()) Then Return False

        'conectamos a la BD correspondiente, si no conecto, devolvemos el resultado de la funcion
        If Not EMERETAIL.bConectar_EME() Then Return False

        'devolvemos el resultado de la funcion
        Return True

    End Function

    ''' <summary>
    ''' PREPARA EL MODULO 'SICS' PARA SU USO Y DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <returns>Devuelve 'True' si se Procedio Correctamente</returns>
    ''' <remarks></remarks>
    Private Function bolPreparar_SICS() As Boolean

        'cargamos los Parametros del Sistema de Gestion, si no se pudieron cargar, devolvemos el resultado de la funcion
        If Not SICS.bCargar_Parametros_Sistema(oSistemaPrincipal.nId.ToString()) Then Return False

        'conectamos a la BD correspondiente, si no conecto, devolvemos el resultado de la funcion
        If Not SICS.bConectar_SICS() Then Return False

        'devolvemos el resultado de la funcion
        Return True

    End Function

    ''' <summary>
    ''' CREA UN DATATABLE CON EL MAESTRO DE ARTICULOS
    ''' </summary>
    ''' <param name="strID_Local_En_Sistema">ID del Sistema dentro del Sistema de Gestion</param>
    ''' <returns>Devuelve un DataTable con el Maestro de Articulos</returns>
    ''' <remarks></remarks>
    Public Function dtbObtener_Maestro(ByVal strID_Local_En_Sistema As String, ByVal strID_Sector As String) As DataTable
        'variables a utilizar 
        Dim cSentenciaSQL_2 As String = String.Empty
        Dim dtbAuxiliar As DataTable = New DataTable()

        'tomamos el valor del ID del Local dentro del Sistema de Gestion
        cIDLocalEnSistemaPrincipal = strID_Local_En_Sistema

        'evaluamos cual Maestro de Articulos es el que se preparara
        Select Case oSistemaPrincipal.cDescripcion.ToUpper()
            Case "EMERETAIL"
                Try
                    'llamamos a funcion de ejecucion de Consulta SQL y devolvemos el resultado como el resultado de esta funcion
                    Return EMERETAIL.dtObtener_Maestro_Articulos(cIDLocalEnSistemaPrincipal, strID_Sector)

                Catch Ex As Exception
                    'en caso de error, evento a LOG
                    'principal.pInfo_a_Log("Error intentando Obtener Maestro Temporal de Articulos : " & cSentenciaSQL_2)
                    'principal.pInfo_a_Log("Detalles del Error : ", Ex.Message)

                    'devolvemos el resultado de la funcion
                    Return dtbAuxiliar

                End Try

            Case "SICS"
                Try
                    'llamamos a funcion de ejecucion de Consulta SQL y devolvemos el resultado como el resultado de esta funcion
                    Return SICS.dtObtener_Maestro_Articulos(cIDLocalEnSistemaPrincipal, strID_Sector)

                Catch Ex As Exception
                    'en caso de error, evento a LOG
                    'principal.pInfo_a_Log("Error intentando Obtener Maestro Temporal de Articulos : " & cSentenciaSQL_2)
                    'principal.pInfo_a_Log("Detalles del Error : ", Ex.Message)

                    'devolvemos el resultado de la funcion
                    Return dtbAuxiliar

                End Try

            Case Else
                'si el nombre del sistema no esta previsto, evento a LOG
                'principal.pInfo_a_Log("Error intentando Obtener Maestro del Sistema no Previsto : " & cNombreSistemaPrincipal)

                'devolvemos el resultado de la funcion
                Return dtbAuxiliar

        End Select

    End Function

    ''' <summary>
    ''' CREA UN DATATABLE CON LOS REGISTROS DE SECTORES
    ''' </summary>
    ''' <returns>Devuelte un DataTable con los Resultados</returns>
    ''' <remarks></remarks>
    Public Function dtbObtener_Sectores() As DataTable

        'evaluamos cual Colecion de Sectores de sistema sera devuelta
        Select Case oSistemaPrincipal.cDescripcion.ToUpper()
            Case "EMERETAIL"
                'llamamos a funcion de Obtencion de Sectores del Modulo EMERETAIL y devolvemos el datatable creado
                Return EMERETAIL.dtbObtener_Sectores()

            Case "SICS"
                'llamamos a funcion de Obtencion de Sectores del Modulo EMERETAIL y devolvemos el datatable creado
                Return SICS.dtbObtener_Sectores()

            Case Else
                'si el nombre del sistema no esta previsto, evento a LOG
                'principal.pInfo_a_Log("Error intentando Obtener Sectores del Sistema no Previsto : " & cNombreSistemaPrincipal)

                'un objeto para cubrir el resultado
                Dim dtbAuxiliar As DataTable = New DataTable()

                'devolvemos el resultado de la funcion
                Return dtbAuxiliar

        End Select

    End Function

    ''' <summary>
    ''' CREA UN DATATABLE CON LOS SECTORES CUYO ID_SECTOR INICIEN CON EL VALOR DEL PARAMETRO
    ''' </summary>
    ''' <param name="strIDSector">ID del/de los Sectores que seran devueltos</param>
    ''' <returns>Devuelve un DataTable con los Resultados</returns>
    ''' <remarks></remarks>
    Public Function dtbSeleccionar_Sectores(ByVal strIDSector As String) As DataTable
        'variables a utilizar
        Dim oSistemaActual As SistemaOTD = IIf(__oSistemaTemporal.nId.Equals(0), oSistemaPrincipal, __oSistemaTemporal)

        'evaluamos cual Colecion de Sectores de sistema sera devuelta
        Select Case oSistemaActual.cDescripcion.ToUpper()
            Case "EMERETAIL"
                'llamamos a funcion de Seleccion de Sectores del Modulo EMERETAIL y devolvemos el datatable creado
                Return EMERETAIL.dtbSeleccionar_Sectores(strIDSector)

            Case "SICS"
                'llamamos a funcion de Seleccion de Sectores del Modulo SICS y devolvemos el datatable creado
                Return SICS.dtbSeleccionar_Sectores(strIDSector)

            Case Else
                'si el nombre del sistema no esta previsto, evento a LOG
                'principal.pInfo_a_Log("Error intentando Filtrar Sectores del Sistema no Previsto : " & cNombreSistemaPrincipal)

                'devolvemos el resultado de la funcion
                Return New DataTable()

        End Select

    End Function

    ''' <summary>
    ''' PREPARA EL MODULO DEL SISTEMA TEMPORAL CUYO NOMBRE y ID SON PASADOS COMO PARAMETROS
    ''' </summary>
    ''' <param name="oSistemaParam">Instancia del Sistema a Preparar</param>
    ''' <returns>Devuelve 'True' si se Cargo Correctamente</returns>
    ''' <remarks></remarks>
    Public Function bPreparar_sistema_temporal(ByVal oSistemaParam As SistemaOTD) As Boolean

        'asignamos el valor a la variable de Nombre del Sistema
        __oSistemaTemporal = oSistemaParam

        'evaluamos cual Modulo de Sistema es el que se preparara
        Select Case oSistemaParam.cDescripcion.ToUpper()
            Case "EMERETAIL"
                'llamamos a la funcion de preparacion del Modulo EMERETAIL y devolvemos el resultado de la misma
                Return SISTEMAS.bPreparar_eme_temporal()

            Case "SICS"
                'llamamos a la funcion de preparacion del Modulo SICS y devolvemos el resultado de la misma
                Return SISTEMAS.bPreparar_spmo_temporal()

            Case Else
                'si el nombre del sistema no esta previsto, evento a LOG
                'principal.pInfo_a_Log("Error intentando Preparar Modulo del Sistema no Previsto : " & strNombreSistema)

                'devolvemos el resultado de la funcion
                Return False

        End Select

    End Function

    ''' <summary>
    ''' PREPARA EL MODULO 'EMERETAIL' PARA SU USO TEMPORAL Y DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <returns>Devuelve 'True' si se Cargo Correctamente</returns>
    ''' <remarks></remarks>
    Private Function bPreparar_eme_temporal() As Boolean

        'cargamos los Parametros del Sistema de Gestion, si no se pudieron cargar, devolvemos el resultado de la funcion
        If Not EMERETAIL.bCargar_Parametros_Sistema(__oSistemaTemporal.nId.ToString()) Then Return False

        'conectamos a la BD correspondiente, si no conecto, devolvemos el resultado de la funcion
        If Not EMERETAIL.bConectar_EME() Then Return False

        'devolvemos el resultado de la funcion
        Return True

    End Function

    ''' <summary>
    ''' PREPARA EL MODULO 'SICS' PARA SU USO TEMPORAL Y DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <returns>Devuelve 'True' si se Cargo Correctamente</returns>
    ''' <remarks></remarks>
    Private Function bPreparar_spmo_temporal() As Boolean

        'cargamos los Parametros del Sistema de Gestion, si no se pudieron cargar, devolvemos el resultado de la funcion
        If Not SICS.bCargar_Parametros_Sistema(__oSistemaTemporal.nId.ToString()) Then Return False

        'conectamos a la BD correspondiente, si no conecto, devolvemos el resultado de la funcion
        If Not SICS.bConectar_SICS() Then Return False

        'devolvemos el resultado de la funcion
        bPreparar_spmo_temporal = True

    End Function

    ''' <summary>
    ''' EJECUTA EL PROCEDIMINETO DE AJUSTE DE INVENTARIO Y DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <returns>Devuelve 'True' si se Ejecuto Correctamente</returns>
    ''' <remarks></remarks>
    Public Function bolEjecutar_Ajuste(ByVal sIDLocalEnSistema As String _
                                        , ByVal sFecha As String _
                                        , ByVal sScanning As String _
                                        , ByVal sCantidadAjuste As String _
                                        , ByVal sCantidadTeorica As String _
                                        , ByVal sCosto As String) As Boolean
        'evaluamos cual Sistema es el Activo
        Select Case oSistemaPrincipal.cDescripcion.ToUpper()
            Case "EMERETAIL"
                'llamamos a funcion de Ejecucion de Ajuste de Inventario
                If EMERETAIL.bAplicar_Ajustes(sIDLocalEnSistema, sFecha, sScanning _
                                        , sCantidadAjuste, sCosto) Then
                    'si se ejecuto correctamente, devolvemos el resultado de la funcion
                    bolEjecutar_Ajuste = True

                End If

                'devolvemos el resultado de la funcion
                bolEjecutar_Ajuste = False

            Case "SICS"
                'llamamos a funcion de Ejecucion de Ajuste de Inventario
                If SICS.bAplicar_Ajustes(sIDLocalEnSistema, sFecha, sScanning _
                                        , sCantidadAjuste, sCosto, sCantidadTeorica) Then
                    'si se ejecuto correctamente, devolvemos el resultado de la funcion
                    bolEjecutar_Ajuste = True

                End If

                'devolvemos el resultado de la funcion
                bolEjecutar_Ajuste = False

            Case Else
                'si el nombre del sistema no esta previsto, evento a LOG
                'principal.pInfo_a_Log("Error intentando Aplicar Ajustes del Sistema no Previsto : " & cNombreSistemaPrincipal)

                'devolvemos el resultado de la funcion
                bolEjecutar_Ajuste = False

        End Select

    End Function

End Module
