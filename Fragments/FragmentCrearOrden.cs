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

using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using appOrdenTecnica.Adapter;
using AndroidX.RecyclerView.Widget;
using appOrdenTecnica.Model;

namespace appOrdenTecnica.Fragments
{
    public class FragmentCrearOrden : AndroidX.Fragment.App.Fragment , ListaTecnicosAdapter.OnItemListener, SearchView.IOnQueryTextListener
    {
        // Llamando a los controles del fragmento
        EditText hora, fecha, cliente, sucursal, dispositivo, problema;
        Button agregar, generarOrden, btnAsignarTecnico;
        TextView txtTecnicoAsignado;
        // Llamando la clase de alert
        AlertDialog.Builder alert;

        //box
        private SearchView searchView; 
        private RecyclerView recyclerview;
        ListaTecnicosAdapter adapter;
        TextView txtidTecnico, txtnomTecnico;
        Dialog dialog;
        //
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Activity.Title="NUEVO REPORTE";
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.layout_nueva_orden, container, false);

            //Botones a eliminar
            //Button btnAsignarTecnico = view.FindViewById<Button>(Resource.Id.btnAsignarTecnico);
            //btnAsignarTecnico.Visibility = Android.Views.ViewStates.Gone;
            FrameLayout layoutv_nuevaOrden2 = view.FindViewById<FrameLayout>(Resource.Id.layoutv_nuevaOrden2);
            layoutv_nuevaOrden2.Visibility = Android.Views.ViewStates.Gone;

             // Obteniendo lo ID de los controles
             hora = view.FindViewById<EditText>(Resource.Id.txtHora);
            fecha = view.FindViewById<EditText>(Resource.Id.txtFecha);
            cliente = view.FindViewById<EditText>(Resource.Id.txtCliente);
            sucursal = view.FindViewById<EditText>(Resource.Id.txtSucursal);
            dispositivo = view.FindViewById<EditText>(Resource.Id.txtModeloDisp);
            problema = view.FindViewById<EditText>(Resource.Id.txtProblema);

            txtTecnicoAsignado = view.FindViewById<TextView>(Resource.Id.txtTecnicoAsignado);

            agregar = view.FindViewById<Button>(Resource.Id.btnAgregarLista);
            generarOrden = view.FindViewById<Button>(Resource.Id.btnGenerarOrden);
            btnAsignarTecnico = view.FindViewById<Button>(Resource.Id.btnAsignarTecnico);

            agregar.Click += Agregar_Click;
            generarOrden.Click += GenerarOrden_Click;

           
            //TECNICOS
            btnAsignarTecnico.Click += (sender, e) =>
            {
                Android.App.AlertDialog.Builder dg = new Android.App.AlertDialog.Builder(Activity);
                dg.SetTitle("DESCRIBA SU PROBLEMA");
                dg.SetMessage("Rellene los campos para reportar su problema (1 item por problema)"); ;
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
            //

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
            alert.SetMessage("Orden generado correctamente");
            alert.SetPositiveButton("OK", (sender, args) =>
            {
                // Limpiamos los campos de texto despues de aceptar el mensaje
                alert.Dispose();
                limpiarText();

            });
            Dialog dialog = alert.Create();
            dialog.Show();
            // Limpiamos las cajas de texto

        }

        private void Agregar_Click(object sender, EventArgs e)
        {
            string h, f, c, s, d, p;
            h = hora.Text.ToString();
            f = fecha.Text.ToString();
            c = cliente.Text.ToString();
            s = sucursal.Text.ToString();
            d = dispositivo.Text.ToString();
            p = problema.Text.ToString();

            if (textVacios(h, f, c, s, d, p).Equals(true))
            {
                Toast.MakeText(Activity, "Campos vacios!, ingrese un valor", ToastLength.Short).Show();
            }
            else
            {

                Console.WriteLine("enviando datos al servicio");

                // Mostrar mensaje de que el problema ha sido agregado correctamente
                alert = new AlertDialog.Builder(Activity);
                alert.SetTitle("Mensaje de confirmacion");
                alert.SetMessage("Problema agregado correctamente");
                alert.SetPositiveButton("OK", (sender, args) =>
                {
                });
                Dialog dialog = alert.Create();
                dialog.Show();
                // Limpiamos las cajas de texto
                limpiarText();
            }
        }

        bool textVacios(string h, string f, string c, string s, string d, string p)
        {
            // Validacion de campos vacios
            if (h.Equals("") || f.Equals("")
                || c.Equals("") || s.Equals("")
                || d.Equals("") || p.Equals(""))
            {
                return true;
            }
            return false;
        }

        void limpiarText()
        {
            fecha.Text = "";
            hora.Text = "";
            cliente.Text = "";
            sucursal.Text = "";
            dispositivo.Text = "";
            problema.Text = "";
        }

      

        //box
        private void GenerarItem()
        {
            List<Tecnicos> lista = new List<Tecnicos>();
            for (int i = 0; i < 5; i++)
            {
                lista.Add(new Tecnicos("T000" + i,
                    "Tecnico"+i, 1));
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
        //
    }
}