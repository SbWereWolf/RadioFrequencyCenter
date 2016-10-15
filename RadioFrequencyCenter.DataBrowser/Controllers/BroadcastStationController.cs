using System.Web.Mvc;
using RadioFrequencyCenter.DataBrowser.Models;

namespace RadioFrequencyCenter.DataBrowser.Controllers
{
    public class BroadcastStationController : Controller
    {
        public ActionResult AllRecords()
        {
            var allRecords = BroadcastStation.GetAllRecords();
            return View(allRecords);
        }

        public ActionResult DownloadAllRecords()
        {
            // ReSharper disable UnusedVariable
            var result = BroadcastStation.DownloadAllRecords();
            // ReSharper restore UnusedVariable
            return RedirectToAction("AllRecords", "BroadcastStation");
            
        }
        public ActionResult DeleteAllRecords()
        {
            // ReSharper disable UnusedVariable
            var result = BroadcastStation.DeleteAllRecords();
            // ReSharper restore UnusedVariable
            return RedirectToAction("AllRecords", "BroadcastStation");
        }

        public ActionResult Index()
        {
            return RedirectToAction("AllRecords", "BroadcastStation");
        }
        public ActionResult RecordPage(string id)
        {
            var broadcastStation = BroadcastStation.GetRecord(id);
            return View(broadcastStation);
        }
        public ActionResult RecordPopup(string id)
        {
            var broadcastStation = BroadcastStation.GetRecord(id);
            return View(broadcastStation);
        }
    }
}