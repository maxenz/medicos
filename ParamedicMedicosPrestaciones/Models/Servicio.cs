using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParamedicMedicosPrestaciones.Models
{
    public class Servicio
    {
        public string IncidenteID { get; set; }
        public string Fecha { get; set; }
        public string NroInc { get; set; }
        public string Iva { get; set; }
        public string Paciente { get; set; }
        public string Localidad { get; set; }
        public string Cdn { get; set; }
        public string Tarifa { get; set; }
        public string Dia { get; set; }
        public string Tur { get; set; }
        public string Grado { get; set; }
        public double CoPago { get; set; }
        public double Importe { get; set; }


    }
}