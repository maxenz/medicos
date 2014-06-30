using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ParamedicMedicosPrestaciones.Models;

namespace ParamedicMedicosPrestaciones.Controllers
{
    public class OperativaClientesController : Controller
    {
        //
        // GET: /OperativaClientes/

        public ActionResult Index()
        {
            Session["UserName"] = "OSDE";
            ViewBag.Title = "Consulta de Servicios";

            return View();
        }

        // --> Obtengo los servicios en curso del servidor
        public JsonResult GetServiciosEnCurso()
        {
            // --> Ejecuto web service y traigo datos
            DataTable dtOpEnCurso = getServiciosEnCursoFromWebService();
            List<OperativaEnCurso> lstOpEnCurso = new List<OperativaEnCurso>();

            // --> Si no falla el webservice ..
            if (dtOpEnCurso != null)
            {
                // --> Formateo la datatable a una lista de objetos OperativaEnCurso
                lstOpEnCurso = getOpEnCursoFormatted(dtOpEnCurso);

                return Json(lstOpEnCurso, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        // --> Ejecuto el webservice y traigo los servicios en curso
        private DataTable getServiciosEnCursoFromWebService()
        {
            
            WSOperativaClientes.ClientesOperativosSoapClient wsClient = new WSOperativaClientes.ClientesOperativosSoapClient();
            try
            {

                wsClient.Open();
                //DataSet dsEnCurso = wsClient.GetOperativaEnCurso(getUserID());
                DataSet dsEnCurso = wsClient.GetOperativaEnCurso(29);
                wsClient.Abort();
                return dsEnCurso.Tables[0];
            }
            catch (Exception ex)
            {
                wsClient.Abort();
                var msg = ex.Message;
                return null;
            }

        }

        // --> Formateo los servicios en curso a una lista de OperativaEnCurso
        private List<OperativaEnCurso> getOpEnCursoFormatted(DataTable dt)
        {

            List<OperativaEnCurso> lstOpEnCurso = new List<OperativaEnCurso>();

            foreach (DataRow r in dt.Rows)
            {
                OperativaEnCurso opEnCurso = new OperativaEnCurso();
                opEnCurso.dataRowToOperativaEnCurso(r);
                lstOpEnCurso.Add(opEnCurso);

            }

            return lstOpEnCurso;
        }


        // --> Obtengo los servicios finalizados del servidor
        public JsonResult GetServiciosFinalizados()
        {
            var query = Request.QueryString;
            long fecDesde = Convert.ToInt32(query.GetValues("fecDesde")[0]);
            long fecHasta = Convert.ToInt32(query.GetValues("fecHasta")[0]);
            // --> Ejecuto web service y traigo datos
            DataTable dtFinalizados = getFinalizadosFromWebService(fecDesde,fecHasta);
            List<ServFinalizados> lstFinalizados = new List<ServFinalizados>();
            // --> Si no falla el webservice ..
            if (dtFinalizados != null) {
                lstFinalizados = getServFinalizadosFormatted(dtFinalizados);
                return Json(lstFinalizados, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        // --> Ejecuto el webservice y traigo los servicios en curso
        private DataTable getFinalizadosFromWebService(long fecDesde, long fecHasta)
        {

            WSOperativaClientes.ClientesOperativosSoapClient wsClient = new WSOperativaClientes.ClientesOperativosSoapClient();
            try
            {

                wsClient.Open();
                //DataSet dsEnCurso = wsClient.GetOperativaEnCurso(getUserID());
                DataSet dsFinalizados = wsClient.GetFinalizados(29, fecDesde, fecHasta);
                wsClient.Abort();
                return dsFinalizados.Tables[0];
            }
            catch (Exception ex)
            {
                wsClient.Abort();
                var msg = ex.Message;
                return null;
            }

        }

        // --> Formateo los servicios en curso a una lista de Servicios Finalizados
        private List<ServFinalizados> getServFinalizadosFormatted(DataTable dt)
        {

            List<ServFinalizados> lstFinalizados = new List<ServFinalizados>();

            foreach (DataRow r in dt.Rows)
            {
                ServFinalizados servFinalizado = new ServFinalizados();
                servFinalizado.dataRowToServFinalizados(r);
                lstFinalizados.Add(servFinalizado);

            }

            return lstFinalizados;
        }


        // --> Obtengo el user_id del cliente
        private int getUserID()
        {

            return Convert.ToInt32(Session["usr_id"]);

        }

    }
}
