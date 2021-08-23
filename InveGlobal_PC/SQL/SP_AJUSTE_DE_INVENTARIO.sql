create or replace PROCEDURE SP_AJUSTE_DE_INVENTARIO
   ( param_destination_id IN NUMBER, 
     param_fecha IN VARCHAR2,  
     param_scanning IN VARCHAR2,
     param_ajuste IN NUMBER,    
     param_cost IN NUMBER, 
     param_error OUT NUMBER) 
   IS
   
    v_systimestamp     TIMESTAMP := SYSTIMESTAMP;
    var_user           VARCHAR(10):= 'ticretail'; 
    var_justify_id     NUMBER := 11;
    var_stock_id       NUMBER := 0;
    var_laststock      NUMBER := 0;
    var_saleprice      NUMBER := 0;
    v_error            VARCHAR(255):= '';
    var_destination_id NUMBER := 0;
    var_product_id     NUMBER := 0;
    v_rows_processed   NUMBER:= 0;
     
    
  
  -- Purpose: Inserta en el update_stock_log y stock_level los ajustes de inventario
--
--  Parametros:
--  param_destination_id = SUCURSAL
--  param_fecha = fecha de ajuste
--  param_scanninig = codigo de barras del producto
--  param_ajuste = cantidad del ajuste del articulo (Fisico - teorico)
--  param_cost = costo en el momento del ajuste
--
-- MODIFICATION HISTORY
-- Person      Date         Comments
-- ---------   ------       -------------------------------------------       
--  cocampos   24/09/09     Version inicial
--  

BEGIN   --Begin Procedure

            param_error:= 0;
            -----------------------------------------------------------
            --- OBTIENE EL DESTINATION_ID DEL SCANNING PASADO POR PARAMETRO --
            -----------------------------------------------------------
            begin            
                SELECT id 
                INTO var_destination_id
                from destination where id = param_destination_id;
            exception
                WHEN OTHERS THEN 
                v_error := SubStr('destination_id :'|| param_destination_id ||'-'|| SQLERRM,1,255);             
                INSERT INTO TMP_AJUSTE_INVENTARIO_ERR (LOCAL_NUMBER,FECHA,SCANNING,AJUSTE,COST,ERROR) 
                VALUES (param_destination_id,v_systimestamp,param_scanning,param_ajuste,param_cost,V_ERROR);  
                COMMIT;
                RAISE_APPLICATION_ERROR(-20000,SQLERRM);
            end;    
            -----------------------------------------------------------
            --- OBTIENE EL PRODUCT_ID DEL SCANNING PASADO POR PARAMETRO --
            -----------------------------------------------------------
             begin
                SELECT product_id 
                INTO var_product_id 
                FROM product_look_up 
                WHERE id= param_scanning;
            exception
                WHEN OTHERS THEN 
                v_error := SubStr('product_id :'|| param_scanning || '-' || SQLERRM,1,255); 
                dbms_output.put_line(v_error); 
                INSERT INTO TMP_AJUSTE_INVENTARIO_ERR (LOCAL_NUMBER,FECHA,SCANNING,AJUSTE,COST,ERROR) 
                VALUES (param_destination_id,v_systimestamp,param_scanning,param_ajuste,param_cost,V_ERROR); 
                COMMIT;
                RAISE_APPLICATION_ERROR(-20001,SQLERRM);            
            end;    
            -- param_error:= 15;
            -----------------------------------------------------------
            --- OBTIENE EL STOCK_ID DEL SCANNING PASADO POR PARAMETRO --
            -----------------------------------------------------------
            begin
                SELECT s.id  
                INTO var_stock_id    
                FROM stock s, product p
                WHERE s.product_id = p.id
                    AND p.id = var_product_id
                    AND s.destination_id = var_destination_id; 
                exception
                    WHEN OTHERS THEN 
                    v_error := SubStr('stock_id -- '|| var_stock_id || '-' || SQLERRM,1,255); 
                    INSERT INTO TMP_AJUSTE_INVENTARIO_ERR (LOCAL_NUMBER,FECHA,SCANNING,AJUSTE,COST,ERROR) 
                    VALUES (param_destination_id,v_systimestamp,param_scanning,param_ajuste,param_cost,V_ERROR); 
                    COMMIT;
                    RAISE_APPLICATION_ERROR(-20002,SQLERRM);             
            end;  
            ---------------------------------------------------------------------------
            --- OBTIENE EL ULTIMO STOCK DISPONIBLE DEL SCANNING PASADO POR PARAMETRO ---
            ----------------------------------------------------------------------------
            begin   
                SELECT STL.disponible
                INTO var_laststock   
                FROM stock_level STL 
                INNER JOIN stock S on STL.id= S.sale_room_level_id
                    AND S.id = var_stock_id;  
            exception
                WHEN OTHERS THEN 
                    v_error := SubStr('stock_level_disponible-- '|| var_laststock || '-' || SQLERRM,1,255); 
                    INSERT INTO TMP_AJUSTE_INVENTARIO_ERR (LOCAL_NUMBER,FECHA,SCANNING,AJUSTE,COST,ERROR) 
                    VALUES (param_destination_id,v_systimestamp,param_scanning,param_ajuste,param_cost,V_ERROR); 
                    COMMIT;
                    RAISE_APPLICATION_ERROR(-20003,SQLERRM);                      
            end;        
            ------------------------------------------------------------------------- 
            --- OBTIENE EL SALEPRICE DE PRICING DEL SCANNING PASADO POR PARAMETRO---
            -------------------------------------------------------------------------          
            begin
                SELECT P.sale_price
                INTO var_saleprice   
                FROM pricing P
                WHERE P.product_id = var_product_id
                     AND P.destination_id = var_destination_id; 
             exception
                WHEN OTHERS THEN  
                    v_error := SubStr('sale_price-- '|| var_saleprice || '-' || SQLERRM,1,255); 
                    INSERT INTO TMP_AJUSTE_INVENTARIO_ERR (LOCAL_NUMBER,FECHA,SCANNING,AJUSTE,COST,ERROR) 
                    VALUES (param_destination_id,v_systimestamp,param_scanning,param_ajuste,param_cost,V_ERROR); 
                    COMMIT;
                    RAISE_APPLICATION_ERROR(-20004,SQLERRM);             
            end;      
            
            --- VALIDA LA EXISTENCIA DE UN STOCK_ID / STOCK_LEVEL / PRICING DEL SCANNING PASADO POR PARAMETRO 
                    
            IF (var_stock_id > 0 and var_product_id > 0 and var_destination_id > 0) THEN 
          
            ----------------------------------------------------
            --- INSERTA LOS REGISTROS EN EL UPDATE_STOCK_LOG ---
            ----------------------------------------------------                 
            INSERT INTO update_stock_log (ID, QUANTITY, DATES, USER_ID, CODE, ADJUSTMENT_DATE, 					
                JUSTIFY_MODIFICATION_ID, STOCK_ID, COST, LASTSTOCKAVAILABLE, SALEPRICE, ERASURE)
			VALUES (update_stock_log_id.nextval, param_ajuste, v_systimestamp, var_user, USL_SEQ.NEXTVAL, to_timestamp(param_fecha,'dd/mm/yyyy HH24:MI:SS'),
			var_justify_id, var_stock_id, param_cost, var_laststock, var_saleprice, 0);			     
            --- VALIDA LA INSERSION DEL REGISTRO  EN EL UPDATE_STOCK_LOG
            v_rows_processed := SQL%ROWCOUNT;
            
            IF (v_rows_processed > 0) THEN        
            -----------------------------------------------
            --- Actualiza LOS REGISTROS EN EL STOCK_LEVEL ---
            -----------------------------------------------            
                    UPDATE stock_level 
                    SET disponible = (disponible + param_ajuste)
                    WHERE id in (SELECT sale_room_level_id FROM stock WHERE id = var_stock_id);
            
            --- VALIDA LA INSERSION DEL REGISTRO  EN EL STOCK_LEVEL
                    v_rows_processed := SQL%ROWCOUNT;
                        IF (v_rows_processed > 0) THEN   
                            COMMIT;
                        ELSE
                            dbms_output.put_line('NO INSERTADO EN EL STOCK_LEVEL !'); 
                            ROLLBACK;   
                        END IF;
                ELSE
                    dbms_output.put_line('NO INSERTADO EN EL UPDATE_STOCK_LOG !'); 
                        ROLLBACK;   
                END IF;
             END IF;  
 
EXCEPTION
    WHEN OTHERS THEN
     param_error := SQLCODE;  
END; -- End Procedure
