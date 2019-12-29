using System;
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
        public UniversalPager<V_CD_CDD_P_D_U, dynamic> pager_V_CD_CDD_P_D_U { get; set; }
        [Inject]
        public UniversalPager<V_Dl_D_D, dynamic> pager_V_Dl_D_D { get; set; }
        [Inject]
        public UniversalPager<V_Sl_SD_P_U, dynamic> pager_V_Sl_SD_P_U { get; set; }


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
            pager_V_CD_CDD_P_D_U.IsAsc = true;
            pager_V_CD_CDD_P_D_U.PageSize = ps;
            pager_V_CD_CDD_P_D_U.PageIndex = pi;
            pager_V_CD_CDD_P_D_U.WhereLambda = a => true;
            pager_V_CD_CDD_P_D_U.OrderByLambda = a => a.CDID;
            var set = await pager_V_CD_CDD_P_D_U.Paging().ConfigureAwait(false);
            return new Dictionary<string, dynamic>() { { "data",set},{ "total", pager_V_CD_CDD_P_D_U.Count} };
        }
        [HttpGet]
        [Route("GetVDlD")]
        public async Task<dynamic> GetVDlD(int ps,int pi)
        {
            pager_V_Dl_D_D.IsAsc = true;
            pager_V_Dl_D_D.PageSize = ps;
            pager_V_Dl_D_D.PageIndex = pi;
            pager_V_Dl_D_D.WhereLambda = a => true;
            pager_V_Dl_D_D.OrderByLambda = a => a.DevID;
            var set = await pager_V_Dl_D_D.Paging().ConfigureAwait(false);
            return new Dictionary<string, dynamic>() { { "data", set }, { "total", pager_V_Dl_D_D.Count } };
        }

        [HttpGet]
        [Route("GetVSSDPU")]
        public async Task<dynamic> GetVSSDPU(int ps,int pi)  
        {
            pager_V_Sl_SD_P_U.IsAsc = true;
            pager_V_Sl_SD_P_U.PageSize = ps;
            pager_V_Sl_SD_P_U.PageIndex = pi;   
            pager_V_Sl_SD_P_U.OrderByLambda = a => a.SplitID;
            pager_V_Sl_SD_P_U.WhereLambda = a => true;
            var set = await pager_V_Sl_SD_P_U.Paging().ConfigureAwait(false);
            return new Dictionary<string, dynamic>() { { "data", set }, { "total", pager_V_Dl_D_D.Count } };
        }

        //[HttpGet]
        //[Route("GetVInOutDepotDetail")]
        //public async Task<dynamic> GetVInOutDepotDetail(int ps,int pi)
        //{
            
        //   pager_V_InOutDepotDetail.IsAsc = true;
        //   pager_V_InOutDepotDetail.PageSize = ps;
        //   pager_V_InOutDepotDetail.PageIndex = pi;
        //   pager_V_InOutDepotDetail.OrderByLambda = a => a.IODDID;
        //    pager_V_InOutDepotDetail.WhereLambda = a => true;
        //    var set = await pager_V_InOutDepotDetail.Paging().ConfigureAwait(false);
        //    return new Dictionary<string, dynamic>() { { "data", set }, { "total", pager_V_InOutDepotDetail.Count } };
        //}
        //[HttpGet]
        //[Route("GetVProducts/{DepotID}")]
        //public async Task<List<V_Products>> GetVProducts(String DepotID)
        //{
        //    return await ssss.Select_KeyProduct(DepotID).ConfigureAwait(false);
        //}




        public void Options() { }  //这是预请求
    }
}
