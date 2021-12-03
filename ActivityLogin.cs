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
    [Activity(Label = "ActivityLogin", MainLauncher =true)]
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
            AlertDialog.Builder alert = new AlertDialog.Builder(this);

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
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
                cleanText();
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