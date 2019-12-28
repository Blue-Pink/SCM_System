using Ninject;
using SCM_System.DAL;
using SCM_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SCM_System.API.Controllers
{
    [RoutePrefix("api/Basic")]
    public class BasicController : ApiController
    {
        [Inject]
        //通用模块
        //商品分类表 
        public DAL_UniversalModuel<ProductTypes> universalModuelProductTypes { get; set; }

        [Inject]
        //通用模块
        //仓库设置表
        public DAL_UniversalModuel<Depots> universalModuelDepots { get; set; }

        [Inject]
        //通用模块
        //客户资料表
        public DAL_UniversalModuel<Products> universalModuelProducts { get; set; }

        [Inject]
        //通用模块
        //供应商表
        public DAL_UniversalModuel<ProductLend> universalModuelProductLend { get; set; }

        [Inject]
        //通用模块
        //客户资料资料表
        public DAL_UniversalModuel<Vw_CL> universalModuelCustomers { get; set; }

        //[Inject]
        ////项目对应模块
        //public DAL_BasicModuel<CheckDepot> basicModuel { get; set; }
        [Inject]
        //分页器
        public UniversalPager<CheckDepot, dynamic> pager { get; set; }

        [Route("GetProductTypes")]
        public async Task<List<ProductTypes>> GetProductTypes() {
            return await universalModuelProductTypes.Select_All();
        }

        [Route("GetDepots")]
        public async Task<List<Depots>> GetDepots()
        {
            return await universalModuelDepots.Select_All();
        }

        [Route("GetProducts")]
        public async Task<List<Products>> GetProducts()
        {
            return await universalModuelProducts.Select_All();
        }

        [Route("GetProductLend")]
        public async Task<List<ProductLend>> GetProductLend()
        {
            return await universalModuelProductLend.Select_All();
        }

        [Route("GetCustomers")]
        public async Task<List<Vw_CL>> GetCustomers()
        {
            return await universalModuelCustomers.Select_All();
        }





        public void Options() { }  //这是预请求
    }
}
