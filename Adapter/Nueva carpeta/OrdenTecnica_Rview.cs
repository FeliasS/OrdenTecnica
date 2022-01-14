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
using appOrdenTecnica.Model;

namespace appOrdenTecnica.Adapter
{
    public class OrdenTecnica_Rview: RecyclerView.Adapter  //<OrdenTecnica_Rview.ViewHolder>
    {
        Context context;
        List<OrdenTecnica> items; // Obtenemos los datos de la BD
       

        // Constructor
        public OrdenTecnica_Rview(Context context, List<OrdenTecnica> items)
        {
            this.context = context;
            this.items = items;
        }

        public override int ItemCount => items.Count;

       

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MyViewHolder myViewHolder = holder as MyViewHolder;
            myViewHolder.txt_codigo_orden.Text = items[position].Codigo;
            myViewHolder.txt_fecha_orden.Text = items[position].Fecha;
            myViewHolder.txt_hora_orden.Text = items[position].Hora;
            myViewHolder.txt_accion_orden.Text = items[position].Accion;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // El item_list_holder mostrara los datos obtenidos
            View itemView = LayoutInflater.From(context).Inflate(Resource.Layout.item_list_holder, parent, false);
            return new MyViewHolder(itemView);
            //getAdapterPosition();
        }

       
    }
}