@echo off
@echo DES - MONTANDO UNIDAD DE RED Z:
mountvol Z: /d
@echo MONTANDO UNIDAD DE RED Z:
net use /user:NT_AJVIERCI\cprecios Z: \\172.31.10.68\INVESTOCK cprecios
@echo FINALIZADO
