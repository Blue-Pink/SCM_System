using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCM_System.Model;
using SCM_System.IDAL;
using Ninject;
using System.Data.Entity;
using System.Web.Script.Serialization;

namespace SCM_System.DAL
{
    public class DAL_UniversalModuel<T> : IDAL_UniversalModuel<T> where T : BaseModel
    {
        [Inject]
        public SCMEntities entities { get; set; }
        private readonly Type t = typeof(T);

        /// <summary>
        /// 将JSON字符串转换成Dictionary<string, dynamic>类型
        /// </summary>
        /// <param name="json_data">JSON格式字符串</param>
        /// <returns>转换结果</returns>
        public Dictionary<string, dynamic> JsonToDictionary(string json_data)
        {
            try
            {
                return new JavaScriptSerializer().Deserialize<Dictionary<string, dynamic>>(json_data);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> Delete_Key(dynamic key)
        {
            try
            {
                var temp = entities.Set<T>().FindAsync(key).Result;
                entities.Entry(temp).State = EntityState.Deleted;
                var result = await entities.SaveChangesAsync();
                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<int> Delete_Properties(Dictionary<string, dynamic> properties)
        {
            try
            {
                //循环 properties 属性集
                foreach (var item in properties)
                {
                    //判断属性集是否存在于 T 内
                    if (t.GetProperty(item.Key) == null)
                    {
                        throw new Exception("当前操作对象未包含此属性名:" + item.Key);
                    }
                }

                //存放 T 内所有数据
                var t_set = await entities.Set(t).Cast<T>().ToListAsync();

                foreach (var t_item in t_set)
                {
                    //操作开关
                    bool on_off = true;
                    foreach (var p_item in properties)
                    {
                        //获取当前单例对应属性的值
                        var property = t_item.GetType().GetProperty(p_item.Key).GetValue(t_item);

                        //当前单例非法即关闭开关
                        if (!(property is string ? property.ToString().Trim() : property).ToString().Equals(p_item.Value.ToString()))
                            on_off = false;
                    }
                    if (on_off)
                        entities.Entry<T>(t_item).State = EntityState.Deleted;
                }
                return await entities.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> Insert(T model)
        {
            try
            {
                entities.Entry<T>(model).State = EntityState.Added;
                var result = await entities.SaveChangesAsync();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<T>> Select_All()
        {
            try
            {
                var set = await entities.Set(t).Cast<T>().ToListAsync();
                return set;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Task<T> Select_Key(dynamic key)
        {
            try
            {
                var temp = entities.Set<T>().FindAsync(key);
                return temp;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<List<T>> Select_Properties(Dictionary<string, dynamic> properties)
        {
            try
            {
                //循环 properties 属性集
                foreach (var item in properties)
                {
                    //判断属性集是否存在于 T 内
                    if (t.GetProperty(item.Key) == null)
                    {
                        throw new Exception("当前操作对象未包含此属性名:" + item.Key);
                    }
                }

                //存放符合对应条件的 T
                var temp_set = new List<T>();
                //存放 T 内所有数据
                var t_set = await entities.Set(t).Cast<T>().ToListAsync();

                foreach (var t_item in t_set)
                {
                    //操作开关
                    bool on_off = true;
                    foreach (var p_item in properties)
                    {
                        //获取当前单例对应属性的值
                        var property = t_item.GetType().GetProperty(p_item.Key).GetValue(t_item);

                        //当前单例非法即关闭开关
                        if (!(property is string ? property.ToString().Trim() : property).ToString().Equals(p_item.Value.ToString()))
                            on_off = false;
                    }
                    if (on_off)
                        temp_set.Add(t_item);
                }
                return temp_set;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> Update_Properties(Dictionary<string, dynamic> properties, T model)
        {
            //try
            //{
            //循环 properties 属性集
            foreach (var item in properties)
            {
                //判断属性集是否存在于 T 内
                if (t.GetProperty(item.Key) == null)
                {
                    throw new Exception("当前操作对象未包含此属性名:" + item.Key);
                }
            }

            //存放 T 内所有数据
            var t_set = await entities.Set(t).Cast<T>().ToListAsync();
            //获取 T 内所有属性
            var t_properties = t.GetProperties();
            foreach (var t_item in t_set)
            {
                //操作开关
                bool on_off = true;
                foreach (var p_item in properties)
                {
                    //获取当前单例对应属性的值
                    var property = t_item.GetType().GetProperty(p_item.Key).GetValue(t_item);

                    //当前单例非法即关闭开关
                    if (!(property is string ? property.ToString().Trim() : property).ToString().Equals(p_item.Value.ToString()))
                        on_off = false;
                }
                if (on_off)
                {
                    foreach (var t_p_item in t_properties)
                    {
                        //将新数据内非空属性的值赋予当前单例对应属性
                        if (t_p_item.GetValue(model) != null)
                            t_p_item.SetValue(t_item, t_p_item.GetValue(model));
                    }
                    entities.Entry<T>(t_item).State = EntityState.Modified;
                }
            }
            return await entities.SaveChangesAsync();
            //}
            //catch (Exception e)
            //{
            //    throw new Exception(e.Message);
            //}
        }

        public async Task<int> Update_Key(dynamic key, T model)
        {
            try
            {
                var temp = entities.Set<T>().FindAsync(key).Result;
                if (temp == null)
                    throw new Exception("指定主键无效,无法执行后续操作");
                //获取 T 内所有属性
                var properties = t.GetProperties();
                foreach (var item in properties)
                {
                    //判断当前属性项是否为 T 内主键
                    if (item.CustomAttributes.Count() > 0)
                    {
                        if (item.CustomAttributes.ToList()[0].AttributeType.Name == "KeyAttribute")
                        {
                            //判断指定主键与操作单例主键是否相同
                            if (item.GetValue(model) != null)
                            {
                                if (item.GetValue(model).ToString().Trim() != key.ToString().Trim())
                                    throw new Exception("指定主键与操作单例主键不相同");
                                else
                                    item.SetValue(model, null);
                            }
                        }
                    }
                    //将新数据内非空属性的值赋予当前单例对应属性
                    if (item.GetValue(model) != null)
                        item.SetValue(temp, item.GetValue(model));
                }
                entities.Entry(temp).State = EntityState.Modified;
                var result = await entities.SaveChangesAsync();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> ObtainCount_All()
        {
            try
            {
                return await entities.Set<T>().CountAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> ObtainCount_Properties(Dictionary<string, dynamic> properties)
        {
            try
            {
                //循环 properties 属性集
                foreach (var item in properties)
                {
                    //判断属性集是否存在于 T 内
                    if (t.GetProperty(item.Key) == null)
                    {
                        throw new Exception("当前操作对象未包含此属性名:" + item.Key);
                    }
                }

                //存放符合对应条件的 T
                var count = 0;
                //存放 T 内所有数据
                var t_set = await entities.Set(t).Cast<T>().ToListAsync();

                foreach (var t_item in t_set)
                {
                    //操作开关
                    bool on_off = true;
                    foreach (var p_item in properties)
                    {
                        //获取当前单例对应属性的值
                        var property = t_item.GetType().GetProperty(p_item.Key).GetValue(t_item);

                        //当前单例非法即关闭开关
                        if (!(property is string ? property.ToString().Trim() : property).ToString().Equals(p_item.Value.ToString()))
                            on_off = false;
                    }
                    if (on_off)
                        count++;
                }
                return count;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
