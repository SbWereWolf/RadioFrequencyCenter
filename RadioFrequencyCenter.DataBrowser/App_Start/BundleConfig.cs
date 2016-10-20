using System.Web.Optimization;

namespace RadioFrequencyCenter.DataBrowser
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            if (bundles != null)
            {
                bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-1.*"));

                bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr-*"));

                bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/bootstrap*"));
                bundles.Add(new ScriptBundle("~/bundles/common").Include(
                    "~/Scripts/common.js"));

                var include = new StyleBundle("~/Content/css")
                    .Include("~/Content/site.css");
                if (include != null)
                {
                    bundles.Add(include /* не перепутайте порядок */
                        .Include("~/Content/bootstrap*"));
                }

                bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                    "~/Scripts/jquery-ui-1.*"));

                bundles.Add(new StyleBundle("~/Content/css/jqueryui")
                    .Include("~/Content/jquery-ui-1*"));
            }
        }
    }
}
