using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace appOrdenTecnica.Model
{
    public class Usuario
    {
        public string idUser { get; set; }
        public string nomUser { get; set; }
        public int cargoUser { get; set; }
        public string passwordUser { get; set; }

        public Usuario()
        {
        }

        public Usuario(string idUser, string nomUser, int cargoUser, string passwordUser)
        {
            this.idUser = idUser;
            this.nomUser = nomUser;
            this.cargoUser = cargoUser;
            this.passwordUser = passwordUser;
        }

        int perm=0;
        public Usuario Tempbd(string _user, string _pass) {

            List<Usuario> objUsu = new List<Usuario>();
            objUsu.Add(new Usuario() { idUser = "U01", nomUser = "Karina12", cargoUser = 1 , passwordUser = "123"}); //1 = secretario = supervisor operacional;
            objUsu.Add(new Usuario() { idUser = "U02", nomUser = "Lopez5", cargoUser = 2 , passwordUser = "123" }); //2 = supervisor tecnico;
            objUsu.Add(new Usuario() { idUser = "U03", nomUser = "Juan08", cargoUser = 3 , passwordUser = "123" }); //3 = tecnico;

            List<Usuario> newlist = objUsu.Where(x => x.nomUser.Equals(_user)).ToList(); //StartsWith, Contains
            Usuario obj = new Usuario();
            foreach (Usuario aPart in newlist)
            {
                obj.idUser = aPart.idUser;
                obj.cargoUser = aPart.cargoUser;
            }
            perm = newlist.Count;

            return obj;

        }

    }
}