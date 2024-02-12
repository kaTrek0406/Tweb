using System.Web;
using System.Web.Optimization;

namespace JW.WebApi
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts")
                .Include("~/Scripts/jquery-3.4.1.min.js")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/custom.js"));

            bundles.Add(new StyleBundle("~/bundles/styles")
                .Include("~/Content/css/style.css")
                .Include("~/Content/css/bootstrap.css")
                .Include("~/Content/css/responsive.css")
                .Include("~/Content/css/font-awesome.css"));
            
        }
    }
}