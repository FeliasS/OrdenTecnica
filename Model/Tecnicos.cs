using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace appOrdenTecnica.Model
{
    public class Tecnicos
    {
        public string idTecnico { get; set; }
        public string nomTecnico { get; set; }
        public int estado { get; set; } //0 - inactivo //1 -activo

        public Tecnicos()
        {
        }

        public Tecnicos(string idTecnico, string nomTecnico, int estado)
        {
            this.idTecnico = idTecnico;
            this.nomTecnico = nomTecnico;
            this.estado = estado;
        }
    }
}