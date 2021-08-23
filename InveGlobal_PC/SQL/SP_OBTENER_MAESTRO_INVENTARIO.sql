create or replace PROCEDURE SP_OBTENER_MAESTRO_INVENTARIO
   ( param_destination_id IN NUMBER, param_cat_code IN VARCHAR, param_rec_quantity IN NUMBER,
     TMP_CURSOR OUT SYS_REFCURSOR)
   IS
--
--
-- Purpose: Retorna un ResultSet con el ultimo inventario para una sucursal
--
--  Parametros:
--  param_destination_id = SUCURSAL
--  param_cat_code = CATEGORIA
--  param_rec_quantity =  cantidad de registros retornados en el cursor TMP_CURSOR
--
-- MODIFICATION HISTORY
-- Person      Date         Comments
-- ---------   ------       -------------------------------------------       
--  JICM       01/09/09     Version inicial
--  

    var_last_header_id      NUMBER:=0;

BEGIN
    IF param_rec_quantity>0 THEN
        OPEN TMP_CURSOR FOR 
        select * from 
            (select a.*, rownum as rnum_ from             
                (select 
                    TO_CHAR(c.id) as "SCANNING",
                    TO_CHAR(b.description) as "DESCRIPCION",
                    TO_CHAR(NVL(f.cost, 0)) as "COSTO",
                    'S/D' as "DETALLE",
                    TO_CHAR(NVL(d.disponible, 0)) as "CANTIDAD_TEORICA",
                    TO_CHAR(h.code) as "ID_SECTOR",
                    (case 
                        when b.weighable = 0 then 'N' 
                            else 'P' 
                    end) as "PESABLE"
                from 
                    stock a
                    left join product b on a.product_id = b.id
                    left join product_look_up c on b.id = c.product_id
                    left join stock_level d on a.sale_room_level_id= d.id 
                    left join pricing f on a.product_id = f.product_id and a.destination_id = f.destination_id
                    left join product_category_stock g on a.product_id = g.product_id 
                    left join category_stock h on g.category_id = h.id
                where 
                    a.destination_id = param_destination_id 
                    and b.sale = 1
                    and h.code like (param_cat_code||'%')
                order by 1) a )
        where rnum_ <= param_rec_quantity;
    ELSE
      OPEN TMP_CURSOR FOR 
      select 
            TO_CHAR(c.id) as "SCANNING",
            TO_CHAR(b.description) as "DESCRIPCION",
            TO_CHAR(NVL(f.cost, 0)) as "COSTO",
            'S/D' as "DETALLE",
            TO_CHAR(NVL(d.disponible, 0)) as "CANTIDAD_TEORICA",
            TO_CHAR(h.code) as "ID_SECTOR",
            (case 
                when b.weighable = 0 then 'N' 
                    else 'P' 
            end) as "PESABLE"
      from 
        stock a
        left join product b on a.product_id = b.id
        left join product_look_up c on b.id = c.product_id
        left join stock_level d on a.sale_room_level_id= d.id 
        left join pricing f on a.product_id = f.product_id and a.destination_id = f.destination_id
        left join product_category_stock g on a.product_id = g.product_id 
        left join category_stock h on g.category_id = h.id
      where 
        a.destination_id = param_destination_id 
        and b.sale = 1
        and h.code like (param_cat_code||'%')
      order by 1;
    END IF;

EXCEPTION
    WHEN OTHERS THEN
        dbms_output.put_line(SubStr('Error: '||SQLERRM,1,255));         
END; -- Procedure
