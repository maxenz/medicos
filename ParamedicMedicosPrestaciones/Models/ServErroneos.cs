using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParamedicMedicosPrestaciones.Models
{
    public class ServErroneos
    {
        public int ID { get; set; }
        public string Fecha { get; set; }
        public string NroInc { get; set; }
        public string Orden { get; set; }
        public string NroAfiliado { get; set; }
        public string Paciente { get; set; }
        public string Domicilio { get; set; }
        public string Grado { get; set; }
        public string Error { get; set; }
        public string FH { get; set; }
    }
}