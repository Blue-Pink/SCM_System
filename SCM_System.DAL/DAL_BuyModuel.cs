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
    }
}
