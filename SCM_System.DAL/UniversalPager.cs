using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SCM_System.Model;
namespace SCM_System.DAL
{
    public class UniversalPager<T,TKey>:IDAL.IUniversalPager<T,TKey> where T : class
    {

        /// <summary>
        /// 筛选 T 内单例,此属性不可为空,无条件使用时请赋予该属性:变量名 => true
        /// </summary>
        public Expression<Func<T, bool>> WhereLambda { get; set; }

        /// <summary>
        /// 据 T 内主键执行排序,此属性不可为空,使用时赋予该属性:变量名 => T 内主键
        /// </summary>
        public Expression<Func<T, TKey>> OrderByLambda { get; set; }

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
        /// WhereLambda 筛选后页总量
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// WhereLambda 筛选后 T 内单例总数
        /// </summary>
        public int Count { get; set; }

        [Ninject.Inject]
        public SCMEntities entities { get; set; }

        public async Task<List<T>> Paging()
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

            return await list.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToListAsync();
        }
    }
}