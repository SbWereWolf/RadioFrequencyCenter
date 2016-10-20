using System.Web.Mvc;
using RadioFrequencyCenter.DataBrowser.Models;

namespace RadioFrequencyCenter.DataBrowser.Controllers
{
    public class CramDataController : Controller
    {
        public ActionResult Index()
        {
            var getIndexView = View(new CramData());
            return getIndexView;
        }

        public ActionResult CramWholeData()
        {
            var cram = new CramData();
            // ReSharper disable UnusedVariable
            var result = cram.WholeData();
            // ReSharper restore UnusedVariable
            var redirectToIndex = RedirectToAction("Index", "CramData");
            return redirectToIndex;
        }

        public ActionResult CramNewerOnly()
        {
            var cram = new CramData();
            // ReSharper disable UnusedVariable
            var isSuccess = cram.NewerOnly();
            // ReSharper restore UnusedVariable

            var redirectToIndex = RedirectToAction("Index", "CramData");
            return redirectToIndex;
        }

        public ActionResult CramDevicesByParameters()
        {
            var requestForm = Request?.Form;
            var cram = new CramData();
            // ReSharper disable UnusedVariable
            var result = cram.DevicesByParameters(requestForm);
            // ReSharper restore UnusedVariable
            var redirectToIndex = RedirectToAction("Index", "CramData");
            return redirectToIndex;
        }
        
        public ActionResult DeleteAllRecords()
        {
            // ReSharper disable UnusedVariable
            var result = RadioDevice.DeleteAllRecords();
            // ReSharper restore UnusedVariable
            var redirectToIndex = RedirectToAction("Index", "CramData");
            return redirectToIndex;
        }

    }
}