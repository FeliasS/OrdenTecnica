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
    class Cliente
    {
        internal int _ID_CLIENTE { get; set; }
        internal string _COD_CLIENTE { get; set; }
        internal string _RAZON_SOCIAL { get; set; }
        internal string _RUC { get; set; }
        internal string _TELEFONO_1 { get; set; }
        internal string _TELEFONO_2 { get; set; }
        internal string _DIRECCION { get; set; }
        internal int _ESTADO { get; set; }
    }
}