using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace appOrdenTecnica
{
    [Activity(Label = "nuev_ord_activity", Theme = "@style/AppTheme")]
    public class nuev_ord_activity :AppCompatActivity
    {
        EditText fecha, hora, cliente, sucursal, dispositivo, problema;
        Button agregarList, generarOrden;

        Android.App.AlertDialog.Builder alert;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Designando layout nuev_orden_activity.xml
            SetContentView(Resource.Layout.layout_nueva_orden);
           
            // obteniendo los ID de los controles
            fecha = FindViewById<EditText>(Resource.Id.txtFecha);
            hora = FindViewById<EditText>(Resource.Id.txtHora);
            cliente = FindViewById<EditText>(Resource.Id.txtCliente);
            sucursal = FindViewById<EditText>(Resource.Id.txtSucursal);
            dispositivo = FindViewById<EditText>(Resource.Id.txtModeloDisp);
            problema = FindViewById<EditText>(Resource.Id.txtProblema);
            agregarList = FindViewById<Button>(Resource.Id.btnAgregarLista);
            generarOrden = FindViewById<Button>(Resource.Id.btnGenerarOrden);

            // Create your application here
            agregarList.Click += AgregarList_Click;
            generarOrden.Click += GenerarOrden_Click;



        }

        private void GenerarOrden_Click(object sender, EventArgs e)
        {
            // Validamos que por lo menos exista un problema en la orden
            alert = new Android.App.AlertDialog.Builder(this);
            alert.SetTitle("Mensaje de Confirmacion");
            alert.SetMessage("Orden Generada");
            alert.SetPositiveButton("OK", (sender, args) => {

                alert.Dispose();
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            });
            //Dialog dialog = alert.Create();
            alert.Show();
        }

        private void AgregarList_Click(object sender, EventArgs e)
        {
            string f = fecha.Text.ToString();
            string h = hora.Text.ToString();
            string c = cliente.Text.ToString();
            string s = sucursal.Text.ToString();
            string d = dispositivo.Text.ToString();
            string p = problema.Text.ToString();

            alert = new Android.App.AlertDialog.Builder(this);
            // Validar campos 
            if (cajasVacias(f, h, c, s, d, p).Equals(true))
            {
                Toast.MakeText(this, "Campos Vacios, Ingrese Datos!", ToastLength.Short).Show();
            }
            else
            {
                // realizando el mensaje para confirmar la que el problema se agregara al recyclerview
                alert.SetTitle("Mensaje de Confirmacion");
                alert.SetMessage("Agregar a Lista");
                alert.SetPositiveButton("OK", (sender, args) =>
                {
                });
                Dialog dialog = alert.Create();
                dialog.Show();
                // limpiamos los campos
                cleanText();
            }

            bool cajasVacias(string fe, string ho, string cli, string suc, string dis, string pro)
            {
                //string  _fecha, _hora, _cliente, _sucursal, _dispositivo, _problema;
                if (fe.Equals("") || ho.Equals("") || cli.Equals("") || suc.Equals("") || dis.Equals("") || pro.Equals(""))
                {
                    return true;
                }
                return false;
            }

            void cleanText()
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
}