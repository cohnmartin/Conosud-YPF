using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Web.SessionState;
using System.Linq;
using System.Collections.Generic;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (this.Session["idusu"] == null)
            Response.Redirect("~/LoginNew.aspx");

        ((MasterPage)this.Master).Titulo = "BIENVENIDO A SCS";

        //bool tieneMensajes = false;
        //using (Entidades.EntidadesConosud dc = new Entidades.EntidadesConosud())
        //{
        //    var pendientes = (from p in dc.DomiciliosPersonal
        //                      where p.EstadoActulizacion == "PENDIENTE"
        //                      select p).Count();

        //    if (pendientes > 0)
        //        tieneMensajes = true;

        //}

        //((MasterPage)this.Master).TieneMensajes = true;
        //((MasterPage)this.Master).Titulo = "BIENVENIDO A SCS";
        //((MasterPage)this.Master).Usurio = Convert.ToString(this.Session["nombreusu"]);

    }
}
