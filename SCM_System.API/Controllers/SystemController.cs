using Ninject;
using SCM_System.DAL;
using SCM_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SCM_System.API.Controllers
{
    [RoutePrefix("api/system")]
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

        public void Options() { }  //这是预请求
    }
}
