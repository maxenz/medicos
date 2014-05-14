using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParamedicMedicosPrestaciones.Models
{
    public class Guardia
    {
        public int ID { get; set; }
        public string Tarifa { get; set; }
        public int Movil { get; set; }
        public string HorarioEntrada { get; set; }
        public string HorarioSalida { get; set; }
        public int HorasTrabajadas { get; set; }
        public int Rojos { get; set; }
        public int Amarillos { get; set; }
        public int Verdes { get; set; }
        public int Tps { get; set; }
        public int Incidentes { get; set; }
        public double Importe { get; set; }

        public Guardia(int id,string tarifa, int movil, string horarioEntrada, string horarioSalida, int horasTrabajadas,
            int rojos, int amarillos, int verdes, int tps, int incidentes, double importe)
        {
            this.ID = id;
            this.Tarifa = tarifa;
            this.Movil = movil;
            this.HorarioEntrada = horarioEntrada;
            this.HorarioSalida = horarioSalida;
            this.HorasTrabajadas = horasTrabajadas;
            this.Rojos = rojos;
            this.Amarillos = amarillos;
            this.Verdes = verdes;
            this.Tps = tps;
            this.Incidentes = incidentes;
            this.Importe = importe;
        }

    }
}