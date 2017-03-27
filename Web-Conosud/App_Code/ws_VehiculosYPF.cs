using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Entidades;
using System.Web.UI.WebControls;
using System.Web.UI;

/// <summary>
/// Summary description for ws_VehiculosYPF
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ws_VehiculosYPF : System.Web.Services.WebService
{

    public ws_VehiculosYPF()
    {

    }


    [WebMethod]
    public bool GrabarVehiculo(IDictionary<string, object> vehiculo)
    {
        DateTime? fechaNula = null;

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            VehiculosYPF current = null;

            if (vehiculo.ContainsKey("Id"))
            {
                long id = long.Parse(vehiculo["Id"].ToString());
                current = (from v in dc.VehiculosYPF
                           where v.Id == id
                           select v).FirstOrDefault();
            }
            else
            {
                current = new VehiculosYPF();
                dc.AddToVehiculosYPF(current);
            }

            current.Patente = vehiculo["Patente"].ToString();
            current.Modelo = vehiculo["Modelo"].ToString();

            if (vehiculo.ContainsKey("IdDepartamento") && vehiculo["IdDepartamento"] != null)
                current.Departamento = long.Parse(vehiculo["IdDepartamento"].ToString());

            if (vehiculo.ContainsKey("IdSector") && vehiculo["IdSector"] != null)
                current.Sector = long.Parse(vehiculo["IdSector"].ToString());


            current.Titular = vehiculo["Titular"].ToString();

            if (vehiculo.ContainsKey("Responsable") && vehiculo["Responsable"] != null)
                current.Responsable = vehiculo["Responsable"].ToString();

            current.Combustible = long.Parse(vehiculo["IdTipoCombustible"].ToString());

            if (vehiculo.ContainsKey("IdTipoAsignacion") && vehiculo["IdTipoAsignacion"] != null)
                current.TipoAsignacion = long.Parse(vehiculo["IdTipoAsignacion"].ToString());

            if (vehiculo.ContainsKey("CentroCosto") && vehiculo["CentroCosto"] != null)
                current.CentroCosto = vehiculo["CentroCosto"].ToString();

            current.FechaVtoTarjVerde = vehiculo["VtoTarjVerde"].ToString() != null ? Convert.ToDateTime(vehiculo["VtoTarjVerde"].ToString()) : fechaNula;
            current.FechaVtoRevTecnica = vehiculo.ContainsKey("VtoRevTecnica") && vehiculo["VtoRevTecnica"] != null ? Convert.ToDateTime(vehiculo["VtoRevTecnica"].ToString()) : fechaNula;
            current.VelocimetroFecha = vehiculo.ContainsKey("VelocimetroFecha") && vehiculo["VelocimetroFecha"] != null ? Convert.ToDateTime(vehiculo["VelocimetroFecha"].ToString()) : fechaNula;

            if (vehiculo.ContainsKey("Contrato") && vehiculo["Contrato"] != null)
                current.Contrato = vehiculo["Contrato"].ToString();

            if (vehiculo.ContainsKey("NroTarjeta") && vehiculo["NroTarjeta"] != null)
                current.NroTarjeta = vehiculo["NroTarjeta"].ToString();

            if (vehiculo.ContainsKey("VelocimetroOdometro") && vehiculo["VelocimetroOdometro"] != null)
                current.VelocimetroOdometro = vehiculo["VelocimetroOdometro"].ToString();

            current.Año = vehiculo["Anio"].ToString();

            if (vehiculo.ContainsKey("Observacion") && vehiculo["Observacion"] != null)
                current.Observacion = vehiculo["Observacion"].ToString();


            if (vehiculo.ContainsKey("RazonSocial") && vehiculo["RazonSocial"] != null)
                current.RazonSocial = vehiculo["RazonSocial"].ToString();

            if (vehiculo.ContainsKey("TarjetasActivas") && vehiculo["TarjetasActivas"] != null)
                current.TarjetasActivas = int.Parse(vehiculo["TarjetasActivas"].ToString());

            if (vehiculo.ContainsKey("LimiteCredito") && vehiculo["LimiteCredito"] != null)
                current.LimiteCredito = int.Parse(vehiculo["LimiteCredito"].ToString());

            if (vehiculo.ContainsKey("PIN") && vehiculo["PIN"] != null && vehiculo["PIN"].ToString() != "")
                current.PIN = int.Parse(vehiculo["PIN"].ToString());
            else
                current.PIN = null;


            if (vehiculo.ContainsKey("TitularPin") && vehiculo["TitularPin"] != null)
                current.TitularPin = vehiculo["TitularPin"].ToString();


            if (vehiculo.ContainsKey("PIN1") && vehiculo["PIN1"] != null && vehiculo["PIN1"].ToString() != "")
                current.PIN1 = int.Parse(vehiculo["PIN1"].ToString());
            else
                current.PIN1 = null;

            if (vehiculo.ContainsKey("TitularPin1") && vehiculo["TitularPin1"] != null)
                current.TitularPin1 = vehiculo["TitularPin1"].ToString();


            if (vehiculo.ContainsKey("PIN2") && vehiculo["PIN2"] != null && vehiculo["PIN2"].ToString() != "")
                current.PIN2 = int.Parse(vehiculo["PIN2"].ToString());
            else
                current.PIN2 = null;

            if (vehiculo.ContainsKey("TitularPin2") && vehiculo["TitularPin2"] != null)
                current.TitularPin2 = vehiculo["TitularPin2"].ToString();



            if (vehiculo.ContainsKey("PIN3") && vehiculo["PIN3"] != null && vehiculo["PIN3"].ToString() != "")
                current.PIN3 = int.Parse(vehiculo["PIN3"].ToString());
            else
                current.PIN3 = null;

            if (vehiculo.ContainsKey("TitularPin3") && vehiculo["TitularPin3"] != null)
                current.TitularPin3 = vehiculo["TitularPin3"].ToString();



            if (vehiculo.ContainsKey("PIN4") && vehiculo["PIN4"] != null && vehiculo["PIN4"].ToString() != "")
                current.PIN4 = int.Parse(vehiculo["PIN4"].ToString());
            else
                current.PIN4 = null;


            if (vehiculo.ContainsKey("TitularPin4") && vehiculo["TitularPin4"] != null)
                current.TitularPin4 = vehiculo["TitularPin4"].ToString();


            if (vehiculo.ContainsKey("PIN5") && vehiculo["PIN5"] != null && vehiculo["PIN5"].ToString() != "")
                current.PIN5 = int.Parse(vehiculo["PIN5"].ToString());
            else
                current.PIN5 = null;


            if (vehiculo.ContainsKey("TitularPin5") && vehiculo["TitularPin5"] != null)
                current.TitularPin5 = vehiculo["TitularPin5"].ToString();


            if (vehiculo.ContainsKey("PIN6") && vehiculo["PIN6"] != null && vehiculo["PIN6"].ToString() != "")
                current.PIN6 = int.Parse(vehiculo["PIN6"].ToString());
            else
                current.PIN6 = null;


            if (vehiculo.ContainsKey("TitularPin6") && vehiculo["TitularPin6"] != null)
                current.TitularPin6 = vehiculo["TitularPin6"].ToString();

            if (vehiculo.ContainsKey("PIN7") && vehiculo["PIN7"] != null && vehiculo["PIN7"].ToString() != "")
                current.PIN7 = int.Parse(vehiculo["PIN7"].ToString());
            else
                current.PIN7 = null;


            if (vehiculo.ContainsKey("TitularPin7") && vehiculo["TitularPin7"] != null)
                current.TitularPin7 = vehiculo["TitularPin7"].ToString();


            if (vehiculo.ContainsKey("IdEstado") && vehiculo["IdEstado"] != null)
                current.Estado = long.Parse(vehiculo["IdEstado"].ToString());

            if (vehiculo.ContainsKey("IdTipoVehiculo") && vehiculo["IdTipoVehiculo"] != null)
                current.TipoVehiculo = long.Parse(vehiculo["IdTipoVehiculo"].ToString());


            if (vehiculo.ContainsKey("Chasis") && vehiculo["Chasis"] != null)
                current.Chasis = vehiculo["Chasis"].ToString();

            if (vehiculo.ContainsKey("Motor") && vehiculo["Motor"] != null)
                current.Motor = vehiculo["Motor"].ToString();

            if (vehiculo.ContainsKey("ABS") && vehiculo["ABS"] != null)
                current.ABS = bool.Parse(vehiculo["ABS"].ToString());

            if (vehiculo.ContainsKey("AIRBAGS") && vehiculo["AIRBAGS"] != null)
                current.AIRBAGS = bool.Parse(vehiculo["AIRBAGS"].ToString());

            if (vehiculo.ContainsKey("Llave") && vehiculo["Llave"] != null)
                current.Llave = bool.Parse(vehiculo["Llave"].ToString());

            if (vehiculo.ContainsKey("ControlAlarma") && vehiculo["ControlAlarma"] != null)
                current.ControlAlarma = bool.Parse(vehiculo["ControlAlarma"].ToString());

            if (vehiculo.ContainsKey("LlaveAlarma") && vehiculo["LlaveAlarma"] != null)
                current.LlaveAlarma = bool.Parse(vehiculo["LlaveAlarma"].ToString());

            if (vehiculo.ContainsKey("Posicion") && vehiculo["Posicion"] != null)
                current.Posicion = vehiculo["Posicion"].ToString();

            if (vehiculo.ContainsKey("LimiteConsMensual") && vehiculo["LimiteConsMensual"] != null)
                current.LimiteConsMensual = int.Parse(vehiculo["LimiteConsMensual"].ToString());

            if (vehiculo.ContainsKey("MICROTRACK") && vehiculo["MICROTRACK"] != null)
                current.MICROTRACK = bool.Parse(vehiculo["MICROTRACK"].ToString());

            dc.SaveChanges();
        }
        return true;
    }

    [WebMethod]
    public bool BajaVehiculo(long Id)
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {

            var vehiculo = (from v in dc.VehiculosYPF
                            where v.Id == Id
                            select v).FirstOrDefault();


            if (vehiculo != null)
            {

                vehiculo.FechaBaja = DateTime.Now;
                dc.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }



    }

    [WebMethod]
    public object filtrarVehiculos(string nroPatente)
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            long? nullValue = null;

            var datos = (from v in dc.VehiculosYPF
                         where v.Patente.Contains(nroPatente)
                         select new
                         {
                             v.Id,
                             v.Patente,
                             v.Modelo,
                             Departamento = v.objDepartamento,
                             Sector = v.objSector,
                             TipoCombustible = v.objTipoCombustible,
                             TipoAsignacion = v.objTipoAsignacion,
                             v.Titular,
                             v.FechaBaja,
                             CentroCosto = v.CentroCosto,
                             VtoTarjVerde = v.FechaVtoTarjVerde,
                             VtoRevTecnica = v.FechaVtoRevTecnica,
                             VelocimetroFecha = v.VelocimetroFecha,
                             v.Contrato,
                             v.NroTarjeta,
                             v.VelocimetroOdometro,
                             v.Año,
                             v.Observacion,
                             v.RazonSocial,
                             v.TarjetasActivas,
                             v.LimiteCredito,
                             v.PIN,
                             v.TitularPin,
                             v.Responsable,
                             v.PIN1,
                             v.TitularPin1,
                             v.PIN2,
                             v.TitularPin2,
                             v.PIN3,
                             v.TitularPin3,
                             v.PIN4,
                             v.TitularPin4,
                             v.PIN5,
                             v.TitularPin5,
                             v.PIN6,
                             v.TitularPin6,
                             v.PIN7,
                             v.TitularPin7,
                             Estado = v.objEstado,
                             TipoVehiculo = v.objTipoVehiculo,
                             Chasis = v.Chasis,
                             Motor = v.Motor,
                             ABS = v.ABS,
                             AIRBAGS = v.AIRBAGS,
                             MICROTRACK = v.MICROTRACK,
                             v.Llave,
                             v.ControlAlarma,
                             v.LlaveAlarma,
                             v.Posicion,
                             v.LimiteConsMensual

                         }).Take(25).ToList();


            return (from v in datos
                    select new
                    {
                        v.Id,
                        v.Patente,
                        v.Modelo,

                        IdDepartamento = v.Departamento != null ? v.Departamento.IdClasificacion : nullValue,
                        Departamento = v.Departamento != null ? v.Departamento.Descripcion : "",

                        IdSector = v.Sector != null ? v.Sector.IdClasificacion : nullValue,
                        Sector = v.Sector != null ? v.Sector.Descripcion : "",

                        IdTipoAsignacion = v.TipoAsignacion != null ? v.TipoAsignacion.IdClasificacion : nullValue,
                        TipoAsignacion = v.TipoAsignacion != null ? v.TipoAsignacion.Descripcion : "",

                        IdTipoCombustible = v.TipoCombustible != null ? v.TipoCombustible.IdClasificacion : nullValue,
                        TipoCombustible = v.TipoCombustible != null ? v.TipoCombustible.Descripcion : "",

                        IdEstado = v.Estado != null ? v.Estado.IdClasificacion : nullValue,
                        Estado = v.Estado != null ? v.Estado.Descripcion : "",

                        IdTipoVehiculo = v.TipoVehiculo != null ? v.TipoVehiculo.IdClasificacion : nullValue,
                        TipoVehiculo = v.TipoVehiculo != null ? v.TipoVehiculo.Descripcion : "",

                        v.Titular,
                        FechaBaja = string.Format("{0:dd/MM/yyyy}", v.FechaBaja),
                        v.CentroCosto,
                        VtoTarjVerde = string.Format("{0:dd/MM/yyyy}", v.VtoTarjVerde),
                        VtoRevTecnica = string.Format("{0:dd/MM/yyyy}", v.VtoRevTecnica),
                        VelocimetroFecha = string.Format("{0:dd/MM/yyyy}", v.VelocimetroFecha),
                        v.Contrato,
                        v.NroTarjeta,
                        v.VelocimetroOdometro,
                        Anio = v.Año,
                        v.Observacion,
                        v.RazonSocial,
                        v.TarjetasActivas,
                        v.LimiteCredito,
                        v.PIN,
                        v.TitularPin,
                        v.Responsable,
                        v.PIN1,
                        v.TitularPin1,
                        v.PIN2,
                        v.TitularPin2,
                        v.PIN3,
                        v.TitularPin3,
                        v.PIN4,
                        v.TitularPin4,
                        v.PIN5,
                        v.TitularPin5,
                        v.PIN6,
                        v.TitularPin6,
                        v.PIN7,
                        v.TitularPin7,
                        Chasis = v.Chasis,
                        Motor = v.Motor,
                        ABS = v.ABS == null ? false : v.ABS,
                        AIRBAGS = v.AIRBAGS == null ? false : v.AIRBAGS,
                        MICROTRACK = v.MICROTRACK == null ? false : v.MICROTRACK,
                        Llave = v.Llave == null ? false : v.Llave,
                        ControlAlarma = v.ControlAlarma == null ? false : v.ControlAlarma,
                        LlaveAlarma = v.LlaveAlarma == null ? false : v.LlaveAlarma,
                        Posicion = v.Posicion,
                        LimiteConsMensual = v.LimiteConsMensual,


                    }).ToList();



        }

    }

    [WebMethod]
    public object getVehiculos()
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            long? nullValue = null;

            var datos = (from v in dc.VehiculosYPF
                         select new
                         {
                             v.Id,
                             v.Patente,
                             v.Modelo,
                             Departamento = v.objDepartamento,
                             Sector = v.objSector,
                             TipoCombustible = v.objTipoCombustible,
                             TipoAsignacion = v.objTipoAsignacion,
                             v.Titular,
                             v.Responsable,
                             v.FechaBaja,
                             CentroCosto = v.CentroCosto,
                             VtoTarjVerde = v.FechaVtoTarjVerde,
                             VtoRevTecnica = v.FechaVtoRevTecnica,
                             VelocimetroFecha = v.VelocimetroFecha,
                             v.Contrato,
                             v.NroTarjeta,
                             v.VelocimetroOdometro,
                             v.Año,
                             v.Observacion,
                             v.RazonSocial,
                             v.TarjetasActivas,
                             v.LimiteCredito,
                             v.PIN,
                             v.TitularPin,
                             v.PIN1,
                             v.TitularPin1,
                             v.PIN2,
                             v.TitularPin2,
                             v.PIN3,
                             v.TitularPin3,
                             v.PIN4,
                             v.TitularPin4,
                             v.PIN5,
                             v.TitularPin5,
                             v.PIN6,
                             v.TitularPin6,
                             v.PIN7,
                             v.TitularPin7,
                             Estado = v.objEstado,
                             TipoVehiculo = v.objTipoVehiculo,
                             Chasis = v.Chasis,
                             Motor = v.Motor,
                             ABS = v.ABS,
                             AIRBAGS = v.AIRBAGS,
                             MICROTRACK = v.MICROTRACK,
                             v.Llave,
                             v.ControlAlarma,
                             v.LlaveAlarma,
                             v.Posicion,
                             v.LimiteConsMensual

                         }).Take(10).ToList();


            return (from v in datos
                    select new
                    {
                        v.Id,
                        v.Patente,
                        v.Modelo,

                        IdDepartamento = v.Departamento != null ? v.Departamento.IdClasificacion : nullValue,
                        Departamento = v.Departamento != null ? v.Departamento.Descripcion : "",

                        IdSector = v.Sector != null ? v.Sector.IdClasificacion : nullValue,
                        Sector = v.Sector != null ? v.Sector.Descripcion : "",

                        IdTipoAsignacion = v.TipoAsignacion != null ? v.TipoAsignacion.IdClasificacion : nullValue,
                        TipoAsignacion = v.TipoAsignacion != null ? v.TipoAsignacion.Descripcion : "",

                        IdTipoCombustible = v.TipoCombustible != null ? v.TipoCombustible.IdClasificacion : nullValue,
                        TipoCombustible = v.TipoCombustible != null ? v.TipoCombustible.Descripcion : "",

                        IdEstado = v.Estado != null ? v.Estado.IdClasificacion : nullValue,
                        Estado = v.Estado != null ? v.Estado.Descripcion : "",

                        IdTipoVehiculo = v.TipoVehiculo != null ? v.TipoVehiculo.IdClasificacion : nullValue,
                        TipoVehiculo = v.TipoVehiculo != null ? v.TipoVehiculo.Descripcion : "",

                        Chasis = v.Chasis,
                        Motor = v.Motor,
                        ABS = v.ABS == null ? false : v.ABS,
                        AIRBAGS = v.AIRBAGS == null ? false : v.AIRBAGS,
                        MICROTRACK = v.MICROTRACK == null ? false : v.MICROTRACK,
                        Llave = v.Llave == null ? false : v.Llave,
                        ControlAlarma = v.ControlAlarma == null ? false : v.ControlAlarma,
                        LlaveAlarma = v.LlaveAlarma == null ? false : v.LlaveAlarma,
                        Posicion = v.Posicion,
                        LimiteConsMensual = v.LimiteConsMensual,

                        v.Titular,
                        v.Responsable,
                        FechaBaja = string.Format("{0:dd/MM/yyyy}", v.FechaBaja),
                        v.CentroCosto,
                        VtoTarjVerde = string.Format("{0:dd/MM/yyyy}", v.VtoTarjVerde),
                        VtoRevTecnica = string.Format("{0:dd/MM/yyyy}", v.VtoRevTecnica),
                        VelocimetroFecha = string.Format("{0:dd/MM/yyyy}", v.VelocimetroFecha),
                        v.Contrato,
                        v.NroTarjeta,
                        v.VelocimetroOdometro,
                        Anio = v.Año,
                        v.Observacion,
                        v.RazonSocial,
                        v.TarjetasActivas,
                        v.LimiteCredito,
                        v.PIN,
                        v.TitularPin,
                        v.PIN1,
                        v.TitularPin1,
                        v.PIN2,
                        v.TitularPin2,
                        v.PIN3,
                        v.TitularPin3,
                        v.PIN4,
                        v.TitularPin4,
                        v.PIN5,
                        v.TitularPin5,
                        v.PIN6,
                        v.TitularPin6,
                        v.PIN7,
                        v.TitularPin7

                    }).ToList();

        }

    }

    [WebMethod]
    public object getContextoClasificaciones()
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {

            var tipos = (from c in dc.Clasificacion
                         where c.Tipo == Helpers.Constantes.ContextoVehiculosYPF
                         select c).FirstOrDefault().Hijos.Select(c => c.Tipo).Distinct().ToList();


            return (from c in dc.Clasificacion
                    where tipos.Contains(c.Tipo)
                    select new
                    {
                        Id = c.IdClasificacion,
                        Descripcion = c.Descripcion,
                        Tipo = c.Tipo
                    }).Distinct().ToList();

        }

    }

    [WebMethod]
    public List<dynamic> getExportacion()
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            long? nullValue = null;

            var datos = (from v in dc.VehiculosYPF
                         select new
                         {
                             v.Id,
                             v.Patente,
                             v.Modelo,
                             Departamento = v.objDepartamento,
                             Sector = v.objSector,
                             TipoCombustible = v.objTipoCombustible,
                             TipoAsignacion = v.objTipoAsignacion,
                             v.Titular,
                             v.Responsable,
                             v.FechaBaja,
                             CentroCosto = v.CentroCosto,
                             VtoTarjVerde = v.FechaVtoTarjVerde,
                             VtoRevTecnica = v.FechaVtoRevTecnica,
                             VelocimetroFecha = v.VelocimetroFecha,
                             v.Contrato,
                             v.NroTarjeta,
                             v.VelocimetroOdometro,
                             v.Año,
                             v.Observacion,
                             v.RazonSocial,
                             v.TarjetasActivas,
                             v.LimiteCredito,
                             v.PIN,
                             v.TitularPin,
                             v.PIN1,
                             v.TitularPin1,
                             v.PIN2,
                             v.TitularPin2,
                             v.PIN3,
                             v.TitularPin3,
                             v.PIN4,
                             v.TitularPin4,
                             v.PIN5,
                             v.TitularPin5,
                             v.PIN6,
                             v.TitularPin6,
                             v.PIN7,
                             v.TitularPin7,
                             Estado = v.objEstado,
                             TipoVehiculo = v.objTipoVehiculo,
                             Chasis = v.Chasis,
                             Motor = v.Motor,
                             ABS = v.ABS,
                             AIRBAGS = v.AIRBAGS,
                             MICROTRACK = v.MICROTRACK,
                             v.Llave,
                             v.ControlAlarma,
                             v.LlaveAlarma,
                             v.Posicion,
                             v.LimiteConsMensual


                         }).ToList();



            List<dynamic> datosExportar = (from v in datos
                                           select new
                                           {
                                               Patente = v.Patente,
                                               Modelo = v.Modelo,
                                               Año = v.Año,
                                               TipoCombustible = v.TipoCombustible != null ? v.TipoCombustible.Descripcion : "",
                                               Chasis = v.Chasis,
                                               Motor = v.Motor,
                                               ABS = v.ABS == null || v.ABS == false ? "NO" : "SI",
                                               AIRBAGS = v.AIRBAGS == null || v.AIRBAGS == false ? "NO" : "SI",
                                               MICROTRACK = v.MICROTRACK == null || v.MICROTRACK == false ? "NO" : "SI",
                                               Estado = v.Estado != null ? v.Estado.Descripcion : "",
                                               VtoRevTecnica = v.VtoRevTecnica.ToString(),
                                               VtoTarjVerde = v.VtoTarjVerde.ToString(),
                                               TipoVehiculo = v.TipoVehiculo != null ? v.TipoVehiculo.Descripcion : "",
                                               VelocimetroFecha = v.VelocimetroFecha.ToString(),
                                               VelocimetroOdometro = v.VelocimetroOdometro,

                                               Llave = v.Llave == null || v.Llave == false ? "NO" : "SI",
                                               ControlAlarma = v.ControlAlarma == null || v.ControlAlarma == false ? "NO" : "SI",
                                               LlaveAlarma = v.LlaveAlarma == null || v.LlaveAlarma == false ? "NO" : "SI",


                                               Departamento = v.Departamento != null ? v.Departamento.Descripcion : "",
                                               Sector = v.Sector != null ? v.Sector.Descripcion : "",
                                               Responsable = v.Responsable,
                                               Posicion = v.Posicion,
                                               Titular = v.Titular,
                                               TipoAsignacion = v.TipoAsignacion != null ? v.TipoAsignacion.Descripcion : "",


                                               RazonSocial = v.RazonSocial,
                                               NroTarjeta = v.NroTarjeta,
                                               Contrato = v.Contrato,
                                               CentroCosto = v.CentroCosto,
                                               TarjetasActivas = v.TarjetasActivas.ToString(),
                                               LimiteCredito = v.LimiteCredito.ToString(),
                                               LimiteConsMensual = v.LimiteConsMensual.ToString(),


                                               PIN = v.PIN.ToString(),
                                               TitularPin = v.TitularPin,
                                               PIN1 = v.PIN1.ToString(),
                                               TitularPin1 = v.TitularPin1,
                                               PIN2 = v.PIN2.ToString(),
                                               TitularPin2 = v.TitularPin2,
                                               PIN3 = v.PIN3.ToString(),
                                               TitularPin3 = v.TitularPin3,
                                               PIN4 = v.PIN4.ToString(),
                                               TitularPin4 = v.TitularPin4,
                                               PIN5 = v.PIN5.ToString(),
                                               TitularPin5 = v.TitularPin5,
                                               PIN6 = v.PIN6.ToString(),
                                               TitularPin6 = v.TitularPin6,
                                               PIN7 = v.PIN7.ToString(),
                                               TitularPin7 = v.TitularPin7,
                                               Observacion = v.Observacion

                                           }).ToList<dynamic>();


            return datosExportar;
        }


    }


    [WebMethod]
    public object getContratistas(string nombre)
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {

            return (from c in dc.Empresa
                    where c.RazonSocial.Contains(nombre)
                    select new
                    {
                        Id = c.IdEmpresa,
                        Nombre = c.RazonSocial,
                    }).ToList();
        }

    }

    [WebMethod]
    public object getContratos(long IdEmpresa)
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {

            return (from c in dc.ContratoEmpresas
                    where c.Empresa.IdEmpresa == IdEmpresa
                    select new
                    {
                        Id = c.Contrato.IdContrato,
                        Codigo = c.Contrato.Codigo,
                    }).ToList();
        }

    }

}
