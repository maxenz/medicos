using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParamedicMedicosPrestaciones.ViewModels
{
    public class OperativaEnCurso
    {

        public int ID { get; set; }
        public string NroInc { get; set; }
        public string ViajeID { get; set; }
        public string ClienteID { get; set; }
        public string NroInterno { get; set; }
        public string Llamada { get; set; }
        public string Nombre { get; set; }
        public string Domicilio { get; set; }
        public string Localidad { get; set; }
        public string Grado { get; set; }
        public string WebMovil { get; set; }
        public string Arribo { get; set; }
        public string WebEstado { get; set; }
        public string Respuesta { get; set; }
        public string Reclamo { get; set; }
        public string Ecg { get; set; }
    }
}