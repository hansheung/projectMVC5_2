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
                        "~/Content/colreorder-1.5.1/colReorder.bootstrap4.min.css",
                        "~/Content/myCSS/myCSS.css"
                        ));

            bundles.Add(new ScriptBundle("~/javascript").Include(
                        "~/Content/myJavascripts/jQuery-3.3.1/jquery-3.3.1.js",
                        "~/Content/myJavascripts/DataTables_1.10.19/jquery.dataTables.min.js",
                        "~/Content/AdminLTE-2.4.8/bower_components/jquery/dist/jquery.min.js",
                        "~/Content/AdminLTE-2.4.8/bower_components/bootstrap/dist/js/bootstrap.min.js",
                        "~/Content/AdminLTE-2.4.8/bower_components/datatables.net/js/jquery.dataTables.min.js",
                        "~/Content/AdminLTE-2.4.8/bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js",
                        "~/Content/AdminLTE-2.4.8/bower_components/jquery-slimscroll/jquery.slimscroll.min.js",
                        "~/Content/AdminLTE-2.4.8/bower_components/fastclick/lib/fastclick.js",
                        "~/Content/AdminLTE-2.4.8/dist/js/adminlte.min.js",
                        "~/Content/myJavascripts/DataTables_1.5.6/dataTables.buttons.min.js",
                        "~/Content/bootstrap-session-timeout-master/dist/bootstrap-session-timeout.js",
                        "~/Content/colreorder-1.5.1/dataTables.colReorder.min.js",
                        "~/Content/myJavascripts/DataTables_1.5.6/jszip-3.1.3/jszip.min.js",
                        "~/Content/myJavascripts/DataTables_1.5.6/pdfmake-0.1.53/pdfmake.min.js",
                        "~/Content/myJavascripts/DataTables_1.5.6/pdfmake-0.1.53/vfs_fonts.js",
                        "~/Content/myJavascripts/DataTables_1.5.6/buttons.html5.min.js",
                        "~/Content/myJavascripts/DataTables_1.5.6/buttons.print.min.js",
                        "~/Content/myJavascripts/DataTables_1.5.6/select-1.3.0/dataTables.select.min.js",
                        "~/Content/sweetalert-8.10.7/dist/sweetalert2.all.min.js"
                        ));

            BundleTable.EnableOptimizations = true;
        }
    }
}