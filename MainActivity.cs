using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.View;
using AndroidX.DrawerLayout.Widget;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Google.Android.Material.Snackbar;
using appOrdenTecnica.Fragments; // Traemos a los Fragmentos


namespace appOrdenTecnica
{
    [Activity(Label = "@string/labelMain", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false , WindowSoftInputMode = SoftInput.StateHidden)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        //Fragments_nueva_orden nueva_orden;
        Fragments_lista_ordenes frgListOrdenes;
        FragmentCrearOrden FragmentCrearOrden; 
        FragmentPorAsignarLista FragmentPorAsignarLista;
        FragmentAsignadosTodosLista FragmentAsignadosTodosLista;//todos los pedidos asignados FragmentPendientesLista
        FragmentAsignadosxTecLista FragmentAsignadosxTecLista; //solo los pedidos asignados a ese tecnico FragmentAsignadosLista
        FragmentCulminadosLista FragmentCulminadosLista;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();


            //
            ISharedPreferences pref = GetSharedPreferences("MisPreferencias", FileCreationMode.Private);
            pref.GetString(("iduser"), null);
            int cargo = pref.GetInt(("cargo"), 0);
            Console.WriteLine("cargo VERRRRRRR" + cargo);
            //
            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
            switch (cargo)
            {
                case 1:
                    navigationView.Menu.FindItem(Resource.Id.nuev_orden).SetVisible(true);
                    navigationView.Menu.FindItem(Resource.Id.reg_ord).SetVisible(true);
                    navigationView.Menu.FindItem(Resource.Id.asig_tecn).SetVisible(true);
                    navigationView.Menu.FindItem(Resource.Id.ord_pend).SetVisible(true);
                    // Instanciamos el objeto
                    frgListOrdenes = new Fragments_lista_ordenes();
                    // Asignamos variable a Fragment Manager e iniciamos la transaccion
                    var transaccion = SupportFragmentManager.BeginTransaction();
                    // Le asignamos el contenedor de los fragmentos y el fragmento que cargara
                    transaccion.Add(Resource.Id.ConteinerLayout, frgListOrdenes);
                    // Confirmamos la transaccion
                    transaccion.Commit();
                    break;
                case 2:
                    navigationView.Menu.FindItem(Resource.Id.asig_tecn).SetVisible(true);
                    navigationView.Menu.FindItem(Resource.Id.ord_pend).SetVisible(true);

                    //primer fragment
                    FragmentPorAsignarLista = new FragmentPorAsignarLista();
                    var transaccion2 = SupportFragmentManager.BeginTransaction();
                    transaccion2.Replace(Resource.Id.ConteinerLayout, FragmentPorAsignarLista);
                    transaccion2.Commit();
                    break;
                case 3:
                    navigationView.Menu.FindItem(Resource.Id.item_asignados).SetVisible(true);
                    navigationView.Menu.FindItem(Resource.Id.item_culminados).SetVisible(true);
                    //primer fragment
                    FragmentAsignadosxTecLista = new FragmentAsignadosxTecLista();
                    var transaccion3 = SupportFragmentManager.BeginTransaction();
                    transaccion3.Replace(Resource.Id.ConteinerLayout, FragmentAsignadosxTecLista);
                    transaccion3.Commit();
                    break;

            }
           

            

            //SupportFragmentManager.BeginTransaction().Add(Resource.Id.ConteinerLayout, frgListOrdenes).Commit();


        }
        int counter = 0;
        public override void OnBackPressed()
        {
            int count = SupportFragmentManager.BackStackEntryCount;
            
            
            if (count == 0)
            {
                counter++;
                
                if (counter == 1)
                {
                    Android.Widget.Toast.MakeText(this, "Tap una vez más para salir", Android.Widget.ToastLength.Short).Show();
                }
                else if (counter == 2) {

                    base.OnBackPressed();
                }
                
                /*DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                if (drawer.IsDrawerOpen(GravityCompat.Start))
                {
                    drawer.CloseDrawer(GravityCompat.Start);
                }
                else
                {
                    base.OnBackPressed();
                }*/
                //additional code
            }
            else
            {
                SupportFragmentManager.PopBackStack();
                counter = 0;
            }

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;


            ///pasar cargo por el fragmento , este recibe y clasifica la activacion o desactivacion de botones 
            ///
            if (id == Resource.Id.nuev_orden)//Item FragmentOrden nuev_orden
            {

                FragmentCrearOrden = new FragmentCrearOrden();
                var transaccion = SupportFragmentManager.BeginTransaction();
                transaccion.Replace(Resource.Id.ConteinerLayout, FragmentCrearOrden);
                transaccion.Commit();
            }
            else if (id == Resource.Id.reg_ord)
            {
                // Instanciamos el objeto
                frgListOrdenes = new Fragments_lista_ordenes();
                // Asignamos variable a Fragment Manager e iniciamos la transaccion
                var transaccion = SupportFragmentManager.BeginTransaction();
                // Le asignamos el contenedor de los fragmentos y el fragmento que cargara
                transaccion.Replace(Resource.Id.ConteinerLayout, frgListOrdenes);
                // Confirmamos la transaccion
                transaccion.Commit();
            }
            else if (id == Resource.Id.asig_tecn)
            {

                FragmentPorAsignarLista = new FragmentPorAsignarLista();
                var transaccion = SupportFragmentManager.BeginTransaction();
                transaccion.Replace(Resource.Id.ConteinerLayout, FragmentPorAsignarLista);
                transaccion.Commit();
            }
            else if (id == Resource.Id.ord_pend)
            {

                FragmentAsignadosTodosLista = new FragmentAsignadosTodosLista();
                var transaccion = SupportFragmentManager.BeginTransaction();
                transaccion.Replace(Resource.Id.ConteinerLayout, FragmentAsignadosTodosLista);
                transaccion.Commit();
            }
            else if (id == Resource.Id.item_asignados)
            {

                FragmentAsignadosxTecLista = new FragmentAsignadosxTecLista();
                var transaccion = SupportFragmentManager.BeginTransaction();
                transaccion.Replace(Resource.Id.ConteinerLayout, FragmentAsignadosxTecLista);
                transaccion.Commit();
            }
            else if (id == Resource.Id.item_culminados)
            {

                FragmentCulminadosLista = new FragmentCulminadosLista();
                var transaccion = SupportFragmentManager.BeginTransaction();
                transaccion.Replace(Resource.Id.ConteinerLayout, FragmentCulminadosLista);
                transaccion.Commit();
            }
            else if (id == Resource.Id.cerr_ses)
            {
                //this.MoveTaskToBack(true);
                this.FinishAffinity();
            }
           

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

