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