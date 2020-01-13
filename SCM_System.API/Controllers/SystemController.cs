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
using System.Web;
using System.Web.Http;

namespace SCM_System.API.Controllers
{
    [RoutePrefix("api/System")]
    public class SystemController : ApiController
    {
        [Inject]
        //通用模块
        public DAL_SystemModuel<BaseModel> DAL_SystemModuel { get; set; }

        [HttpPost]
        [Route("PostLogin")]
        public dynamic PostLogin(dynamic users)
        {
            if (users == null)
            {
                return null;
            }
            try
            {
                if (DAL_SystemModuel.Login(new Users() { UserLoginName = users.UserLoginName.Value, UserLoginPwd = users.UserLoginPwd.Value }))
                {

                    if (users.UserLoginState.Value)
                    {
                        return new Users() { UserLoginName = users.UserLoginName.Value, UserLoginPwd = users.UserLoginPwd.Value };
                    }
                    return new Users() { UserLoginName = users.UserLoginName.Value };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        //WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW
        //增删改查
        [Inject]
        //通用模块
        //客户资料
        public DAL_UniversalModuel<Popedoms> universalModuelPopedoms { get; set; }

        [Inject]
        public DAL_UniversalModuel<Users> universalModuelUsers { get; set; }

        [Inject]
        public SCMEntities db { get; set; }

        //分页数据加载
        [Inject]
        //通用模块
        //仓库设置表
        public UniversalPager<Users, int> pager_Users { get; set; }


        [Route("GetUsers")]
        public async Task<dynamic> GetUsers(int ps, int pi,string name)
        {
            pager_Users.IsAsc = true;
            pager_Users.PageSize = ps;
            pager_Users.PageIndex = pi;
           
            pager_Users.WhereLambda = a => name!="请输入"?a.UsersName.Contains(name):true;
            pager_Users.OrderByLambda = a => a.UsersID;
            var set = await pager_Users.Paging().ConfigureAwait(false);
            return new Dictionary<string, dynamic>() { { "data", set }, { "total", pager_Users.Count } };
        }

        [HttpGet]
        [Route("GetuniversalModuelPopedoms")]
        public async Task<List<Popedoms>> GetuniversalModuelPopedoms()
        {
            return await db.Popedoms.ToListAsync();
        }

        [HttpGet]
        [Route("DeluniversalModuelUsers/{id}")]
        public async Task<int> DeluniversalModuelUsers(int id)
        {

            return await universalModuelUsers.Delete_Key(id);

        }


        /// <summary>
        /// 根据主键找到用户表的相应对象
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpGet]
        [Route("FindUsersUponMainKey/{id}")]
        public async Task<Users> FindUsersUponMainKey(int id) {
            return await universalModuelUsers.Select_Key(id);
        }
        public void Options() { }  //这是预请求
    }
}
