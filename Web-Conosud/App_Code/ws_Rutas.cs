using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Entidades;

/// <summary>
/// Descripción breve de ws_Rutas
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
[System.Web.Script.Services.ScriptService]
public class ws_Rutas : System.Web.Services.WebService
{

    public ws_Rutas()
    {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public object getRutas()
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            return (from c in dc.CabeceraRutasTransportes
                    where c.TipoTurno != "Temporal "
                    orderby c.Empresa, c.Linea, c.TipoTurno, c.TipoRecorrido
                    select new
                    {
                        Id = c.Id,
                        Descripcion = c.Empresa + " - LINEA " + c.Linea + " - " + c.TipoTurno + " - " + c.TipoRecorrido,
                        Selected = false
                    }).ToList();

        }

    }

}
