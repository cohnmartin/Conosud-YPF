using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using System.Web.Services;

public partial class ConsultaRecorridosTransportes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            (Page.Master as DefaultMasterPage).OcultarSoloEncabezado();

            using (EntidadesConosud dc = new EntidadesConosud())
            {
                cboRecorridos.DataTextField = "Descripcion";
                cboRecorridos.DataValueField = "Id";
                cboRecorridos.DataSource = (from c in dc.CabeceraRutasTransportes
                                            where c.TipoTurno != "Temporal "
                                            orderby c.Empresa, c.Linea, c.TipoTurno, c.TipoRecorrido
                                            select new
                                                {
                                                    Id = c.Id,
                                                    Descripcion = c.Empresa + " - LINEA " + c.Linea + " - " + c.TipoTurno + " - " + c.TipoRecorrido 
                                            }).ToList();
                cboRecorridos.DataBind();
            }


        }
    }

    [WebMethod(EnableSession = true)]
    public static object GetRecorrido(long idcab)
    {
        Dictionary<string, object> datos = new Dictionary<string, object>();

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            var recorrido = (from c in dc.CabeceraRutasTransportes
                             where c.Id == idcab
                             select new
                             {
                                 recorrido = c.RutasTransportes.Select(w => new { w.Latitud, w.Longitud }),
                                 c.Empresa,
                                 Horario = c.HorariosSalida + " - " + c.HorariosLlegada,
                                 TipoRecorrido = "IDA"
                                 

                             }).FirstOrDefault();

            datos.Add("InfoRecorrido", recorrido);

        }


        return datos;
    }

    [WebMethod(EnableSession = true)]
    public static object GetRecorridoAll(List<long> ids)
    {
        Dictionary<string, object> datos = new Dictionary<string, object>();

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            var recorrido = (from c in dc.CabeceraRutasTransportes
                             where ids.Contains(c.Id)
                             select new
                             {
                                 /// Es importante que los puntos de las rutas vengan ordenas siempre igual.-
                                 recorrido = c.RutasTransportes.OrderBy(w=>w.Id).Select(w => new { w.Latitud, w.Longitud }),
                                 c.Empresa,
                                 Horario = c.HorariosSalida + " - " + c.HorariosLlegada,
                                 TipoRecorrido = "IDA",
                                 EmpresaDestino = c.EmpresaDestinoRuta.RazonSocial

                             }).ToList();

            datos.Add("InfoRecorrido", recorrido);

        }


        return datos;
    }
}