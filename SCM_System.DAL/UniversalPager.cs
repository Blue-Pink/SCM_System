using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SCM_System.Model;
namespace SCM_System.DAL
{
    public class UniversalPager<T,TKey>:IDAL.IUniversalPager<T,TKey> where T : class
    {
        public Expression<Func<T, bool>> WhereLambda { get; set; }
        public Expression<Func<T, TKey>> OrderByLambda { get; set; }
        public Expression<Func<T, TKey>> SelectLambda { get; set; }
        /// <summary>
        /// 页内单例数量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// true = OrderBy,false = OrderByDescending
        /// </summary>
        public bool IsAsc { get; set; }
        /// <summary>
        /// T 内单例总数
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// 页量总数
        /// </summary>
        public int Count { get; set; }

        [Ninject.Inject]
        public SCMEntities entities { get; set; }

        public List<T> Paging()
        {
            //注意顺序
            Count = entities.Set<T>().Where(WhereLambda).Count();
            PageCount = Count % PageSize == 0 ? Count / PageSize:Count / PageSize + 1;
            var list = entities.Set<T>().Where(WhereLambda);
            if (IsAsc)
            {
                list = list.OrderBy(OrderByLambda);
            }
            else
            {
                list = list.OrderByDescending(OrderByLambda);
            }

            return list.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

    }
}