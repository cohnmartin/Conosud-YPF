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
            Response.Redirect("~/Login.aspx");

        this.lblNombreUsu.Text = Convert.ToString(this.Session["nombreusu"]);;


        //using (Entidades.EntidadesConosud dc = new Entidades.EntidadesConosud())
        //{
        //    Dictionary<string, object> datos = new Dictionary<string, object>();

        //    #region Busqueda de hojas para asignar auditor
        //    CabeceraHojasDeRuta hh = new CabeceraHojasDeRuta();
        //    int mes = DateTime.Now.AddMonths(-1).Month;
        //    int año = DateTime.Now.AddMonths(-1).Year;
        //    DateTime? fechaNula= null;

        //    var ResultadoConsulta = (from cab in dc.CabeceraHojasDeRuta.Include("colSeguimientoAuditoria")
        //                             where cab.Periodo.Month == mes && cab.Periodo.Year == año
        //                            && cab.ContratoEmpresas.Empresa != null
        //                            && cab.ContratoEmpresas.Contrato != null
        //                             orderby cab.ContratoEmpresas.Empresa.RazonSocial
        //                             , cab.ContratoEmpresas.Contrato.Codigo
        //                             select new
        //                             {
        //                                 CabeceraHojasDeRuta = cab,
        //                                 ContratoEmp = cab.ContratoEmpresas,
        //                                 Codigo = cab.ContratoEmpresas.Contrato.Codigo,
        //                                 FechaInicio = cab.ContratoEmpresas.Contrato.FechaInicio.Value,
        //                                 Estado = cab.Estado.Descripcion,
        //                                 NroCarpeta = cab.NroCarpeta,
        //                                 Periodo = cab.Periodo,
        //                                 Empresa = cab.ContratoEmpresas.Empresa,
        //                                 EsContratista = cab.ContratoEmpresas.EsContratista.Value,
        //                                 EsFueraTermino = cab.EsFueraTermino,
        //                                 IdCabeceraHojasDeRuta = cab.IdCabeceraHojasDeRuta,
        //                                 ConstratistaParaSubConstratista = "",
        //                                 Seguimiento = cab.colSeguimientoAuditoria
        //                             }).ToList();


        //    var ResultadoFormateado = (from cab in ResultadoConsulta
        //                               select new
        //                               {
        //                                   Periodo = string.Format("{0:yyyy/MM}",cab.Periodo),
        //                                   ContratoEmp = cab.ContratoEmp,
        //                                   NroContrato = cab.Codigo,
        //                                   FechaInicio = cab.FechaInicio,
        //                                   FechaFin = cab.ContratoEmp.Contrato.Prorroga != null ? cab.ContratoEmp.Contrato.Prorroga : cab.ContratoEmp.Contrato.FechaVencimiento, 
        //                                   Clasificacion = cab.ContratoEmp.Contrato.TipoContrato.Descripcion,
        //                                   Estado = cab.Estado,
        //                                   NroCarpeta = cab.NroCarpeta,
        //                                   Contratista = cab.Empresa.RazonSocial,
        //                                   IdCabeceraHojasDeRuta = cab.IdCabeceraHojasDeRuta,
        //                                   ConstratistaParaSubConstratista = !cab.EsContratista ? ResultadoConsulta.Where(w => w.Codigo == cab.Codigo && w.EsContratista).FirstOrDefault().Empresa.RazonSocial : "",
        //                                   Seguimiento = cab.Seguimiento,
        //                                   Auditor = cab.Seguimiento.Count() > 0 && cab.Seguimiento.Last().objAuditorAsignado != null ? cab.Seguimiento.Last().objAuditorAsignado.Login.ToString() : "Sin Auditor Asignado",
        //                                   SituacionAlCierre = cab.CabeceraHojasDeRuta.EstadoAlCierre,
                                           
        //                                   FechaRecepcion1 = cab.Seguimiento.Count() > 0 ? cab.Seguimiento.First().FechaRecepcion : fechaNula,
        //                                   FechaAuditoria1 = cab.Seguimiento.Count() > 0 ? cab.Seguimiento.First().FechaResultado : fechaNula,
        //                                   Resultado1 = cab.Seguimiento.Count() > 0 && cab.Seguimiento.First().objResultado != null ? cab.Seguimiento.First().objResultado.Descripcion : "",
        //                                   FechaRetencion1 = cab.Seguimiento.Count() > 0 && cab.Seguimiento.First().FechaRetencion != null ? cab.Seguimiento.First().FechaRetencion : fechaNula,
        //                                   Retencion1 = cab.Seguimiento.Count() > 0 && cab.Seguimiento.First().FechaRetencion != null ? cab.Seguimiento.First().Retencion.ToString() : "",

        //                                   FechaRecepcion2 = cab.Seguimiento.Count() > 1 ? cab.Seguimiento.Skip(1).Take(1).First().FechaRecepcion : fechaNula,
        //                                   FechaAuditoria2 = cab.Seguimiento.Count() > 1 ? cab.Seguimiento.Skip(1).Take(1).First().FechaResultado : fechaNula,
        //                                   Resultado2 = cab.Seguimiento.Count() > 1 && cab.Seguimiento.Skip(1).Take(1).First().objResultado != null ? cab.Seguimiento.Skip(1).Take(1).First().objResultado.Descripcion : "",
        //                                   FechaRetencion2 = cab.Seguimiento.Count() > 1 && cab.Seguimiento.Skip(1).Take(1).First().FechaRetencion != null ? cab.Seguimiento.Skip(1).Take(1).First().FechaRetencion : fechaNula,
        //                                   Retencion2 = cab.Seguimiento.Count() > 1 && cab.Seguimiento.Skip(1).Take(1).First().FechaRetencion != null ? cab.Seguimiento.Skip(1).Take(1).First().Retencion.ToString() : "",

        //                                   FechaRecepcion3 = cab.Seguimiento.Count() > 2 ? cab.Seguimiento.Skip(2).Take(1).First().FechaRecepcion : fechaNula,
        //                                   FechaAuditoria3 = cab.Seguimiento.Count() > 2 ? cab.Seguimiento.Skip(2).Take(1).First().FechaResultado : fechaNula,
        //                                   Resultado3 = cab.Seguimiento.Count() > 2 && cab.Seguimiento.Skip(2).Take(1).First().objResultado != null ? cab.Seguimiento.Skip(2).Take(1).First().objResultado.Descripcion : "",
        //                                   FechaRetencion3 = cab.Seguimiento.Count() > 2 && cab.Seguimiento.Skip(2).Take(1).First().FechaRetencion != null ? cab.Seguimiento.Skip(2).Take(1).First().FechaRetencion : fechaNula,
        //                                   Retencion3 = cab.Seguimiento.Count() > 2 && cab.Seguimiento.Skip(2).Take(1).First().FechaRetencion != null ? cab.Seguimiento.Skip(2).Take(1).First().Retencion.ToString() : "",


        //                                   FechaRecepcionUltima = cab.Seguimiento.Count() > 0 ? cab.Seguimiento.Last().FechaRecepcion : fechaNula,
        //                                   FechaAuditoriaUltima = cab.Seguimiento.Count() > 0 ? cab.Seguimiento.Last().FechaResultado : fechaNula,
        //                                   ResultadoUltima = cab.Seguimiento.Count() > 0 && cab.Seguimiento.Last().objResultado != null ? cab.Seguimiento.Last().objResultado.Descripcion : "",
        //                                   FechaRetencionUltima = cab.Seguimiento.Count() > 0 && cab.Seguimiento.Last().FechaRetencion != null ? cab.Seguimiento.Last().FechaRetencion : fechaNula,
        //                                   RetencionUltima = cab.Seguimiento.Count() > 0 && cab.Seguimiento.Last().FechaRetencion != null ? cab.Seguimiento.Last().Retencion.ToString() : "",
        //                                   EstadoActualAuditoria = cab.Seguimiento.Count() == 0 ? "Sin Documentacion" :  (cab.Seguimiento.Last().FechaResultado==null ? "En Proceso":"Terminado")




        //                               }).ToList();

        //    #endregion

        //}
    }
}
