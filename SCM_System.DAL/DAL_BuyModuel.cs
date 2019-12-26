using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SCM_System.Model;
namespace SCM_System.DAL
{
    public class DAL_BuyModuel<T> : DAL_UniversalModuel<T>, IDAL.IDAL_UniversalModuel<T> where T : class
    {
        #region 表名标注
        //P     -> 商品表(Products)
        //S     -> 采购单(Stocks)
        //PL   -> 供货商(ProductLend)
        //U    -> 用户表(Users)
        //DS  -> 库存表(DepotStock) 
        //D    -> 仓库(Depots)
        //PT   -> 商品类别(ProductTypes)
        #endregion

        #region 示例代码
        public async Task<Dictionary<string, dynamic>> GetS_PL_Us()
        {
            var temp = await entities.Stocks.Join(entities.ProductLend,
                S => S.PPID,
                PL => PL.PPID,
                (S, PL) => new
                {
                    采购单编号 = S.StockID,
                    供货商编号 = S.PPID,
                    供货商名称 = PL.PPName,
                    下单时间 = S.StockDate,
                    预计交货时间 = S.StockInDate,
                    创建人 = S.StockUser,
                    审核状态 = S.StockState,
                    备注 = S.StockDesc
                }
                ).Join(entities.Users,
                S => S.创建人,
                U => U.UsersID,
                (S, U) => new
                {
                    S.采购单编号,
                    S.供货商编号,
                    S.供货商名称,
                    S.下单时间,
                    S.预计交货时间,
                    创建人 = U.UsersName,
                    S.审核状态,
                    S.备注
                }
                ).ToListAsync();
            return new Dictionary<string, dynamic>() { { "data", temp } };
        }
        #endregion

        public async Task<Dictionary<string, dynamic>> GetDS_P_D(int pagesize, int pageindex)
        {
            var count = entities.DepotStock.Count();
            var temp = await entities.DepotStock.OrderBy(DS => DS.DSID)
                .Join(entities.Products,
                DS => DS.ProID,
                P => P.ProID,
                (DS, P) => new
                {
                    DS.DSID,
                    P.ProName,
                    P.ProWorkShop,
                    P.PTID,
                    P.ProPrice,
                    DS.DSAmount,
                    P.ProInPrice,
                    P.ProMax,
                    P.ProMin,
                }).Join(entities.ProductTypes,
                    DS_P => DS_P.PTID,
                    PT => PT.PTID,
                    (DS_P, PT) => new
                    {
                        DS_P.DSID,
                        DS_P.ProName,
                        DS_P.ProWorkShop,
                        DS_P.ProPrice,
                        DS_P.DSAmount,
                        DS_P.ProInPrice,
                        PT.PTName,
                        DS_P.ProMax,
                        DS_P.ProMin,
                    }
                )
                .OrderBy(a=>a.DSID)
                .Skip(pagesize * (pageindex - 1))
                .Take(pagesize)
                .ToListAsync();
            return new Dictionary<string, dynamic>() { { "data", temp },{ "total", count%pagesize==0?count/pagesize:count/pagesize+1 } };
        }
    }
}
