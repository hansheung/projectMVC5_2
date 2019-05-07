using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace BRO.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            
            bundles.Add(new StyleBundle("~/css").Include(

                        "~/Content/AdminLTE-2.4.8/bower_components/bootstrap/dist/css/bootstrap.css",
                        "~/Content/fontawesome-4.7.0/font-awesome.min.css",
                        "~/Content/ionicons-2.0.1/css/ionicons.min.css",
                        "~/Content/AdminLTE-2.4.8/bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css",
                        "~/Content/AdminLTE-2.4.8/dist/css/AdminLTE.min.css",
                        "~/Content/AdminLTE-2.4.8/dist/css/skins/_all-skins.min.css",
                        "~/Content/AdminLTE-2.4.8/dist/css/Validation.css",
                        "~/Content/myCSS/DataTables_1.5.6/buttons.dataTables.css",
                        "~/Content/myCSS/myCSS.css"
                        ));

            bundles.Add(new ScriptBundle("~/javascript").Include(

                        "~/Content/AdminLTE-2.4.8/bower_components/jquery/dist/jquery.min.js",
                        "~/Content/AdminLTE-2.4.8/bower_components/bootstrap/dist/js/bootstrap.min.js",
                        "~/Content/AdminLTE-2.4.8/bower_components/datatables.net/js/jquery.dataTables.min.js",
                        "~/Content/AdminLTE-2.4.8/bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js",
                        "~/Content/AdminLTE-2.4.8/bower_components/jquery-slimscroll/jquery.slimscroll.min.js",
                        "~/Content/AdminLTE-2.4.8/bower_components/fastclick/lib/fastclick.js",
                        "~/Content/AdminLTE-2.4.8/dist/js/adminlte.min.js",
                        "~/Content/myJavascripts/DataTables_1.5.6/dataTables.buttons.min.js",
                        "~/Content/bootstrap-session-timeout-master/dist/bootstrap-session-timeout.js",
                        "~/Content/myJavascripts/myJavascripts.js"
                        ));

            BundleTable.EnableOptimizations = true;
        }
    }
}