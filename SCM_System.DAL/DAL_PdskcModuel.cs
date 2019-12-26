using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using SCM_System.Model;
using SCM_System.IDAL;
using Ninject;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;

namespace SCM_System.DAL
{
    public class DAL_PdskcModuel<T> : DAL_UniversalModuel<T>, IDAL.IDAL_UniversalModuel<T> where T : class
    {
        
        //entities ==db
        /// <summary>
        /// 库存盘点
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, dynamic>> GetVwCDUs() {

            var temp = await entities.CheckDepot.Join(entities.Depots,
                S => S.DepotID,
                PL => PL.DepotID,
                (S, PL) => new
                {
                    盘点编号 = S.CDID,
                    盘点仓库 = PL.DepotName,
                    盘点日期 = S.CDDate,
                    盈亏总额 = 100,
                    备注     = S.CDDesc,
                    状态     = (S.CDState == 1 ? "盘点中" : S.CDState == 2 ? "盘点核算" : "盘点结束"),
                    US=S.UserID
                }
                ).Join(entities.Users,
                S => S.US,
                U => U.UsersID,
                (S, U) => new
                {
                    
                    S.盘点编号,
                    S.盘点仓库,
                    S.盘点日期,
                    经手人 = U.UsersName,
                    S.盈亏总额,
                    S.备注,
                    S.状态,
                }
                ).ToListAsync();
            return new Dictionary<string, dynamic>() { { "data", temp } };
        }


        /// <summary>
        /// 根据仓库编号CK0001查出商品清单
        /// </summary>
        /// <returns></returns>
        //public async Task<Dictionary<string, dynamic>> GetVwptcs()
        //{

        //    var temp = await entities.Products.Join(entities.ProductTypes,
        //        S => S.PTID,
        //        PL => PL.PTID,
        //        (S, PL) => new
        //        {
        //            编号 = S.ProID,
        //            名称 = S.ProName,
        //            类别 = S.CDDate,
        //            颜色 = 100,
        //            价格 = S.CDDesc,
        //            数量 = (S.CDState == 1 ? "盘点中" : S.CDState == 2 ? "盘点核算" : "盘点结束"),
        //            盘点数 = S.UserID,
        //            盈亏 = S.
        //        }
        //        ).Join(entities.Users,
        //        S => S.US,
        //        U => U.UsersID,
        //        (S, U) => new
        //        {

        //            S.盘点编号,
        //            S.盘点仓库,
        //            S.盘点日期,
        //            经手人 = U.UsersName,
        //            S.盈亏总额,
        //            S.备注,
        //            S.状态,
        //        }
        //        ).ToListAsync();
        //    return new Dictionary<string, dynamic>() { { "data", temp } };
        ////}
    }
}
