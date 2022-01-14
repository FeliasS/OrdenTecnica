using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.CardView.Widget;
using AndroidX.RecyclerView.Widget;
using AndroidX.Transitions;
using appOrdenTecnica.Adapter;
using appOrdenTecnica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace appOrdenTecnica.Fragments
{
    public class FragmentAsignadosxTecLista : AndroidX.Fragment.App.Fragment, ListaAsignadosTodosAdapter.OnItemListener, SearchView.IOnQueryTextListener
    {
        private SearchView searchView; //*1
        private RecyclerView recyclerview;
        private LinearLayoutManager linearLayoutManager;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Activity.Title = "ASIGNADOS";
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.layout_lista_ordenes, container, false);
            searchView = view.FindViewById<SearchView>(Resource.Id.SearchOrden);//*2
            SearchListener();//*4
            recyclerview = view.FindViewById<RecyclerView>(Resource.Id.recyViewListaOrdenes);
            recyclerview.HasFixedSize = true;
            linearLayoutManager = new LinearLayoutManager(Activity);
            GenerarItem();
            recyclerview.SetLayoutManager(linearLayoutManager);

            return view;
        }

        LinearLayout hiddenView;
        CardView cardView;
        TextView codigo = null;
        String codigoValue = "";
        public void onItemClick(int position, View view)//*3
        {
            Toast.MakeText(Activity, "Item :" + position, ToastLength.Short).Show();
            hiddenView = view.FindViewById<LinearLayout>(Resource.Id.hidden_view);
            cardView = view.FindViewById<CardView>(Resource.Id.base_cardview);

            Button testButton = view.FindViewById<Button>(Resource.Id.btnEdit);
            ordenes.GetEnumerator();
            testButton.Click += btn_Clicked;

            //throw new NotImplementedException();
            if (hiddenView.Visibility == Android.Views.ViewStates.Visible)
            {
                TransitionManager.BeginDelayedTransition(cardView, new AutoTransition());
                hiddenView.Visibility = Android.Views.ViewStates.Gone;
            }
            else
            {
                TransitionManager.BeginDelayedTransition(cardView, new AutoTransition());
                hiddenView.Visibility = Android.Views.ViewStates.Visible;
            }

            codigo = view.FindViewById<TextView>(Resource.Id.txtCodigoOrden);
            codigoValue = codigo.Text.ToString();

            void btn_Clicked(object sender, EventArgs e)
            {
                Toast.MakeText(Activity, "here :" + position, ToastLength.Short).Show();
                Bundle data = new Bundle(); //pasando codigo de orden
                data.PutString("codigo", codigoValue);
                AndroidX.Fragment.App.Fragment fragment = new FragmentDelTecnicoOrden();
                fragment.Arguments = data;
                Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.ConteinerLayout, fragment).AddToBackStack(null).Commit();

            }

        }
        ListaAsignadosTodosAdapter adapter;
        List<OrdenTecnica> ordenes;
        private void GenerarItem()
        {
            ordenes = new List<OrdenTecnica>();
            for (int i = 0; i < 50; i++)
            {
                ordenes.Add(new OrdenTecnica("C000" + i,
                    "15/10/2021", "08:00 AM",
                    "ASIGNADOS Y"));
            }
            adapter = new ListaAsignadosTodosAdapter(Activity, ordenes, this);
            recyclerview.SetAdapter(adapter);
        }

        private void SearchListener()
        {
            searchView.SetOnQueryTextListener(this);
        }
        public bool OnQueryTextChange(string newText)//cambiar texto
        {
            adapter.filter(newText);
            return false;
        }

        public bool OnQueryTextSubmit(string query)
        {
            throw new NotImplementedException();
        }
    }
}