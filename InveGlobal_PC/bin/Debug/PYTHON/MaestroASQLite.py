#MaestroASQLite.py
# ===========================================================
# Author:		CRISTIAN GARAY
# Create date: 	16/10/2009
# Description:	CARGA EL ARCHIVO CSV DEL MAESTRO 
#  DE ARTICULOS A LA TABLA [MAESTRO] DE LA BASE DE 
# DATOS EMBEBIDA SQLite.
# ===========================================================

coding= 'ISO-8859-15'

#importamos los modulos a utilizar
import sys, os, csv, sqlite3
from cdg.cdg_pqt import *

#variables a utilizar
oConSQLite = ''
cArchivoBD = ''
cArchivoCSV = ''

#funcion de conexion a la Base de Datos
def bConectarBD(cArchivoBD):
	'''Conecta al Archivo de Base de Datos [cArchivoBD] y devuelve un Boolean'''
	#referenciamos a las variables globales
	global oConSQLite
		
	#intentamos conectar a la Base de Datos
	try:
		oConSQLite = sqlite3.connect(cArchivoBD)
			
		#devolvemos el resultado de la funcion
		return True 
			
	except:
		#en caso de error, devolvemos el  resultado de la funcion
		return False
			
	

#funcion de carga del archivo CSV a la BD SQLite
def bCargarCSV():
	'''Carga el Archivo CSV en la Tabla [MAESTRO] del SQLite'''
	#referenciamos a las variables globales
	global cArchivoCSV, oConSQLite
		
	#intentamos abrir el archivo CSV
	try:
		print 'Abriendo Archivo CSV'
		oCSV= open(cArchivoCSV)
		print 'Ok!..'
			
	except:
		#en caso de error, mensaje de Notificacion
		print 'Error.. No se Puede Abrir el Archivo CSV : ' + cArchivoCSV
			
		#devolvemos el resultado de la funcion
		return False
			
		
	#intentamos cargar el archivo CSV a la BD
	try:
		input = csv.reader(oCSV, delimiter=';')
			
		#creamos un cursor
		curSQL= oConSQLite.cursor()
			
		#un contador de filas
		fila = 0
		
		print 'Cargando Datos...'
			
		#recorremos las filas del archivo CSV
		for row in input:
			#intentamos insertar la fila en la tabla
			try:
				#incrementamos el contador de filas
				fila += 1
					
				cSentencia = 'INSERT INTO MAESTRO (SCANNING, DESCRIPCION, DETALLE, PESABLE, COSTO, ID_SECTOR) '
				cSentencia+= ' VALUES(' + str(row[0]) + ',' + str(row[1]) + ',' + str(row[2]) + ',' + str(row[3]) + ',' + str(row[4]) + ',' + str(row[5]) + ')'
					
				#la insertamos en la tabla [MAESTRO]
				a = curSQL.execute(cSentencia)
					
			except BaseException, oError:
				#en caso de error, mensajes de notificaciones
				print 'Error Intentando Cargar la Fila #', fila
				print "Detalles del ERROR: " + str(oError)
				print cSentencia
					
				#cerramos el archivo CSV
				oCSV.close()
					
				#devolvemos el resultado de la funcion
				return False
					
			
		#cerramos el archivo CSV
		oCSV.close()
			
		print 'Ok!...'
			
		#devolvemos el resultado de la funcion
		return True
			
		
	except:
		#en caso de error,  mensaje de notificacion
		print 'Error Intentando Cargar la Fila #', fila
			
		#cerramos el archivo CSV
		oCSV.close()
			
		#devolvemos el resultado de la funcion
		return False
			
		
	
#procedimiento principal del programa
def pCargarArchivoCSV(cArchivoCSV1, cArchivoBD1):
	'''Procedimiento Principal del Programa'''
	#referenciamos a las variabels globales
	global cArchivoBD, cArchivoCSV
		
	#les pasamos los valores de parametros recibidos
	cArchivoBD, cArchivoCSV = cArchivoBD1, cArchivoCSV1
		
	#llamamos a la Funcion de Conexion a la BD
	if bConectarBD(cArchivoBD1):
		#si se conecto, llamamos a Funcion de Carga del Archivo CSV
		if bCargarCSV():
			#si se cargo correctamente, intentamos comprometer la transaccion
			try:
				print 'Confirmado Transacciones'
				oConSQLite.commit()
				print 'Ok!...'
					
			except:
				#en caso de error, mensaje de notificacion
				print 'No se pudieron Grabar los Datos del Maestro en la BD'
					
				
			
		else:
			#si no se cargo correctamente, mensaje de notificacion
			print 'No se pudo Cargar el CSV :', cArchivoCSV
				
			
		#desconectamos de la BD
		oConSQLite.close()
			
	else:
		#si no se pudo conectar, mensaje de notificacion
		print 'No se pudo Conectar a la BD:', cArchivoBD
			
		
	
#esto espera el archivo procesar que se pasa como parametro del programa
cArchivos= sys.argv

#si la lista de parametros no esta vacia
if len(cArchivos) > 1:
	#si todo esta en orden, intentamos ejecutar la funcion
	try:
		pCargarArchivoCSV(cArchivos[1], cArchivos[2])
			
		
	except:
		#en caso de error, guardamso el evento en el LOG
		info_a_log('No se Pudo Cargar el CSV ' + cArchivos[1] + ' a la Base de Datos ' + cArchivos[2])
			
		#mensaje al usuario
		print 'NO SE PUDO CARGAR LA BASE DE DATOS DE COLECTORES'
			
		
	