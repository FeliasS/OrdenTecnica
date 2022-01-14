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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace appOrdenTecnica.Fragments
{
    public class FragmentPorAsignarOrden : AndroidX.Fragment.App.Fragment, ListaTecnicosAdapter.OnItemListener, SearchView.IOnQueryTextListener
    {
        // Llamando la clase de alert
        AlertDialog.Builder alert;
        //box
        private SearchView searchView;
        private RecyclerView recyclerview;
        ListaTecnicosAdapter adapter;
        TextView txtidTecnico, txtnomTecnico;
        Dialog dialog;
        Button btnAsignarTecnico;
        TextView txtTecnicoAsignado;
        //
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Activity.Title = "REPORTE";
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.layout_nueva_orden, container, false);

            TextView codigo = view.FindViewById<TextView>(Resource.Id.lblCodigo);
            codigo.Visibility = Android.Views.ViewStates.Visible;


            Bundle data = Arguments;
            String codValue = data.GetString("codigo");
            codigo.Text = codValue;
            //vistas modelando

            FrameLayout layoutv_nuevaOrden2 = view.FindViewById<FrameLayout>(Resource.Id.layoutv_nuevaOrden2);
            layoutv_nuevaOrden2.Visibility = Android.Views.ViewStates.Gone;
            Button btnAgregarLista = view.FindViewById<Button>(Resource.Id.btnAgregarLista);
            btnAgregarLista.Visibility = Android.Views.ViewStates.Gone;
            Button btnGenerarOrden = view.FindViewById<Button>(Resource.Id.btnGenerarOrden);
            btnGenerarOrden.Text = "GUARDAR CAMBIOS";
            LinearLayout part1 = view.FindViewById<LinearLayout>(Resource.Id.part1);
            part1.Background = Context.GetDrawable(Resource.Drawable.custom_input_notedit);

            EditText txtFecha = view.FindViewById<EditText>(Resource.Id.txtFecha);
            txtFecha.Clickable = false; txtFecha.Focusable = false; txtFecha.SetOnKeyListener(null);
            txtFecha.Background = Context.GetDrawable(Resource.Drawable.custom_input_notedit);

            EditText txtHora = view.FindViewById<EditText>(Resource.Id.txtHora);
            txtHora.Clickable = false; txtHora.Focusable = false; txtHora.SetOnKeyListener(null);
            txtHora.Background = Context.GetDrawable(Resource.Drawable.custom_input_notedit);

            EditText txtCliente = view.FindViewById<EditText>(Resource.Id.txtCliente);
            txtCliente.Clickable = false; txtCliente.Focusable = false; txtCliente.SetOnKeyListener(null);
            txtCliente.Background = Context.GetDrawable(Resource.Drawable.custom_input_notedit);

            EditText txtSucursal = view.FindViewById<EditText>(Resource.Id.txtSucursal);
            txtSucursal.Clickable = false; txtSucursal.Focusable = false; txtSucursal.SetOnKeyListener(null);
            txtSucursal.Background = Context.GetDrawable(Resource.Drawable.custom_input_notedit);

            EditText txtModeloDisp = view.FindViewById<EditText>(Resource.Id.txtModeloDisp);
            txtModeloDisp.Clickable = false; txtModeloDisp.Focusable = false; txtModeloDisp.SetOnKeyListener(null);
            txtModeloDisp.Background = Context.GetDrawable(Resource.Drawable.custom_input_notedit);

            EditText txtProblema = view.FindViewById<EditText>(Resource.Id.txtProblema);
            txtProblema.Clickable = false; txtProblema.Focusable = false; txtProblema.SetOnKeyListener(null);
            txtProblema.Background = Context.GetDrawable(Resource.Drawable.custom_input_notedit);
            //

            btnAsignarTecnico = view.FindViewById<Button>(Resource.Id.btnAsignarTecnico);
            txtTecnicoAsignado = view.FindViewById<TextView>(Resource.Id.txtTecnicoAsignado);
            //TECNICOS
            btnAsignarTecnico.Click += (sender, e) =>
            {
                Android.App.AlertDialog.Builder dg = new Android.App.AlertDialog.Builder(Activity);
                dg.SetTitle("SELECCIONE UN TECNICO");
                dg.SetMessage("Puede Filtrar a los tecnicos por su nombre"); ;
                view = inflater.Inflate(Resource.Layout.layout_lista_tecnicos, null, true);
                searchView = view.FindViewById<SearchView>(Resource.Id.SearchTecnico);//*2
                SearchListener();//*4
                recyclerview = view.FindViewById<RecyclerView>(Resource.Id.recyViewListaTecnicos);
                recyclerview.HasFixedSize = true;
                LinearLayoutManager linearLayoutManager = new LinearLayoutManager(Activity);
                GenerarItem();
                recyclerview.SetLayoutManager(linearLayoutManager);

                dg.SetView(view);
                dialog = dg.Create();
                dialog.Show();
            };
            return view;
        }
       

        private void GenerarOrden_Click(object sender, EventArgs e)
        {
            /* Aqui enviamos los datos a la interface para que valide y
                 nos envie al fragmento de la lista de ordenes sin asignar 
                 cliente */

            // Mostrar mensaje de que el problema ha sido agregado correctamente
            alert = new AlertDialog.Builder(Activity);
            alert.SetTitle("Mensaje de confirmacion");
            alert.SetMessage("Orden confirmado correctamente");
            alert.SetPositiveButton("OK", (sender, args) =>
            {
                // Limpiamos los campos de texto despues de aceptar el mensaje
                alert.Dispose();

            });
            Dialog dialog = alert.Create();
            dialog.Show();
            // Limpiamos las cajas de texto

        }
        //box
        private void GenerarItem()
        {
            List<Tecnicos> lista = new List<Tecnicos>();
            for (int i = 0; i < 5; i++)
            {
                lista.Add(new Tecnicos("T000" + i,
                    "Tecnico" + i, 1));
            }
            adapter = new ListaTecnicosAdapter(Activity, lista, this);
            recyclerview.SetAdapter(adapter);
        }

        private void SearchListener()
        {

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

        public void onItemClick(int position, View v)
        {

            txtidTecnico = v.FindViewById<TextView>(Resource.Id.txtidTecnico);
            txtnomTecnico = v.FindViewById<TextView>(Resource.Id.txtnomTecnico);


            string idTecnico = txtidTecnico.Text.ToString().Trim();
            string nomTecnico = txtnomTecnico.Text.ToString().Trim();

            Toast.MakeText(Activity, "Tecnico Seleccionado", ToastLength.Short).Show();
            dialog.Dismiss();
            txtTecnicoAsignado.Text = idTecnico + "  " + nomTecnico;

        }
    }
}