using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SCM_System.DAL;
using SCM_System.Model;
using Ninject;

namespace SCM_System.API.Controllers
{
    [RoutePrefix("api/Public")]
    public class PublicController : ApiController
    {

        [Inject]
        //通用模块
        public DAL_UniversalModuel<CheckDepot> universalModuel { get; set; }
        [Inject]
        //项目对应模块
        public DAL_BasicModuel<CheckDepot> basicModuel { get; set; }
        [Inject]
        //分页器
        public UniversalPager<CheckDepot, dynamic> pager { get; set; }

      
        public void Options() { }  //这是预请求
    }
}
