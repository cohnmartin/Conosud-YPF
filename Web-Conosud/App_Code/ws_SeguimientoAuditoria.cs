using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Entidades;
using System.Data.Objects;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Reflection;

/// <summary>
/// Descripción breve de ws_SeguimientoAuditoria
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
[System.Web.Script.Services.ScriptService]
public class ws_SeguimientoAuditoria : System.Web.Services.WebService
{

    public ws_SeguimientoAuditoria()
    {

        //Eliminar la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public object getHojasAsignacionAuditor()
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            Dictionary<string, object> datos = new Dictionary<string, object>();

            #region Busqueda de hojas para asignar auditor

            var hojas = (from s in dc.SeguimientoAuditoria.Include("CabeceraRutas")
                         where s.AuditorAsignado == null && (s.FechaRecepcion.Month == DateTime.Now.Month && s.FechaRecepcion.Year == DateTime.Now.Year)
                         select new
                         {
                             CodigoContrato = s.objCabecera.ContratoEmpresas.Contrato.Codigo,
                             Contratista = s.objCabecera.ContratoEmpresas.Empresa.RazonSocial,
                             Periodo = s.objCabecera.Periodo,
                             EstadoAlCierre = s.objCabecera.EstadoAlCierre,
                             IdCabeceraHojasDeRuta = s.objCabecera.IdCabeceraHojasDeRuta,
                             AuditorAsignado = s.objAuditorAsignado,
                             IdSeguimiento = s.Id

                         }).ToList();

            long? auditorNulo = null;
            var hojasFormateadas = (from s in hojas
                                    select new
                                    {
                                        CodigoContrato = s.CodigoContrato,
                                        Contratista = s.Contratista,
                                        Periodo = string.Format("{0:MM/yyyy}", s.Periodo),
                                        EstadoAlCierre = s.EstadoAlCierre == "" ? "NO PRESENTO" : s.EstadoAlCierre,
                                        PeriodoFecha = s.Periodo,
                                        IdCabeceraHojasDeRuta = s.IdCabeceraHojasDeRuta,
                                        AuditorAsignado = s.AuditorAsignado != null ? s.AuditorAsignado.IdSegUsuario : auditorNulo,
                                        IdSeguimiento = s.IdSeguimiento
                                    }).ToList();


            List<long> ids = new List<long>();
            DateTime periodoVigente = DateTime.Now.AddMonths(-1);
            var hojasEnTermino = hojasFormateadas.Where(w => w.EstadoAlCierre == "EN TERMINO" && (w.PeriodoFecha.Month == periodoVigente.Month && w.PeriodoFecha.Year == periodoVigente.Year)).ToList();
            var hojasFueraTermino = hojasFormateadas.Where(w => w.EstadoAlCierre == "FUERA DE TERMINO" && (w.PeriodoFecha.Month == periodoVigente.Month && w.PeriodoFecha.Year == periodoVigente.Year)).ToList();


            ids.AddRange(hojasEnTermino.Select(w => w.IdCabeceraHojasDeRuta));
            ids.AddRange(hojasFueraTermino.Select(w => w.IdCabeceraHojasDeRuta));
            var hojasResto = hojasFormateadas.Where(w => !ids.Contains(w.IdCabeceraHojasDeRuta)).ToList();

            datos.Add("HojasET", hojasEnTermino);
            datos.Add("HojasFT", hojasFueraTermino);
            datos.Add("HojasOT", hojasResto);

            #endregion

            #region Busqueda de auditores habilitados para asignar

            long[] idsRolAuditor = new long[] { ((long)Helpers.RolesEspeciales.Auditor), ((long)Helpers.RolesEspeciales.AuditorProvisional), ((long)Helpers.RolesEspeciales.AuditorSueldos) };
            var auditores = (from a in dc.SegUsuarioRol
                             where idsRolAuditor.Contains(a.SegRol.IdSegRol)
                             select new
                             {
                                 Id = a.SegUsuario.IdSegUsuario,
                                 Nombre = a.SegUsuario.Login
                             }).ToList().Distinct();


            #endregion
            datos.Add("Auditores", auditores);
            return datos;

        }

    }


    [WebMethod(EnableSession = true)]
    public object getHojasAsignacionResultado()
    {
        long idAuditor = (long)HttpContext.Current.Session["idusu"];

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            Dictionary<string, object> datos = new Dictionary<string, object>();

            #region Busqueda de hojas para asignar resultado

            var hojas = (from s in dc.SeguimientoAuditoria.Include("CabeceraRutas")
                         where ((s.AuditorAsignado != null && s.AuditorAsignado.Value == idAuditor) || (s.AudtorInterino != null && s.AudtorInterino.Value == idAuditor))
                         && s.objResultado == null
                         select new
                         {
                             CodigoContrato = s.objCabecera.ContratoEmpresas.Contrato.Codigo,
                             Contratista = s.objCabecera.ContratoEmpresas.Empresa.RazonSocial,
                             Periodo = s.objCabecera.Periodo,
                             EstadoAlCierre = s.objCabecera.EstadoAlCierre,
                             IdCabeceraHojasDeRuta = s.objCabecera.IdCabeceraHojasDeRuta,
                             AuditorAsignado = s.objAuditorAsignado,
                             ResultadoAsignado = s.objResultado,
                             IdSeguimiento = s.Id

                         }).ToList();

            long? auditorNulo = null;
            var hojasFormateadas = (from s in hojas
                                    select new
                                    {
                                        CodigoContrato = s.CodigoContrato,
                                        Contratista = s.Contratista,
                                        Periodo = string.Format("{0:MM/yyyy}", s.Periodo),
                                        EstadoAlCierre = s.EstadoAlCierre == "" ? "NO PRESENTO" : s.EstadoAlCierre,
                                        PeriodoFecha = s.Periodo,
                                        IdCabeceraHojasDeRuta = s.IdCabeceraHojasDeRuta,
                                        AuditorAsignado = s.AuditorAsignado != null ? s.AuditorAsignado.IdSegUsuario : auditorNulo,
                                        IdSeguimiento = s.IdSeguimiento,
                                        ResultadoAsignado = s.ResultadoAsignado != null ? s.ResultadoAsignado.IdClasificacion : auditorNulo,
                                    }).ToList();

            datos.Add("Hojas", hojasFormateadas);


            #endregion

            #region Busqueda de resultados posibles
            string agrupador = Helpers.Constantes.ResultadosAuditoria;

            var resultados = (from c in dc.Clasificacion
                              where c.Tipo == agrupador
                              select new
                  {
                      Id = c.IdClasificacion,
                      Nombre = c.Descripcion,
                  }).ToList();

            datos.Add("ResultadosPosibles", resultados);

            #endregion
            
            return datos;

        }

    }

    [WebMethod(EnableSession = true)]
    public object getHojasConResultado(string IdContratista, string IdContrato)
    {
        long idAuditor = (long)HttpContext.Current.Session["idusu"];

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            Dictionary<string, object> datos = new Dictionary<string, object>();

            #region Busqueda de hojas para asignar resultado
            long? nullValue = null;
            long? idContatista = IdContratista != "" ? long.Parse(IdContratista) : nullValue; 
            long? idContrato = IdContrato != "" ? long.Parse(IdContrato) : nullValue;
            

            IQueryable<SeguimientoAuditoria> query = null;
            query = dc.SeguimientoAuditoria;

            query = query.Where(s => ((s.AuditorAsignado != null && s.AuditorAsignado.Value == idAuditor) || (s.AudtorInterino != null && s.AudtorInterino.Value == idAuditor)) && s.objResultado != null);

            if (idContatista != null)
                query = query.Where(s => s.objCabecera.ContratoEmpresas.Empresa.IdEmpresa == idContatista);

            if (idContrato != null)
                query = query.Where(s => s.objCabecera.ContratoEmpresas.Contrato.IdContrato == idContrato);

            var hojas1 = query.Select(s => new
            {
                CodigoContrato = s.objCabecera.ContratoEmpresas.Contrato.Codigo,
                Contratista = s.objCabecera.ContratoEmpresas.Empresa.RazonSocial,
                Periodo = s.objCabecera.Periodo,
                EstadoAlCierre = s.objCabecera.EstadoAlCierre,
                IdCabeceraHojasDeRuta = s.objCabecera.IdCabeceraHojasDeRuta,
                AuditorAsignado = s.objAuditorAsignado,
                ResultadoAsignado = s.objResultado,
                IdSeguimiento = s.Id

            }).ToList();


            long? auditorNulo = null;
            var hojasFormateadas = (from s in hojas1
                                    select new
                                    {
                                        CodigoContrato = s.CodigoContrato,
                                        Contratista = s.Contratista,
                                        Periodo = string.Format("{0:MM/yyyy}", s.Periodo),
                                        EstadoAlCierre = s.EstadoAlCierre == "" ? "NO PRESENTO" : s.EstadoAlCierre,
                                        PeriodoFecha = s.Periodo,
                                        IdCabeceraHojasDeRuta = s.IdCabeceraHojasDeRuta,
                                        AuditorAsignado = s.AuditorAsignado != null ? s.AuditorAsignado.IdSegUsuario : auditorNulo,
                                        IdSeguimiento = s.IdSeguimiento,
                                        ResultadoAsignado = s.ResultadoAsignado != null ? s.ResultadoAsignado.IdClasificacion : auditorNulo,
                                    }).ToList();

            datos.Add("Hojas", hojasFormateadas);


            #endregion


            return datos;

        }

    }


    protected IEnumerable<IDataObject> GetData(Type entityType, Expression<Func<dynamic, bool>> whereClause, Expression<Func<dynamic, dynamic>>[] includes)
    {
        if (typeof(IDataObject).IsAssignableFrom(entityType.BaseType))
        {
            return GetData(entityType.BaseType, whereClause, includes);
        }
        var contextType = this.Context.GetType();
        MethodInfo createObjectSetMethod = contextType.GetMethod("CreateObjectSet", new Type[] { }).MakeGenericMethod(entityType);
        // Builds up an ObjectSet<EntityType> 
        dynamic objectSet = createObjectSetMethod.Invoke(this.Context, new object[] { });
        dynamic query = objectSet;
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        if (whereClause == null)
        {
            whereClause = (item) => true;
        }
        query = query.Where(whereClause);
        return query.ToList().OfType<IDataObject>();
    }
}
