using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace appOrdenTecnica.Adapter
{
    class MyViewHolder: RecyclerView.ViewHolder
    {
        public TextView txt_codigo_orden, txt_fecha_orden, txt_hora_orden, txt_accion_orden;

        public MyViewHolder(View itemView) : base(itemView)
        {
            // Definimos la estructura de los holder en el recyclerView
            txt_codigo_orden = itemView.FindViewById<TextView>(Resource.Id.txtCodigoOrden);
            txt_fecha_orden = itemView.FindViewById<TextView>(Resource.Id.txtFechaOrden);
            txt_hora_orden = itemView.FindViewById<TextView>(Resource.Id.txtHoraOrden);
            txt_accion_orden = itemView.FindViewById<TextView>(Resource.Id.txtAccionOrden);
        }
    }
}