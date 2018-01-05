using System.Web.Optimization;

namespace WeatherCheckerFrontend
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Scripts Lib
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/Lib/jQuery/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/Lib/Bootstrap/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/knockout").IncludeDirectory(
                "~/Scripts/Lib/Knockout", "*.js", true));
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                "~/Scripts/Lib/Kendo/kendo.all.min.js"));

            // Scripts App
            bundles.Add(new ScriptBundle("~/bundles/Weater/Check").Include(
                "~/Scripts/App/Weather/Check.js"));

            // Content Lib
            bundles.Add(new StyleBundle("~/content/bootstrap").Include(
                "~/Content/Lib/Bootstrap/*.css"));
            bundles.Add(new StyleBundle("~/content/kendo").Include(
                "~/Content/Lib/Kendo/kendo.common.min.css").Include(
                "~/Content/Lib/Kendo/kendo.default.min.css").Include(
                "~/Content/Lib/Kendo/kendo.default.mobile.min.css"));

            // Content App
            bundles.Add(new StyleBundle("~/content/layout").Include(
                "~/Content/App/Styles/Shared/Layout.css"));

            bundles.Add(new StyleBundle("~/content/weather").Include(
                "~/Content/App/Styles/Weather/Weather.css"));
        }
    }
}