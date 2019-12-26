using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCM_System.Model;

namespace SCM_System.IDAL
{
    public interface IDAL_UniversalModuel<T> where T:class
    {
        /// <summary>
        /// 针对 T 获取对应所有数据
        /// </summary>
        /// <returns>结果集</returns>
        Task<List<T>> Select_All();

        /// <summary>
        /// 针对 T 内主键获取对应所有数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns>结果集</returns>
        Task<T> Select_Key(dynamic key);

        /// <summary>
        /// 针对 T 内属性获取对应所有数据
        /// </summary>
        /// <param name="properties">T 内属性,键值格式为: {"属性名","条件值"}</param>
        /// <returns>结果集</returns>
        Task<List<T>> Select_Properties(Dictionary<string, dynamic> properties);

        /// <summary>
        /// 针对 T 进行数据添加
        /// </summary>
        /// <param name="model">具体数据</param>
        /// <returns>操作结果</returns>
        Task<int> Insert(T model);

        /// <summary>
        /// 针对 T 内主键进行删除
        /// </summary>
        /// <param name="key"> T 内主键</param>
        /// <returns>操作结果</returns>
        Task<int> Delete_Key(dynamic key);

        /// <summary>
        /// 针对 T 内多属性进行删除
        /// </summary>
        /// <param name="properties">T 内属性,键值格式为: {"属性名","条件值"}</param>
        /// <returns>操作结果</returns>
        Task<int> Delete_Properties(Dictionary<string,dynamic> properties);

        /// <summary>
        /// 针对 T 内主键进行修改
        /// </summary>
        /// <param name="key">T 内主键</param>
        /// <param name="model">修改所需新数据</param>
        /// <returns>操作结果</returns>
        //Task<int> Update_Key(dynamic key,T model);

        /// <summary>
        /// 针对 T 内属性进行修改
        /// </summary>
        /// <param name="properties">T 内属性,键值格式为: {"属性名","条件值"}</param>
        /// <param name="model">修改所需新数据</param>
        /// <returns>操作结果</returns>
        Task<int> Update_Properties(Dictionary<string, dynamic> properties, T model);
      
        /// <summary>
        /// (无条件)针对 T 获取对应数据条数
        /// </summary>
        /// <returns>数据条数</returns>
        Task<int> ObtainCount_All();

        /// <summary>
        /// (据条件)针对 T 获取对应数据条数
        /// </summary>
        /// <param name="properties">T 内属性,键值格式为: {"属性名","条件值"}</param>
        /// <returns>数据条数</returns>
        Task<int> ObtainCount_Properties(Dictionary<string, dynamic> properties);
    }
}
