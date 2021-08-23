SELECT comercio as LOCAL, caja, substr(hora, 1, 2)||':'|| substr(hora, 3, 2) as HORA , substr(to_char(msisdn), 1, 3) as PREFIJO
                              , substr(to_char(msisdn), 4, 3)||' '|| substr(to_char(msisdn), 7) as NUMERO
                              , to_char(monto, '9G999G999') as MONTO, to_char(nuevo_saldo, '9G999G999D99') as NUEVO_SALDO, confirmada
                              FROM emedb.transaction_to_tigo
                              ORDER BY fecha DESC, hora DESC