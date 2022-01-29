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

namespace appOrdenTecnica.Model
{
    public class Usuariobd
    {

        public string ID_USUARIO { get; set; }
        public string COD_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public string CONTRASEÑA { get; set; }
        public string FOTO { get; set; }
        public string ESTADO { get; set; }
        public string FK_PERFIL { get; set; }
        public string FK_EMPLEADO { get; set; }

        public Usuariobd()
        {
        }

        public Usuariobd(string iD_USUARIO, string cOD_USUARIO, string uSUARIO, string cONTRASEÑA, string fOTO, string eSTADO, string fK_PERFIL, string fK_EMPLEADO)
        {
            ID_USUARIO = iD_USUARIO;
            COD_USUARIO = cOD_USUARIO;
            USUARIO = uSUARIO;
            CONTRASEÑA = cONTRASEÑA;
            FOTO = fOTO;
            ESTADO = eSTADO;
            FK_PERFIL = fK_PERFIL;
            FK_EMPLEADO = fK_EMPLEADO;
        }


    }


}