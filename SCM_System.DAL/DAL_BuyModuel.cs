using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCM_System.Model;
namespace SCM_System.DAL
{
    public class DAL_BuyModuel<T> : DAL_UniversalModuel<T>, IDAL.IDAL_UniversalModuel<T> where T : BaseModel
    {
        //!表明标注: P -> 商品表(Products)

        //表明标注: S -> 采购单(Stocks)
        //表明标注: PL -> 供货商(ProductLend)
        //表明标注: U -> 用户表(Users)

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
                    创建人=U.UsersName,
                    S.审核状态,
                    S.备注
                }
                ).ToListAsync();
            return new Dictionary<string, dynamic>() { { "data", temp } };
        } 

        public async Task<Dictionary<string,dynamic>> GetDS_P_D()
        {
            var temp = await entities.DepotStock.OrderBy(DS=>DS.DSID).Join(entities.Products,
                DS => DS.ProID,
                P => P.ProID,
                (DS, P) => new {
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
                    DS_P=>DS_P.PTID,
                    PT=>PT.PTID,
                    (DS_P,PT)=>new {
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
                .ToListAsync();
            return new Dictionary<string, dynamic>() { { "data",temp} };
        }
    }
}
