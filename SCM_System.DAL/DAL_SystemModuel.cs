using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCM_System.Model;
namespace SCM_System.DAL
{
    public class DAL_SystemModuel<T> : DAL_UniversalModuel<T>, IDAL.IDAL_UniversalModuel<T> where T : class
    {
        public bool Login(Users user)
        {
            if (entities.Users.Where(a => a.UserLoginName == user.UserLoginName && a.UserLoginPwd == user.UserLoginPwd).ToList().Count()>0)
                return true;
            return false;
        }
    }
}
