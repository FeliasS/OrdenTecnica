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
    [Activity(Label = "list_ord_tecn_activity")]
    public class list_ord_tecn_activity : AppCompatActivity
    {
        Button nuevOrden;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_list_ord_tecn);
            // Asignando id de componentes
            nuevOrden = FindViewById<Button>(Resource.Id.btnNuevaOrden);
            // Create your application here
            nuevOrden.Click += NuevOrden_Click;
        }

        private void NuevOrden_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(nuev_ord_activity));
            StartActivity(intent);
        }
    }
}