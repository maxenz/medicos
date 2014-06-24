using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ParamedicMedicosPrestaciones.ViewModels;

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

        public JsonResult GetServiciosEnCurso()
        {

            DataTable dtOpEnCurso = getServiciosEnCursoFromWebService();
            List<OperativaEnCurso> lstOpEnCurso = new List<OperativaEnCurso>();
            if (dtOpEnCurso != null)
            {

                lstOpEnCurso = getOpEnCursoFormatted(dtOpEnCurso);

                return Json(lstOpEnCurso, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

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

        private List<OperativaEnCurso> getOpEnCursoFormatted(DataTable dt)
        {

            List<OperativaEnCurso> lstOpEnCurso = new List<OperativaEnCurso>();

            foreach (DataRow r in dt.Rows)
            {
                OperativaEnCurso opEnCurso = new OperativaEnCurso();
                opEnCurso.Arribo = r["Arribo"].ToString();
                opEnCurso.ClienteID = r["ClienteID"].ToString();
                opEnCurso.Domicilio = r["dm_virDomicilio"].ToString();
                opEnCurso.Ecg = r["ECG"].ToString();
                opEnCurso.Grado = r["virFACConceptoId"].ToString();
                opEnCurso.ID = Convert.ToInt32(r["ID"]);
                opEnCurso.Llamada = r["Llamada"].ToString();
                opEnCurso.Localidad = r["Loc"].ToString();
                opEnCurso.Nombre = r["Nombre"].ToString();
                opEnCurso.NroInc = r["NroIncidente"].ToString();
                opEnCurso.NroInterno = r["NroInterno"].ToString();
                opEnCurso.Reclamo = r["Rcl"].ToString();
                opEnCurso.Respuesta = r["Rta"].ToString();
                opEnCurso.ViajeID = r["ViajeId"].ToString();
                opEnCurso.WebEstado = r["virWebEstado"].ToString();
                opEnCurso.WebMovil = r["virWebMovil"].ToString();

                lstOpEnCurso.Add(opEnCurso);

            }

            return lstOpEnCurso;
        }

        private int getUserID()
        {

            return Convert.ToInt32(Session["usr_id"]);

        }

    }
}
