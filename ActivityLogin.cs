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

namespace appOrdenTecnica
{
    [Activity(Label = "@string/labelLogin", MainLauncher =true)]
    public class ActivityLogin : Activity
    {
        // obtenemos las id de los botones
        EditText _userName, _password;
        Button _login;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Asignamos el layout activity_login
            SetContentView(Resource.Layout.activity_login);
            // Obtenemos los id de los botones
            _userName = FindViewById<EditText>(Resource.Id.txtUser);
            _password = FindViewById<EditText>(Resource.Id.txtPassword);
            _login = FindViewById<Button>(Resource.Id.btnLogin);
            // Create your application here

            _login.Click += _login_Click;
        }

        private void _login_Click(object sender, EventArgs e)
        {
            string _user = _userName.Text.ToString();
            string _pass = _password.Text.ToString();
            // Instanciamos el componente Mensaje de alerta
            //AlertDialog.Builder alert = new AlertDialog.Builder(this);

            if (cajasVacias(_user, _pass).Equals(true))
            {
                Console.WriteLine("las cajas estan vacias");
                //alert.SetTitle("Mensaje de validacion");
                //alert.SetMessage("Camplos Vacios, Ingrese Datos!");
                //alert.SetPositiveButton("OK", (sender, args) =>
                //     {
                //     });
                //Dialog dialog = alert.Create();
                //dialog.Show();
                Toast.MakeText(this, "Campos vacios, Ingrese Datos!", ToastLength.Short).Show();

            }
            else
            {
                Console.WriteLine("campos llenos");

                Usuario operador = new Usuario();
                Usuario usuario = new Usuario();
                usuario = operador.Tempbd(_user, _pass);

             
                if (usuario.idUser != null) {

                    var intent = new Intent(this, typeof(MainActivity));

                    ISharedPreferences prefs = GetSharedPreferences("MisPreferencias", FileCreationMode.Private);
                    ISharedPreferencesEditor  editor = prefs.Edit();
                    editor.PutString("iduser", usuario.idUser);
                    editor.PutInt("cargo", usuario.cargoUser);
                    editor.Apply();

                    /*intent.PutExtra("iduser", "" + usuario.idUser);
                    intent.PutExtra("cargo", "" + usuario.cargoUser);*/
                    //intent.PutExtra("nombrecliente", "" + );
                    StartActivity(intent);
                    cleanText();


                }

                
            
    }
        }

        // Validamos campos vacios
        bool cajasVacias(string username, string password)
        {
            if (username.Equals("") || password.Equals(""))
            {
                return true;
            }
            return false;
        }

        // Limpiamos los campos
        void cleanText()
        {
            _userName.Text = "";
            _password.Text = "";
            //_userName.Focusable = true;

        }
    }
}