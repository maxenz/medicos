using ParamedicMedicosPrestaciones.Models;
using ParamedicMedicosPrestaciones.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.ServiceModel;
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
        // Reviso si me viene usr por parametro (aca hay que meter seguridad, despues vemos como)
        // Si no me viene nada, tiro 404. Si me viene un parametro usr, lo valido, y si esta ok, retorno el index comun.

        public ActionResult Index(long usr = 0)
        {
            if (usr == 0)
            {
                return RedirectToAction("Index", "Error");
            }
            else if (!validarUsuario(usr))
            {
                return RedirectToAction("Index", "Error");
            }
            return View();
        }

        //
        // Acá valido el usuario contra el web service. En ViewBags pongo algunas variables que me van a servir
        // del lado del cliente por el tema del select de medicos en modo administrador, etc.

        public bool validarUsuario(long usr)
        {
            WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient wsClient = new WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient();
            DataTable dtUsuario = wsClient.GetUsuarioValidacion(usr).Tables[0];
            if (Convert.ToInt32(dtUsuario.Rows[0]["Acceso"]) == 0)
            {
                return false;
            }
            else
            {
                ViewBag.UserName = dtUsuario.Rows[0]["NombreUsuario"].ToString();
                ViewBag.MedicoName = dtUsuario.Rows[0]["NombreMedico"].ToString();
                ViewBag.Acceso = Convert.ToInt32(dtUsuario.Rows[0]["Acceso"]);
                ViewBag.Usuario_Id_Medico = usr;
                return true;
            }

        }

        //
        // Obtengo los datos para el filtro de periodos (obtengo el actual y los dos anteriores)

        public JsonResult getFiltroPeriodos()
        {
            List<FiltroPeriodos> lstPeriodos = new List<FiltroPeriodos>();
            DateTime fecActual = DateTime.Now;
            DateTime fecAnterior = fecActual.AddMonths(-1);
            DateTime fecAntePenultimo = fecActual.AddMonths(-2);
            lstPeriodos.Add(new FiltroPeriodos(getFormattedPeriod(fecAntePenultimo), getFormattedDescriptionOfPeriod(fecAntePenultimo)));
            lstPeriodos.Add(new FiltroPeriodos(getFormattedPeriod(fecAnterior), getFormattedDescriptionOfPeriod(fecAnterior)));
            lstPeriodos.Add(new FiltroPeriodos(getFormattedPeriod(fecActual), getFormattedDescriptionOfPeriod(fecActual)));

            return Json(lstPeriodos, JsonRequestBehavior.AllowGet);
        }

        //
        // Obtengo los datos para el filtro de coordinaciones (recibe el id del medico para buscar todas las coordinaciones de él)

        public JsonResult getFiltroCoordinaciones(long usr_id)
        {
            WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient wsClient = new WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient();
            DataSet dsCoordinaciones = wsClient.GetCoordinaciones(usr_id);

            List<FiltroCoordinaciones> lstCoord = new List<FiltroCoordinaciones>();

            DataTable dtCoordinaciones = dsCoordinaciones.Tables[0];

            foreach (DataRow dtRow in dtCoordinaciones.Rows)
            {
                FiltroCoordinaciones coord = new FiltroCoordinaciones(Convert.ToInt32(dtRow["CoordinacionMedicaId"]),
                                                                        dtRow["Nombre"].ToString());
                lstCoord.Add(coord);
            }

            return Json(lstCoord, JsonRequestBehavior.AllowGet);

        }


        //
        // Obtengo los datos para el filtro de motivos de reclamo (popup guardia / servicios)
        public JsonResult getFiltroMotivoReclamo(int flgTipoReclamo)
        {
            WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient wsClient = new WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient();
            DataSet dsReclamos = wsClient.GetMotivosReclamo(flgTipoReclamo);

            List<FiltroReclamo> lstFtrReclamo = new List<FiltroReclamo>();

            DataTable dtReclamos = dsReclamos.Tables[0];

            foreach (DataRow dtRow in dtReclamos.Rows)
            {
                //Esto lo hago para no tener que ir a la base de datos cada vez que hago un select en un motivo
                //Entonces tengo id/difIngreso en el value del select, y si esta en 1, habilito para modificar horario.
                string idConDifIngreso = dtRow["ID"].ToString() + "/" + dtRow["flgDifIngreso"].ToString();
                FiltroReclamo reclamo = new FiltroReclamo(idConDifIngreso,dtRow["Descripcion"].ToString());
                lstFtrReclamo.Add(reclamo);
            }

            return Json(lstFtrReclamo, JsonRequestBehavior.AllowGet);

        }

        //
        // Obtengo los datos para el filtro de medicos (Si me llega el id de un medico, es porque es un medico el que está solicitando
        // el filtro, entonces solamente accede a su registro.

        public JsonResult getFiltroMedicos(long usr_id = 0)
        {
            WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient wsClient = new WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient();
            DataSet dsMedicos = wsClient.GetMedicos();

            List<Medico> lstMedicos = new List<Medico>();

            DataTable dtMedicos = dsMedicos.Tables[0];

            foreach (DataRow dtRow in dtMedicos.Rows)
            {
                Medico medico = new Medico();
                medico.UsuarioID = Convert.ToInt64(dtRow["UsuarioId"]);
                medico.Nombre = dtRow["Nombre"].ToString();
                lstMedicos.Add(medico);
            }

            if (usr_id != 0)
            {
                lstMedicos = lstMedicos.Where(x => x.UsuarioID == usr_id).ToList();
            }

            return Json(lstMedicos, JsonRequestBehavior.AllowGet);

        }

        //
        // Obtengo los datos para la grilla de guardias (los parametros me llegan por querystring)

        public JsonResult GetGuardias()
        {
            var query = Request.QueryString;
            long periodo = Convert.ToInt64(query.GetValues("periodo")[0]);
            int dia = Convert.ToInt32(query.GetValues("dia")[0]);
            int coordinacion = Convert.ToInt32(query.GetValues("coordinacion")[0]);
            long medico = Convert.ToInt64(query.GetValues("medico")[0]);
            DataSet dsGuardias = getGuardiasFromWebService(periodo, coordinacion, medico);
            List<Guardia> guardias = getGuardiasFormatted(dsGuardias, dia);

            return Json(guardias, JsonRequestBehavior.AllowGet);
        }

        //
        // Obtengo los datos para la grilla de servicios (los parametros me llegan por querystring)

        public JsonResult GetServicios()
        {
            var query = Request.QueryString;
            long periodo = Convert.ToInt64(query.GetValues("periodo")[0]);
            int dia = Convert.ToInt32(query.GetValues("dia")[0]);
            int coordinacion = Convert.ToInt32(query.GetValues("coordinacion")[0]);
            long medico = Convert.ToInt64(query.GetValues("medico")[0]);
            DataSet dsServicios = getServiciosFromWebService(periodo, coordinacion, medico);
            List<Servicio> servicios = getServiciosFormatted(dsServicios, dia);

            return Json(servicios, JsonRequestBehavior.AllowGet);
        }

        //
        // Obtengo los datos para la grilla de resumen de liquidacion (los parametros me llegan por querystring)

        public JsonResult GetResumenLiquidacion()
        {
            var query = Request.QueryString;
            long periodo = Convert.ToInt64(query.GetValues("periodo")[0]);
            int coordinacion = Convert.ToInt32(query.GetValues("coordinacion")[0]);
            long medico = Convert.ToInt64(query.GetValues("medico")[0]);
            DataSet dsResumen = getResumenFromWebService(periodo, coordinacion, medico);
            List<ResumenLiquidacion> resLiquidacion = getResumenLiquidacionFormatted(dsResumen);

            GetHorarios();

            return Json(resLiquidacion, JsonRequestBehavior.AllowGet);
        }

        //
        // Obtengo los datos para la grilla de horarios del medico (los parametros me llegan por querystring)

        public JsonResult GetHorarios()
        {
            var query = Request.QueryString;
            long periodo = Convert.ToInt64(query.GetValues("periodo")[0]);
            long medico = Convert.ToInt64(query.GetValues("medico")[0]);
            DataSet dsHorarios = getHorariosFromWebService(periodo, medico);
            List<Horario> lstHorarios = getHorariosFormatted(dsHorarios);

            return Json(lstHorarios, JsonRequestBehavior.AllowGet);
        }

        //
        // Obtengo datos de los diferentes webservices

        private DataSet getGuardiasFromWebService(long periodo, int coord, long usr_id)
        {
            WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient wsClient = new WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient();
            return wsClient.GetGuardiasDetalle(usr_id, periodo, coord);

        }

        private DataSet getServiciosFromWebService(long periodo, int coord, long medico)
        {
            WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient wsClient = new WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient();
            return wsClient.GetIncidentes(medico, periodo, coord);
        }

        private DataSet getResumenFromWebService(long periodo, int coord, long medico)
        {
            WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient wsClient = new WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient();
            return wsClient.GetResumen(medico, periodo, coord);
        }

        private DataSet getHorariosFromWebService(long periodo, long medico)
        {
            WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient wsClient = new WSContratadosLiquidaciones.ContratadosLiquidacionesSoapClient();
            try
            {
                DataSet ds = wsClient.GetHorarios(medico, periodo);
                return ds;
            }
            catch
            {
                return null;
            }

        }

        //Formateo los dataset traidos de los webservices para armar los datos estructurados en objetos de distintas clases

        private List<Guardia> getGuardiasFormatted(DataSet dsGuardias, int dia)
        {

            List<Guardia> lstGuardias = new List<Guardia>();

            DataTable dtGuardias = dsGuardias.Tables[0];

            foreach (DataRow dtRow in dtGuardias.Rows)
            {
                Guardia guardia = new Guardia();
                guardia.ID = dtRow["ID"].ToString();
                guardia.Dia = Convert.ToInt32((dtRow["FecMovimiento"].ToString()).Substring(6, 2));
                guardia.DiaDeLaSemana = getGuardiaFechaFormatted(dtRow["FecMovimiento"].ToString(), 1);
                guardia.Periodo = getGuardiaFechaFormatted(dtRow["FecMovimiento"].ToString(), 2);
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
                guardia.Estado = Convert.ToInt32(dtRow["Estado"]);
                guardia.FecMovimiento = getGuardiaFechaFormatted(dtRow["FecMovimiento"].ToString(), 3);

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
                servicio.Dia = Convert.ToInt32((dtRow["FecIncidente"].ToString()).Substring(6, 2));

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
                    resLiq.Importe = dtRow["Importe"].ToString();

                    lstResumenLiq.Add(resLiq);
                }
            }

            return lstResumenLiq;

        }

        private List<Horario> getHorariosFormatted(DataSet dsHorarios)
        {
            List<Horario> lstHorarios = new List<Horario>();
            List<string> Dias = new List<string>() { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };
            foreach (string dia in Dias)
            {
                Horario horario = new Horario();
                horario.DiaDeLaSemana = dia;
                lstHorarios.Add(horario);
            }

            DataTable dt = dsHorarios.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                string diaDeLaSemana = Convert.ToDateTime(row["FecEntrada"]).ToString("dddd");
                diaDeLaSemana = char.ToUpper(diaDeLaSemana[0]) + diaDeLaSemana.Substring(1);
                string entrada = Convert.ToDateTime(row["HorEntrada"]).ToString("hh:mm");
                string salida = Convert.ToDateTime(row["HorSalida"]).ToString("hh:mm");
                string movil = row["MovilId"].ToString();

                var hor = lstHorarios.SingleOrDefault(x => x.DiaDeLaSemana == diaDeLaSemana);
                if (hor.Entrada1 != null)
                {
                    hor.Entrada2 = entrada;
                    hor.Salida2 = salida;
                    hor.Movil2 = movil;
                }
                else
                {
                    hor.Entrada1 = entrada;
                    hor.Salida1 = salida;
                    hor.Movil1 = movil;
                }

            }

            return lstHorarios;

        }

        //Métodos auxiliares


        private string getFormattedDescriptionOfPeriod(DateTime fec)
        {
            string mes = fec.ToString("MMMM");
            mes = char.ToUpper(mes[0]) + mes.Substring(1);
            string year = fec.ToString("yyyy");
            return mes + " " + year;
        }

        private string getFormattedPeriod(DateTime fec)
        {

            int month = fec.Month;
            string strMonth = month.ToString();

            if (month < 10)
            {
                strMonth = "0" + month.ToString();
            }

            return fec.ToString("yyyy") + strMonth;
        }

        private string getGuardiaFechaFormatted(string fecha, int pOpcion)
        {

            string retVal = "";
            int dia = Convert.ToInt32(fecha.Substring(6, 2));
            int mes = Convert.ToInt32(fecha.Substring(4, 2));
            int año = Convert.ToInt32(fecha.Substring(0, 4));

            DateTime fecFormatted = new DateTime(año, mes, dia);
            string diaDeLaSemana = fecFormatted.ToString("ddd");

            switch (pOpcion)
            {
                case 1:
                    retVal = diaDeLaSemana + " " + dia;
                    break;
                case 2:
                    retVal = mes + "/" + (año.ToString()).Substring(2, 2);
                    break;
                case 3:
                    retVal = dia + "/" + mes + "/" + año;
                    break;
            }

            return retVal;

        }

    }
}
