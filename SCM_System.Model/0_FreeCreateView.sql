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
--DD		->仓库调拨单详单(DevolveDetail)
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
select a.ProID,
a.ProName,
a.ProJP,
a.ProTM,
b.PCName,
c.PUName,
d.PSName from Products a join ProductColor b on a.PCID = b.PCID
join ProductUnit c on a.PUID = c.PUID
join ProductSpec d on a.PSID = d.PSID
go


if(exists(select * from sysobjects where name='V_Dl_D_D_U'))
drop view V_Dl_D_D_U
go
create view V_Dl_D_D_U
as
select dl_d_d.*,
u.UsersName from (select dl_d.*,
'InDepot'=d.DepotName from (select dl.*,
'OutDepot'=d.DepotName from Devolves dl left join 
Depots d on dl.DevInID=d.DepotID) dl_d left join 
Depots d on dl_d.DevInID=d.DepotID) dl_d_d  left join 
Users u on dl_d_d.UserID=u.UsersID
go


if(exists(select * from sysobjects where name='V_DS_D'))
drop view V_DS_D
go
create view V_DS_D
as
select ds.*,
d.DepotName,
amount=ds.DSAmount*DSPrice from DepotStock ds left join 
Depots d on ds.DepotID=d.DepotID
go


-----------------------------MrYao----------------------------------
--创建视图。用户表跟用户层级表的融合(CustomerPage页面)
if(exists(select * from sysobjects where name='V_CustomersAndLevel'))
drop view V_CustomersAndLevel
go
create view V_CustomersAndLevel  
as
select c.*,cl.CLName,cl.CLAgio from Customers c inner join CustomerLevel cl on c.CLID=cl.CLID
go


--创建视图，商品视图跟库存表的融合（DepostProducts页面）
if(exists(select * from sysobjects where name='V_VProductsAndDepotStock'))
drop view V_VProductsAndDepotStock
go
create view V_VProductsAndDepotStock  
as
select  s.ProID,p.PTID,p.ProName,p.PTName,p.ProInPrice,p.ProPrice,s.DSID,ProCount=sum(s.DSAmount) from V_Products p inner join	DepotStock s on p.ProID=s.ProID group by s.ProID,p.ProName,p.PTName,p.ProInPrice,p.ProPrice,p.PTID,s.DSID
go

select * from V_VProductsAndDepotStock


--创建客户订单表和客户等级表和客户表的视图（CustomerOrderPage页面）
if(exists(select * from sysobjects where name='V_CusAndCusOrderAndClv'))
drop view V_CusAndCusOrderAndClv
go
create view V_CusAndCusOrderAndClv
as
select od.CODID,
ct.CusID,
ct.CusName,
od.COID,
cl.CLName,
od.CODDiscont,
CODCount=od.CODDisPrice*od.CODSale from CustomerOrderDetail od inner join 
CustomerOrder co on od.COID=co.COID inner join 
Customers ct on co.CusID=ct.CusID inner join
CustomerLevel cl on ct.CLID=cl.CLID
go


--创建报价单跟报价详情单和客户表的视图（QuotePricepage页面）
if(exists(select * from sysobjects where name='V_QuotePriceAndDetailAndCustomers'))
drop view V_QuotePriceAndDetailAndCustomers
go
create view V_QuotePriceAndDetailAndCustomers
as
select d.QPDID,p.QPID,c.CusID,c.CusName,d.QPDDiscont,QPDCount=d.QPDAmount*d.QPDDiscont from QuotePriceDetail d inner join QuotePrice p on d.QPID=p.QPID inner join Customers c on p.CusID=c.CusID
go




--创建采购单和采购详单和供货商表和用户表的视图（Stocks页面）
if(exists(select * from sysobjects where name='V_StockAndDetailAndProLendAndUsers'))
drop view V_StockAndDetailAndProLendAndUsers
go
create view V_StockAndDetailAndProLendAndUsers
as
select sd.SDetailID,sk.StockID,pl.PPID,pl.PPCompany,us.UsersName,sk.StockDate,sk.StockInDate,sk.StockState,StockCount=sd.SDetailAmount*sd.SDetailPrice from StockDetail sd inner join Stocks sk on sd.StockID=sk.StockID inner join Users us on sk.StockUser=us.UsersID inner join ProductLend pl on sk.PPID=pl.PPID
go




--Depot页面
select * from V_InDepot




--创建商品表和库存表和商品类型表的视图（Reserve页面）
if(exists(select * from sysobjects where name='V_ProAndDepotAndProType'))
drop view V_ProAndDepotAndProType
go
create view V_ProAndDepotAndProType
as
select p.*,t.PTName,c.PCName,u.PUName,ps.PSName,ProCount=(select SUM(d.DSAmount) from DepotStock d where ProID=p.ProID),ProFit=(select SUM(d.DSAmount) from DepotStock d where ProID=p.ProID)*p.ProPrice from Products p inner join ProductTypes t on p.PTID=t.PTID inner join ProductColor c on c.PCID=p.PCID inner join ProductUnit u on u.PUID=p.PUID inner join ProductSpec ps on p.PSID=ps.PSID
go


select * from V_ProAndDepotAndProType

--创建库存表和仓库表的视图
if(exists(select * from sysobjects where name='V_DepotsAndDepotsStock'))
drop view V_DepotsAndDepotsStock
go
create view V_DepotsAndDepotsStock
as
select s.*,d.DepotName from DepotStock s inner join Depots d on d.DepotID=s.DepotID 
go




--创建商品表、类型、颜色、采购详单的视图
if(exists(select * from sysobjects where name='V_StockDetailAndProAndColorAndTypes'))
drop view V_StockDetailAndProAndColorAndTypes
go
create view V_StockDetailAndProAndColorAndTypes
as
select s.*,c.PCName,t.PTName,p.ProName,SDetailSum=s.SDetailPrice*s.SDetailAmount from StockDetail s inner Join Products p on s.ProID=p.ProID inner join ProductColor c on p.PCID=c.PCID inner join ProductTypes t on p.PTID=t.PTID where s.ProID='P20190408000001'
go
-----------------------Pdskc---------------------------------
--Pdskc-->Split
--if(exists(select * from sysobjects where name='V_SplitsAndDepotsAndUsersAndProducts'))
--drop view V_SplitsAndDepotsAndUsersAndProducts
--go
--create view V_SplitsAndDepotsAndUsersAndProducts
--as
--select s.*,d.DepotName,u.UsersName,p.ProName from Splits s inner join Users u on s.UserID=u.UsersID inner join Depots d on s.DepotID=d.DepotID inner join Products p on s.ProID=p.ProID
--go


---Pdskc--》PayOff
if(exists(select * from sysobjects where name='V_DepotsAndUsersAndDetailAndProducts'))
drop view V_DepotsAndUsersAndDetailAndProducts
go
create view V_DepotsAndUsersAndDetailAndProducts
as
select s.*,u.UsersName,d.DepotName,PayOffCount=SUM(pd.PODAmount*pd.PODPrice) from PayOffs s inner join Depots d on s.DepotID=d.DepotID inner join Users u on s.UserID=u.UsersID inner join PayOffDetail pd on s.POID=pd.POID group by s.POID,s.DepotID,s.PODate,s.PODesc,s.UserID,s.PODate,s.POState,u.UsersName,d.DepotName
go



---Pdskc-->Losts
if(exists(select * from sysobjects where name='V_LostsAndDetailAndUsersAndDepots'))
drop view V_LostsAndDetailAndUsersAndDepots
go
create view V_LostsAndDetailAndUsersAndDepots
as
select l.*,u.UsersName,d.DepotName,LostsCount=SUM(ld.LDAmount*ld.LDPrice) from Losts l inner join Users u on l.UserID=u.UsersID inner join Depots d on l.DepotID=d.DepotID inner join LostDetail ld on ld.LostID = l.LostID group by ld.ProID,l.LostID,l.DepotID,l.LostDate,l.LostDesc,l.LostState,l.LostState,l.UserID,u.UsersName,d.DepotName