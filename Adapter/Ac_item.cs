using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using appOrdenTecnica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace appOrdenTecnica.Adapter
{
    class Ac_item : ArrayAdapter<Cliente>
    {
        //Definimos una lista de Clientes
        public List<Cliente> lstItemAc;

        public Ac_item(Context context, List<Cliente> listItem) : base(context, 0, listItem)
        {
            //Instanciamos una nueva lista de clientes
            lstItemAc = new List<Cliente>(listItem);
        }
    }
}