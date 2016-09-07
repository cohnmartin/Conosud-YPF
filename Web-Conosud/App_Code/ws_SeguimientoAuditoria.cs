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
            /// Saco esta condicion para traer todas las hojas sin importar en que periodo se recepciono.
            //&& (s.FechaRecepcion.Month == DateTime.Now.Month && s.FechaRecepcion.Year == DateTime.Now.Year)

            var hojas = (from s in dc.SeguimientoAuditoria.Include("CabeceraRutas")
                         where (s.AuditorAsignado == null || (s.AuditorAsignado != null && s.Resultado == null))
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

            //datos.Add("HojasET", hojasEnTermino);
            datos.Add("HojasET", hojasFormateadas);

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
                                        ResultadoAsignadoDesc = s.ResultadoAsignado != null ? s.ResultadoAsignado.Descripcion : "",
                                    }).ToList();

            datos.Add("Hojas", hojasFormateadas);


            #endregion


            return datos;

        }

    }

    [WebMethod]
    public object getHojasAsignacionRetencion(string contratista, string contrato)
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            Dictionary<string, object> datos = new Dictionary<string, object>();

            #region Busqueda de hojas para asignar auditor


            IQueryable<SeguimientoAuditoria> query = null;
            query = dc.SeguimientoAuditoria;


            query = query.Where(s => s.objResultado != null && s.objResultado.Codigo == Helpers.Constantes.CodigoResultadosAuditoria_Retencion);

            if (contratista != "")
                query = query.Where(s => s.objCabecera.ContratoEmpresas.Empresa.RazonSocial.Contains(contratista));

            if (contrato != "")
                query = query.Where(s => s.objCabecera.ContratoEmpresas.Contrato.Codigo.Contains(contrato));


            var hojas = query.Select(s => new
                {
                    CodigoContrato = s.objCabecera.ContratoEmpresas.Contrato.Codigo,
                    Contratista = s.objCabecera.ContratoEmpresas.Empresa.RazonSocial,
                    Periodo = s.objCabecera.Periodo,
                    EstadoAlCierre = s.objCabecera.EstadoAlCierre,
                    IdCabeceraHojasDeRuta = s.objCabecera.IdCabeceraHojasDeRuta,
                    IdSeguimiento = s.Id,
                    NroPresentacion = s.NroPresentacion,
                    Retencion = s.objRetencion,
                    Auditor = s.objAuditorAsignado.Login
                }).ToList();



            //var hojas = (from s in dc.SeguimientoAuditoria.Include("CabeceraRutas")
            //             where s.objResultado != null && s.objResultado.Codigo == Helpers.Constantes.CodigoResultadosAuditoria_Retencion && ((s.Retencion != null && (s.FechaRetencion.Value.Month == DateTime.Now.Month && s.FechaRetencion.Value.Year == DateTime.Now.Year)) || s.Retencion == null)
            //             select new
            //             {
            //                 CodigoContrato = s.objCabecera.ContratoEmpresas.Contrato.Codigo,
            //                 Contratista = s.objCabecera.ContratoEmpresas.Empresa.RazonSocial,
            //                 Periodo = s.objCabecera.Periodo,
            //                 EstadoAlCierre = s.objCabecera.EstadoAlCierre,
            //                 IdCabeceraHojasDeRuta = s.objCabecera.IdCabeceraHojasDeRuta,
            //                 IdSeguimiento = s.Id,
            //                 NroPresentacion = s.NroPresentacion,
            //                 Retencion = s.objRetencion
            //             }).ToList();


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
                                        RetencionAplicada = s.Retencion == null ? "" : s.Retencion.Descripcion,
                                        Auditor = s.Auditor
                                    }).OrderBy(w => w.Contratista).ThenBy(w => w.CodigoContrato).ToList();

            datos.Add("Hojas", hojasFormateadas);

            #endregion


            #region Busqueda de los tipos de retenciones que se pueden aplicar
            var retenciones = (from a in dc.Clasificacion
                               where a.Tipo == "RETENCION_AUDITORIA"
                               select new
                               {
                                   Id = a.IdClasificacion,
                                   Nombre = a.Descripcion,
                                   Pos = a.Codigo
                               }).ToList().Distinct();


            datos.Add("Retenciones", retenciones);
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
                seg.AuditorAsignado = item["AuditorAsignado"] != null && item["AuditorAsignado"].ToString() != "" ? long.Parse(item["AuditorAsignado"].ToString()) : longNulo;
                seg.FechaAsignacion = item["AuditorAsignado"] != null && item["AuditorAsignado"].ToString() != "" ? DateTime.Now : fechaNula;
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
            var segs = (from s in dc.SeguimientoAuditoria.Include("objResultado")
                        where idsSeg.Contains(s.Id)
                        select s).ToList();

            List<Entidades.Clasificacion> clasificacionesAuditoria = (from H in dc.Clasificacion
                                                                      where H.Tipo.Contains("AUDITORIA")
                                                                      select H).ToList<Entidades.Clasificacion>();


            foreach (var item in Hojas)
            {
                var seg = segs.Where(w => w.Id == long.Parse(item["IdSeguimiento"].ToString())).First();

                /// 1. Si el siguimiento ya posee un resultado quiere decir que estoy modificando el resultado existente
                /// o es un resultado que se puso en la recepción por ser fuera de termino.
                long? resultadoOriginal = seg.Resultado;

                seg.Resultado = item["ResultadoAsignado"] != null && item["ResultadoAsignado"].ToString() != "" ? long.Parse(item["ResultadoAsignado"].ToString()) : longNulo;
                seg.FechaResultado = item["ResultadoAsignado"] != null && item["ResultadoAsignado"].ToString() != "" ? DateTime.Now : fechaNula;

                if (seg.Resultado == null)
                {
                    /// 1. Si el resultado es nulo, es decir, que no se le asigna uno o que se le saca el anterior
                    /// debo dejar como no finalizada la hoja.
                    var itemsHoja = (from H in dc.HojasDeRuta
                                     where H.CabeceraHojasDeRuta.IdCabeceraHojasDeRuta == seg.Cabcera
                                     select H).ToList();

                    foreach (var itemHoja in itemsHoja)
                    {
                        itemHoja.AuditoriaTerminada = false;
                    }
                
                }
                else if (resultadoOriginal != seg.Resultado)
                {
                    /// 1. Si se produce esta situación significa que se esta cambiando el valor de resultado 
                    /// con lo cual debo poner la hoja de ruta como finalizada.
                    /// 2. Si el resultado es el mismo no debo poner como finalizado la hoja, dado a que ya se hizo en el momento
                    /// original o es por que el resultado fue puesto en la recepción de documentacion.
                    var itemsHoja = (from H in dc.HojasDeRuta
                                   where H.CabeceraHojasDeRuta.IdCabeceraHojasDeRuta == seg.Cabcera
                                   select H).ToList();

                    foreach (var itemHoja in itemsHoja)
                    {
                        itemHoja.AuditoriaTerminada = true;
                    }

                }


                if (seg.Resultado != null && seg.objResultado.Codigo == "HIBILITADO")
                {
                    seg.objRetencion = null;
                    seg.FechaRetencion = null;
                }
                else if (seg.Resultado != null && seg.objResultado.Codigo == "RETENCION")
                {

                    /// 1. Se debe calcular el porcentaje de retencion que posee, la regla es:
                    /// Si ya posee una retención en algun de los seguimientos de cualquier otra hoja
                    /// debo poner la retención siguiente, la ultima retención es la 2.

                    List<Entidades.SeguimientoAuditoria> seguimientosAnteriores = (from H in dc.SeguimientoAuditoria
                                                                                   where H.objCabecera.ContratoEmpresas.IdContratoEmpresas == seg.objCabecera.ContratoEmpresas.IdContratoEmpresas
                                                                                   && H.Resultado == seg.Resultado && H.Cabcera != seg.Cabcera && H.objRetencion != null
                                                                                   select H).ToList<Entidades.SeguimientoAuditoria>();

                    if (seguimientosAnteriores.Count == 0)
                    {
                        seg.Retencion = clasificacionesAuditoria.Where(w => w.Tipo == "RETENCION_AUDITORIA" && w.Codigo == "0").FirstOrDefault().IdClasificacion;
                    }
                    else
                    {
                        string codigoRetencionAsignar = int.Parse(seguimientosAnteriores.Last().objRetencion.Codigo) == 0 ? "1" : "2";

                        seg.Retencion = clasificacionesAuditoria.Where(w => w.Tipo == "RETENCION_AUDITORIA" && w.Codigo == codigoRetencionAsignar).FirstOrDefault().IdClasificacion;
                    }

                    seg.FechaRetencion = DateTime.Now;

                }
            }

            dc.SaveChanges();
        }


        return true;
    }

    [WebMethod]
    public bool GrabarAsignacionRetencion(IList<IDictionary<string, object>> Hojas)
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
                seg.Retencion = item["RetencionAplicada"] != null && item["RetencionAplicada"].ToString() != "" ? long.Parse(item["RetencionAplicada"].ToString()) : longNulo;
                seg.FechaRetencion = item["RetencionAplicada"] != null && item["RetencionAplicada"].ToString() != "" ? DateTime.Now : fechaNula;
            }

            dc.SaveChanges();
        }


        return true;
    }

    [WebMethod]
    public object getReporteSeguimiento()
    {
        Dictionary<string, object> datos = new Dictionary<string, object>();

        using (Entidades.EntidadesConosud dc = new Entidades.EntidadesConosud())
        {


            #region Busqueda de hojas para asignar auditor
            CabeceraHojasDeRuta hh = new CabeceraHojasDeRuta();
            int mes = DateTime.Now.AddMonths(-1).Month;
            int año = DateTime.Now.AddMonths(-1).Year;
            DateTime? fechaNula = null;

            var ResultadoConsulta = (from cab in dc.CabeceraHojasDeRuta.Include("colSeguimientoAuditoria")
                                     where cab.Periodo.Month == mes && cab.Periodo.Year == año
                                    && cab.ContratoEmpresas.Empresa != null
                                    && cab.ContratoEmpresas.Contrato != null
                                     orderby cab.ContratoEmpresas.Empresa.RazonSocial
                                     , cab.ContratoEmpresas.Contrato.Codigo
                                     select new
                                     {
                                         CabeceraHojasDeRuta = cab,
                                         ContratoEmp = cab.ContratoEmpresas,
                                         Codigo = cab.ContratoEmpresas.Contrato.Codigo,
                                         FechaInicio = cab.ContratoEmpresas.Contrato.FechaInicio.Value,
                                         Estado = cab.Estado.Descripcion,
                                         NroCarpeta = cab.NroCarpeta,
                                         Periodo = cab.Periodo,
                                         Empresa = cab.ContratoEmpresas.Empresa,
                                         EsContratista = cab.ContratoEmpresas.EsContratista.Value,
                                         EsFueraTermino = cab.EsFueraTermino,
                                         IdCabeceraHojasDeRuta = cab.IdCabeceraHojasDeRuta,
                                         ConstratistaParaSubConstratista = "",
                                         Seguimiento = cab.colSeguimientoAuditoria
                                     }).ToList();


            var ResultadoFormateado = (from cab in ResultadoConsulta
                                       select new
                                       {
                                           Periodo = string.Format("{0:yyyy/MM}", cab.Periodo),
                                           NroContrato = cab.Codigo,
                                           FechaInicio = cab.FechaInicio.ToShortDateString(),
                                           FechaFin = cab.ContratoEmp.Contrato.Prorroga != null ? cab.ContratoEmp.Contrato.Prorroga.Value.ToShortDateString() : cab.ContratoEmp.Contrato.FechaVencimiento.Value.ToShortDateString(),
                                           Clasificacion = cab.ContratoEmp.Contrato.TipoContrato.Descripcion,
                                           Estado = cab.Estado,
                                           NroCarpeta = cab.NroCarpeta,
                                           Contratista = cab.Empresa.RazonSocial.ToUpper(),
                                           IdCabeceraHojasDeRuta = cab.IdCabeceraHojasDeRuta,
                                           ConstratistaParaSubConstratista = !cab.EsContratista ? ResultadoConsulta.Where(w => w.Codigo == cab.Codigo && w.EsContratista).FirstOrDefault().Empresa.RazonSocial : "",
                                           Auditor = cab.Seguimiento.Count() > 0 && cab.Seguimiento.Last().objAuditorAsignado != null ? cab.Seguimiento.Last().objAuditorAsignado.Login.ToString() : "-",
                                           SituacionAlCierre = cab.CabeceraHojasDeRuta.EstadoAlCierre == null || cab.CabeceraHojasDeRuta.EstadoAlCierre == "" ? "SIN PRESENTACION" : cab.CabeceraHojasDeRuta.EstadoAlCierre,

                                           //FechaRecepcion1 = cab.Seguimiento.Count() > 0 ? cab.Seguimiento.First().FechaRecepcion : fechaNula,
                                           //FechaAuditoria1 = cab.Seguimiento.Count() > 0 ? cab.Seguimiento.First().FechaResultado : fechaNula,
                                           //Resultado1 = cab.Seguimiento.Count() > 0 && cab.Seguimiento.First().objResultado != null ? cab.Seguimiento.First().objResultado.Descripcion : "",
                                           //FechaRetencion1 = cab.Seguimiento.Count() > 0 && cab.Seguimiento.First().FechaRetencion != null ? cab.Seguimiento.First().FechaRetencion : fechaNula,
                                           //Retencion1 = cab.Seguimiento.Count() > 0 && cab.Seguimiento.First().FechaRetencion != null ? cab.Seguimiento.First().Retencion.ToString() : "",

                                           //FechaRecepcion2 = cab.Seguimiento.Count() > 1 ? cab.Seguimiento.Skip(1).Take(1).First().FechaRecepcion : fechaNula,
                                           //FechaAuditoria2 = cab.Seguimiento.Count() > 1 ? cab.Seguimiento.Skip(1).Take(1).First().FechaResultado : fechaNula,
                                           //Resultado2 = cab.Seguimiento.Count() > 1 && cab.Seguimiento.Skip(1).Take(1).First().objResultado != null ? cab.Seguimiento.Skip(1).Take(1).First().objResultado.Descripcion : "",
                                           //FechaRetencion2 = cab.Seguimiento.Count() > 1 && cab.Seguimiento.Skip(1).Take(1).First().FechaRetencion != null ? cab.Seguimiento.Skip(1).Take(1).First().FechaRetencion : fechaNula,
                                           //Retencion2 = cab.Seguimiento.Count() > 1 && cab.Seguimiento.Skip(1).Take(1).First().FechaRetencion != null ? cab.Seguimiento.Skip(1).Take(1).First().Retencion.ToString() : "",

                                           //FechaRecepcion3 = cab.Seguimiento.Count() > 2 ? cab.Seguimiento.Skip(2).Take(1).First().FechaRecepcion : fechaNula,
                                           //FechaAuditoria3 = cab.Seguimiento.Count() > 2 ? cab.Seguimiento.Skip(2).Take(1).First().FechaResultado : fechaNula,
                                           //Resultado3 = cab.Seguimiento.Count() > 2 && cab.Seguimiento.Skip(2).Take(1).First().objResultado != null ? cab.Seguimiento.Skip(2).Take(1).First().objResultado.Descripcion : "",
                                           //FechaRetencion3 = cab.Seguimiento.Count() > 2 && cab.Seguimiento.Skip(2).Take(1).First().FechaRetencion != null ? cab.Seguimiento.Skip(2).Take(1).First().FechaRetencion : fechaNula,
                                           //Retencion3 = cab.Seguimiento.Count() > 2 && cab.Seguimiento.Skip(2).Take(1).First().FechaRetencion != null ? cab.Seguimiento.Skip(2).Take(1).First().Retencion.ToString() : "",


                                           FechaRecepcionUltima = cab.Seguimiento.Count() > 0 ? cab.Seguimiento.Last().FechaRecepcion.ToShortDateString() : "",
                                           FechaAuditoriaUltima = cab.Seguimiento.Count() > 0 && cab.Seguimiento.Last().FechaResultado != null ? cab.Seguimiento.Last().FechaResultado.Value.ToShortDateString() : "",
                                           ResultadoUltima = cab.Seguimiento.Count() > 0 && cab.Seguimiento.Last().objResultado != null ? cab.Seguimiento.Last().objResultado.Descripcion : "",
                                           FechaRetencionUltima = cab.Seguimiento.Count() > 0 && cab.Seguimiento.Last().FechaRetencion != null ? cab.Seguimiento.Last().FechaRetencion.Value.ToShortDateString() : "",
                                           RetencionUltima = cab.Seguimiento.Count() > 0 && cab.Seguimiento.Last().FechaRetencion != null ? cab.Seguimiento.Last().Retencion.ToString() : "",
                                           EstadoActualAuditoria = cab.Seguimiento.Count() == 0 ? "Sin Documentacion" : (cab.Seguimiento.Last().FechaResultado == null ? "En Proceso" : "Terminado")




                                       }).ToList();

            #endregion


            datos.Add("Hojas", ResultadoFormateado.ToList());


        }

        return datos;


    }


    [WebMethod]
    public List<dynamic> getReporteSeguimientoExcel()
    {
        Dictionary<string, object> datos = new Dictionary<string, object>();

        using (Entidades.EntidadesConosud dc = new Entidades.EntidadesConosud())
        {

            #region Busqueda de hojas para asignar auditor
            CabeceraHojasDeRuta hh = new CabeceraHojasDeRuta();
            int mes = DateTime.Now.AddMonths(-1).Month;
            int año = DateTime.Now.AddMonths(-1).Year;

            var ResultadoConsulta = (from cab in dc.CabeceraHojasDeRuta.Include("colSeguimientoAuditoria")
                                     where cab.Periodo.Month == mes && cab.Periodo.Year == año
                                    && cab.ContratoEmpresas.Empresa != null
                                    && cab.ContratoEmpresas.Contrato != null
                                     orderby cab.ContratoEmpresas.Empresa.RazonSocial
                                     , cab.ContratoEmpresas.Contrato.Codigo
                                     select new
                                     {
                                         CabeceraHojasDeRuta = cab,
                                         ContratoEmp = cab.ContratoEmpresas,
                                         Codigo = cab.ContratoEmpresas.Contrato.Codigo,
                                         FechaInicio = cab.ContratoEmpresas.Contrato.FechaInicio.Value,
                                         Estado = cab.Estado.Descripcion,
                                         NroCarpeta = cab.NroCarpeta,
                                         Periodo = cab.Periodo,
                                         Empresa = cab.ContratoEmpresas.Empresa,
                                         EsContratista = cab.ContratoEmpresas.EsContratista.Value,
                                         EsFueraTermino = cab.EsFueraTermino,
                                         IdCabeceraHojasDeRuta = cab.IdCabeceraHojasDeRuta,
                                         ConstratistaParaSubConstratista = "",
                                         Seguimiento = cab.colSeguimientoAuditoria
                                     }).ToList();


            List<dynamic> ResultadoFormateado = (from cab in ResultadoConsulta
                                                 select new
                                                 {
                                                     Periodo = string.Format("{0:yyyy/MM}", cab.Periodo),
                                                     NroContrato = cab.Codigo,
                                                     FechaInicio = cab.FechaInicio.ToShortDateString(),
                                                     FechaFin = cab.ContratoEmp.Contrato.Prorroga != null ? cab.ContratoEmp.Contrato.Prorroga.Value.ToShortDateString() : cab.ContratoEmp.Contrato.FechaVencimiento.Value.ToShortDateString(),
                                                     Clasificacion = cab.ContratoEmp.Contrato.TipoContrato.Descripcion,
                                                     Estado = cab.Estado,
                                                     NroCarpeta = cab.NroCarpeta,
                                                     Contratista = cab.Empresa.RazonSocial.ToUpper(),
                                                     IdCabeceraHojasDeRuta = cab.IdCabeceraHojasDeRuta,
                                                     ConstratistaParaSubConstratista = !cab.EsContratista ? ResultadoConsulta.Where(w => w.Codigo == cab.Codigo && w.EsContratista).FirstOrDefault().Empresa.RazonSocial : "",
                                                     Auditor = cab.Seguimiento.Count() > 0 && cab.Seguimiento.Last().objAuditorAsignado != null ? cab.Seguimiento.Last().objAuditorAsignado.Login.ToString() : "-",
                                                     SituacionAlCierre = cab.CabeceraHojasDeRuta.EstadoAlCierre == null || cab.CabeceraHojasDeRuta.EstadoAlCierre == "" ? "SIN PRESENTACION" : cab.CabeceraHojasDeRuta.EstadoAlCierre,

                                                     FechaRecepcion1 = cab.Seguimiento.Count() > 0 ? cab.Seguimiento.First().FechaRecepcion.ToShortDateString() : "",
                                                     FechaAuditoria1 = cab.Seguimiento.Count() > 0 && cab.Seguimiento.First().FechaResultado != null ? cab.Seguimiento.First().FechaResultado.Value.ToShortDateString() : "",
                                                     Resultado1 = cab.Seguimiento.Count() > 0 && cab.Seguimiento.First().objResultado != null ? cab.Seguimiento.First().objResultado.Descripcion : "",
                                                     FechaRetencion1 = cab.Seguimiento.Count() > 0 && cab.Seguimiento.First().FechaRetencion != null ? cab.Seguimiento.First().FechaRetencion.Value.ToShortDateString() : "",
                                                     Retencion1 = cab.Seguimiento.Count() > 0 && cab.Seguimiento.First().FechaRetencion != null ? cab.Seguimiento.First().Retencion.ToString() : "",

                                                     FechaRecepcion2 = cab.Seguimiento.Count() > 1 ? cab.Seguimiento.Skip(1).Take(1).First().FechaRecepcion.ToShortDateString() : "",
                                                     FechaAuditoria2 = cab.Seguimiento.Count() > 1 && cab.Seguimiento.Skip(1).Take(1).First().FechaResultado != null ? cab.Seguimiento.Skip(1).Take(1).First().FechaResultado.Value.ToShortDateString() : "",
                                                     Resultado2 = cab.Seguimiento.Count() > 1 && cab.Seguimiento.Skip(1).Take(1).First().objResultado != null ? cab.Seguimiento.Skip(1).Take(1).First().objResultado.Descripcion : "",
                                                     FechaRetencion2 = cab.Seguimiento.Count() > 1 && cab.Seguimiento.Skip(1).Take(1).First().FechaRetencion != null ? cab.Seguimiento.Skip(1).Take(1).First().FechaRetencion.Value.ToShortDateString() : "",
                                                     Retencion2 = cab.Seguimiento.Count() > 1 && cab.Seguimiento.Skip(1).Take(1).First().FechaRetencion != null ? cab.Seguimiento.Skip(1).Take(1).First().Retencion.ToString() : "",

                                                     FechaRecepcion3 = cab.Seguimiento.Count() > 2 ? cab.Seguimiento.Skip(2).Take(1).First().FechaRecepcion.ToShortDateString() : "",
                                                     FechaAuditoria3 = cab.Seguimiento.Count() > 2 && cab.Seguimiento.Skip(2).Take(1).First().FechaResultado != null ? cab.Seguimiento.Skip(2).Take(1).First().FechaResultado.Value.ToShortDateString() : "",
                                                     Resultado3 = cab.Seguimiento.Count() > 2 && cab.Seguimiento.Skip(2).Take(1).First().objResultado != null ? cab.Seguimiento.Skip(2).Take(1).First().objResultado.Descripcion : "",
                                                     FechaRetencion3 = cab.Seguimiento.Count() > 2 && cab.Seguimiento.Skip(2).Take(1).First().FechaRetencion != null ? cab.Seguimiento.Skip(2).Take(1).First().FechaRetencion.Value.ToShortDateString() : "",
                                                     Retencion3 = cab.Seguimiento.Count() > 2 && cab.Seguimiento.Skip(2).Take(1).First().FechaRetencion != null ? cab.Seguimiento.Skip(2).Take(1).First().Retencion.ToString() : "",


                                                     FechaRecepcionUltima = cab.Seguimiento.Count() > 0 ? cab.Seguimiento.Last().FechaRecepcion.ToShortDateString() : "",
                                                     FechaAuditoriaUltima = cab.Seguimiento.Count() > 0 && cab.Seguimiento.Last().FechaResultado != null ? cab.Seguimiento.Last().FechaResultado.Value.ToShortDateString() : "",
                                                     ResultadoUltima = cab.Seguimiento.Count() > 0 && cab.Seguimiento.Last().objResultado != null ? cab.Seguimiento.Last().objResultado.Descripcion : "",
                                                     FechaRetencionUltima = cab.Seguimiento.Count() > 0 && cab.Seguimiento.Last().FechaRetencion != null ? cab.Seguimiento.Last().FechaRetencion.Value.ToShortDateString() : "",
                                                     RetencionUltima = cab.Seguimiento.Count() > 0 && cab.Seguimiento.Last().FechaRetencion != null ? cab.Seguimiento.Last().Retencion.ToString() : "",
                                                     EstadoActualAuditoria = cab.Seguimiento.Count() == 0 ? "Sin Documentacion" : (cab.Seguimiento.Last().FechaResultado == null ? "En Proceso" : "Terminado")




                                                 }).ToList<dynamic>();

            #endregion


            return ResultadoFormateado;

        }



    }


    [WebMethod(EnableSession = true)]
    public object getMenues()
    {
        Dictionary<string, object> datos = new Dictionary<string, object>();

        using (Entidades.EntidadesConosud dcAux = new Entidades.EntidadesConosud())
        {

            //this.lblNombreUsu.Text = Convert.ToString(this.Session["nombreusu"]);

            long IdSegUsuario = (long)HttpContext.Current.Session["idusu"];
            //long IdSegUsuario = 8;

            Entidades.SegUsuario usu = (from us in dcAux.SegUsuario
                                        .Include("SegUsuarioRol.SegRol.SegRolMenu.SegMenu.Padre")
                                        where us.IdSegUsuario == IdSegUsuario
                                        select us).First<Entidades.SegUsuario>();

            List<Entidades.SegMenu> menues = new List<Entidades.SegMenu>();

            foreach (Entidades.SegUsuarioRol UsuRol in usu.SegUsuarioRol)
            {
                foreach (Entidades.SegRolMenu confseg in UsuRol.SegRol.SegRolMenu)
                {
                    if (menues.FindAll(d => d.IdSegMenu == confseg.SegMenu.IdSegMenu).Count == 0)
                    {
                        menues.Add(confseg.SegMenu);
                    }
                }
            }

            var resultado = (from M in menues
                             orderby M.Posicion
                             select new
                             {
                                 M.IdSegMenu,
                                 M.Descripcion,
                                 M.Target,
                                 M.IdPadre,
                                 Url = M.Url != null ? M.Url.Replace("~/", "") : ""
                             }).ToList();

            datos.Add("Menu", resultado);

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
