using Microsoft.AspNetCore.Mvc;

namespace OilShop.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult Iternal()
        {
            Response.StatusCode = 500;
            return View();
        }
        public ActionResult Forbidden()
        {
            Response.StatusCode = 403;
            return View();
        }   
    }
}
