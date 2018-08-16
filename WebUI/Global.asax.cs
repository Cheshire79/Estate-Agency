
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace WebUI
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
		protected void Application_Error(object sender, EventArgs e)
		{
			Exception exception = Server.GetLastError();
			Response.Clear();
			string action = "General";
			HttpException httpException = exception as HttpException;

			if (httpException != null)
				switch (httpException.GetHttpCode())
				{
					case 400:
						action = "BadRequest";
						break;
					case 404:
						action = "NotFound";
						break;
					case 403:
						action = "NForbidden";
						break;
					default:
						action = "General";
						break;
				}
			Server.ClearError();
			Response.Redirect(String.Format("~/Error/{0}/?message={1}", action, exception.Message));
		}
	}
}
