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
    // Adapter Lista Ordenes
    public class ListaOrdenAdapter : RecyclerView.Adapter
    {
        // Definiendo los elementos para la Lista de ordenes
        Context context;
        List<OrdenTecnica> items;
        private List<OrdenTecnica> mList = new List<OrdenTecnica>();
        private OnItemListener gOnItemListener;
        private List<OrdenTecnica> originalitems;


        public ListaOrdenAdapter(Context context, List<OrdenTecnica> items, OnItemListener OnItemListener)
        {
            this.context = context;
            this.items = items;
            this.gOnItemListener = OnItemListener;
            this.originalitems = new List<OrdenTecnica>();
            originalitems.AddRange(items);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Asignamos el ViewHolder con la vista que contendra los items 
            View itemView = LayoutInflater.From(context).Inflate(Resource.Layout.item_list_holder, parent, false);
            return new MyViewHolder(itemView,gOnItemListener);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            // Instanciamos y seteamos en en el view los atributos del modelo
            MyViewHolder myViewHolder = holder as MyViewHolder;
            myViewHolder.txt_codigo_orden.Text = items[position].Codigo;
            myViewHolder.txt_fecha_orden.Text = items[position].Fecha;
            myViewHolder.txt_hora_orden.Text = items[position].Hora;
            myViewHolder.txt_accion_orden.Text = items[position].Accion;
        }

        // Obtenemos la cantida de la lista 
        public override int ItemCount => items.Count;

        /*
        * ===========================================================
        * ===========================================================
        */

        // View Holder del lista Ordenes
        public class MyViewHolder : RecyclerView.ViewHolder, View.IOnClickListener //Java.Lang.Object, IDialogInterfaceOnClickListener
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
        public interface OnItemListener {
            void onItemClick(int position, View v);
        }

        //Funcion para buscar en el recyclerView de la lista de Ordenes
        public void filter(String SearchInfo) {

            if (SearchInfo.Length == 0)
            {
                items.Clear();
                items.AddRange(originalitems);
            }
            else
            {
                items.Clear();
                List<OrdenTecnica> newlist = originalitems.Where(x => x.Codigo.StartsWith(SearchInfo)).ToList(); //StartsWith, Contains
                items.AddRange(newlist);
            }
            NotifyDataSetChanged();

        }
    }
}