using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Ninject;
using SCM_System.DAL;
using SCM_System.Model;

namespace SCM_System.API.Controllers
{
    [RoutePrefix("api/Home")]
    public class HomeController : ApiController
    {
        [Inject]
        public DAL_UniversalModuel<CheckDepot> universalModuel { get; set; }
        [Inject]
        public UniversalPager<CheckDepot,dynamic> pager { get; set; }
        #region API测试代码
        [HttpGet]
        [Route("GetCDsA")]
        public async Task<List<CheckDepot>> GetCDsA()
        {
            return await universalModuel.Select_All();
        }

        [HttpGet]

        [Route("GetCDA/{key}")]
        public async Task<CheckDepot> GetCDA([FromUri]dynamic key)
        {
            return await universalModuel.Select_Key(key);
        }

        [HttpGet]
        [Route("GetCDsA_P")]
        public async Task<List<CheckDepot>> GetCDsA_P([FromUri]string properties_json)
        {
            Dictionary<string, dynamic> properties = universalModuel.JsonToDictionary(properties_json);
            return await universalModuel.Select_Properties(properties);
        }

        [HttpGet]
        [Route("GetCD_K")]
        public async Task<CheckDepot> GetCD_K([FromUri]dynamic key)
        {
            return await universalModuel.Select_Key(key);
        }

        [HttpDelete]
        [Route("DeleteCD")]
        public async Task<int> DeleteCD([FromUri]dynamic key)
        {
            return await universalModuel.Delete_Key(key);
        }

        [HttpDelete]
        [Route("DeleteCD_P")]
        public async Task<int> DeleteCD_P([FromUri]string properties_json)
        {
            Dictionary<string, dynamic> properties = universalModuel.JsonToDictionary(properties_json);
            return await universalModuel.Delete_Properties(properties);
        }

        [HttpPost]
        [Route("PostCDI")]
        public async Task<int> PostCDI([FromBody]CheckDepot checkDepot)
        {
            return await universalModuel.Insert(checkDepot);
        }

        [HttpPut]
        [Route("PutCD_K")]
        public async Task<int> PutCD_K([FromUri] dynamic key, [FromBody]CheckDepot checkDepot)
        {
            return await universalModuel.Update_Key(key, checkDepot);
        }

        [HttpPut]
        [Route("PutCD_P")]
        public async Task<int> PutCD_P([FromUri]string properties_json, [FromBody]CheckDepot checkDepot)
        {
            Dictionary<string, dynamic> properties = universalModuel.JsonToDictionary(properties_json);
            return await universalModuel.Update_Properties(properties, checkDepot);
        }

        [HttpGet]
        [Route("GetCDCA")]
        public async Task<int> GetCDCA()
        {
            return await universalModuel.ObtainCount_All();
        }

        [HttpGet]
        [Route("GetCDC_P")]
        public async Task<int> GetCDC_P([FromUri]string properties_json)
        {
            Dictionary<string, dynamic> properties = universalModuel.JsonToDictionary(properties_json);
            return await universalModuel.ObtainCount_Properties(properties);
        }

        [HttpGet]
        [Route("GetCDP")]
        public Task<List<CheckDepot>> GetCDP(int page_index, int page_size)
        {
            throw new Exception("");
        }

        [HttpGet]
        [Route("GetPaging")]
        public Dictionary<string,dynamic> GetPaging()
        {
            pager.IsAsc = true;
            pager.PageSize = 3;
            pager.OrderByLambda = a => a.CDID;
            pager.WhereLambda = a => true;
            pager.PageIndex = 1;
            return new Dictionary<string, dynamic>() { {"count", pager.Count },{"data",pager.Paging()} };
        }
        #endregion
    }
}
