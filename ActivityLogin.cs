using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using appOrdenTecnica.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        private async void _login_Click(object sender, EventArgs e)
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

                Usuario1 log = new Usuario1();
                log.usuario = _user;
                log.password = _pass;

                HttpClient client = new HttpClient();
                Uri url = new Uri("http://micmaproyectos.com/usuario/login");

                var json = JsonConvert.SerializeObject(log);
                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, contentJson);

                //Usuariobd second = new Usuariobd();

                if (response.StatusCode == System.Net.HttpStatusCode.OK) // System.Net.HttpStatusCode.OK
                {
                        string content = await response.Content.ReadAsStringAsync();
                        var resultado = JsonConvert.DeserializeObject<Usuariobd>(content);

                        if (resultado.status == true && resultado.code == 1)
                        {

                            var intent = new Intent(this, typeof(MainActivity));

                            ISharedPreferences prefs = GetSharedPreferences("MisPreferencias", FileCreationMode.Private);
                            ISharedPreferencesEditor editor = prefs.Edit();
                            editor.PutString("iduser", resultado.objeto.ID_USUARIO);
                            editor.PutString("nomuserid", resultado.objeto.FK_EMPLEADO); //aqui quisiera ek nombre de usuario de una vez pero se tendra que llamar desde main
                            editor.PutInt("cargo", int.Parse(resultado.objeto.FK_PERFIL));
                            editor.Apply();

                            StartActivity(intent);
                            cleanText();

                        }
                        else if (resultado.status == true && resultado.code == 2) {

                            Toast.MakeText(this, "Credenciales Incorrectas", ToastLength.Short).Show();
                        }

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {

                    Toast.MakeText(this, "Credenciales Incorrectas", ToastLength.Short).Show();
                }
                /* if (response.StatusCode == System.Net.HttpStatusCode.OK) // System.Net.HttpStatusCode.OK
                 {

                     try
                     {
                         string content = await response.Content.ReadAsStringAsync();
                         var resultado = JsonConvert.DeserializeObject<Usuariobd>(content);


                        //second.ID_USUARIO = resultado.ID_USUARIO;
                         //second.FK_PERFIL = resultado.FK_PERFIL;

                         var intent = new Intent(this, typeof(MainActivity));

                         ISharedPreferences prefs = GetSharedPreferences("MisPreferencias", FileCreationMode.Private);
                         ISharedPreferencesEditor editor = prefs.Edit();
                         editor.PutString("iduser", resultado.ID_USUARIO);
                         editor.PutString("nomuserid", resultado.FK_EMPLEADO); //aqui quisiera ek nombre de usuario de una vez pero se tendra que llamar desde main
                         editor.PutInt("cargo", int.Parse(resultado.FK_PERFIL));
                         editor.Apply();

                         StartActivity(intent);
                         cleanText();
                     }
                     catch (WebException we)
                     {
                         Toast.MakeText(this, "Credenciales Incorrectas", ToastLength.Short).Show();

                     }



                 }
                 else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                 {

                     Toast.MakeText(this, "Credenciales Incorrectas", ToastLength.Short).Show();
                 }*/

                /*try
                {
                    response = (HttpWebResponse)request.GetResponse();
                    // This will have statii from 200 to 30x
                    statusNumber = (int)response.StatusCode;
                }
                catch (WebException we)
                {
                    // Statii 400 to 50x will be here
                    statusNumber = (int)we.Response.StatusCode;
                }
                */



                //Usuario operador = new Usuario();
                /*Dao operador = new Dao();
                //Usuario usuario = new Usuario();

                Task<Usuariobd> usuario = new Task<Usuariobd>();
                usuario = operador.UsuarioDaoAsync(_user, _pass);


                if (usuario.ID_USUARIO != null)
                {

                    var intent = new Intent(this, typeof(MainActivity));

                    ISharedPreferences prefs = GetSharedPreferences("MisPreferencias", FileCreationMode.Private);
                    ISharedPreferencesEditor editor = prefs.Edit();
                    editor.PutString("iduser", usuario.ID_USUARIO);
                    editor.PutInt("cargo", int.Parse(usuario.FK_PERFIL));
                    editor.Apply();

                    StartActivity(intent);
                    cleanText();

                }*/

                /*if (usuario.idUser != null) {

                    var intent = new Intent(this, typeof(MainActivity));

                    ISharedPreferences prefs = GetSharedPreferences("MisPreferencias", FileCreationMode.Private);
                    ISharedPreferencesEditor  editor = prefs.Edit();
                    editor.PutString("iduser", usuario.idUser);
                    editor.PutInt("cargo", usuario.cargoUser);
                    editor.Apply();

                    //intent.PutExtra("iduser", "" + usuario.idUser);
                    //intent.PutExtra("cargo", "" + usuario.cargoUser);
                    //intent.PutExtra("nombrecliente", "" + );
                    StartActivity(intent);
                    cleanText();

                }*/



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