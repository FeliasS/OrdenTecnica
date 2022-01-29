using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace appOrdenTecnica.Model
{
    public class Dao
    {
        public async Task<Usuariobd> UsuarioDaoAsync(string _user, string _pass) {

            /*var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://jsonplaceholder.typicode.com/posts");
            request.Method = HttpMethod.Get;*/


            Usuariobd log = new Usuariobd();
            log.ID_USUARIO = _user;
            log.CONTRASEÑA = _pass;

            HttpClient client = new HttpClient();
            Uri url = new Uri("");

            var json = JsonConvert.SerializeObject(log);
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, contentJson);

            Usuariobd second = new Usuariobd();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Usuariobd>(content);

                
                second.ID_USUARIO = resultado.ID_USUARIO;
                second.FK_PERFIL = resultado.FK_PERFIL;


            }
            return second; 
        }
    }
}