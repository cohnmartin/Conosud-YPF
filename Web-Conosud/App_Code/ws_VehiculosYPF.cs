﻿using System;
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
                             v.TitularPin7

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
                        v.TitularPin7


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
                             v.TitularPin7
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

            return (from c in dc.Clasificacion
                    where c.Tipo == Helpers.Constantes.ContextoVehiculosYPF
                    select c).FirstOrDefault().Hijos.Select(c => new
                    {
                        Id = c.IdClasificacion,
                        Descripcion = c.Descripcion,
                        Tipo = c.Tipo
                    }).ToList();
        }

    }

    [WebMethod]
    public List<vehiculosYpfTemp> getExportacion()
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
                             v.TitularPin7


                         }).ToList();



            List<vehiculosYpfTemp> datosExportar = (from v in datos
                                                    select new vehiculosYpfTemp
                                                    {
                                                        Patente = v.Patente,
                                                        Modelo = v.Modelo,
                                                        Departamento = v.Departamento != null ? v.Departamento.Descripcion : "",

                                                        TipoCombustible = v.TipoCombustible != null ? v.TipoCombustible.Descripcion : "",
                                                        TipoAsignacion = v.TipoAsignacion != null ? v.TipoAsignacion.Descripcion : "",
                                                        Sector = v.Sector != null ? v.Sector.Descripcion : "",
                                                        Responsable = v.Responsable,
                                                        Titular = v.Titular,
                                                        CentroCosto = v.CentroCosto,
                                                        VtoTarjVerde = v.VtoTarjVerde.ToString(),
                                                        VtoRevTecnica = v.VtoRevTecnica.ToString(),
                                                        VelocimetroFecha = v.VelocimetroFecha.ToString(),
                                                        Contrato = v.Contrato,
                                                        NroTarjeta = v.NroTarjeta,
                                                        VelocimetroOdometro = v.VelocimetroOdometro,
                                                        Año = v.Año,
                                                        RazonSocial = v.RazonSocial,
                                                        TarjetasActivas = v.TarjetasActivas.ToString(),
                                                        LimiteCredito = v.LimiteCredito.ToString(),
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

                                                    }).ToList<vehiculosYpfTemp>();


            return datosExportar;
        }


    }
}
