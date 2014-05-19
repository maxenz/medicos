using ParamedicMedicosPrestaciones.Models;
using ParamedicMedicosPrestaciones.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;
namespace ParamedicMedicosPrestaciones.Controllers
{
    public class MedicosController : Controller
    {
        //
        // GET: /Medicos/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getFiltroPeriodos()
        {
            List<FiltroPeriodos> lstPeriodos = new List<FiltroPeriodos>();
            DateTime fecActual = DateTime.Now;
            DateTime fecAnterior = fecActual.AddMonths(-1);
            DateTime fecAntePenultimo = fecActual.AddMonths(-2);
            lstPeriodos.Add(new FiltroPeriodos(getFormattedPeriod(fecAntePenultimo),getFormattedDescriptionOfPeriod(fecAntePenultimo)));
            lstPeriodos.Add(new FiltroPeriodos(getFormattedPeriod(fecAnterior), getFormattedDescriptionOfPeriod(fecAnterior)));
            lstPeriodos.Add(new FiltroPeriodos(getFormattedPeriod(fecActual), getFormattedDescriptionOfPeriod(fecActual)));

            return Json(lstPeriodos, JsonRequestBehavior.AllowGet);
        }

        private string getFormattedDescriptionOfPeriod(DateTime fec)
        {
            string mes = fec.ToString("MMMM");
            mes = char.ToUpper(mes[0]) + mes.Substring(1);
            string year = fec.ToString("yyyy");
            return mes + " " + year;
        }

        private string getFormattedPeriod(DateTime fec) {

            int month = fec.Month;
            string strMonth = month.ToString();

            if (month < 10) {
                strMonth = "0" + month.ToString(); 
            }

            return fec.ToString("yyyy") + strMonth;
        }

        private DataSet getGuardiasFromWebService(long periodo,int coord)
        {
            WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient wsClient = new WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient();
            return wsClient.GetGuardiasDetalle(1055,periodo,coord);
            
        }

        private DataSet getServiciosFromWebService(long periodo, int coord)
        {
            WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient wsClient = new WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient();
            return wsClient.GetIncidentes(1055, periodo,coord);
        }

        private DataSet getResumenFromWebService(long periodo, int coord)
        {
            WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient wsClient = new WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient();
            return wsClient.GetResumen(1055, periodo, coord);
        }

        private string getGuardiaFechaFormatted(string fecha, int pOpcion) {

            string retVal = "";
            int dia = Convert.ToInt32(fecha.Substring(6,2));
            int mes = Convert.ToInt32(fecha.Substring(4,2));
            int año = Convert.ToInt32(fecha.Substring(0,4));

            DateTime fecFormatted = new DateTime(año, mes, dia);
            string diaDeLaSemana = fecFormatted.ToString("ddd");

            switch (pOpcion)
            {
                case 1:
                    retVal = diaDeLaSemana + " " + dia;
                    break;
                case 2:
                    retVal = mes + "/" + (año.ToString()).Substring(2,2);
                    break;
            }

            return retVal;

        }

        private List<Guardia> getGuardiasFormatted(DataSet dsGuardias, int dia)
        {

            List<Guardia> lstGuardias = new List<Guardia>();

            DataTable dtGuardias = dsGuardias.Tables[0];

            foreach (DataRow dtRow in dtGuardias.Rows)
            {
                Guardia guardia = new Guardia();
                guardia.ID = dtRow["ID"].ToString();
                guardia.Dia = Convert.ToInt32((dtRow["FecMovimiento"].ToString()).Substring(6,2));
                guardia.DiaDeLaSemana = getGuardiaFechaFormatted(dtRow["FecMovimiento"].ToString(),1);
                guardia.Periodo = getGuardiaFechaFormatted(dtRow["FecMovimiento"].ToString(),2);
                guardia.Tarifa = dtRow["TipoLiquidacionId"].ToString();
                guardia.HorarioEntrada = dtRow["HorDesde"].ToString();
                guardia.MinutosLlegadaTarde = Convert.ToInt32(dtRow["minTarde"]);
                guardia.HorarioSalida = dtRow["HorHasta"].ToString();
                guardia.MinutosRetiroAnticipado = Convert.ToInt32(dtRow["minRetAnticipado"]);
                guardia.Movil = dtRow["MovilId"].ToString();
                guardia.HorasTrabajadas = dtRow["TotalHoras"].ToString();
                guardia.Rojos = Convert.ToInt32(dtRow["Rojos"]);
                guardia.Amarillos = Convert.ToInt32(dtRow["Amarillos"]);
                guardia.Verdes = Convert.ToInt32(dtRow["Verdes"]);
                guardia.TrasladosProgramados = Convert.ToInt32(dtRow["Traslados"]);
                guardia.ImpTotalHoras = Convert.ToDouble(dtRow["ImpHora"]);
                guardia.ImpEspecialidad = Convert.ToDouble(dtRow["ImpEspecialidad"]);
                guardia.ImpPrestacion = Convert.ToDouble(dtRow["ImpPrestacion"]);
                guardia.ImpPrestacionExcedente = Convert.ToDouble(dtRow["ImpPrestacionExcedente"]);
                guardia.ImpAnticipo = Convert.ToDouble(dtRow["ImpAnticipos"]);
                guardia.ImpFinal = Convert.ToDouble(dtRow["ImpFinal"]);

                lstGuardias.Add(guardia);

            }

            if (dia != 0)
            {
                lstGuardias = lstGuardias.Where(x => x.Dia == dia).ToList();
            }

            return lstGuardias;

        }

        private List<Servicio> getServiciosFormatted(DataSet dsServicios, int dia)
        {

            List<Servicio> lstServicios = new List<Servicio>();

            DataTable dtServicios = dsServicios.Tables[0];

            foreach (DataRow dtRow in dtServicios.Rows)
            {
                Servicio servicio = new Servicio();
                servicio.IncidenteID = dtRow["IncidenteId"].ToString();
                servicio.NroInc = dtRow["NroIncidente"].ToString();
                servicio.Fecha = getGuardiaFechaFormatted(dtRow["FecIncidente"].ToString(), 1);
                servicio.Iva = dtRow["Iva"].ToString();
                servicio.Paciente = dtRow["Paciente"].ToString();
                servicio.Localidad = dtRow["Localidad"].ToString();
                servicio.Cdn = dtRow["Cdn"].ToString();
                servicio.Tarifa = dtRow["Tar"].ToString();
                servicio.DiaDeLaSemana = dtRow["Dia"].ToString();
                servicio.Tur = dtRow["Tur"].ToString();
                servicio.Grado = dtRow["Grado"].ToString();
                servicio.CoPago = Convert.ToDouble(dtRow["CoPago"]);
                servicio.Importe = Convert.ToDouble(dtRow["Importe"]);
                servicio.Dia = Convert.ToInt32((dtRow["FecIncidente"].ToString()).Substring(6,2));

                lstServicios.Add(servicio);

            }

            if (dia != 0)
            {
                lstServicios = lstServicios.Where(x => x.Dia == dia).ToList();
            }

            return lstServicios;

        }

        private List<ResumenLiquidacion> getResumenLiquidacionFormatted(DataSet dsResumenLiq)
        {

            List<ResumenLiquidacion> lstResumenLiq = new List<ResumenLiquidacion>();

            DataTable dtResumenLiq = dsResumenLiq.Tables[0];

            foreach (DataRow dtRow in dtResumenLiq.Rows)
            {
                Double valImporte = Convert.ToDouble(dtRow["Importe"]);
                if (valImporte != 0)
                {
                    ResumenLiquidacion resLiq = new ResumenLiquidacion();
                    resLiq.Item = dtRow["Item"].ToString();
                    resLiq.Importe = getImporteResumenFormatted(dtRow["Importe"].ToString());

                    lstResumenLiq.Add(resLiq);
                }
            }

            return lstResumenLiq;

        }

        private string getImporteResumenFormatted(string importe) {

            if (importe.Contains('-')) {
                importe = importe.Remove(0, 1);
                importe = "(" + importe;
                importe = importe + ")";
            }

            return importe;

        }

        public JsonResult getFiltroCoordinaciones()
        {
            WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient wsClient = new WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient();
            DataSet dsCoordinaciones = wsClient.GetCoordinaciones(1055);

            List<FiltroCoordinaciones> lstCoord = new List<FiltroCoordinaciones>();

            DataTable dtCoordinaciones = dsCoordinaciones.Tables[0];

            foreach (DataRow dtRow in dtCoordinaciones.Rows)
            {
                FiltroCoordinaciones coord = new FiltroCoordinaciones(Convert.ToInt32(dtRow["CoordinacionMedicaId"]), dtRow["Nombre"].ToString());
                lstCoord.Add(coord);
            }

            return Json(lstCoord, JsonRequestBehavior.AllowGet);

        }


        public JsonResult GetGuardias()
        {
            var query = Request.QueryString;
            long periodo = Convert.ToInt64(query.GetValues("periodo")[0]);
            int dia = Convert.ToInt32(query.GetValues("dia")[0]);
            int coordinacion = Convert.ToInt32(query.GetValues("coordinacion")[0]);
            DataSet dsGuardias = getGuardiasFromWebService(periodo,coordinacion);
            List<Guardia> guardias = getGuardiasFormatted(dsGuardias,dia);
            
            return Json(guardias, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetServicios()
        {
            var query = Request.QueryString;
            long periodo = Convert.ToInt64(query.GetValues("periodo")[0]);
            int dia = Convert.ToInt32(query.GetValues("dia")[0]);
            int coordinacion = Convert.ToInt32(query.GetValues("coordinacion")[0]);
            DataSet dsServicios = getServiciosFromWebService(periodo,coordinacion);
            List<Servicio> servicios = getServiciosFormatted(dsServicios, dia);

            return Json(servicios, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetResumenLiquidacion()
        {
            var query = Request.QueryString;
            long periodo = Convert.ToInt64(query.GetValues("periodo")[0]);
            int coordinacion = Convert.ToInt32(query.GetValues("coordinacion")[0]);
            DataSet dsResumen = getResumenFromWebService(periodo, coordinacion);
            List<ResumenLiquidacion> resLiquidacion = getResumenLiquidacionFormatted(dsResumen);

            return Json(resLiquidacion, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Medicos/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Medicos/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Medicos/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Medicos/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Medicos/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Medicos/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Medicos/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
