using System.Web;
using System.Web.Optimization;

namespace ParamedicMedicosPrestaciones
{
    public class BundleConfig
    {
        // Para obtener más información acerca de Bundling, consulte http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/vendor/bootstrap.js",
                        "~/Scripts/bootstrap-select.js"));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                        "~/Scripts/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqwidgets").Include(
                        "~/Content/jqwidgets/jqxcore.js",
                        "~/Content/jqwidgets/jqxdata.js",
                        "~/Content/jqwidgets/jqxbuttons.js",
                        "~/Content/jqwidgets/jqxscrollbar.js",
                        "~/Content/jqwidgets/jqxmenu.js",
                        "~/Content/jqwidgets/jqxgrid.js",
                        "~/Content/jqwidgets/jqxgrid.pager.js",
                        "~/Content/jqwidgets/jqxgrid.selection.js",
                        "~/Content/jqwidgets/jqxnumberinput.js",
                        "~/Content/jqwidgets/jqxwindow.js",
                        "~/Content/jqwidgets/jqxlistbox.js",
                        "~/Content/jqwidgets/jqxdropdownlist.js",
                        "~/Content/jqwidgets/jqxgrid.filter.js"
                        ));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de creación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/styles/droid-font.css",
                        "~/Content/styles/bootstrap.css",
                        "~/Content/styles/bootstrap-theme.css",
                        "~/Content/styles/bootstrap-select.css",
                        "~/Content/styles/shapes.css",
                        "~/Content/jqwidgets/styles/jqx.base.css",
                        "~/Content/jqwidgets/styles/jqx.arctic.css",
                        "~/Content/styles/main.css"));

            

        }
    }
}