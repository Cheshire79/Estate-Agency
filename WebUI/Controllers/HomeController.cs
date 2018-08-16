
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //throw new HttpException(404,"Mine");
            //int x = 0;
            //int y = 8 / x;
            return View();
        }
    }
}