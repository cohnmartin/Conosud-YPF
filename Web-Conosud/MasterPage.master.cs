using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{

    public string Titulo
    {
        set {
            lblTipoGestion.Text = value;
        }
    }

    public string Usurio
    {
        set
        {
            lblUsuario.Text = value;
        }
    }

    public bool TieneMensajes
    {
        set
        {
            indicadorMensaje.Visible = value;
            divMensaje.Visible = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        bool tieneMensajes = false;
        using (Entidades.EntidadesConosud dc = new Entidades.EntidadesConosud())
        {
            var pendientes = (from p in dc.DomiciliosPersonal
                              where p.EstadoActulizacion == "PENDIENTE"
                              select p).Count();

            if (pendientes > 0)
                tieneMensajes = true;

        }

        TieneMensajes = tieneMensajes;
        Usurio = Convert.ToString(this.Session["nombreusu"]);
    }
}
