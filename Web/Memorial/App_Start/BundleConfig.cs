using System.Web.Optimization;

namespace Memorial
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js",
            
            //            "~/Scripts/toastr.js"));
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/scripts/bootbox.js",
                        "~/Scripts/DataTables/jquery.dataTables.js",
                        "~/Scripts/DataTables/dataTables.bootstrap4.js",
                        "~/Scripts/toastr.js",
                        "~/Scripts/common.js",
                        "~/Scripts/constants.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/DataTables/css/dataTables.bootstrap4.css",
                      "~/Content/site.css",
                      "~/Content/toastr.css",
                      "~/Content/PagedList.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                    "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/jqueryuicss").Include(
                   "~/Content/themes/base/jquery-ui.css"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryui-timepicker").Include(
                    "~/Scripts/jquery-ui-timepicker-addon.js"));

            bundles.Add(new StyleBundle("~/Content/jqueryuicss-timepicker").Include(
                   "~/Content/jquery-ui-timepicker-addon.css"));

            bundles.Add(new ScriptBundle("~/bundles/fullcalendar").Include(
                    "~/Scripts/moment.min.js",
                    "~/FullCalendar/main.min.js"));

            bundles.Add(new StyleBundle("~/Content/fullcalendar").Include(
                   "~/FullCalendar/main.min.css"));
        }
    }
}