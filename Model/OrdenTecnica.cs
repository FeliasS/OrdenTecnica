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
    public class OrdenTecnica
    {
        public string Codigo { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Accion { get; set; }

        public OrdenTecnica(string codigo, string fecha, string hora, string accion)
        {
            this.Codigo = codigo;
            this.Fecha = fecha;
            this.Hora = hora;
            this.Accion = accion;
        }
    }
}