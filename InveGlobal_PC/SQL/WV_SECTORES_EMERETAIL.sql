SELECT CODE AS "ID_SECTOR",
        DESCRIPTION AS "NOMBRE_SECTOR",
        LEVEL_ID AS "NIVEL",
        CASE LEVEL_ID
          WHEN 1 THEN '0'
          WHEN 2 THEN SUBSTR(CODE, 1, 2) || '0000000'
          WHEN 3 THEN SUBSTR(CODE, 1, 4) || '00000'
          WHEN 4 THEN SUBSTR(CODE, 1, 6) || '000'
          WHEN 5 THEN SUBSTR(CODE, 1, 8) || '0'
        END AS "ID_SECTOR_PADRE",
        SUBSTR(CODE, 1, 2) AS "N1",
        SUBSTR(CODE, 3, 2) AS "N2",
        SUBSTR(CODE, 5, 2) AS "N3",
        SUBSTR(CODE, 7, 2) AS "N4",
        SUBSTR(CODE, 9, 2) AS "N5"
    FROM category_stock
    ORDER BY 5, 6, 7, 8, 9