using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using appOrdenTecnica.Adapter;
using appOrdenTecnica.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace appOrdenTecnica.Fragments
{
    public class FragmentCerradasLista : AndroidX.Fragment.App.Fragment, ListaAsignadosTodosAdapter.OnItemListener, SearchView.IOnQueryTextListener
    {
        //Definimos los elementos de la vista
        private SearchView searchView; //*1
        private RecyclerView recyclerview;
        private LinearLayoutManager linearLayoutManager;

        ListaAsignadosTodosAdapter adapter;
        List<ListaOrdenTecnica> ordenes;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Activity.Title = "CERRADAS";
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.layout_lista_ordenes, container, false);
            searchView = view.FindViewById<SearchView>(Resource.Id.SearchOrden);//*2
            SearchListener();//*4
            recyclerview = view.FindViewById<RecyclerView>(Resource.Id.recyViewListaOrdenes);
            recyclerview.HasFixedSize = true;
            linearLayoutManager = new LinearLayoutManager(Activity);
            LoadList();
            recyclerview.SetLayoutManager(linearLayoutManager);
            return view;
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

        private async void LoadList()
        {
            BuscarEstadoOrdenTecnica log = new BuscarEstadoOrdenTecnica();
            log.estado = 5;

            HttpClient client = new HttpClient();
            Uri url = new Uri("http://micmaproyectos.com/orden/buscarOrdenByEstado");

            var json = JsonConvert.SerializeObject(log);
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, contentJson);
            //var response = await client.GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.OK) // System.Net.HttpStatusCode.OK
            {
                string content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<OrdenTecnica>(content);

                ordenes = resultado.lista;
                adapter = new ListaAsignadosTodosAdapter(Activity, resultado.lista, this);
                recyclerview.SetAdapter(adapter);

            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                Toast.MakeText(Activity, "No existen registros", ToastLength.Short).Show();
            }

        }

        public void onItemClick(int position, View v)
        {
            
        }
    }
}