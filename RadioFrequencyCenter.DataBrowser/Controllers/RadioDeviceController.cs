using System.Web.Mvc;
using RadioFrequencyCenter.DataBrowser.Models;

namespace RadioFrequencyCenter.DataBrowser.Controllers
{
    public class RadioDeviceController : Controller
    {
        public ActionResult BrowseData()
        {
            var requestForm = Request?.Form;
            var allRecords = RadioDevice.SearchRadioDevice(requestForm);
            return View(allRecords);
        }
        
        public ActionResult DeleteAllRecords()
        {
            // ReSharper disable UnusedVariable
            var result = RadioDevice.DeleteAllRecords();
            // ReSharper restore UnusedVariable
            return RedirectToAction("BrowseData", "RadioDevice");
        }

        public ActionResult Index()
        {
            return RedirectToAction("BrowseData", "RadioDevice");
        }
        public ActionResult RecordPage(string id)
        {
            var broadcastStation = new RadioDevice();
            broadcastStation.Boot(id);
            var view = View(broadcastStation);
            return view;
        }
        public ActionResult RecordPopup(string id)
        {
            var broadcastStation = new RadioDevice();
            broadcastStation.Boot(id);
            var view = View(broadcastStation);
            return view;
        }
    }
}