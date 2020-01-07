using Ninject;
using SCM_System.DAL;
using SCM_System.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        //客户资料
        public DAL_UniversalModuel<Customers> universalModuelCustomers { get; set; }

        [Inject]
        ////通用模块
        ////客户等级
        public DAL_UniversalModuel<CustomerLevel> universalModuelCustomerLevel { get; set; }

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
        //供应商表
        public DAL_UniversalModuel<Depots> universalModuelDepots { get; set; }
        
                [Inject]
        //通用模块
        //供应商表
        public DAL_UniversalModuel<ProductTypes> universalModuelProductTypes { get; set; }
        //[Inject]
        ////通用模块
        ////客户资料资料表
        //public DAL_UniversalModuel<Vw_CL> universalModuelCustomers { get; set; }

        //[Inject]
        ////通用模块
        ////商品资料资料表
        //public DAL_UniversalModuel<Vw_DPP> universalModuelVw_DPP { get; set; }

        //[Inject]
        ////项目对应模块
        //public DAL_BasicModuel<CheckDepot> basicModuel { get; set; }


        //分页后
        [Inject]
        ////通用模块
        ////客户资料资料表
        public UniversalPager<Vw_CL, dynamic> pager_Vw_CL { get; set; }

        [Inject]
        ////通用模块
        ////商品资料资料表
        public UniversalPager<Vw_DPP, dynamic> pager_Vw_DPP { get; set; }

        [Inject]
        //通用模块
        //供应商表
        public UniversalPager<ProductLend, dynamic> pager_ProductLend { get; set; }

        [Inject]
        ////通用模块
        ////客户资料表
        public UniversalPager<Products, dynamic> pager_Products { get; set; }

        [Inject]
        //通用模块
        //仓库设置表
        public UniversalPager<Depots, dynamic> pager_Depots { get; set; }

        [Inject]

        ////通用模块
        ////商品分类表 
        public UniversalPager<ProductTypes, int> pager_ProductTypes { get; set; }


        [Inject]
        public SCMEntities db { get; set; }


        [HttpGet]
        [Route("DeluniversalModuelCustomers/{id}")]
        public async Task<int> DeluniversalModuelCustomers(string id)
        {
            return await universalModuelCustomers.Delete_Key(id);
        }

        [HttpGet]
        [Route("DeluniversalModuelProducts/{id}")]
        public async Task<int> DeluniversalModuelProducts(string id)
        {
            return await universalModuelProducts.Delete_Key(id);
        }

        [HttpGet]
        [Route("DeluniversalModuelProductLend/{id}")]
        public async Task<int> DeluniversalModuelProductLend(string id)
        {
            return await universalModuelProductLend.Delete_Key(id);
        }

        [HttpGet]
        [Route("DeluniversalModuelDepots/{id}")]
        public async Task<int> DeluniversalModuelDepots(string id)
        {
            return await universalModuelDepots.Delete_Key(id);
        }

        [HttpGet]
        [Route("DeluniversalModuelProductTypes/{id}")]
        public async Task<int> DeluniversalModuelProductTypes(int id)
        {
            return await universalModuelProductTypes.Delete_Key(id);
        }

        [HttpPost]
        [Route("UPDATEuniversalModuelCustomers")]
        public async Task<int> UPDATEuniversalModuelCustomers(Customers s)
        {
            return await universalModuelCustomers.Update_Key(s.CusID, s);
        }

        [HttpGet]
        [Route("GetuniversalModuelCustomerLevel")]
        public async Task<List<CustomerLevel>> GetuniversalModuelCustomerLevel()
        {
            return await db.CustomerLevel.ToListAsync();
        }
        //修改客户资料ID查询
        [HttpGet]
        [Route("SeleKeyuniversalModuelCustomers/{id}")]
        public async Task<Customers> SeleKeyuniversalModuelCustomers(string id)
        {
            return await universalModuelCustomers.Select_Key(id);
        }

        [Route("GetProductTypes")]
        public async Task<dynamic> GetProductTypes(int ps, int pi) {
            pager_ProductTypes.IsAsc = true;
            pager_ProductTypes.PageSize = ps;
            pager_ProductTypes.PageIndex = pi;
            pager_ProductTypes.WhereLambda = a => true;
            pager_ProductTypes.OrderByLambda = a => a.PTID;
            var set = await pager_ProductTypes.Paging().ConfigureAwait(false);
            return new Dictionary<string, dynamic>() { { "data", set }, { "total", pager_ProductTypes.Count } };
        }

        [Route("GetDepots")]
        public async Task<dynamic> GetDepots(int ps, int pi)
        {

            pager_Depots.IsAsc = true;
            pager_Depots.PageSize = ps;
            pager_Depots.PageIndex = pi;
            pager_Depots.WhereLambda = a => true;
            pager_Depots.OrderByLambda = a => a.DepotID;
            var set = await pager_Depots.Paging().ConfigureAwait(false);
            return new Dictionary<string, dynamic>() { { "data", set }, { "total", pager_Depots.Count } };
        }

        [Route("GetProducts")]
        public async Task<dynamic> GetProducts(int ps, int pi)
        {

            pager_Products.IsAsc = true;
            pager_Products.PageSize = ps;
            pager_Products.PageIndex = pi;
            pager_Products.WhereLambda = a => true;
            pager_Products.OrderByLambda = a => a.ProID;
            var set = await pager_Products.Paging().ConfigureAwait(false);
            return new Dictionary<string, dynamic>() { { "data", set }, { "total", pager_Products.Count } };
        }

        [Route("GetProductLend")]
        public async Task<dynamic> GetProductLend(int ps, int pi)
        {

            pager_ProductLend.IsAsc = true;
            pager_ProductLend.PageSize = ps;
            pager_ProductLend.PageIndex = pi;
            pager_ProductLend.WhereLambda = a => true;
            pager_ProductLend.OrderByLambda = a => a.PPID;
            var set = await pager_ProductLend.Paging().ConfigureAwait(false);
            return new Dictionary<string, dynamic>() { { "data", set }, { "total", pager_ProductLend.Count } };
        }

        [Route("GetCustomers")]
        public async Task<dynamic> GetCustomers(int ps,int pi)
        {
            pager_Vw_CL.IsAsc = true;
            pager_Vw_CL.PageSize = ps;
            pager_Vw_CL.PageIndex = pi;
            pager_Vw_CL.WhereLambda = a => true;
            pager_Vw_CL.OrderByLambda = a => a.CusID;
            var set = await pager_Vw_CL.Paging().ConfigureAwait(false);
            return new Dictionary<string, dynamic>() { { "data", set }, { "total", pager_Vw_CL.Count } };
        }


        [Route("GetVw_DPP")]
        public async Task<dynamic> GetVw_DPP(int ps, int pi)
        {
            pager_Vw_DPP.IsAsc = true;
            pager_Vw_DPP.PageSize = ps;
            pager_Vw_DPP.PageIndex = pi;
            pager_Vw_DPP.WhereLambda = a => true;
            pager_Vw_DPP.OrderByLambda = a => a.ProID;
            var set = await pager_Vw_DPP.Paging().ConfigureAwait(false);
            return new Dictionary<string, dynamic>() { { "data", set }, { "total", pager_Vw_DPP.Count } };
        }


        public void Options() { }  //这是预请求
    }
}
