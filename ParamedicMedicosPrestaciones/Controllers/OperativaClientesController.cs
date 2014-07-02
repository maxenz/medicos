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

        // --> Obtengo los servicios finalizados del servidor
        public JsonResult GetServiciosClientes()
        {
            var query = Request.QueryString;
            long fecDesde = 0, fecHasta = 0, pWS;
            string[] vDesde = query.GetValues("fecDesde");
            string[] vHasta = query.GetValues("fecHasta");
            if (vDesde != null)
            {
                fecDesde = Convert.ToInt32(vDesde[0]);
                fecHasta = Convert.ToInt32(vHasta[0]);
            }

            pWS = Convert.ToInt32(query.GetValues("pWS")[0]);
            DataTable dt = new DataTable();
            dt = getDataFromWebService(pWS,fecDesde,fecHasta);

            // --> Si no falla el webservice ..
            if (dt != null)
            {
                JsonResult result = new JsonResult();
                switch (pWS)
                {
                    case 1:
                        result = Json(getOpEnCursoFormatted(dt), JsonRequestBehavior.AllowGet);
                        break;
                    case 2:
                        result = Json(getServFinalizadosFormatted(dt), JsonRequestBehavior.AllowGet);
                        break;
                    case 3:
                        result =  Json(getErroneosFormatted(dt), JsonRequestBehavior.AllowGet);
                        break;
                }

                return result;
                
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        private DataTable getDataFromWebService(long pWS, long fDesde, long fHasta) {

            WSOperativaClientes.ClientesOperativosSoapClient wsClient = new WSOperativaClientes.ClientesOperativosSoapClient();
            DataSet ds = new DataSet();

            try
            {
                wsClient.Open();

                switch (pWS)
                {
                    case 1:
                        ds = wsClient.GetOperativaEnCurso(29);
                        break;
                    case 2:
                        ds = wsClient.GetFinalizados(29, fDesde, fHasta);
                        break;
                    case 3:
                        ds = wsClient.GetErroresAutorizacion(29, fDesde, fHasta);
                        break;
                }

                wsClient.Abort();
                return ds.Tables[0];
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

        // --> Formateo los servicios en curso a una lista de OperativaEnCurso
        private List<ServErroneos> getErroneosFormatted(DataTable dt)
        {

            List<ServErroneos> lst = new List<ServErroneos>();

            foreach (DataRow r in dt.Rows)
            {
                ServErroneos servErroneo = new ServErroneos();
                servErroneo.dataRowToServErroneos(r);
                lst.Add(servErroneo);

            }

            return lst;
        }

    
        // --> Obtengo el user_id del cliente
        private int getUserID()
        {

            return Convert.ToInt32(Session["usr_id"]);

        }

    }
}
