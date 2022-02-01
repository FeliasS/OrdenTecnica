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
    // Adapter Lista de tecnicos
    public class ListaTecnicosAdapter : RecyclerView.Adapter
    {
        // Definiendo los elementos de items_list_tecnicos
        Context context;
        List<Tecnicos> items;

        private OnItemListener gOnItemListener;
        private List<Tecnicos> originalitems;

        public ListaTecnicosAdapter(Context context, List<Tecnicos> items, OnItemListener OnItemListener)
        {
            this.context = context;
            this.items = items;

            this.gOnItemListener = OnItemListener;
            this.originalitems = new List<Tecnicos>();
            originalitems.AddRange(items);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Asignamos el ViewHolder con la vista que contendra los items 
            View itemView = LayoutInflater.From(context).Inflate(Resource.Layout.item_list_tecnicos, parent, false);
            return new MyViewHolder(itemView, gOnItemListener);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            // Instanciamos y seteamos en el view los atributos del modelo
            MyViewHolder myViewHolder = holder as MyViewHolder;
            myViewHolder.txtidTecnico.Text = items[position].idTecnico;
            myViewHolder.txtnomTecnico.Text = items[position].nomTecnico;
            myViewHolder.txtestadoTec.Text = items[position].estado.ToString();
        }

        // Obtenemos la cantida de la lista 
        public override int ItemCount => items.Count;

        /*
         * ===========================================================
         * ===========================================================
         */

        // View Holder del lista Asignados
        public class MyViewHolder : RecyclerView.ViewHolder, View.IOnClickListener //Java.Lang.Object, IDialogInterfaceOnClickListener
        {
            //Definimos los elementos de la vista
            public TextView txtidTecnico, txtnomTecnico, txtestadoTec;
            OnItemListener OnItemListener;

            public MyViewHolder(View itemView, OnItemListener OnItemListener) : base(itemView)
            {
                // Instanciamos los elementos
                txtidTecnico = itemView.FindViewById<TextView>(Resource.Id.txtidTecnico);
                txtnomTecnico = itemView.FindViewById<TextView>(Resource.Id.txtnomTecnico);
                txtestadoTec = itemView.FindViewById<TextView>(Resource.Id.txtestadoTec);

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

        //Funcion para buscar en el recyclerView de la lista Tecnicos
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
                List<Tecnicos> newlist = originalitems.Where(x => x.nomTecnico.StartsWith(SearchInfo)).ToList(); 
                items.AddRange(newlist);
            }
            NotifyDataSetChanged();
        }
    }
}