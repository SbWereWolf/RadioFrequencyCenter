using System.Web.Mvc;
using RadioFrequencyCenter.DataBrowser.Models;

namespace RadioFrequencyCenter.DataBrowser.Controllers
{
    public class BroadcastStationsController : Controller
    {
        // GET: BroadcastStations
        public ActionResult AllRecords()
        {
            var allRecords = BroadcastStation.GetAllRecords();
            return View(allRecords);
        }

        public ActionResult DownloadAllRecords()
        {
            var result = BroadcastStation.DownloadAllRecords();
            return RedirectToAction("AllRecords", "BroadcastStations");
            
        }
        public ActionResult DeleteAllRecords()
        {
            var result = BroadcastStation.DeleteAllRecords();
            return RedirectToAction("AllRecords", "BroadcastStations");

        }
    }
}