using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SCM_System.Model;
using System.Threading.Tasks;
using SCM_System.DAL;

namespace SCM_System.API.Controllers
{
    [RoutePrefix("api/buy")]
    public class BuyController : ApiController
    {

        [Inject]
        public DAL.DAL_BuyModuel<BaseModel> buyModuel { get; set; }
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
        public async Task<dynamic> GetDS_P_Ds(int ps,int pi)
        {
            var temp = await buyModuel.GetDS_P_D(ps,pi).ConfigureAwait(false);
            return temp;
        }

        public void Options() { }  //这是预请求
    }
}
