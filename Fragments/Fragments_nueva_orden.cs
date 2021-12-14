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

namespace appOrdenTecnica.Fragments
{
    public class Fragments_nueva_orden : AndroidX.Fragment.App.Fragment
    {

        // Llamando a los controles del fragmento
        EditText hora, fecha, cliente, sucursal, dispositivo, problema;
        Button agregar, generarOrden;

        // Llamando la clase de alert
        AlertDialog.Builder alert;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.layout_nueva_orden, container, false);

            // Obteniendo lo ID de los controles
            hora = view.FindViewById<EditText>(Resource.Id.txtHora);
            fecha = view.FindViewById<EditText>(Resource.Id.txtFecha);
            cliente = view.FindViewById<EditText>(Resource.Id.txtCliente);
            sucursal = view.FindViewById<EditText>(Resource.Id.txtSucursal);
            dispositivo = view.FindViewById<EditText>(Resource.Id.txtModeloDisp);
            problema = view.FindViewById<EditText>(Resource.Id.txtProblema);

            agregar = view.FindViewById<Button>(Resource.Id.btnAgregarLista);
            generarOrden = view.FindViewById<Button>(Resource.Id.btnGenerarOrden);

            agregar.Click += Agregar_Click;
            generarOrden.Click += GenerarOrden_Click;

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

            if (textVacios(h,f,c,s,d,p).Equals(true))
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

        bool textVacios(string h, string f, string c, string s, string d, string p )
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

        void limpiarText ()
        {
            fecha.Text = "";
            hora.Text = "";
            cliente.Text = "";
            sucursal.Text = "";
            dispositivo.Text = "";
            problema.Text = "";
        }
    }
}