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
        public DAL_BuyModuel<BaseModel> buyModuel { get; set; }
        [Inject]
        public UniversalPager<V_DS_P_PT, int> Pager_V_DS_P_PT { get; set; }
        [Inject]
        public DAL_UniversalModuel<V_Products> UV_Products { get; set; }
        [Inject]
        public DAL_UniversalModuel<V_DS_D> UV_DS_D { get; set; }

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

        /// <summary>
        /// 获取库存详细信息
        /// </summary>
        /// <param name="ps">单次显示单例条数</param>
        /// <param name="pi">折叠页码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDS_P_Ds")]
        public async Task<dynamic> GetDS_P_Ds(int ps, int pi)
        {
            Pager_V_DS_P_PT.PageSize = ps;
            Pager_V_DS_P_PT.PageIndex = pi;
            Pager_V_DS_P_PT.IsAsc = true;
            Pager_V_DS_P_PT.WhereLambda = a => true;
            Pager_V_DS_P_PT.OrderByLambda = a => a.DSID;
            var set = await Pager_V_DS_P_PT.Paging().ConfigureAwait(false);
            return new Dictionary<string, dynamic>() { { "data", set }, { "total", Pager_V_DS_P_PT.Count } };
        }

        [HttpGet]
        [Route("GetProduct")]
        public async Task<V_Products> GetProduct(string ProID)
        {
            return await UV_Products.Select_Key(ProID).ConfigureAwait(false);
        }

        [HttpGet]
        [Route("GetVDSD_P")]
        public async Task<List<V_DS_D>> GetVDSD_P(string properties_json)
        {
            return await UV_DS_D.Select_Properties(UV_DS_D.JsonToDictionary(properties_json)).ConfigureAwait(false);
        }

        [HttpGet]
        [Route("GetDSPDs_P")]
        public async Task<dynamic> GetDSPDs_P(int ps,int pi,int ? DSID, string Status,string Depot)
        {
            Pager_V_DS_P_PT.PageSize = ps;
            Pager_V_DS_P_PT.PageIndex = pi;
            Pager_V_DS_P_PT.IsAsc = true;
            Pager_V_DS_P_PT.WhereLambda = a => (DSID != null?a.DSID == DSID : true);
            Pager_V_DS_P_PT.OrderByLambda = a => a.DSID;
            var set = await Pager_V_DS_P_PT.Paging().ConfigureAwait(false);
            return new Dictionary<string, dynamic>() { { "data", set }, { "total", Pager_V_DS_P_PT.Count } };
        }
        public void Options() { }  //这是预请求
    }
}
