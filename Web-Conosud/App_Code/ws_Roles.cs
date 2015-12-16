using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Entidades;

/// <summary>
/// Summary description for ws_Roles
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ws_Roles : System.Web.Services.WebService
{

    public ws_Roles()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public object getRoles()
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            Dictionary<string, object> datos = new Dictionary<string, object>();
            var roles = (from d in dc.SegRol
                         orderby d.Descripcion
                         select new
                         {
                             Descripcion = d.Descripcion,
                             Id = d.IdSegRol,
                             Accesos = d.SegRolMenu.Select(w => new
                             {
                                 IdMenu = w.SegMenu.IdSegMenu,
                                 Lectura = w.Lectura,
                                 Modificacion = w.Modificacion,
                                 Creacion = w.Creacion,
                                 Posicion = w.SegMenu.Posicion,
                             })
                         }).ToList();

            var menu = (from w in dc.SegMenu.Include("Hijos")
                        select w).ToList().OrderBy(w => w.IdPadre).ThenBy(w => w.Posicion).ToList();








            datos.Add("Roles", roles);

            return datos;

        }

    }


}
