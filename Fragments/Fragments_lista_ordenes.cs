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

namespace appOrdenTecnica.Fragments
{
    public class Fragments_lista_ordenes : AndroidX.Fragment.App.Fragment // Heredamos de la clase FRAGAMENTS
    {
        private SearchView searchView;
        private RecyclerView recyclerview;

        OrdenTecnica_Rview adapter;
        LinearLayoutManager linearLayoutManager;
        List<OrdenTecnica> ordenes;
        ListAdapter listAdapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            
            // Asigamos al fragment un layout
            View view = inflater.Inflate(Resource.Layout.layout_lista_ordenes, container, false);

            // Capturamos los IDS de los controles y Widget
            searchView = view.FindViewById<SearchView>(Resource.Id.SearchOrden);
            recyclerview = view.FindViewById<RecyclerView>(Resource.Id.recyViewListaOrdenes);
            recyclerview.HasFixedSize = true; // ¿Para qué funciona esto?
            // Instanciamos el lineaLayoutManager en el contexto activity
            linearLayoutManager = new LinearLayoutManager(Activity);
            // Asignamos al recyclerView la estructura del LinerLayoutManager
            recyclerview.SetLayoutManager(linearLayoutManager);

            GenerarItem();
            
            return view;
            
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
            adapter = new OrdenTecnica_Rview(Activity, ordenes);
            recyclerview.SetAdapter(adapter);
        }
    }
}