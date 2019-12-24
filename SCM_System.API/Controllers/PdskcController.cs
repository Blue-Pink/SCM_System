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
        //项目对应模块
        public DAL_PdskcModuel<CheckDepot> basicModuel { get; set; }
        [Inject]
        //分页器
        public UniversalPager<CheckDepot, dynamic> pager { get; set; }


        /// <summary>
        /// 查询当前所有库存
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetVw_CDUsAsync")]
        public  int GetVw_CDUsAsync()
        {
            return 1;
        }

        public void Options() { }  //这是预请求
    }
}
