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
                        "~/Scripts/bootstrap-select.js",
                        "~/Scripts/jquery.maskedinput.js",
                        "~/Scripts/bootstrapValidator.js",
                        "~/Scripts/messenger.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/messenger-theme-future.js",
                        "~/Scripts/jquery.serialize-object.js",
                        "~/Scripts/jquery.maskMoney.js"                      
                        ));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                        "~/Scripts/site/main.js",
                        "~/Scripts/site/guardias.js",
                        "~/Scripts/site/servicios.js",
                        "~/Scripts/site/resumen.js"));

            bundles.Add(new ScriptBundle("~/bundles/opClientes").Include(
                        "~/Scripts/site/main.js",
                        "~/Scripts/site/OperativaClientes/opClientes.js",
                        "~/Scripts/site/OperativaClientes/svEnCurso.js",
                        "~/Scripts/site/OperativaClientes/svFinalizados.js",
                        "~/Scripts/site/OperativaClientes/svDenuncias.js",
                        "~/Scripts/site/OperativaClientes/svErroneos.js"));

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
                        "~/Content/jqwidgets/jqxcheckbox.js",
                        "~/Content/jqwidgets/jqxcombobox.js",
                        "~/Content/jqwidgets/jqxgrid.filter.js",
                        "~/Content/jqwidgets/jqxdatetimeinput.js",
                        "~/Content/jqwidgets/jqxcalendar.js",
                        "~/Content/jqwidgets/jqxgrid.aggregates.js",
                        "~/Content/jqwidgets/jqxtooltip.js",
                        "~/Content/jqwidgets/jqxdata.export.js",
                        "~/Content/jqwidgets/jqxgrid.export.js",
                        "~/Content/jqwidgets/jqxvalidator.js",
                        "~/Content/jqwidgets/globalization/globalize.js",
                        "~/Content/jqwidgets/globalization/globalize.culture.es-AR.js",
                        "~/Content/jqwidgets/jqxgrid.columnsresize.js" 
                        ));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de creación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/styles/droid-font.css",
                        //"~/Content/styles/bootstrap.css",
                        //"~/Content/styles/bootstrap-theme.css",
                        "~/Content/styles/bootstrap-select.css",
                        "~/Content/styles/bootstrapValidator.css",
                        "~/Content/styles/messenger.css",
                        "~/Content/styles/messenger-theme-future.css",
                        "~/Content/styles/messenger-spinner.css",
                        "~/Content/styles/shapes.css",
                        "~/Content/jqwidgets/styles/jqx.base.css",
                        "~/Content/jqwidgets/styles/jqx.arctic.css",
                        "~/Content/jqwidgets/styles/jqx.bootstrap.css",
                        "~/Content/styles/main.css"));          
        }
    }
}