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
                         where (s.AuditorAsignado == null || (s.AuditorAsignado != null && s.Resultado == null)) && (s.FechaRecepcion.Month == DateTime.Now.Month && s.FechaRecepcion.Year == DateTime.Now.Year)
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
                         && (s.objResultado == null || (s.objResultado != null && (s.FechaResultado.Value.Month == DateTime.Now.Month && s.FechaResultado.Value.Year == DateTime.Now.Year)))
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

    [WebMethod]
    public object getHojasAsignacionRetencion()
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            Dictionary<string, object> datos = new Dictionary<string, object>();

            #region Busqueda de hojas para asignar auditor

            var hojas = (from s in dc.SeguimientoAuditoria.Include("CabeceraRutas")
                         where s.objResultado != null && s.objResultado.Codigo ==  Helpers.Constantes.CodigoResultadosAuditoria_Retencion && s.Retencion == null
                         select new
                         {
                             CodigoContrato = s.objCabecera.ContratoEmpresas.Contrato.Codigo,
                             Contratista = s.objCabecera.ContratoEmpresas.Empresa.RazonSocial,
                             Periodo = s.objCabecera.Periodo,
                             EstadoAlCierre = s.objCabecera.EstadoAlCierre,
                             IdCabeceraHojasDeRuta = s.objCabecera.IdCabeceraHojasDeRuta,
                             IdSeguimiento = s.Id,
                             NroPresentacion = s.NroPresentacion
                         }).ToList();

            var hojasFormateadas = (from s in hojas
                                    select new
                                    {
                                        CodigoContrato = s.CodigoContrato,
                                        Contratista = s.Contratista,
                                        Periodo = string.Format("{0:MM/yyyy}", s.Periodo),
                                        EstadoAlCierre = s.EstadoAlCierre == "" ? "NO PRESENTO" : s.EstadoAlCierre,
                                        PeriodoFecha = s.Periodo,
                                        IdCabeceraHojasDeRuta = s.IdCabeceraHojasDeRuta,
                                        IdSeguimiento = s.IdSeguimiento,
                                        NroPresentacion = s.NroPresentacion == 0 ? "1º PRESENTACION" : s.NroPresentacion.ToString() + "º ADICIONAL",
                                        Retencion = 0
                                    }).ToList();


            datos.Add("Hojas", hojasFormateadas);

            #endregion
          
            return datos;

        }

    }
    
    [WebMethod]
    public bool GrabarAsignacion(IList<IDictionary<string, object>> Hojas)
    {
        DateTime? fechaNula = null;
        long? longNulo = null;
        List<long> idsSeg = new List<long>();
        foreach (var item in Hojas)
        {
            idsSeg.Add(long.Parse(item["IdSeguimiento"].ToString()));
        }

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            var segs = (from s in dc.SeguimientoAuditoria
                        where idsSeg.Contains(s.Id)
                        select s).ToList();


            foreach (var item in Hojas)
            {
                var seg = segs.Where(w => w.Id == long.Parse(item["IdSeguimiento"].ToString())).First();
                seg.AuditorAsignado = item["AuditorAsignado"] != null && item["AuditorAsignado"].ToString() != "" ? long.Parse(item["AuditorAsignado"].ToString()): longNulo;
                seg.FechaAsignacion = DateTime.Now;
            }

            dc.SaveChanges();
        }

        //using (EntidadesConosud dc = new EntidadesConosud())
        //{
        //    VehiculosYPF current = null;

        //    if (vehiculo.ContainsKey("Id"))
        //    {
        //        long id = long.Parse(vehiculo["Id"].ToString());
        //        current = (from v in dc.VehiculosYPF
        //                   where v.Id == id
        //                   select v).FirstOrDefault();
        //    }
        //    else
        //    {
        //        current = new VehiculosYPF();
        //        dc.AddToVehiculosYPF(current);
        //    }

        //    current.Patente = vehiculo["Patente"].ToString();
        //    current.Modelo = vehiculo["Modelo"].ToString();

        //    if (vehiculo.ContainsKey("IdDepartamento") && vehiculo["IdDepartamento"] != null)
        //        current.Departamento = long.Parse(vehiculo["IdDepartamento"].ToString());

        //    if (vehiculo.ContainsKey("IdSector") && vehiculo["IdSector"] != null)
        //        current.Sector = long.Parse(vehiculo["IdSector"].ToString());


        //    current.Titular = vehiculo["Titular"].ToString();

        //    if (vehiculo.ContainsKey("Responsable") && vehiculo["Responsable"] != null)
        //        current.Responsable = vehiculo["Responsable"].ToString();

        //    current.Combustible = long.Parse(vehiculo["IdTipoCombustible"].ToString());

        //    if (vehiculo.ContainsKey("IdTipoAsignacion") && vehiculo["IdTipoAsignacion"] != null)
        //        current.TipoAsignacion = long.Parse(vehiculo["IdTipoAsignacion"].ToString());

        //    if (vehiculo.ContainsKey("CentroCosto") && vehiculo["CentroCosto"] != null)
        //        current.CentroCosto = vehiculo["CentroCosto"].ToString();

        //    current.FechaVtoTarjVerde = vehiculo["VtoTarjVerde"].ToString() != null ? Convert.ToDateTime(vehiculo["VtoTarjVerde"].ToString()) : fechaNula;
        //    current.FechaVtoRevTecnica = vehiculo.ContainsKey("VtoRevTecnica") && vehiculo["VtoRevTecnica"] != null ? Convert.ToDateTime(vehiculo["VtoRevTecnica"].ToString()) : fechaNula;
        //    current.VelocimetroFecha = vehiculo.ContainsKey("VelocimetroFecha") && vehiculo["VelocimetroFecha"] != null ? Convert.ToDateTime(vehiculo["VelocimetroFecha"].ToString()) : fechaNula;

        //    if (vehiculo.ContainsKey("Contrato") && vehiculo["Contrato"] != null)
        //        current.Contrato = vehiculo["Contrato"].ToString();

        //    if (vehiculo.ContainsKey("NroTarjeta") && vehiculo["NroTarjeta"] != null)
        //        current.NroTarjeta = vehiculo["NroTarjeta"].ToString();

        //    if (vehiculo.ContainsKey("VelocimetroOdometro") && vehiculo["VelocimetroOdometro"] != null)
        //        current.VelocimetroOdometro = vehiculo["VelocimetroOdometro"].ToString();

        //    current.Año = vehiculo["Anio"].ToString();

        //    if (vehiculo.ContainsKey("Observacion") && vehiculo["Observacion"] != null)
        //        current.Observacion = vehiculo["Observacion"].ToString();


        //    if (vehiculo.ContainsKey("RazonSocial") && vehiculo["RazonSocial"] != null)
        //        current.RazonSocial = vehiculo["RazonSocial"].ToString();

        //    if (vehiculo.ContainsKey("TarjetasActivas") && vehiculo["TarjetasActivas"] != null)
        //        current.TarjetasActivas = int.Parse(vehiculo["TarjetasActivas"].ToString());

        //    if (vehiculo.ContainsKey("LimiteCredito") && vehiculo["LimiteCredito"] != null)
        //        current.LimiteCredito = int.Parse(vehiculo["LimiteCredito"].ToString());

        //    if (vehiculo.ContainsKey("PIN") && vehiculo["PIN"] != null && vehiculo["PIN"].ToString() != "")
        //        current.PIN = int.Parse(vehiculo["PIN"].ToString());
        //    else
        //        current.PIN = null;


        //    if (vehiculo.ContainsKey("TitularPin") && vehiculo["TitularPin"] != null)
        //        current.TitularPin = vehiculo["TitularPin"].ToString();


        //    if (vehiculo.ContainsKey("PIN1") && vehiculo["PIN1"] != null && vehiculo["PIN1"].ToString() != "")
        //        current.PIN1 = int.Parse(vehiculo["PIN1"].ToString());
        //    else
        //        current.PIN1 = null;

        //    if (vehiculo.ContainsKey("TitularPin1") && vehiculo["TitularPin1"] != null)
        //        current.TitularPin1 = vehiculo["TitularPin1"].ToString();


        //    if (vehiculo.ContainsKey("PIN2") && vehiculo["PIN2"] != null && vehiculo["PIN2"].ToString() != "")
        //        current.PIN2 = int.Parse(vehiculo["PIN2"].ToString());
        //    else
        //        current.PIN2 = null;

        //    if (vehiculo.ContainsKey("TitularPin2") && vehiculo["TitularPin2"] != null)
        //        current.TitularPin2 = vehiculo["TitularPin2"].ToString();



        //    if (vehiculo.ContainsKey("PIN3") && vehiculo["PIN3"] != null && vehiculo["PIN3"].ToString() != "")
        //        current.PIN3 = int.Parse(vehiculo["PIN3"].ToString());
        //    else
        //        current.PIN3 = null;

        //    if (vehiculo.ContainsKey("TitularPin3") && vehiculo["TitularPin3"] != null)
        //        current.TitularPin3 = vehiculo["TitularPin3"].ToString();



        //    if (vehiculo.ContainsKey("PIN4") && vehiculo["PIN4"] != null && vehiculo["PIN4"].ToString() != "")
        //        current.PIN4 = int.Parse(vehiculo["PIN4"].ToString());
        //    else
        //        current.PIN4 = null;


        //    if (vehiculo.ContainsKey("TitularPin4") && vehiculo["TitularPin4"] != null)
        //        current.TitularPin4 = vehiculo["TitularPin4"].ToString();


        //    if (vehiculo.ContainsKey("PIN5") && vehiculo["PIN5"] != null && vehiculo["PIN5"].ToString() != "")
        //        current.PIN5 = int.Parse(vehiculo["PIN5"].ToString());
        //    else
        //        current.PIN5 = null;


        //    if (vehiculo.ContainsKey("TitularPin5") && vehiculo["TitularPin5"] != null)
        //        current.TitularPin5 = vehiculo["TitularPin5"].ToString();


        //    if (vehiculo.ContainsKey("PIN6") && vehiculo["PIN6"] != null && vehiculo["PIN6"].ToString() != "")
        //        current.PIN6 = int.Parse(vehiculo["PIN6"].ToString());
        //    else
        //        current.PIN6 = null;


        //    if (vehiculo.ContainsKey("TitularPin6") && vehiculo["TitularPin6"] != null)
        //        current.TitularPin6 = vehiculo["TitularPin6"].ToString();

        //    if (vehiculo.ContainsKey("PIN7") && vehiculo["PIN7"] != null && vehiculo["PIN7"].ToString() != "")
        //        current.PIN7 = int.Parse(vehiculo["PIN7"].ToString());
        //    else
        //        current.PIN7 = null;


        //    if (vehiculo.ContainsKey("TitularPin7") && vehiculo["TitularPin7"] != null)
        //        current.TitularPin7 = vehiculo["TitularPin7"].ToString();


        //    dc.SaveChanges();
        //}
        return true;
    }
    
    [WebMethod]
    public bool GrabarAsignacionResultado(IList<IDictionary<string, object>> Hojas)
    {
        DateTime? fechaNula = null;
        long? longNulo = null;
        List<long> idsSeg = new List<long>();
        foreach (var item in Hojas)
        {
            idsSeg.Add(long.Parse(item["IdSeguimiento"].ToString()));
        }

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            var segs = (from s in dc.SeguimientoAuditoria
                        where idsSeg.Contains(s.Id)
                        select s).ToList();


            foreach (var item in Hojas)
            {
                var seg = segs.Where(w => w.Id == long.Parse(item["IdSeguimiento"].ToString())).First();
                seg.Resultado = item["ResultadoAsignado"] != null &&  item["ResultadoAsignado"].ToString() != "" ? long.Parse(item["ResultadoAsignado"].ToString()) : longNulo;
                seg.FechaResultado= DateTime.Now;
            }

            dc.SaveChanges();
        }

      
        return true;
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
