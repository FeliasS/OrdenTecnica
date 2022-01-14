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
    public class ListaTecnicosAdapter : RecyclerView.Adapter
    {
        Context context;
        List<Tecnicos> items;

        private OnItemListener gOnItemListener;
        private List<Tecnicos> originalitems;//*5 readonly


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
            View itemView = LayoutInflater.From(context).Inflate(Resource.Layout.item_list_tecnicos, parent, false);
            return new MyViewHolder(itemView, gOnItemListener);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MyViewHolder myViewHolder = holder as MyViewHolder;
            myViewHolder.txtidTecnico.Text = items[position].idTecnico;
            myViewHolder.txtnomTecnico.Text = items[position].nomTecnico;
            myViewHolder.txtestadoTec.Text = items[position].estado.ToString();
        }

        public override int ItemCount => items.Count;

        public class MyViewHolder : RecyclerView.ViewHolder, View.IOnClickListener //Java.Lang.Object, IDialogInterfaceOnClickListener
        {
            public TextView txtidTecnico, txtnomTecnico, txtestadoTec;
            OnItemListener OnItemListener;

            public MyViewHolder(View itemView, OnItemListener OnItemListener) : base(itemView)
            {
                // Definimos la estructura de los holder en el recyclerView
                txtidTecnico = itemView.FindViewById<TextView>(Resource.Id.txtidTecnico);
                txtnomTecnico = itemView.FindViewById<TextView>(Resource.Id.txtnomTecnico);
                txtestadoTec = itemView.FindViewById<TextView>(Resource.Id.txtestadoTec);

                this.OnItemListener = OnItemListener;
                itemView.SetOnClickListener(this);
            }

            public void OnClick(View v)
            {
                OnItemListener.onItemClick(AdapterPosition, v);
            }
        }

        public interface OnItemListener
        {
            void onItemClick(int position, View v);
        }

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