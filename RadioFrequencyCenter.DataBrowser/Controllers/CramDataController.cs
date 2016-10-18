using System.Web.Mvc;
using RadioFrequencyCenter.DataBrowser.Models;

namespace RadioFrequencyCenter.DataBrowser.Controllers
{
    public class CramDataController : Controller
    {
        public ActionResult Index()
        {
            var actionResult = View(new CramData());
            return actionResult;
        }

        public ActionResult CramWholeData()
        {
            // ReSharper disable UnusedVariable
            var result = Models.CramData.DownloadAllRecords();
            // ReSharper restore UnusedVariable
            return RedirectToAction("Index", "CramData");
            
        }
        public ActionResult CramNewerOnly()
        {
            // ReSharper disable UnusedVariable
            //var result = Models.CramData.DownloadAllRecords();
            // ReSharper restore UnusedVariable
            return RedirectToAction("Index", "CramData");

        }
        public ActionResult CramDevicesByParameters()
        {
            // ReSharper disable UnusedVariable
            //var result = Models.CramData.DownloadAllRecords();
            // ReSharper restore UnusedVariable

            if (Request?.Form != null)
            {
                var numSvid = Request.Form["Station.NumSvid"];
                var zavNum = Request.Form["Station.ZavNum"];
            }

            var cramDevicesByParameters = RedirectToAction("Index", "CramData");
            return cramDevicesByParameters;
        }

        public ActionResult DeleteAllRecords()
        {
            // ReSharper disable UnusedVariable
            var result = RadioDevice.DeleteAllRecords();
            // ReSharper restore UnusedVariable
            var redirectToRouteResult = RedirectToAction("Index", "CramData");
            return redirectToRouteResult;
        }

    }
}