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
    public class ListaOrdenAdapter : RecyclerView.Adapter
    {

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
            View itemView = LayoutInflater.From(context).Inflate(Resource.Layout.item_list_holder, parent, false);
            return new MyViewHolder(itemView,gOnItemListener);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MyViewHolder myViewHolder = holder as MyViewHolder;
            myViewHolder.txt_codigo_orden.Text = items[position].Codigo;
            myViewHolder.txt_fecha_orden.Text = items[position].Fecha;
            myViewHolder.txt_hora_orden.Text = items[position].Hora;
            myViewHolder.txt_accion_orden.Text = items[position].Accion;
        }

        public override int ItemCount => items.Count;

        public class MyViewHolder : RecyclerView.ViewHolder, View.IOnClickListener //Java.Lang.Object, IDialogInterfaceOnClickListener
        {
            public TextView txt_codigo_orden, txt_fecha_orden, txt_hora_orden, txt_accion_orden;
            OnItemListener OnItemListener;

            public MyViewHolder(View itemView, OnItemListener OnItemListener) : base(itemView)
            {
                // Definimos la estructura de los holder en el recyclerView
                txt_codigo_orden = itemView.FindViewById<TextView>(Resource.Id.txtCodigoOrden);
                txt_fecha_orden = itemView.FindViewById<TextView>(Resource.Id.txtFechaOrden);
                txt_hora_orden = itemView.FindViewById<TextView>(Resource.Id.txtHoraOrden);
                txt_accion_orden = itemView.FindViewById<TextView>(Resource.Id.txtAccionOrden);

                this.OnItemListener = OnItemListener;
                itemView.SetOnClickListener(this);
            }

            public void OnClick(View v)
            {
                OnItemListener.onItemClick(AdapterPosition, v);
                //throw new NotImplementedException();
            }
        }

        public interface OnItemListener {
            void onItemClick(int position, View v);
        }

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