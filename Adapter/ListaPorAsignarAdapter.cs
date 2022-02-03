using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using appOrdenTecnica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace appOrdenTecnica.Adapter
{
    // Adapter Lista por Asignar
    public class ListaPorAsignarAdapter : RecyclerView.Adapter
    {
        // Definiendo los elementos de item_list_revisar
        Context context;
        List<ListaOrdenTecnica> items;

        private OnItemListener gOnItemListener;
        private List<ListaOrdenTecnica> originalitems;

        public ListaPorAsignarAdapter(Context context, List<ListaOrdenTecnica> items, OnItemListener OnItemListener)
        {
            this.context = context;
            this.items = items;

            this.gOnItemListener = OnItemListener;
            this.originalitems = new List<ListaOrdenTecnica>();
            originalitems.AddRange(items);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Asignamos el ViewHolder con la vista que contendra los items 
            View itemView = LayoutInflater.From(context).Inflate(Resource.Layout.item_list_porasignar, parent, false);
            return new MyViewHolder(itemView, gOnItemListener);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            // Instanciamos y seteamos en en el view los atributos del modelo
            MyViewHolder myViewHolder = holder as MyViewHolder;
            myViewHolder.txt_codigo_orden.Text = items[position].COD_ORDEN;
            myViewHolder.txt_fecha_orden.Text = items[position].FECHA_ORDEN;
            myViewHolder.txt_hora_orden.Text = items[position].HORA_ORDEN;
            myViewHolder.txt_accion_orden.Text = items[position].ESTADO;
        }

        // Obtenemos la cantida de la lista 
        public override int ItemCount => items.Count;

        /*
         * ===========================================================
         * ===========================================================
         */

        // View Holder del lista Asignados
        public class MyViewHolder : RecyclerView.ViewHolder, View.IOnClickListener 
        {
            //Definimos los elementos de la vista
            public TextView txt_codigo_orden, txt_fecha_orden, txt_hora_orden, txt_accion_orden;
            OnItemListener OnItemListener;

            public MyViewHolder(View itemView, OnItemListener OnItemListener) : base(itemView)
            {
                // Instanciamos los elementos
                txt_codigo_orden = itemView.FindViewById<TextView>(Resource.Id.txtCodigoOrden);
                txt_fecha_orden = itemView.FindViewById<TextView>(Resource.Id.txtFechaOrden);
                txt_hora_orden = itemView.FindViewById<TextView>(Resource.Id.txtHoraOrden);
                txt_accion_orden = itemView.FindViewById<TextView>(Resource.Id.txtAccionOrden);

                // Instanciamos el evento click
                this.OnItemListener = OnItemListener;
                itemView.SetOnClickListener(this);
            }

            public void OnClick(View v)
            {
                OnItemListener.onItemClick(AdapterPosition, v);
            }
        }

        /*
         * ===========================================================
         * ===========================================================
         */

        // Interface para conectar el evento click
        public interface OnItemListener
        {
            void onItemClick(int position, View v);
        }

        //Funcion para buscar en el recyclerView de la lista por Asignar
        public void filter(String SearchInfo)
        {
            if (SearchInfo.Length == 0)
            {
                items.Clear();
                items.AddRange(originalitems);
            }
            else
            {
                items.Clear();
                List<ListaOrdenTecnica> newlist = originalitems.Where(x => x.COD_ORDEN.StartsWith(SearchInfo)).ToList(); //StartsWith, Contains
                items.AddRange(newlist);
                //List<OrdenTecnica> newlist = originalitems.Where(x => x.Codigo.StartsWith(SearchInfo)).ToList(); //StartsWith, Contains
                //items.AddRange(newlist);
            }
            NotifyDataSetChanged();

        }
    }
}