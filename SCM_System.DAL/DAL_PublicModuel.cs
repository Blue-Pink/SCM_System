using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCM_System.Model;
using SCM_System.Model.PageClass;
namespace SCM_System.DAL
{
    public class DAL_PublicModuel<T> : DAL_UniversalModuel<T>, IDAL.IDAL_UniversalModuel<T> where T : class
    {

        #region V_QuotePriceAndDetailAndCustomers
        /// <summary>
        /// 根据条件对V_QuotePriceAndDetailAndCustomers视图分页
        /// </summary>
        /// <param name="pname">客户名称</param>
        /// <param name="plevel">报价订单编号</param>
        /// <param name="index">页码</param>
        /// <param name="size">页大小</param>
        /// <returns></returns>
        public MyPage<V_QuotePriceAndDetailAndCustomers> uponConditionPageForV_QuotePriceAndDetailAndCustomers(string pname, string plevel, int index = 1, int size = 3)
        {
            List<V_QuotePriceAndDetailAndCustomers> ls = base.entities.V_QuotePriceAndDetailAndCustomers.ToList();
            if (!pname.Equals("输入名称"))
            {
                ls = ls.Where(x => x.CusName.Contains(pname)).ToList();
            }
            if (!plevel.Equals("输入名称"))
            {
                ls = ls.Where(x => x.QPID.Contains(plevel)).ToList();
            }
            int total = ls.Count();
            ls = ls.OrderBy(x => x.QPDID).Skip((index - 1) * size).Take(size).ToList();
            return base.uponConditionPage<V_QuotePriceAndDetailAndCustomers>(ls, total);
        }
        #endregion

        #region V_CustomersAndLevel
        /// < summary >
        /// 对用户表根据条件进行分页查询
        /// </ summary >
        /// < param name = "cname" > 用户名 </ param >
        /// < param name = "clevel" > 用户等级 </ param >
        /// < param name = "index" > 页码 </ param >
        /// < param name = "size" > 页大小 </ param >
        /// < returns ></ returns >
        public MyPage<V_CustomersAndLevel> uponConditionPageForCustomers(string cname, int clevel, int index = 1, int size = 3)
        {
            List<V_CustomersAndLevel> ls = base.entities.V_CustomersAndLevel.ToList();
            if (!cname.Equals("输入名称"))
            {
                ls = ls.Where(x => x.CusName.Contains(cname)).ToList();
            }
            if (clevel > 0)
            {
                ls = ls.Where(x => x.CLID == clevel).ToList();
            }
            ls = ls.OrderBy(x => x.CusID).Skip((index - 1) * size).Take(size).ToList();
            int total = ls.Count();
            return base.uponConditionPage<V_CustomersAndLevel>(ls, total);
        }
        #endregion


        #region 根据条件对客户订单表,订单详表和客户等级表和客户表的视图进行分页查询
        /// <summary>
        /// 对VCusAndCusOrderAndClv视图根据条件分页
        /// </summary>
        /// <param name="cname">客户名称</param>
        /// <param name="index">页码</param>
        /// <param name="size">页大小</param>
        /// <returns></returns>
        public MyPage<V_CusAndCusOrderAndClv> uponConditionPageFor_VCusAndCusOrderAndClv(string cname, int index = 1, int size = 3)
        {
            List<V_CusAndCusOrderAndClv> ls = base.entities.V_CusAndCusOrderAndClv.ToList();
            if (!cname.Equals("输入名称"))
            {
                ls = ls.Where(x => x.CusName.Contains(cname)).ToList();
            }
            int total = ls.Count();
            ls = ls.OrderBy(x => x.CODID).Skip((index - 1) * size).Take(size).ToList();
            return base.uponConditionPage<V_CusAndCusOrderAndClv>(ls, total);
        }
        #endregion


        #region V_Products

        /// <summary>
        /// 在商品视图里根据条件查询并且分页(没有仓库数量一列)
        /// </summary>
        /// <param name="pname"></param>
        /// <param name="plevel"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public MyPage<V_Products> uponConditionPageForVProducts(string pname, int plevel, int index = 1, int size = 3)
        {
            List<V_Products> ls = base.entities.V_Products.ToList(); ;
            if (!pname.Equals("输入名称"))
            {
                ls = ls.Where(x => x.ProName.Contains(pname)).ToList();
            }
            if (plevel > 0)
            {
                ls = ls.Where(x => x.PTID == plevel).ToList();
            }
            int total = ls.Count();
            ls = ls.OrderBy(x => x.ProID).Skip((index - 1) * size).Take(size).ToList();
            return base.uponConditionPage<V_Products>(ls, total);
        }

        /// <summary>
        /// 根据条件分页对于VProducts视图并且显示库存
        /// </summary>
        /// <param name="pname">客户名称</param>
        /// <param name="plevel">客户等级</param>
        /// <param name="index">页码</param>
        /// <param name="size">页大小</param>
        /// <returns></returns>
        public MyPage<V_VProductsAndDepotStock> uponConditionPageForVProductsHaveCount(string pname, int plevel, int index = 1, int size = 3)
        {
            List<V_VProductsAndDepotStock> ls = base.entities.V_VProductsAndDepotStock.ToList();
            if (!pname.Equals("输入名称"))
            {
                ls = ls.Where(x => x.ProName.Contains(pname)).ToList();
            }
            if (plevel > 0)
            {
                ls = ls.Where(x => x.PTID == plevel).ToList();
            }
            int total = ls.Count();
            ls = ls.OrderBy(x => x.ProID).Skip((index - 1) * size).Take(size).ToList();
            return base.uponConditionPage<V_VProductsAndDepotStock>(ls, total);
        }
        #endregion



        #region V_StockAndDetailAndProLendAndUsers
        /// <summary>
        /// 对V_StockAndDetailAndProLendAndUsers视图根据条件进行分页
        /// </summary>
        /// <param name="cname">订单编号</param>
        /// <param name="clevel">供货商名称</param>
        /// <param name="index">页码</param>
        /// <param name="size">页大小</param>
        /// <param name="uname">创建人</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public MyPage<V_StockAndDetailAndProLendAndUsers> uponConditionPageForV_StockAndDetailAndProLendAndUsers(string cname, string clevel, string uname, int state, int index = 1, int size = 3)
        {
            List<V_StockAndDetailAndProLendAndUsers> ls = base.entities.V_StockAndDetailAndProLendAndUsers.ToList();
            if (!cname.Equals("输入名称"))
            {
                ls = ls.Where(x => x.StockID.Contains(cname)).ToList();
            }
            if (!clevel.Equals("输入名称"))
            {
                ls = ls.Where(x => x.PPCompany.Equals(clevel)).ToList();
            }
            if (!uname.Equals("输入名称"))
            {
                ls = ls.Where(x => x.UsersName.Contains(cname)).ToList();
            }
            if (state > 0)
            {
                ls = ls.Where(x => x.StockState == state).ToList();
            }
            int total = ls.Count();
            ls = ls.OrderBy(x => x.SDetailID).Skip((index - 1) * size).Take(size).ToList();
            return base.uponConditionPage<V_StockAndDetailAndProLendAndUsers>(ls, total);
        }
        #endregion


        /// <summary>
        /// 对V_StockAndDetailAndProLendAndUsers视图根据条件进行分页
        /// </summary>
        /// <param name="cname">订单编号</param>
        /// <param name="clevel">供货商名称</param
        /// <param name="index">页码</param>
        /// <param name="size">页大小</param>
        /// <returns></returns>
        public MyPage<V_StockAndDetailAndProLendAndUsers> TwouponConditionPageForV_StockAndDetailAndProLendAndUsers(string cname, string clevel, int index = 1, int size = 3)
        {
            List<V_StockAndDetailAndProLendAndUsers> ls = base.entities.V_StockAndDetailAndProLendAndUsers.ToList();
            if (!cname.Equals("输入名称"))
            {
                ls = ls.Where(x => x.UsersName.Contains(cname)).ToList();
            }
            if (!clevel.Equals("输入名称"))
            {
                ls = ls.Where(x => x.PPID.Equals(clevel)).ToList();
            }
            int total = ls.Count();
            ls = ls.OrderBy(x => x.SDetailID).Skip((index - 1) * size).Take(size).ToList();
            return base.uponConditionPage<V_StockAndDetailAndProLendAndUsers>(ls, total);
        }

        #region ProductLend
        /// <summary>
        /// 根据条件查询供货商表的信息并分页
        /// </summary>
        /// <param name="pname">供货商名字</param>
        /// <param name="index">页码</param>
        /// <param name="size">页大小</param>
        /// <returns></returns>
        public MyPage<ProductLend> uponConditionPageForProductLend(string pname, int index = 1, int size = 3)
        {
            List<ProductLend> ls = base.entities.ProductLend.ToList();
            if (!pname.Equals("输入名称"))
            {
                ls = ls.Where(x => x.PPName.Contains(pname)).ToList();
            }
            int total = ls.Count();
            ls = ls.OrderBy(x => x.PPID).Skip((index - 1) * size).Take(size).ToList();
            return base.uponConditionPage<ProductLend>(ls, total);
        }
        #endregion
    }
}
