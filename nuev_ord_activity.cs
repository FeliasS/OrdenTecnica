using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace appOrdenTecnica
{
    [Activity(Label = "nuev_ord_activity")]
    public class nuev_ord_activity : Activity
    {

        bool cajasLimpias()
        {
            var _fecha = FindViewById<EditText>(Resource.Id.txtFecha);
            var _time = FindViewById<EditText>(Resource.Id.txtTime);
            var _cliente = FindViewById<EditText>(Resource.Id.txtCliente);
            var _sucursal = FindViewById<EditText>(Resource.Id.txtSucursal);
            var _nombDispo = FindViewById<EditText>(Resource.Id.txtnDispos);
            var _problema = FindViewById<EditText>(Resource.Id.txtProblema);
                                                                           
            if (_fecha.Length()>0 || _time.Length()>0 || _cliente.Length()>0 || _sucursal.Length()>0 ||
                _nombDispo.Length()>0 || _problema.Length()>0)
            {
                return false;
            }
            else 
            {
                return true;
            }
            
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Designando layout nuev_orden_activity.xml
            SetContentView(Resource.Layout.nuev_orden_tecn);
            // Create your application here

            Button _registrar = FindViewById<Button>(Resource.Id.btnRegReport);

            _registrar.Click += _registrar_Click;
            


        }

        private void _registrar_Click(object sender, EventArgs e)
        {
            var _list = new Intent(this, typeof(list_ord_tecn_activity));
            StartActivity(_list);


        }
    }
}