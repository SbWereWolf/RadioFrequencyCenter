using System.Web.Mvc;

namespace RadioFrequencyCenter.DataBrowser.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var view = View(); ;
            return view;
        }
    }
}