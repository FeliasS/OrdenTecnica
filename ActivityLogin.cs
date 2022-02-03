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
        // Definimos los elementos del Login
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
           
            _login.Click += _login_Click;
        }

        private async void _login_Click(object sender, EventArgs e)
        {
            string _user = _userName.Text.ToString();
            string _pass = _password.Text.ToString();
            
            if (cajasVacias(_user, _pass).Equals(true))
            {
                Console.WriteLine("las cajas estan vacias");
                
                Toast.MakeText(this, "Campos vacios, Ingrese Datos!", ToastLength.Short).Show();

            }
            else
            {
                Console.WriteLine("campos llenos");

                Usuario1 log = new Usuario1();
                log.usuario = _user;
                log.password = _pass;

                // Instanciamos el servicio para consumir apis
                HttpClient client = new HttpClient();
                Uri url = new Uri("http://micmaproyectos.com/usuario/login");

                // Serializamos los datos enviamos
                var json = JsonConvert.SerializeObject(log);
                
                // Enviamos el contenido al servicio
                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                
                //Enviamos el metodo POST
                var response = await client.PostAsync(url, contentJson);

                if (response.StatusCode == System.Net.HttpStatusCode.OK) // System.Net.HttpStatusCode.OK
                {
                    //Hacemos lectura del contenido enviado al servicio
                    string content = await response.Content.ReadAsStringAsync();

                    //Deserializamos la trama regresada y lo pasamos a un objeto
                    var resultado = JsonConvert.DeserializeObject<Usuariobd>(content);

                    if (resultado.status == true && resultado.code == 1)
                    {

                            ISharedPreferences prefs = GetSharedPreferences("MisPreferencias", FileCreationMode.Private);
                            ISharedPreferencesEditor editor = prefs.Edit();
                            editor.PutString("iduser", resultado.objeto.ID_USUARIO);
                            editor.PutString("nomuserid", resultado.objeto.NOMBRES+' '+resultado.objeto.APELLIDOS); //aqui quisiera ek nombre de usuario de una vez pero se tendra que llamar desde main
                            editor.PutInt("cargo", int.Parse(resultado.objeto.FK_PERFIL));
                            editor.PutString("fotouser", resultado.objeto.FOTO);
                            editor.PutString("idEmpleado", resultado.objeto.FK_EMPLEADO);
                            editor.Apply();

                        var intent = new Intent(this, typeof(MainActivity));

                        StartActivity(intent);
                        cleanText();

                    }
                    else if (resultado.status == true && resultado.code == 2) 
                    {
                        Toast.MakeText(this, "Credenciales Incorrectas", ToastLength.Short).Show();
                    }

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {

                    Toast.MakeText(this, "Credenciales Incorrectas", ToastLength.Short).Show();
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
        }
    }
}