﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Ninject;
using SCM_System.DAL;
using SCM_System.IDAL;
using SCM_System.Model;

namespace SCM_System.API.Controllers
{
    [RoutePrefix("api/Pdskc")]
    public class PdskcController : ApiController
    {
        [Inject]
        //通用模块
        public DAL_UniversalModuel<CheckDepot> universalModuel { get; set; }

        [Inject]
        //通用模块
        public DAL_UniversalModuel<V_Products> ssss { get; set; }
        [Inject]
        //项目对应模块
        public DAL_PdskcModuel<BaseModel> basicModuel { get; set; }
        //分页器
        [Inject]
        public UniversalPager<V_CD_CDD_P, dynamic> pager_V_CD_CDD_P { get; set; }
        [Inject]
        public UniversalPager<V_Dl_D, dynamic> pager_V_Dl_D { get; set; }


        /// <summary>
        /// 查询当前所有库存
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCDUs")]
        public async Task<Dictionary<string, dynamic>> GetCDUs()
        {
            return await basicModuel.GetVwCDUs().ConfigureAwait(false);
        }

        /// <summary>
        /// 删除选中的库存
        /// </summary>
        /// <param name="CDID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DelVwCDUid/{CDID}")]
        public async Task<int> DelVwCDUid(string CDID)
        {
            return await universalModuel.Delete_Key(CDID).ConfigureAwait(false);
        }


        [HttpGet]
        [Route("GetCheckDepot/{CDID}")]
        public async Task<CheckDepot> GetCheckDepot(String CDID)
        {
            return await universalModuel.Select_Key(CDID).ConfigureAwait(false);
        }

        [HttpGet]
        [Route("GetVCDCDDP")]
        public async Task<dynamic> GetVCDCDDP(int ps,int pi)
        {
            pager_V_CD_CDD_P.IsAsc = true;
            pager_V_CD_CDD_P.PageSize = ps;
            pager_V_CD_CDD_P.PageIndex = pi;
            pager_V_CD_CDD_P.WhereLambda = a => true;
            pager_V_CD_CDD_P.OrderByLambda = a => a.CDID;
            var set = await pager_V_CD_CDD_P.Paging().ConfigureAwait(false);
            return new Dictionary<string, dynamic>() { { "data",set},{ "total", pager_V_CD_CDD_P.PageCount} };
        }
        [HttpGet]
        [Route("GetVDlD")]
        public async Task<dynamic> GetVDlD(int ps,int pi)
        {
            pager_V_Dl_D.IsAsc = true;
            pager_V_Dl_D.PageSize = ps;
            pager_V_Dl_D.PageIndex = pi;
            pager_V_Dl_D.WhereLambda = a => true;
            pager_V_Dl_D.OrderByLambda = a => a.DevID;
            var set = await pager_V_Dl_D.Paging().ConfigureAwait(false);
            return new Dictionary<string, dynamic>() { { "data", set }, { "total", pager_V_Dl_D.PageCount } };
        }

        //[HttpGet]
        //[Route("GetVProducts/{DepotID}")]
        //public async Task<List<V_Products>> GetVProducts(String DepotID)
        //{
        //    return await ssss.Select_KeyProduct(DepotID).ConfigureAwait(false);
        //}




        public void Options() { }  //这是预请求
    }
}
