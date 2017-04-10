using System.Web.Optimization;

namespace Proyecto_ABM.WebSite
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/dataTableJS").Include(
                    "~/Scripts/jquery-3.1.1.js",
                    "~/Scripts/jquery-ui.min.js",
                    "~/Scripts/jquery.dataTables.min.js",
                    "~/Scripts/bootstrap.min.js",
                    "~/Scripts/dataTables.bootstrap.min.js",
                    "~/Scripts/responsive.bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/main.css"));

            bundles.Add(new StyleBundle("~/Content/dataTableCss").Include(
                        "~/Content/jquery-ui.css",
                        "~/Content/bootstrap.min.css",
                        "~/Content/css/dataTables.bootstrap.min.css",
                        "~/Content/css/responsive.bootstrap.min.css"));
        }
    }
}
