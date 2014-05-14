using ParamedicMedicosPrestaciones.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        private DataSet getGuardiasFromWebService()
        {
            WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient wsClient = new WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient();
            return wsClient.GetGuardiasDetalle(1055,201404);
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

        private List<Guardia> getGuardiasFormatted(DataSet dsGuardias)
        {

            List<Guardia> lstGuardias = new List<Guardia>();

            DataTable dtGuardias = dsGuardias.Tables[0];

            foreach (DataRow dtRow in dtGuardias.Rows)
            {
                Guardia guardia = new Guardia();
                guardia.ID = dtRow["ID"].ToString();
                guardia.Dia = getGuardiaFechaFormatted(dtRow["FecMovimiento"].ToString(),1);
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

            return lstGuardias;

        }

        public JsonResult GetGuardias()
        {

            DataSet dsGuardias = getGuardiasFromWebService();
            List<Guardia> guardias = getGuardiasFormatted(dsGuardias);

            return Json(guardias, JsonRequestBehavior.AllowGet);
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
