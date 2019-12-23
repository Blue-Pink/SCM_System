using SCM_System.API.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace SCM_System.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //×¢²áNinjectÈÝÆ÷
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
