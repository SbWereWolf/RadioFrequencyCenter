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
            var radioDevice = new RadioDevice();
            radioDevice.Boot(id);
            var view = View(radioDevice);
            return view;
        }
        public ActionResult RecordPopup(string id)
        {
            var radioDevice = new RadioDevice();
            radioDevice.Boot(id);
            var view = View(radioDevice);
            return view;
        }
    }
}