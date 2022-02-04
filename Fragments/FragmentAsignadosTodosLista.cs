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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace appOrdenTecnica.Fragments
{
    public class FragmentAsignadosTodosLista : AndroidX.Fragment.App.Fragment, ListaAsignadosTodosAdapter.OnItemListener, SearchView.IOnQueryTextListener
    {
        //Definiendo los elementos de la vista Layout_lista_ordenes
        private SearchView searchView; //*1
        private RecyclerView recyclerview;
        private LinearLayoutManager linearLayoutManager;

        ListaAsignadosTodosAdapter adapter;
        List<ListaOrdenTecnica> ordenes;

        LinearLayout hiddenView;
        CardView cardView;
        TextView codigo = null;
        String codigoValue = "";

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Activity.Title = "PENDIENTES";
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

        public void onItemClick(int position, View view)//*3
        {
            

        }

        /*private void GenerarItem()
        {
            ordenes = new List<OrdenTecnica>();
            for (int i = 0; i < 50; i++)
            {
                ordenes.Add(new OrdenTecnica("C000" + i,
                    "15/10/2021", "08:00 AM",
                    "POR ASIGNAR"));
            }
            adapter = new ListaAsignadosTodosAdapter(Activity, ordenes, this);
            recyclerview.SetAdapter(adapter);
        }*/

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

        private async void LoadList() {

            BuscarEstadoOrdenTecnica log = new BuscarEstadoOrdenTecnica();
            log.estado = 2;

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

            /*HttpClient client = new HttpClient();
            Uri url = new Uri("http://micmaproyectos.com/orden/listarTodos");

            //var json = JsonConvert.SerializeObject(log);
            //var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.OK) // System.Net.HttpStatusCode.OK
            {               
                string content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<OrdenTecnica>(content);

                adapter = new ListaAsignadosTodosAdapter(Activity, resultado.lista, this);
                recyclerview.SetAdapter(adapter);

            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                Toast.MakeText(Activity, "No existen registros", ToastLength.Short).Show();
            }*/

        }
    }
}