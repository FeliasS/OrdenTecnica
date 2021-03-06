using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroidX.RecyclerView.Widget;
using appOrdenTecnica.Model;
using appOrdenTecnica.Adapter;
using AndroidX.Transitions;
using AndroidX.CardView.Widget;

namespace appOrdenTecnica.Fragments
{
    public class Fragments_lista_ordenes :  AndroidX.Fragment.App.Fragment, ListaOrdenAdapter.OnItemListener ,SearchView.IOnQueryTextListener //*2 // Heredamos de la clase FRAGAMENTS
    {
        //Definiendo los elementos de la vista
        private SearchView searchView;
        private RecyclerView recyclerview;

        ListaOrdenAdapter adapter;
        LinearLayoutManager linearLayoutManager;
        List<OrdenTecnica> ordenes;

        LinearLayout hiddenView;
        CardView cardView;
        TextView codigo = null;
        String codigoValue = "";

        private List<OrdenTecnica> mList = new List<OrdenTecnica>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Activity.Title = "LISTADO DE ORDENES";
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {   
            // Asigamos al fragment un layout
            View view = inflater.Inflate(Resource.Layout.layout_lista_ordenes, container, false);

            // Capturamos los IDS de los controles y Widget
            searchView = view.FindViewById<SearchView>(Resource.Id.SearchOrden);
            SearchListener();
            recyclerview = view.FindViewById<RecyclerView>(Resource.Id.recyViewListaOrdenes);
            recyclerview.HasFixedSize = true; // ¿Para qué funciona esto?

            // Instanciamos el lineaLayoutManager en el contexto activity
            linearLayoutManager = new LinearLayoutManager(Activity);

            GenerarItem();

            recyclerview.SetLayoutManager(linearLayoutManager);
          
            return view;
            
        } 
        
        public void onItemClick(int position, View view)//*3
        {
            Toast.MakeText(Activity, "Item :" + position, ToastLength.Short).Show();
            hiddenView = view.FindViewById<LinearLayout>(Resource.Id.hidden_view);
            cardView = view.FindViewById<CardView>(Resource.Id.base_cardview);

            Button testButton = view.FindViewById<Button>(Resource.Id.btnEdit);
            ordenes.GetEnumerator();
            testButton.Click += btn_Clicked;
           
            if (hiddenView.Visibility == Android.Views.ViewStates.Visible)
            {
                TransitionManager.BeginDelayedTransition(cardView,new AutoTransition());
                hiddenView.Visibility = Android.Views.ViewStates.Gone;
            }
            else
            {
                TransitionManager.BeginDelayedTransition(cardView,new AutoTransition());
                hiddenView.Visibility = Android.Views.ViewStates.Visible;
            }

            codigo = view.FindViewById<TextView>(Resource.Id.txtCodigoOrden);
            codigoValue = codigo.Text.ToString();

            void btn_Clicked(object sender, EventArgs e)
            {
                Toast.MakeText(Activity, "here :" + position, ToastLength.Short).Show();
                
                Bundle data = new Bundle();
                data.PutString("codigo", codigoValue);
                AndroidX.Fragment.App.Fragment fragment = new FragmentEditarOrden();
                fragment.Arguments = data;
                
                Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.ConteinerLayout, fragment).AddToBackStack(null).Commit();
                
            }

        }

        private void GenerarItem()
        {
            ordenes = new List<OrdenTecnica>();
            for (int i = 0; i < 50; i++)
            {
                ordenes.Add(new OrdenTecnica("C000" + i,
                    "15/10/2021", "08:00 AM",
                    "CREADA"));
            }
            adapter = new ListaOrdenAdapter(Activity, ordenes,this);
            recyclerview.SetAdapter(adapter);
        }

        private void SearchListener() {

            searchView.SetOnQueryTextListener(this);
        }
        public bool OnQueryTextChange(string newText)
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