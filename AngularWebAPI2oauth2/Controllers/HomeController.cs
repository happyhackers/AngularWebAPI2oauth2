using System.Web.Mvc;

namespace AngularWebAPI2oauth2.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns the Index Page as a ActionResult</returns>
        public void Index()
        {
            ViewBag.Title = "Home Page";

            RedirectToAction("Index", "Help");
        }
    }
}
