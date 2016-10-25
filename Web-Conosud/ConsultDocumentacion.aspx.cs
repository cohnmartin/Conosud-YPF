using System;
using System.Collections;
using System.Collections.Generic;
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
using Telerik.Web.UI;
using Entidades;
using System.Web.Services;

public partial class ConsultDocumentacion : System.Web.UI.Page
{
    private EntidadesConosud _dc = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadEmpresas();

        }


        #region Seguridad
        long idUsuario = long.Parse(Session["idusu"].ToString());
        Entidades.SegRolMenu PermisosPagina = Helpers.GetPermisosAcciones(Helpers.Constantes.PaginaMenu_.Documentacion, idUsuario);

        if (PermisosPagina.Lectura && !PermisosPagina.Creacion && !PermisosPagina.Modificacion)
        {
            gridDoc.FunctionsColumns.RemoveAt(0);
            gridDoc.Columns[3].Display = false;
        }
        #endregion
    }


    private void LoadEmpresas()
    {

        cboEmpresas.DataTextField = "RazonSocial";
        cboEmpresas.DataValueField = "IdEmpresa";
        cboEmpresas.DataSource = Helpers.GetEmpresasContratistas(long.Parse(Session["idusu"].ToString()));
        cboEmpresas.DataBind();

        cboEmpresas.Items.Insert(0, new RadComboBoxItem("- Seleccione una Empresa -"));

    }

    private void LoadContratos(int id)
    {

        cboContratos.DataTextField = "Codigo";
        cboContratos.DataValueField = "IdContrato";
        cboContratos.DataSource = Helpers.GetContratos(id);
        cboContratos.DataBind();

        cboContratos.Items.Insert(0, new RadComboBoxItem("- Seleccione un Contrato -"));

    }

    private void LoadContratistas(int id)
    {
        cboContratistas.DataTextField = "RazonSocial";
        cboContratistas.DataValueField = "IdContratoEmpresas";
        cboContratistas.DataSource = Helpers.GetContratistas(id);
        cboContratistas.DataBind();

        cboContratistas.Items.Insert(0, new RadComboBoxItem("- Seleccione un Contratista -"));
    }

    private void LoadPeriodos(int id)
    {
        cboPeriodos.DataTextFormatString = "{0:yyyy/MM}";
        cboPeriodos.DataTextField = "Periodo";
        cboPeriodos.DataValueField = "IdCabeceraHojasDeRuta";
        cboPeriodos.DataSource = Helpers.GetPeriodos(id, long.Parse(this.Session["idusu"].ToString()));
        cboPeriodos.DataBind();

        cboPeriodos.Items.Insert(0, new RadComboBoxItem("- Seleccione un Periodo -"));
    }

    protected void cboEmpresas_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
    {
        LoadEmpresas();
    }

    protected void cboContratos_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
    {
        LoadContratos(int.Parse(e.Text));
    }

    protected void cboContratistas_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
    {
        LoadContratistas(int.Parse(e.Text));
    }

    protected void cboPriodos_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
    {
        LoadPeriodos(int.Parse(e.Text));
    }

    protected void cboPeriodos_SelectedIndexChanged1(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        //OnSelectedIndexChanged="cboPeriodos_SelectedIndexChanged1"
        //System.Threading.Thread.Sleep(2000);

        _dc = new EntidadesConosud();
        if (e.Value != "")
        {

            long idUsuario = long.Parse(Session["idusu"].ToString());
            Entidades.SegRolMenu PermisosPagina = Helpers.GetPermisosAcciones(Helpers.Constantes.PaginaMenu_.Documentacion, idUsuario);

            if (PermisosPagina.Lectura && !PermisosPagina.Creacion && !PermisosPagina.Modificacion)
            {
                gridDoc.FunctionsColumns.RemoveAt(0);
                gridDoc.Columns[3].Display = false;
            }

            int Id = int.Parse(e.Value);

            var cabecera = (from C in _dc.CabeceraHojasDeRuta
                            where C.IdCabeceraHojasDeRuta == Id
                            select C).FirstOrDefault();

            if (cabecera.EsFueraTermino.HasValue)
                chkFueraTermino.Checked = cabecera.EsFueraTermino.Value;

            upFueraTermino.Update();


            var ItemsHoja = (from H in _dc.HojasDeRuta
                             where H.CabeceraHojasDeRuta.IdCabeceraHojasDeRuta == Id
                             orderby H.Plantilla.Codigo
                             select new
                             {
                                 IdHoja = H.IdHojaDeRuta,
                                 Titulo = H.Plantilla.Descripcion,
                                 FechaEntrega = H.DocFechaEntrega,
                                 FechaEntregaOriginal = H.DocFechaEntrega,
                                 Comentario = H.DocComentario,
                                 Presento = false

                             }).ToList();

            gridDoc.DataSource = ItemsHoja.ToList();

            Session["datos"] = ItemsHoja;


        }
    }

    [WebMethod]
    public static IDictionary<string, object> GetData(long Id)
    {
        EntidadesConosud _dc = new EntidadesConosud();
        Dictionary<string, object> datos = new Dictionary<string, object>();


        var cabecera = (from C in _dc.CabeceraHojasDeRuta
                        where C.IdCabeceraHojasDeRuta == Id
                        select C).FirstOrDefault();


        var ItemsHoja = (from H in _dc.HojasDeRuta
                         where H.CabeceraHojasDeRuta.IdCabeceraHojasDeRuta == Id
                         orderby H.Plantilla.Codigo
                         select new
                         {
                             IdHoja = H.IdHojaDeRuta,
                             Titulo = H.Plantilla.Descripcion,
                             FechaEntrega = H.DocFechaEntrega,
                             FechaEntregaOriginal = H.DocFechaEntrega,
                             Comentario = H.DocComentario,
                             Presento = false

                         }).ToList();

        datos.Add("Datos", ItemsHoja.ToList());
        if (cabecera.EsFueraTermino.HasValue)
            datos.Add("check", cabecera.EsFueraTermino.Value);
        else
            datos.Add("check", false);

        return datos;

    }


    [WebMethod]
    public static object UpdateData(List<IDictionary<string, object>> datos, bool fueraTermino)
    {

        long idcabecera = 0;
        Entidades.CabeceraHojasDeRuta cabecera =null;
        EntidadesConosud _dc = new EntidadesConosud();
        foreach (IDictionary<string, object> item in datos)
        {
            if (bool.Parse(item["Presento"].ToString()))
            {
                long id = long.Parse(item["IdHoja"].ToString());


                Entidades.HojasDeRuta itemsHoja = (from H in _dc.HojasDeRuta
                                                   where H.IdHojaDeRuta == id
                                                   select H).First<Entidades.HojasDeRuta>();

                itemsHoja.DocFechaEntrega = DateTime.Now;
                itemsHoja.DocComentario = "Sin Comentarios";

                /// al presnetar documentación para una hoja de ruta que esta publicada
                /// se des-publica automaticamente.
                itemsHoja.CabeceraHojasDeRuta.Publicar = false;
                itemsHoja.CabeceraHojasDeRuta.EsFueraTermino = fueraTermino;
                idcabecera = itemsHoja.CabeceraHojasDeRuta.IdCabeceraHojasDeRuta;
                cabecera = itemsHoja.CabeceraHojasDeRuta;
            }

        }

        if (cabecera != null )
        {
            Entidades.CabeceraHojasDeRuta cab = cabecera;
            if (!cabecera.ContratoEmpresas.EsContratista.Value)
            {
                cab = (from c in _dc.CabeceraHojasDeRuta
                       where c.ContratoEmpresas.Contrato.IdContrato == cabecera.ContratoEmpresas.Contrato.IdContrato 
                       && c.ContratoEmpresas.EsContratista.Value && (c.Periodo.Month == cabecera.Periodo.Month && c.Periodo.Year == cabecera.Periodo.Year)
                       select c).First<Entidades.CabeceraHojasDeRuta>();
            }

            string estado = UpdateSeguimientoAuditoria(cab.IdCabeceraHojasDeRuta, cab.Periodo, cab.ContratoEmpresas.IdContratoEmpresas);

            cab.EstadoAlCierre = estado != "" ? estado : cab.EstadoAlCierre;
        }

        _dc.SaveChanges();


        return (from H in _dc.HojasDeRuta
                where H.CabeceraHojasDeRuta.IdCabeceraHojasDeRuta == idcabecera
                orderby H.Plantilla.Codigo
                select new
                {
                    IdHoja = H.IdHojaDeRuta,
                    Titulo = H.Plantilla.Descripcion,
                    FechaEntrega = H.DocFechaEntrega,
                    FechaEntregaOriginal = H.DocFechaEntrega,
                    Comentario = H.DocComentario,
                    Presento = false

                }).ToList();

    }
    [WebMethod]
    public static object UpdateDataItem(IDictionary<string, object> item, long id)
    {

        long idcabecera = 0;
        EntidadesConosud _dc = new EntidadesConosud();

        Entidades.HojasDeRuta itemsHoja = (from H in _dc.HojasDeRuta
                                           where H.IdHojaDeRuta == id
                                           select H).First<Entidades.HojasDeRuta>();

        if (item["FechaEntrega"] != null)
            itemsHoja.DocFechaEntrega = DateTime.Parse(item["FechaEntrega"].ToString());
        else
            itemsHoja.DocFechaEntrega = null;

        itemsHoja.DocComentario = item["Comentario"].ToString();

        /// al presnetar documentación para una hoja de ruta que esta publicada
        /// se des-publica automaticamente.
        itemsHoja.CabeceraHojasDeRuta.Publicar = false;
        idcabecera = itemsHoja.CabeceraHojasDeRuta.IdCabeceraHojasDeRuta;


        if (itemsHoja.CabeceraHojasDeRuta != null)
        {
            Entidades.CabeceraHojasDeRuta cab = itemsHoja.CabeceraHojasDeRuta;
            if (!itemsHoja.CabeceraHojasDeRuta.ContratoEmpresas.EsContratista.Value)
            {
                cab = (from c in _dc.CabeceraHojasDeRuta
                       where c.ContratoEmpresas.Contrato.IdContrato == itemsHoja.CabeceraHojasDeRuta.ContratoEmpresas.Contrato.IdContrato
                       && c.ContratoEmpresas.EsContratista.Value && c.Periodo == itemsHoja.CabeceraHojasDeRuta.Periodo
                       select c).First<Entidades.CabeceraHojasDeRuta>();
            }

            string estado = UpdateSeguimientoAuditoria(cab.IdCabeceraHojasDeRuta, cab.Periodo, cab.ContratoEmpresas.IdContratoEmpresas);

            cab.EstadoAlCierre = estado != "" ? estado : cab.EstadoAlCierre;
        }
        

        _dc.SaveChanges();

        return (from H in _dc.HojasDeRuta
                where H.CabeceraHojasDeRuta.IdCabeceraHojasDeRuta == idcabecera
                orderby H.Plantilla.Codigo
                select new
                {
                    IdHoja = H.IdHojaDeRuta,
                    Titulo = H.Plantilla.Descripcion,
                    FechaEntrega = H.DocFechaEntrega,
                    FechaEntregaOriginal = H.DocFechaEntrega,
                    Comentario = H.DocComentario,
                    Presento = false

                }).ToList();

    }

    public static string UpdateSeguimientoAuditoria(long idCabecera, DateTime PeriodoPresentacion, long idContratoEmpresa)
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            string estado = "";
            List<Entidades.SeguimientoAuditoria> seguimientosHoja = (from H in dc.SeguimientoAuditoria
                                                              where H.Cabcera == idCabecera
                                                              select H).ToList<Entidades.SeguimientoAuditoria>();


            List<Entidades.Clasificacion> clasificacionesAuditoria = (from H in dc.Clasificacion
                                                                     where H.Tipo.Contains("AUDITORIA")
                                                                     select H).ToList<Entidades.Clasificacion>();


            // Si la documentacion presentada pertenece al periodo actual ( mes actual menos uno) entonces evaluo el dia para 
            // saber si es en termino o no, si es posterior a esto se marca como no presento documentacion.
            DateTime periodoActual = DateTime.Now.AddMonths(-1);
            if (PeriodoPresentacion.Month == periodoActual.Month && PeriodoPresentacion.Year == periodoActual.Year)
            {
                if (periodoActual.Day >= 1 && periodoActual.Day <= 20)
                {
                    estado = "EN TERMINO";
                }
                else
                {
                    estado = "FUERA DE TERMINO";
                }

            }
            else
            {
                // Si no hay otras presentaciones y dado a que el periodo de presentacion no es el actual
                // entonces indico que el estado de la cabecera deber ser NO PRESETNO
                if (seguimientosHoja.Count == 0)
                    estado = "NO PRESENTO";
                else
                    //Como existen otros seguimientos previos el estado de la cabecera ya fue seteado.
                    estado = "";
            }


            // Busco para determinar si ya hay un registro de seguimiento para el mismo mes de la recepcion 
            // si no lo hay debo crearlo o si existe pero ya fue publicado debo crear uno nuevo.
            List<SeguimientoAuditoria> allSegActual = seguimientosHoja.Where(w => w.objCabecera.IdCabeceraHojasDeRuta == idCabecera).ToList();
            SeguimientoAuditoria segActual = allSegActual.Where(w => w.FechaRecepcion.Month == DateTime.Now.Month && w.FechaRecepcion.Year == DateTime.Now.Year).LastOrDefault();
            if (segActual == null || (segActual.Publicado != null && segActual.Publicado.Value == true))
            {
                segActual = new SeguimientoAuditoria();
                segActual.FechaRecepcion = DateTime.Now;
                segActual.Cabcera= idCabecera;
                segActual.NroPresentacion = allSegActual.Count();

                if (estado == "FUERA DE TERMINO")
                { 
                    /// 1. Para este estado debo poner el seguimiento con el resultado de retencion
                    long resultadoRetencion = clasificacionesAuditoria.Where(w => w.Codigo == "RETENCION").FirstOrDefault().IdClasificacion;
                    segActual.FechaResultado = DateTime.Now;
                    segActual.Resultado = resultadoRetencion;

                    /// 2. Se debe calcular el porcentaje de retencion que posee, la regla es:
                    /// Si ya posee una retención en algun de los seguimientos de cualquier otra hoja
                    /// debo poner la retención siguiente, la ultima retención es la 2.

                    List<Entidades.SeguimientoAuditoria> seguimientosAnteriores = (from H in dc.SeguimientoAuditoria
                                                                             where H.objCabecera.ContratoEmpresas.IdContratoEmpresas == idContratoEmpresa
                                                                             && H.Resultado == resultadoRetencion && H.Cabcera != idCabecera && H.objRetencion != null
                                                                             select H).ToList<Entidades.SeguimientoAuditoria>();

                    if (seguimientosAnteriores.Count == 0)
                    {
                        segActual.Retencion = clasificacionesAuditoria.Where(w => w.Tipo == "RETENCION_AUDITORIA" && w.Codigo == "0").FirstOrDefault().IdClasificacion;
                    }
                    else
                    {
                       string codigoRetencionAsignar =  int.Parse(seguimientosAnteriores.Last().objRetencion.Codigo) == 2 ? "2":"1";

                        segActual.Retencion = clasificacionesAuditoria.Where(w => w.Tipo == "RETENCION_AUDITORIA" && w.Codigo == codigoRetencionAsignar).FirstOrDefault().IdClasificacion;
                    }

                    segActual.FechaRetencion = DateTime.Now;
                }

                dc.AddToSeguimientoAuditoria(segActual);
                dc.SaveChanges();
            }

            return estado;
        }
    }
}
