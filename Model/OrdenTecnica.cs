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

    public class BuscarEmpOrdenTecnica
    {
        public string idEmpleado { get; set; }

        public BuscarEmpOrdenTecnica()
        {
        }

        public BuscarEmpOrdenTecnica(string idEmpleado)
        {
            this.idEmpleado = idEmpleado;
        }
    }

    public class OrdenTecnica
    {

        public bool status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public List<ListaOrdenTecnica> lista { get; set; }

        
        public OrdenTecnica()
        {
        }

        public OrdenTecnica(bool status, int code, string message, List<ListaOrdenTecnica> lista)
        {
            this.status = status;
            this.code = code;
            this.message = message;
            this.lista = lista;
        }




        /*
        public string Codigo { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Accion { get; set; }

        //estado

        public OrdenTecnica(string codigo, string fecha, string hora, string accion)
        {
            this.Codigo = codigo;
            this.Fecha = fecha;
            this.Hora = hora;
            this.Accion = accion;
        }*/
    }
    public class ListaOrdenTecnica
    {
        public string ID_ORDEN { get; set; }
        public string COD_ORDEN { get; set; }
        public string ASUNTO { get; set; }
        public string FECHA_ORDEN { get; set; }
        public string HORA_ORDEN { get; set; }
        public string REMITENTE { get; set; }
        public string ESTADO { get; set; }
        public string FK_SUCURSAL { get; set; }
        public string FK_EMPLEADO { get; set; }

        public ListaOrdenTecnica()
        {
        }

        public ListaOrdenTecnica(string iD_ORDEN, string cOD_ORDEN, string aSUNTO, string fECHA_ORDEN, string hORA_ORDEN, string rEMITENTE, string eSTADO, string fK_SUCURSAL, string fK_EMPLEADO)
        {
            ID_ORDEN = iD_ORDEN;
            COD_ORDEN = cOD_ORDEN;
            ASUNTO = aSUNTO;
            FECHA_ORDEN = fECHA_ORDEN;
            HORA_ORDEN = hORA_ORDEN;
            REMITENTE = rEMITENTE;
            ESTADO = eSTADO;
            FK_SUCURSAL = fK_SUCURSAL;
            FK_EMPLEADO = fK_EMPLEADO;
        }
    }

}