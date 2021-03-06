﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Ninject;
using SCM_System.DAL;
using SCM_System.Model;

namespace SCM_System.API.Controllers
{
    [RoutePrefix("api/Home")]
    public class HomeController : ApiController
    {
        #region API测试代码
        [Inject]
        //通用模块
        public DAL_UniversalModuel<CheckDepot> universalModuel { get; set; }
        [Inject]
        //项目对应模块
        public DAL_BasicModuel<CheckDepot> basicModuel { get; set; }

        [Inject]
        //分页器
        public UniversalPager<CheckDepot, dynamic> pager { get; set; }


        [HttpGet]
        [Route("GetCDsA")]
        //GetCheckDepotsAll
        public async Task<List<CheckDepot>> GetCDsA()
        {
            return await universalModuel.Select_All().ConfigureAwait(false);
        }
        
        [HttpGet]
        [Route("GetCDA/{key}")]
        public async Task<CheckDepot> GetCDA([FromUri]dynamic key)
        {
            return await universalModuel.Select_Key(key);
        }

        [HttpGet]
        [Route("GetCDsA_P")]
        //GetCheckDepots_properties_json
        public async Task<List<CheckDepot>> GetCDsA_P([FromUri]string properties_json)
        {
            Dictionary<string, dynamic> properties = universalModuel.JsonToDictionary(properties_json);
            return await universalModuel.Select_Properties(properties).ConfigureAwait(false);
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
            return await universalModuel.Delete_Properties(properties).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("PostCDI")]
        public async Task<int> PostCDI([FromBody]CheckDepot checkDepot)
        {
            return await universalModuel.Insert(checkDepot).ConfigureAwait(false) ;
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
            return await universalModuel.Update_Properties(properties, checkDepot).ConfigureAwait(false) ;
        }

        [HttpGet]
        [Route("GetCDCA")]
        public async Task<int> GetCDCA()
        {
            return await universalModuel.ObtainCount_All().ConfigureAwait(false);
        }

        [HttpGet]
        [Route("GetCDC_P")]
        public async Task<int> GetCDC_P([FromUri]string properties_json)
        {
            Dictionary<string, dynamic> properties = universalModuel.JsonToDictionary(properties_json);
            return await universalModuel.ObtainCount_Properties(properties).ConfigureAwait(false);
        }

        [HttpGet]
        [Route("GetCDP")]
        public Task<List<CheckDepot>> GetCDP(int page_index, int page_size)
        {
            throw new Exception("");
        }

        [HttpGet]
        [Route("GetPaging")]
        public dynamic GetPaging()
        {
            pager.IsAsc = true;
            pager.PageSize = 100;
            pager.OrderByLambda = a => a.CDID;
            pager.WhereLambda = a => a.CDState==4;
            pager.PageIndex = 1;
            return new Dictionary<string, dynamic>() { {"data", pager.Paging() },{"count", pager.Count }};
        }

        [HttpGet]
        [Route("GetTest")]
        public async Task<dynamic> GetTest()
        {
            return await basicModuel.Select_All().ConfigureAwait(false);
        }
        #endregion

        public void Options() { }  //这是预请求
    }
}
