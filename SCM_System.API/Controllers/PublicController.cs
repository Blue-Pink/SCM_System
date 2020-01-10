using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SCM_System.DAL;
using SCM_System.Model;
using Ninject;
using System.Threading.Tasks;
using SCM_System.Model.PageClass;

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

        #region Create
        [Inject]
        public DAL_PublicModuel<CustomerLevel> dcl { get; set; }
        [Inject]
        public DAL_PublicModuel<ProductTypes> dpt { get; set; }
        [Inject]
        public DAL_PublicModuel<V_Products> dvp { get; set; }
        [Inject]
        public DAL_PublicModuel<ProductLend> dpl { get; set; }
        [Inject]
        public DAL_PublicModuel<V_StockAndDetailAndProLendAndUsers> dvsdpu { get; set; }
        [Inject]
        public DAL_PublicModuel<V_QuotePriceAndDetailAndCustomers> dqpdc { get; set; }
        [Inject]
        public DAL_PublicModuel<V_CusAndCusOrderAndClv> dvcl { get; set; }
        [Inject]
        public DAL_PublicModuel<V_VProductsAndDepotStock> dvvpds { get; set; }

        [Inject]
        public DAL_PublicModuel<V_CustomersAndLevel> dvcal { get; set; }


        #endregion



        #region MyRegion
        /// <summary>
        /// 根据条件查询用户表和层级表的信息并分页
        /// </summary>
        /// <param name="cname">客户名称</param>
        /// <param name="level">层级</param>
        /// <param name="index">页码</param>
        /// <param name="size">页大小</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCustomersUponConditionPage")]
        public MyPage<V_CustomersAndLevel> GetCustomersUponConditionPage(string cname = "输入名称", int level = 0, UInt16 index = 1, UInt16 size = 3)
        {
            return dvcal.uponConditionPageForCustomers(cname, level, Convert.ToInt32(index), Convert.ToInt32(size));
        }

        #endregion


        /// <summary>
        /// 查询所有的客户等级信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCustomerLevelAll")]
        public Task<List<CustomerLevel>> GetCustomerLevelAll()
        {
            return dcl.Select_All();
        }
        /// <summary>
        /// 根据条件查询商品视图的信息并分页
        /// </summary>
        /// <param name="pname">商品名称</param>
        /// <param name="plevel">商品类型</param>
        /// <param name="index">页码</param>
        /// <param name="size">页大小</param>
        /// <returns></returns>
        [Route("GetVProductsAllAboutPage")]
        public MyPage<V_Products> GetVProductsAllAboutPage(string pname = "输入名称", int plevel = 0, UInt16 index = 1, UInt16 size = 3)
        {
            return dvp.uponConditionPageForVProducts(pname, plevel, Convert.ToInt32(index), Convert.ToInt32(size));
        }

        #region other
        #region MyRegion
        // <summary>
        /// 根据条件查询商品视图的信息并分页(有数量)
        /// </summary>
        /// <param name="pname">商品名称</param>
        /// <param name="plevel">商品类型</param>
        /// <param name="index">页码</param>
        /// <param name="size">页大小</param>
        /// <returns></returns>
        [Route("GetVProductsAllAboutPageHaveCount")]
        public MyPage<V_VProductsAndDepotStock> GetVProductsAllAboutPageHaveCount(string pname = "输入名称", int plevel = 0, UInt16 index = 1, UInt16 size = 3)
        {
            return dvvpds.uponConditionPageForVProductsHaveCount(pname, plevel, Convert.ToInt32(index), Convert.ToInt32(size));
        }


        /// <summary>
        /// 得到所有的商品类型信息
        /// </summary>
        /// <returns></returns>
        [Route("GetProductTypesAll")]
        public Task<List<ProductTypes>> GetProductTypesAll()
        {
            return dpt.Select_All();
        }



        /// <summary>
        /// 根据条件查询供货商的信息并分页
        /// </summary>
        /// <param name="name">供货商的名字</param>
        /// <param name="index">页码</param>
        /// <param name="size">页大小</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetProductLendsAllAboutPage")]
        public MyPage<ProductLend> GetProductLendsAllAboutPage(string name = "输入名称", UInt16 index = 1, UInt16 size = 3)
        {
            return dpl.uponConditionPageForProductLend(name, Convert.ToInt32(index), Convert.ToInt32(size));
        }


        /// <summary>
        /// 根据条件得到V_CusAndCusOrderAndClv视图的信息并分页
        /// </summary>
        /// <param name="name">客户名称</param>
        /// <param name="index">页码</param>
        /// <param name="size">页大小</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetV_CusAndCusOrderAndClvUponCondtionAndPage")]
        public MyPage<V_CusAndCusOrderAndClv> GetV_CusAndCusOrderAndClvUponCondtionAndPage(string name = "输入名称", UInt16 index = 1, UInt16 size = 3)
        {

            return dvcl.uponConditionPageFor_VCusAndCusOrderAndClv(name, Convert.ToInt32(index), Convert.ToInt32(size));
        }


        /// <summary>
        /// 根据报价单编号和客户名称对V_QuotePriceAndDetailAndCustomers视图进行分页
        /// </summary>
        /// <param name="name">客户名称</param>
        /// <param name="level">报价单编号</param>
        /// <param name="index">页码</param>
        /// <param name="size">页大小</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetV_QuotePriceAndDetailAndCustomersUponCondtionAndPage")]
        public MyPage<V_QuotePriceAndDetailAndCustomers> GetV_QuotePriceAndDetailAndCustomersUponCondtionAndPage(string name = "输入名称", string level = "输入名称", UInt16 index = 1, UInt16 size = 3)
        {

            return dqpdc.uponConditionPageForV_QuotePriceAndDetailAndCustomers(name, level, Convert.ToInt32(index), Convert.ToInt32(size));
        }



        /// <summary>
        /// 根据条件得到V_StockAndDetailAndProLendAndUsers视图的信息并分页
        /// </summary>
        /// <param name="name">创建人</param>
        /// <param name="level">供货商</param>
        /// <param name="index">页码</param>
        /// <param name="size">页大小</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetV_StockAndDetailAndProLendAndUsersPageUponCondition")]
        public MyPage<V_StockAndDetailAndProLendAndUsers> GetV_StockAndDetailAndProLendAndUsersPageUponCondition(string name = "输入名称", string level = "输入名称", UInt16 index = 1, UInt16 size = 3)
        {
            return dvsdpu.TwouponConditionPageForV_StockAndDetailAndProLendAndUsers(name, level, Convert.ToInt32(index), Convert.ToInt32(size));
        }
        #endregion
        #endregion

        /// <summary>
        /// 得到所有个供货商信息
        /// </summary>
        /// <returns></returns>
        [Route("GetProductLendsAll")]
        public Task<List<ProductLend>> GetProductLendsAll()
        {
            return dpl.Select_All();
        }


        public void Options() { }  //这是预请求
    }
}
