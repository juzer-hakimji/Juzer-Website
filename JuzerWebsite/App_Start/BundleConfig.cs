using System.Web;
using System.Web.Optimization;

namespace JuzerWebsite
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            #region Style Bundle

            bundles.Add(new StyleBundle("~/StyleBundle/HomePage").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/HomePage/animate.css",
                      "~/Content/HomePage/magnific-popup.css",
                      "~/Content/HomePage/font-awesome.min.css",
                      "~/Content/css/select2.min.css",
                      "~/Content/HomePage/reset.min.css",
                      "~/Content/HomePage/style.css"));

            bundles.Add(new StyleBundle("~/StyleBundle/Layout").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/HomePage/animate.css",
                      "~/Content/HomePage/magnific-popup.css",
                      "~/Content/HomePage/font-awesome.min.css",
                      "~/Content/HomePage/reset.min.css",
                      "~/Content/CustomCss/Common.css",
                      "~/Content/HomePage/style.css"));

            bundles.Add(new StyleBundle("~/StyleBundle/Notes").Include(
                      "~/Content/DataTables/css/jquery.dataTables.css",
                      "~/Content/themes/base/jquery-ui.min.css",
                      "~/Content/font-awesome.min.css"
                      ));

            bundles.Add(new StyleBundle("~/StyleBundle/Analysis").Include(
                      "~/Content/css/select2.min.css"));


            #endregion

            #region Script Bundle

            bundles.Add(new ScriptBundle("~/ScriptBundle/HomePage").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/HomePage/smoothscroll.js",
                      "~/Scripts/HomePage/jquery.magnific-popup.min.js",
                      "~/Scripts/HomePage/magnific-popup-options.js",
                      "~/Scripts/HomePage/wow.min.js",
                      "~/Scripts/select2.min.js",
                      "~/Scripts/bootstrap-notify.min.js",
                      "~/Scripts/CustomJSScripts/Common.js",
                      "~/Scripts/HomePage/script.js",
                      "~/Scripts/modernizr-2.8.3.js",
                      "~/Scripts/jquery.validate.min.js",
                      "~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/ScriptBundle/Layout").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/bootbox.min.js",
                      "~/Scripts/HomePage/smoothscroll.js",
                      "~/Scripts/HomePage/jquery.magnific-popup.min.js",
                      "~/Scripts/HomePage/magnific-popup-options.js",
                      "~/Scripts/HomePage/wow.min.js",
                      "~/Scripts/bootstrap-notify.min.js",
                      "~/Scripts/CustomJSScripts/Common.js",
                      "~/Scripts/modernizr-2.8.3.js",
                      "~/Scripts/jquery.validate.min.js",
                      "~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/ScriptBundle/Notes").Include(
                      "~/Scripts/DataTables/jquery.dataTables.min.js",
                      "~/Scripts/jquery-ui-1.12.1.min.js",
                      "~/Scripts/CustomJSScripts/jsNotes.js"));

            bundles.Add(new ScriptBundle("~/ScriptBundle/Analysis").Include(
                      "~/Scripts/select2.min.js"
                      , "~/Scripts/CustomJSScripts/jsAnalysis.js"
                      )
                      );

            bundles.Add(new ScriptBundle("~/ScriptBundle/DeleteAccount").Include(
                      "~/Scripts/CustomJSScripts/jsDeleteAccount.js"));

            bundles.Add(new ScriptBundle("~/ScriptBundle/ChangePassword").Include(
                      "~/Scripts/CustomJSScripts/jsChangePassword.js"));

            #endregion


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
