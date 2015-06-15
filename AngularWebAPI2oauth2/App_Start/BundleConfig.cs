using System.Web.Optimization;

namespace AngularWebAPI2oauth2
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/Site.css",
                        "~/Content/CSS-Reset/CSSReset.css",
                        "~/Content/Bootstrap/bootstrap.min.css",
                        "~/Content/Font-Awesome/font-awesome.min.css"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/vendor/external").Include(
                        "~/Scripts/vendor/jquery-{version}.js",
                        "~/Scripts/vendor/bootstrap.js",
                        "~/Scripts/vendor/respond.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/vendor/angular").Include(
                        "~/Scripts/vendor/angular.min.js",
                        "~/Scripts/vendor/angular-ui-router.min.js",
                        "~/Scripts/vendor/angular-animate.min.js",
                        "~/Scripts/vendor/angular-resource.min.js",
                        "~/Scripts/vendor/loading-bar.js",
                        "~/Scripts/vendor/angular-local-storage.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/app/modules").IncludeDirectory(
                        "~/Scripts/app/modules/",
                        "*.js",
                        true
                        ));

            bundles.Add(new ScriptBundle("~/bundles/app/services").IncludeDirectory(
                        "~/Scripts/app/services/",
                        "*.js",
                        true
                        ));

            bundles.Add(new ScriptBundle("~/bundles/app/controllers").IncludeDirectory(
                        "~/Scripts/app/controllers/",
                        "*.js",
                        true
                        ));

            bundles.Add(new ScriptBundle("~/bundles/app/filters").IncludeDirectory(
                        "~/Scripts/app/filters/",
                        "*.js",
                        true
                        ));

            bundles.Add(new ScriptBundle("~/bundles/app/directives").IncludeDirectory(
                        "~/Scripts/app/directives/",
                        "*.js",
                        true
                        ));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
