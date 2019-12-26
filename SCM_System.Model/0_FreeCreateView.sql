    --P         -> 商品表(Products)
--S         -> 采购单(Stocks)
--PL       -> 供货商(ProductLend)
--U        -> 用户表(Users)
--DS      -> 库存表(DepotStock) 
--D        -> 仓库(Depots)
--PT       -> 商品类别(ProductTypes)
--CD      -> 盘点单(CheckDepot)
--CDD   -> 盘点单详单(CheckDepotDetail)
if(exists(select * from sysobjects where name='V_DS_P_PT'))
drop view V_DS_P_PT
go
create view V_DS_P_PT
as 
select DS_P.DSID,
                        DS_P.ProName,
                        DS_P.ProWorkShop,
                        DS_P.ProPrice,
                        DS_P.DSAmount,
                        DS_P.ProInPrice,
                        DS_P.ProMax,
                        DS_P.ProMin,
                        pt.PTName from 
(select  DS.DSID,
                    P.ProName,
                    P.ProWorkShop,
                    P.PTID,
                    P.ProPrice,
                    DS.DSAmount,
                    P.ProInPrice,
                    P.ProMax,
                    P.ProMin
                    from DepotStock ds left join 
Products p on ds.ProID=p.ProID) ds_p  left join	
ProductTypes pt on ds_p.PTID=pt.PTID
go

if(exists(select * from sysobjects where name='V_DS_P_PT'))
drop view V_DS_P_PT
go
create view V_DS_P_PT
as 
select cd.CDID,
cd.DepotID,
cd.CDDate,
cd.UserID,
'diff'=(cdd.CDDAmount1-cdd.DevAmount2),
cdd.ProID,cd.CDDesc,
cd.CDState from CheckDepot cd left join CheckDepotDetail cdd on cd.CDID=cdd.CDID