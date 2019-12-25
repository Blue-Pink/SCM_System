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

        [HttpGet]
        [Route("GetS_PL_Us")]
        public async Task<dynamic> GetS_PL_Us()
        {
            return await buyModuel.GetS_PL_Us().ConfigureAwait(false);
        }

        public void Options() { }  //这是预请求
    }
}
