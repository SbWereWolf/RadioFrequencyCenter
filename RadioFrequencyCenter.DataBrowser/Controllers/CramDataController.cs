using System.Web.Mvc;
using RadioFrequencyCenter.DataBrowser.Models;
using RadioFrequencyCenter.DataBrowser.Proxy;

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
            var crammer = new CramData();
            // ReSharper disable UnusedVariable
            var result = crammer.GetIt();
            // ReSharper restore UnusedVariable
            return RedirectToAction("Index", "CramData");
            
        }
        public ActionResult CramNewerOnly()
        {
            var proxy = new RadioDevices();
            var dateupdate = proxy.GetLastUpdateDate();
            
            var crammer = new CramData();
            var stationCriteria = crammer.SelectionCriteria?.Station;
            if (stationCriteria != null && dateupdate.HasValue )
            {
                stationCriteria.UpdateDate = dateupdate.Value.LocalDateTime ;
            }

            // ReSharper disable UnusedVariable
            var result = crammer.GetIt();
            // ReSharper restore UnusedVariable

            var cramNewerOnly = RedirectToAction("Index", "CramData");
            return cramNewerOnly;
        }

        public ActionResult CramDevicesByParameters()
        {
            if (Request?.Form != null)
            {
                var certificatenumber = Request.Form["SelectionCriteria.Station.CertificateNumber"];
                var factorynumber = Request.Form["SelectionCriteria.Station.FactoryNumber"];
                var crammer = new CramData();
                var selectionCriteria = crammer.SelectionCriteria?.Station;
                if (selectionCriteria != null)
                {
                    selectionCriteria.CertificateNumber = certificatenumber;
                    selectionCriteria.FactoryNumber = null;
                    int factoryNumberInt;
                    var isConverted = int.TryParse(factorynumber, out factoryNumberInt);
                    if (isConverted)
                    {
                        selectionCriteria.FactoryNumber = factoryNumberInt;
                    }
                }
                // ReSharper disable UnusedVariable
                var result = crammer.GetIt();
                // ReSharper restore UnusedVariable
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