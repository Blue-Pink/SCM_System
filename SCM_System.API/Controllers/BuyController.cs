using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SCM_System.Model;
using System.Threading.Tasks;

namespace SCM_System.API.Controllers
{
    [RoutePrefix("api/buy")]
    public class BuyController : ApiController
    {

        [Inject]
        public DAL.DAL_BuyModuel<BaseModel> buyModuel { get; set; }
        [Inject]
        public UniversalPager<V_DS_P_PT, int> Pager_V_DS_P_PT { get; set; }
        #region 表名标注
        //P     -> 商品表(Products)
        //S     -> 采购单(Stocks)
        //PL   -> 供货商(ProductLend)
        //U    -> 用户表(Users)
        //DS  -> 库存表(DepotStock) 
        //D    -> 仓库(Depots)
        //PT   -> 商品类别(ProductTypes)
        #endregion
        [HttpGet]
        [Route("GetS_PL_Us")]
        public async Task<dynamic> GetS_PL_Us()
        {
            return await buyModuel.GetS_PL_Us().ConfigureAwait(false);
        }

        [HttpGet]
        [Route("GetDS_P_Ds")]
        public dynamic GetDS_P_Ds(int ps, int pi)
        { 
            Pager_V_DS_P_PT.PageSize = ps;
            Pager_V_DS_P_PT.PageIndex = pi; 
            Pager_V_DS_P_PT.IsAsc = true;
            Pager_V_DS_P_PT.WhereLambda = a => true;
            Pager_V_DS_P_PT.OrderByLambda = a => a.DSID;
            var temp = Pager_V_DS_P_PT.Paging();
            //var temp = await buyModuel.GetDS_P_D(ps, pi).ConfigureAwait(false);
            return new Dictionary<string, dynamic>() { { "data",temp},{ "total",Pager_V_DS_P_PT.PageCount} };
        }

        public void Options() { }  //这是预请求
    }
}
