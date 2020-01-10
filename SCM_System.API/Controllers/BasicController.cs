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

        [HttpPost]
        [Route("UPDATEuniversalModuelCustomers")]
        public async Task<int> UPDATEuniversalModuelCustomers(Customers s)
        {
            return await universalModuelCustomers.Update_Key(s.CusID, s);
        }
        [HttpPost]
        [Route("INSERTuniversalCustomers")]
        public async Task<int> INSERTuniversalCustomers(Customers s)
        {
            return await universalModuelCustomers.Insert(s);
        }


        //删除商品资料+修改
        [HttpGet]
        [Route("DeluniversalModuelProducts/{id}")]
        public async Task<int> DeluniversalModuelProducts(string id)
        {
            return await universalModuelProducts.Delete_Key(id);
        }
        [HttpPost]
        [Route("UPuniversalModuelProducts")]
        public async Task<int> UPuniversalModuelProducts(Products s)
        {
            return await universalModuelProducts.Update_Key(s.ProID, s);
        }
        [HttpPost]
        [Route("INSERTuniversalModuelProducts")]
        public async Task<int> INSERTuniversalModuelProducts(Products s)
        {
            return await universalModuelProducts.Insert(s);
        }
        [HttpGet]
        [Route("SeleKeyuniversalModuelProducts/{idd}")]
        public async Task<Products> SeleKeyuniversalModuelProducts(string idd)
        {
            return await universalModuelProducts.Select_Key(idd);
        }
        //下拉列表类别表
        [HttpGet]
        [Route("GetuniversalModuelProductTypes")]
        public async Task<List<ProductTypes>> GetuniversalModuelProductTypes()
        {
            return await db.ProductTypes.ToListAsync();
        }
        //下拉列表颜色表
        [HttpGet]
        [Route("GetuniversalModuelProductColor")]
        public async Task<List<ProductColor>> GetuniversalModuelProductColor()
        {
            return await db.ProductColor.ToListAsync();
        }
        //下拉列表单位表
        [HttpGet]
        [Route("GetuniversalModuelProductUnit")]
        public async Task<List<ProductUnit>> GetuniversalModuelProductUnit()
        {
            return await db.ProductUnit.ToListAsync();
        }
        //下拉列表规格表
        [HttpGet]
        [Route("GetuniversalModuelProductSpec")]
        public async Task<List<ProductSpec>> GetuniversalModuelProductSpec()
        {
            return await db.ProductSpec.ToListAsync();
        }


        //供应商增删改查
        [HttpGet]
        [Route("DeluniversalModuelProductLend/{idd}")]
        public async Task<int> DeluniversalModuelProductLend(string idd)
        {
            return await universalModuelProductLend.Delete_Key(idd);
        }
        [HttpPost]
        [Route("UPuniversalModuelProductLend")]
        public async Task<int> UPuniversalModuelProductLend(ProductLend s)
        {
            return await universalModuelProductLend.Update_Key(s.PPID,s);
        }
        [HttpPost]
        [Route("INSERTuniversalProductLend")]
        public async Task<int> INSERTuniversalProductLend(ProductLend s)
        {
            return await universalModuelProductLend.Insert(s);
        }
        [HttpGet]
        [Route("SeleKeyuniversalModuelProductLend/{idd}")]
        public async Task<ProductLend> SeleKeyuniversalModuelProductLend(string idd)
        {
            return await universalModuelProductLend.Select_Key(idd);
        }



        //仓库模板
        [HttpGet]
        [Route("DeluniversalModuelDepots/{id}")]
        public async Task<int> DeluniversalModuelDepots(string id)
        {
            return await universalModuelDepots.Delete_Key(id);
        }
        [HttpPost]
        [Route("UPuniversalModuelDepots")]
        public async Task<int> UPuniversalModuelDepots(Depots s)
        {
            return await universalModuelDepots.Update_Key(s.DepotID, s);
        }
        [HttpPost]
        [Route("INSERTuniversalDepots")]
        public async Task<int> INSERTuniversalDepots(Depots s)
        {
            return await universalModuelDepots.Insert(s);
        }

        [HttpGet]
        [Route("SeleKeyuniversalModuelDepots/{idd}")]
        public async Task<Depots> SeleKeyuniversalModuelDepots(string idd)
        {
            return await universalModuelDepots.Select_Key(idd);
        }

        //商品类型模板
        [HttpGet]
        [Route("DeluniversalModuelProductTypes/{id}")]
        public async Task<int> DeluniversalModuelProductTypes(int id)
        {
            return await universalModuelProductTypes.Delete_Key(id);
        }
        [HttpPost]
        [Route("UPuniversalModuelProductTypes")]
        public async Task<int> UPuniversalModuelProductTypes(ProductTypes s)
        {
            return await universalModuelProductTypes.Update_Key(s.PTID, s);
        }
        [HttpPost]
        [Route("INSERTuniversalProductTypes")]
        public async Task<int> INSERTuniversalProductTypes(ProductTypes s)
        {
            return await universalModuelProductTypes.Insert(s);
        }

        [HttpGet]
        [Route("SeleKeyuniversalModuelProductTypes/{idd}")]
        public async Task<ProductTypes> SeleKeyuniversalModuelProductTypes(int idd)
        {
            return await universalModuelProductTypes.Select_Key(idd);
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
