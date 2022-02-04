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
        public string usuario { get; set; }
        public string password { get; set; }

        public Usuario()
        {
        }
    }
    public class Usuariobd
    {
        public bool status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public ObjetoUsuario objeto { get; set; }

        public Usuariobd(bool status, int code, string message, ObjetoUsuario objeto)
        {
            this.status = status;
            this.code = code;
            this.message = message;
            this.objeto = objeto;
        }

        public Usuariobd()
        {
        }
    }
    public class ObjetoUsuario
    {

        public string ID_USUARIO { get; set; }
        public string COD_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public string CONTRASEÑA { get; set; }
        public string FOTO { get; set; }
        public string ESTADO { get; set; }
        public string FK_PERFIL { get; set; }
        public string FK_EMPLEADO { get; set; }
        public string NOMBRES { get; set; }
        public string APELLIDOS { get; set; }

        public ObjetoUsuario()
        {
        }

        public ObjetoUsuario(string iD_USUARIO, string cOD_USUARIO, string uSUARIO, string cONTRASEÑA, string fOTO, string eSTADO, string fK_PERFIL, string fK_EMPLEADO, string nOMBRES, string aPELLIDOS)
        {
            ID_USUARIO = iD_USUARIO;
            COD_USUARIO = cOD_USUARIO;
            USUARIO = uSUARIO;
            CONTRASEÑA = cONTRASEÑA;
            FOTO = fOTO;
            ESTADO = eSTADO;
            FK_PERFIL = fK_PERFIL;
            FK_EMPLEADO = fK_EMPLEADO;
            NOMBRES = nOMBRES;
            APELLIDOS = aPELLIDOS;
        }
    }
}