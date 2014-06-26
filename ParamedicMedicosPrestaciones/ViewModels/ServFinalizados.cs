using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParamedicMedicosPrestaciones.ViewModels
{
    public class ServFinalizados
    {
        public int ID { get; set; }
        public string NroInc { get; set; }
        public string Orden { get; set; }
        public string Llamada { get; set; }
        public string Paciente { get; set; }
        public string Domicilio { get; set; }
        public string Grado { get; set; }
        public string Arribo { get; set; }
        public string Diagnostico { get; set; }
        public string Deriva { get; set; }
        public string Final { get; set; }
    }
}