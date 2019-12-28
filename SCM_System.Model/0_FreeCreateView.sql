--P         -> 商品表(Products)
--S         -> 采购单(Stocks)
--PL       -> 供货商(ProductLend)
--U         -> 用户表(Users)
--DS        -> 库存表(DepotStock) 
--D         -> 仓库(Depots)
--PT        -> 商品类别(ProductTypes)
--DS        -> 库存表(DepotStock)
--Dl        -> 仓库调拨单(Devolves)
--Sl        -> 拆分表(Splits)
--SD        -> 拆分单详单(SplitDetail)
use SCM
go

if(exists(select * from sysobjects where name='V_DS_P_PT'))
drop view V_DS_P_PT
go
create view V_DS_P_PT
as 
select ds_p.*,
pt.PTName from (select ds.DSID,
DSAmount,
DSPrice,
p.* from DepotStock ds left join 
Products p on ds.ProID=p.ProID) ds_p  left join	
ProductTypes pt on ds_p.PTID=pt.PTID
go


if(exists(select * from sysobjects where name='V_CD_CDD_P_D_U'))
drop view V_CD_CDD_P_D_U
go
create view V_CD_CDD_P_D_U
as 
select cd_cdd_p_d.*,u.UsersName from(select cd_cdd_p.*,
d.DepotName from (select cd_cdd.*,
'pl'=cd_cdd.diff*p.ProInPrice from (select cd.*,
'diff'=(cdd.CDDAmount1-cdd.DevAmount2),
cdd.ProID from CheckDepot cd left join 
CheckDepotDetail cdd on cd.CDID=cdd.CDID) cd_cdd left join 
Products p on cd_cdd.ProID=p.ProID) cd_cdd_p left join
Depots d on cd_cdd_p.DepotID=d.DepotID) cd_cdd_p_d left join
Users u on cd_cdd_p_d.UserID=u.UsersID
go


if(exists(select * from sysobjects where name='V_Dl_D_D'))
drop view V_Dl_D_D
go
create view V_Dl_D_D
as 
select dl_d_d.DevID,
dl_d_d.OutDepot,
dl_d_d.InDepot,
dl_d_d.DevDate,
u.UsersName,
dl_d_d.DevDesc,
dl_d_d.DevState from (select dl_d.*,
'InDepot'=d.DepotName from (select dl.*,
'OutDepot'=DepotName from Devolves dl left join 
Depots d on dl.DevOutID=d.DepotID) dl_d left join 
Depots d on dl_d.DevInID=d.DepotID) dl_d_d left join
Users u on dl_d_d.UserID=u.UsersID
go


if(exists(select * from sysobjects where name='V_Sl_SD_P_U'))
drop view V_Sl_SD_P_U
go
create view V_Sl_SD_P_U
as 
select sl_sd_p_u.*,u.UsersName from (select sl_sd_p.*
,p.ProName from (select sl_sd.*,
d.DepotName from (select sl.*,
sd.SDAmount,
sd.SDPrice,
sd.SDDesc from Splits sl left join 
SplitDetail sd on sl.SplitID=SD.SplitID) sl_sd left join
Depots d on sl_sd.DepotID=d.DepotID) sl_sd_p left join
Products p on sl_sd_p.ProID=p.ProID) sl_sd_p_u left join
Users u on sl_sd_p_u.UserID=u.UsersID
go

if(exists(select * from sysobjects where name='Vw_CL'))
drop view Vw_CL
go
create view Vw_CL
as
select a.*,b.CLAgio,b.CLName from Customers a join CustomerLevel b on a.CLID = b.CLID
go


if(exists(select * from sysobjects where name='Vw_DPP'))
drop view Vw_DPP
go
create view Vw_DPP
as
select a.ProID,a.ProName,a.ProJP,a.ProTM,b.PCName,c.PUName,d.PSName from Products a join ProductColor b on a.PCID = b.PCID
join ProductUnit c on a.PUID = c.PUID
join ProductSpec d on a.PSID = d.PSID
go

select * from Vw_DPP