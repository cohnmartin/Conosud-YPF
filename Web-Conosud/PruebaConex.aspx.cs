using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Telerik.Web.UI;

public partial class PruebaConex : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnConectar_Click(object sender, EventArgs e)
    {

        EntidadesConosud Contexto = new EntidadesConosud();

        var _ContratoAnt = (from v in Contexto.Contrato
                            where v.Codigo == lblCadena.Text
                            select v).FirstOrDefault();

        if (_ContratoAnt != null)
        {
            var ve = (from v in Contexto.VahiculosyEquipos
                      where v.objContrato.IdContrato == _ContratoAnt.IdContrato
                      select v).ToList();


            foreach (var item in ve)
            {
                SortedList fechas = new SortedList();
                if (item.FechaUltimoPagoSeguro != null) { fechas.Add(item.FechaUltimoPagoSeguro, "fechaSeguro"); }
                fechas.Add(_ContratoAnt.Prorroga != null ? _ContratoAnt.Prorroga : _ContratoAnt.FechaVencimiento, "FecahContrato");
                if (item.FechaVencimientoHabilitacion != null) { fechas.Add(item.FechaVencimientoHabilitacion.Value.AddSeFecahContratoconds(3), "fechaCENT"); };

                DateTime menor = (DateTime)fechas.GetKeyList()[0];

                item.VencimientoCredencial = (menor - DateTime.Now).Days > 90 ? DateTime.Now.AddDays(90) : menor;



            }

            Contexto.SaveChanges();

            lblError.Text = "Actualizacion Correcta";
        }
        else
        {
            lblError.Text = "Contrato no encontrado";
        }


        //try
        //{

        //    //Se define el objeto conexión
        //    System.Data.SqlClient.SqlConnection conn;
        //    System.Data.SqlClient.SqlDataReader reader;
        //    System.Data.SqlClient.SqlCommand sql;

        //    //Se especifica el string de conexión
        //    conn = new System.Data.SqlClient.SqlConnection();
        //    conn.ConnectionString = lblCadena.Text;

        //    //Se abre la conexión y se ejecuta la consulta
        //    conn.Open();

        //    sql = new System.Data.SqlClient.SqlCommand();
        //    sql.CommandText = "SELECT * FROM Plantilla";
        //    sql.Connection = conn;

        //    reader = sql.ExecuteReader();
        //    do
        //    {
        //        Response.Write(reader.FieldCount + "<BR>");
        //    } while (reader.Read());

        //}
        //catch (Exception ex)
        //{
        //    lblError.Text = ex.Message;
        //}
    }
    protected void Button1_Click(object sender, System.EventArgs e)
    {
        EntidadesConosud Contexto = new EntidadesConosud();

        var _ContratoAnt = (from v in Contexto.Contrato
                            where v.Codigo == lblCadena.Text
                            select v).FirstOrDefault();

        if (_ContratoAnt != null)
        {
            var ve = (from v in Contexto.VahiculosyEquipos
                      where v.objContrato.IdContrato == _ContratoAnt.IdContrato
                      select v).ToList();

            string fechaUsadas = "";

            foreach (var item in ve)
            {

                SortedList fechas = new SortedList();
                if (item.FechaUltimoPagoSeguro != null)
                {
                    fechas.Add(item.FechaUltimoPagoSeguro, "fechaSeguro");
                    fechaUsadas += "                  fechaSeguro: " + item.FechaUltimoPagoSeguro.Value.ToShortDateString() + "<br />";
                }

                fechas.Add(_ContratoAnt.Prorroga != null ? _ContratoAnt.Prorroga : _ContratoAnt.FechaVencimiento, "FecahContrato");

                DateTime? ff = _ContratoAnt.Prorroga != null ? _ContratoAnt.Prorroga : _ContratoAnt.FechaVencimiento;
                fechaUsadas += "                  fechaCONT: " + ff.Value.ToShortDateString() + "<br />";

                if (item.FechaVencimientoHabilitacion != null)
                {
                    fechas.Add((item.FechaVencimientoHabilitacion).Value.AddSeconds(3), "fechaCENT");
                    fechaUsadas += "                  fechaCENT: " + item.FechaUltimoPagoSeguro.Value.ToShortDateString() + "<br />";
                };

                DateTime menor = (DateTime)fechas.GetKeyList()[0];

                if (menor != null)
                {
                    lblError.Text += "<br />" + item.Patente + " - Fecha Actual:" + (item.VencimientoCredencial != null ? item.VencimientoCredencial.Value.ToShortDateString():"") + " - Nueva fecha:" + menor.ToShortDateString() + "<br />";
                    lblError.Text += fechaUsadas;
                    fechaUsadas = "";
                }
                else {
                    lblError.Text += "No hubo fechas";
                }

            }



        }
        else
        {
            lblError.Text = "Contrato no encontrado";
        }
    }
}
