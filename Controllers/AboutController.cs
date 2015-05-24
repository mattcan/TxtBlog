using Microsoft.AspNet.Mvc;

namespace TxtBlog.Controllers
{
    public class AboutController : Controller
    {

        [HttpGet, Route("about")]
        public IActionResult Index()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}