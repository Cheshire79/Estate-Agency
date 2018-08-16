using System.Web.Mvc;


namespace WebUI.Controllers
{

	public class ErrorController : Controller
	{
		public ActionResult BadRequest(string message)
		{
			Response.StatusCode = 400;
			ViewBag.Error = message;
			return View();
		}

		public ActionResult NotFound(string message)
		{
			Response.StatusCode = 404;
			ViewBag.Error = message;
			return View();
		}

		public ActionResult Forbidden(string message)
		{
			Response.StatusCode = 403;
			ViewBag.Error = message;
			return View();
		}
		public ActionResult General(string message)
		{
			Response.StatusCode = 403;
			ViewBag.Error = message;
			return View();
		}
	}
}