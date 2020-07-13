using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Entidades;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Descripción breve de ws_Rutas
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
[System.Web.Script.Services.ScriptService]
public class ws_Rutas : System.Web.Services.WebService
{
    private const string CLAVE_BASE = "e10adc3949ba59abbe56e057f20f883e";

    public ws_Rutas()
    {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public object getRutas()
    {



        using (EntidadesConosud dc = new EntidadesConosud())
        {
            long IdEmpresa = 0;
            if (Session["TipoUsuario"].ToString() == "Cliente")
            {
                IdEmpresa = long.Parse(Session["IdEmpresaContratista"].ToString());
                return (from c in dc.CabeceraRutasTransportes
                        where c.TipoTurno != "Temporal " && c.DestinoRuta == IdEmpresa
                        orderby c.Empresa, c.Linea, c.TipoTurno, c.TipoRecorrido
                        select new
                        {
                            Id = c.Id,
                            Descripcion = c.Empresa + " - LINEA " + c.Linea + " - " + c.TipoTurno + " - " + c.TipoRecorrido + " " + c.EmpresaDestinoRuta.RazonSocial,
                            Selected = false
                        }).ToList();

            }
            else
            {
                return (from c in dc.CabeceraRutasTransportes
                        where c.TipoTurno != "Temporal "
                        orderby c.Empresa, c.Linea, c.TipoTurno, c.TipoRecorrido
                        select new
                        {
                            Id = c.Id,
                            Descripcion = c.Empresa + " - LINEA " + c.Linea + " - " + c.TipoTurno + " - " + c.TipoRecorrido + " " + c.EmpresaDestinoRuta.RazonSocial,
                            Selected = false
                        }).ToList();

            }




        }

    }

    [WebMethod]
    public object getRecorrido(string ruta)
    {
        using (EntidadesConosud dc = new EntidadesConosud())
        {
            long idRuta = long.Parse(ruta);
            string gRes = "";
            ToExcelIndexCol((int)idRuta, ref gRes);

            var recorrido = (from r in dc.CabeceraRutasTransportes
                             where r.Id == idRuta
                             select new
                             {
                                 recorrido = r.RutasTransportes.Select(w => new { w.Latitud, w.Longitud }),
                                 Empresa = r.Empresa,
                                 TipoVehiculo = r.TipoUnidad,
                                 Turno = r.TipoTurno,
                                 Horario = r.HorariosSalida + " - " + r.HorariosLlegada,
                                 HorarioSalida = r.HorariosSalida,
                                 HorarioLlegada = r.HorariosLlegada,
                                 TipoRecorrido = r.TipoRecorrido,
                                 Qr = r.Empresa.Substring(0, 3) + r.TipoTurno.Substring(0, 1) + gRes,
                                 idRecorrido = r.Id

                             }).FirstOrDefault();

            return recorrido;

        }

    }


    [WebMethod]
    public object LoginApp(string usuario, string clave)
    {
        using (EntidadesConosud dc = new EntidadesConosud())
        {
            Dictionary<string, object> datos = new Dictionary<string, object>();

            var legajos = (from d in dc.DomiciliosPersonal.Include("CabeceraRutas")
                           where d.Legajo == usuario
                           orderby d.NombreLegajo
                           select new
                           {
                               d.Distrito,
                               d.Domicilio,
                               d.Id,
                               d.Latitud,
                               d.LatitudReposicion,
                               d.Legajo,
                               d.LineaAsignada,
                               d.LineaAsignadaVuelta,
                               d.Longitud,
                               d.LongitudReposicion,
                               d.NombreLegajo,
                               d.Poblacion,
                               d.TipoTurno,


                               /*
                                
                                descLineaAsignada : "01-T-IDA"
                                descEmpresaAsignada : "ANDESMAR"
                                descLineaAsignadaVuelta : ""
                                descEmpresaAsignadaVuelta : ""

                                */


                               descLineaAsignada = d.objLineaAsignada.Linea + "-" + d.objLineaAsignada.TipoTurno.Substring(0, 1) + "-" + d.objLineaAsignada.TipoRecorrido,
                               descEmpresaAsignada = d.objLineaAsignada.Empresa.Trim(),
                               idLineaIda = d.objLineaAsignada != null ? d.objLineaAsignada.Id : 0,


                               descLineaAsignadaVuelta = d.objLineaAsignadaVuelta.Linea + "-" + d.objLineaAsignadaVuelta.TipoTurno.Substring(0, 1) + "-" + d.objLineaAsignadaVuelta.TipoRecorrido,
                               descEmpresaAsignadaVuelta = d.objLineaAsignadaVuelta.Empresa.Trim(),
                               idLineaVuelta = d.objLineaAsignadaVuelta != null ? d.objLineaAsignadaVuelta.Id : 0,


                               descEmpresa = d.objEmpresa != null ? d.objEmpresa.RazonSocial : "",

                               HorarioIDA = d.objLineaAsignada.HorariosSalida + " - " + d.objLineaAsignada.HorariosLlegada,
                               HorarioVUELTA = d.objLineaAsignadaVuelta != null ? d.objLineaAsignadaVuelta.HorariosSalida + " - " + d.objLineaAsignadaVuelta.HorariosLlegada : "",


                               TipoVehiculoIDA = d.objLineaAsignada.TipoUnidad,
                               TipoVehiculoVUELTA = d.objLineaAsignadaVuelta != null ? d.objLineaAsignadaVuelta.TipoUnidad : "",


                               HorarioSalidaIDA = d.objLineaAsignada.HorariosSalida,
                               HorarioSalidaVUELTA = d.objLineaAsignadaVuelta != null ? d.objLineaAsignadaVuelta.HorariosSalida : "",


                               HorarioLlegadaIDA = d.objLineaAsignada.HorariosLlegada,
                               HorarioLlegadaVUELTA = d.objLineaAsignadaVuelta != null ? d.objLineaAsignadaVuelta.HorariosLlegada : "",




                               Chofer = d.Chofer == null ? false : d.Chofer,
                               CambiaClave = d.Clave == CLAVE_BASE ? true : false,
                               Clave = d.Clave,
                               qr1 = d.objLineaAsignada.Empresa.Substring(0, 3) + d.objLineaAsignada.TipoTurno.Substring(0, 1),
                               qr2 = d.objLineaAsignadaVuelta.Empresa.Substring(0, 3) + d.objLineaAsignadaVuelta.TipoTurno.Substring(0, 1),

                               d.Telefono,
                               d.Correo
                           }).ToList();

            if (legajos.Count > 0)
            {
                if (clave == legajos.First().Clave)
                {

                    string qr1 = "";
                    string qr2 = "";
                    ToExcelIndexCol((int)legajos.First().idLineaIda, ref qr1);

                    if (legajos.First().idLineaVuelta > 0)
                        ToExcelIndexCol((int)legajos.First().idLineaVuelta, ref qr2);

                    var legajo = (from d in legajos
                                  select new
                                  {
                                      d.Distrito,
                                      d.Domicilio,
                                      d.Id,
                                      d.Latitud,
                                      d.LatitudReposicion,
                                      d.Legajo,
                                      d.LineaAsignada,
                                      d.LineaAsignadaVuelta,
                                      d.Longitud,
                                      d.LongitudReposicion,
                                      d.NombreLegajo,
                                      d.Poblacion,
                                      d.TipoTurno,
                                      d.descLineaAsignada,
                                      d.descEmpresaAsignada,
                                      d.idLineaIda,
                                      d.descLineaAsignadaVuelta,
                                      d.descEmpresaAsignadaVuelta,
                                      d.idLineaVuelta,
                                      d.descEmpresa,
                                      d.HorarioIDA,
                                      d.HorarioVUELTA,
                                      d.HorarioSalidaIDA,
                                      d.HorarioSalidaVUELTA,
                                      d.HorarioLlegadaIDA,
                                      d.HorarioLlegadaVUELTA,
                                      d.TipoVehiculoIDA,
                                      d.TipoVehiculoVUELTA,
                                      d.Chofer,
                                      d.CambiaClave,
                                      Qr1 = d.qr1 + qr1,
                                      Qr2 = d.qr2 + qr2,
                                      d.Telefono,
                                      d.Correo

                                  }).First();

                    return legajo;
                }
                else
                {
                    return null;
                }
            }

            else
                return null;

        }

    }


    [WebMethod]
    public object getUsuario(long idUsuario)
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            Dictionary<string, object> datos = new Dictionary<string, object>();

            var legajos = (from d in dc.DomiciliosPersonal.Include("CabeceraRutas")
                           where d.Id == idUsuario
                           orderby d.NombreLegajo
                           select new
                           {
                               d.Distrito,
                               d.Domicilio,
                               d.Id,
                               d.Latitud,
                               d.LatitudReposicion,
                               d.Legajo,
                               d.LineaAsignada,
                               d.LineaAsignadaVuelta,
                               d.Longitud,
                               d.LongitudReposicion,
                               d.NombreLegajo,
                               d.Poblacion,
                               d.TipoTurno,



                               /*
                                
                                descLineaAsignada : "01-T-IDA"
                                descEmpresaAsignada : "ANDESMAR"
                                descLineaAsignadaVuelta : ""
                                descEmpresaAsignadaVuelta : ""

                                */


                               descLineaAsignada = d.objLineaAsignada.Linea + "-" + d.objLineaAsignada.TipoTurno.Substring(0, 1) + "-" + d.objLineaAsignada.TipoRecorrido,
                               descEmpresaAsignada = d.objLineaAsignada.Empresa.Trim(),
                               idLineaIda = d.objLineaAsignada != null ? d.objLineaAsignada.Id : 0,


                               descLineaAsignadaVuelta = d.objLineaAsignadaVuelta.Linea + "-" + d.objLineaAsignadaVuelta.TipoTurno.Substring(0, 1) + "-" + d.objLineaAsignadaVuelta.TipoRecorrido,
                               descEmpresaAsignadaVuelta = d.objLineaAsignadaVuelta.Empresa.Trim(),
                               idLineaVuelta = d.objLineaAsignadaVuelta != null ? d.objLineaAsignadaVuelta.Id : 0,


                               descEmpresa = d.objEmpresa != null ? d.objEmpresa.RazonSocial : "",

                               HorarioIDA = d.objLineaAsignada.HorariosSalida + " - " + d.objLineaAsignada.HorariosLlegada,
                               HorarioVUELTA = d.objLineaAsignadaVuelta != null ? d.objLineaAsignadaVuelta.HorariosSalida + " - " + d.objLineaAsignadaVuelta.HorariosLlegada : "",


                               TipoVehiculoIDA = d.objLineaAsignada.TipoUnidad,
                               TipoVehiculoVUELTA = d.objLineaAsignadaVuelta != null ? d.objLineaAsignadaVuelta.TipoUnidad : "",


                               HorarioSalidaIDA = d.objLineaAsignada.HorariosSalida,
                               HorarioSalidaVUELTA = d.objLineaAsignadaVuelta != null ? d.objLineaAsignadaVuelta.HorariosSalida : "",


                               HorarioLlegadaIDA = d.objLineaAsignada.HorariosLlegada,
                               HorarioLlegadaVUELTA = d.objLineaAsignadaVuelta != null ? d.objLineaAsignadaVuelta.HorariosLlegada : "",


                               Chofer = d.Chofer == null ? false : d.Chofer,
                               CambiaClave = d.Clave == CLAVE_BASE ? true : false,
                               Clave = d.Clave,

                               qr1 = d.objLineaAsignada.Empresa.Substring(0, 3) + d.objLineaAsignada.TipoTurno.Substring(0, 1),
                               qr2 = d.objLineaAsignadaVuelta.Empresa.Substring(0, 3) + d.objLineaAsignadaVuelta.TipoTurno.Substring(0, 1),
                               d.Telefono,
                               d.Correo
                           }).ToList();



            if (legajos.Count > 0)
            {
                string qr1 = "";
                string qr2 = "";
                ToExcelIndexCol((int)legajos.First().idLineaIda, ref qr1);

                if (legajos.First().idLineaVuelta > 0)
                    ToExcelIndexCol((int)legajos.First().idLineaVuelta, ref qr2);

                var legajo = (from d in legajos
                              select new
                              {
                                  d.Distrito,
                                  d.Domicilio,
                                  d.Id,
                                  d.Latitud,
                                  d.LatitudReposicion,
                                  d.Legajo,
                                  d.LineaAsignada,
                                  d.LineaAsignadaVuelta,
                                  d.Longitud,
                                  d.LongitudReposicion,
                                  d.NombreLegajo,
                                  d.Poblacion,
                                  d.TipoTurno,
                                  d.descLineaAsignada,
                                  d.descEmpresaAsignada,
                                  d.idLineaIda,
                                  d.descLineaAsignadaVuelta,
                                  d.descEmpresaAsignadaVuelta,
                                  d.idLineaVuelta,
                                  d.descEmpresa,
                                  d.HorarioIDA,
                                  d.HorarioVUELTA,
                                  d.Chofer,
                                  d.CambiaClave,
                                  Qr1 = d.qr1 + qr1,
                                  Qr2 = d.qr2 + qr2,
                                  d.Telefono,
                                  d.Correo,
                                  d.HorarioSalidaIDA,
                                  d.HorarioSalidaVUELTA,
                                  d.HorarioLlegadaIDA,
                                  d.HorarioLlegadaVUELTA,
                                  d.TipoVehiculoIDA,
                                  d.TipoVehiculoVUELTA

                              }).First();

                return legajo;
            }
            else
                return null;

        }

    }


    [WebMethod]
    public object getLocalidades()
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {

            var poblacion = (from d in dc.DomiciliosPersonal
                             orderby d.Poblacion
                             select d.Poblacion).Select(w => w.Trim().ToUpper()).ToList().Distinct();

            return poblacion;

        }

    }


    [WebMethod]
    public object getEmpresas()
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {

            var empresas = (from d in dc.Empresa
                            orderby d.RazonSocial
                            select new
                            {
                                IdEmpresa = d.IdEmpresa,
                                RazonSocial = d.RazonSocial
                            }).ToList();

            return empresas;

        }

    }


    [WebMethod]
    public object getTipoTurnos()
    {
        return new string[] { "TURNO", "DIURNO", "6x1", "VEHÍCULO ASIGNADO" };

    }


    [WebMethod]
    public object updatePassword(long idUsuario, string currentpassword, string newpassword)
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {

            var legajo = (from d in dc.DomiciliosPersonal
                          where d.Id == idUsuario
                          select d).FirstOrDefault();

            if (legajo != null)
            {
                if (legajo.Clave == currentpassword)
                {
                    legajo.Clave = newpassword;
                    dc.SaveChanges();
                    return new { resultado = "ok", mensaje = "Cambio de clave realizado con exito" };
                }
                else
                {
                    return new { resultado = "er", mensaje = "La clave actual no es correcta." };
                }

            }
            else
            {
                return new { resultado = "er", mensaje = "El usuario no fue encontrado" };

            }


        }

    }


    [WebMethod]
    public object updateUser(long idUsuario,
        string nombre,
        string direccion,
        string departamento,
        string localidad,
        string Telefono,
        string Correo,
        string Empresa,
        string TipoTurno)
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {

            var legajo = (from d in dc.DomiciliosPersonal
                          where d.Id == idUsuario
                          select d).FirstOrDefault();

            if (legajo != null)
            {
                if (legajo.EstadoActulizacion != "PENDIENTE")
                {
                    legajo.DatosActualizacion = "nombre:" + nombre + ",direccion:" + direccion
                                            + ",departamento:" + departamento
                                            + ",localidad:" + localidad
                                            + ",Telefono:" + Telefono
                                            + ",Correo:" + Correo
                                            + ",Empresa:" + Empresa
                                            + ",TipoTurno:" + TipoTurno;
                    legajo.EstadoActulizacion = "PENDIENTE";
                    legajo.FechaSolicitudActualizacion = DateTime.Now;
                    dc.SaveChanges();

                    return new { resultado = "ok", mensaje = "La solicitud fue ingresada, el administrador le informara la aprobación." };
                }
                else

                {
                    return new { resultado = "er", mensaje = "Ya posee un actualizacion pendiente de aprobación, por el momento no puede actualizar sus datos." };
                }

            }
            else
            {
                return new { resultado = "er", mensaje = "El usuario no fue encontrado" };

            }


        }

    }

    [WebMethod]
    public object checkIn(long idUsuario, string QRRecorrido, string Latitud, string Longitud)
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            long IdRecorrido = getIdFromQR(QRRecorrido);



            Checkin ch = new Checkin();
            dc.AddToCheckin(ch);


            ch.IdUsuario = idUsuario;
            ch.IdRecorrido = IdRecorrido;
            ch.Latitud = Latitud;
            ch.Longitud = Longitud;
            ch.FechaHora = DateTime.Now;


            string recorridoDesc = "(" + IdRecorrido + ")";
            var legajo = (from r in dc.DomiciliosPersonal
                          where r.Id == idUsuario
                          select r).FirstOrDefault();

            bool cambiaRuta = false;
            if (legajo != null)
            {
                // si el atributo RutaConCambio contiene la linea donde acaba de hacer chechik significa que se le informó
                // que hubo cambios en el recorrido, es por eso que se limpia para no volver a informar.
                if (legajo.RutaConCambio != null && legajo.RutaConCambio.Contains(recorridoDesc))
                {
                    legajo.RutaConCambio = legajo.RutaConCambio.Replace(recorridoDesc, "");
                    cambiaRuta = true;
                }

                // Se verifica que la linea donde hace el checkIn es la asignada.
                ch.CheckInValido = (legajo.LineaAsignada != null && legajo.LineaAsignada == IdRecorrido) || (legajo.LineaAsignadaVuelta != null && legajo.LineaAsignadaVuelta == IdRecorrido) ? true : false;
            }


            dc.SaveChanges();

            if (IdRecorrido > 0)
            {
                if (!ch.CheckInValido.Value)
                {
                    return new
                    {
                        resultado = "warning",
                        mensaje = "El checkIn se realizo con exito, pero en linea incorrecta.",
                        CambioRecorrido = cambiaRuta
                    };
                }
                else
                {
                    return new
                    {
                        resultado = "ok",
                        mensaje = "El checkIn se realizo con exito.",
                        CambioRecorrido = cambiaRuta
                    };
                }
            }
            else
            {

                return new
                {
                    resultado = "error",
                    mensaje = "El código no pertenece a ningun recorrido.",
                    CambioRecorrido = false
                };
            }


        }

    }


    [WebMethod]
    public object checkInChofer(long idUsuario, string codigoRecorrido, string Latitud, string Longitud)
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            var empresa = codigoRecorrido.Substring(0, 3);
            var tipoturno = codigoRecorrido.Substring(3, 1);

            var recorridos = (from r in dc.CabeceraRutasTransportes
                              where r.Empresa.StartsWith(empresa) && r.TipoTurno.StartsWith(tipoturno)
                              select new
                              {
                                  recorrido = r.RutasTransportes.Select(w => new { w.Latitud, w.Longitud }),
                                  Empresa = r.Empresa,
                                  Horario = r.HorariosSalida + " - " + r.HorariosLlegada,
                                  TipoRecorrido = r.TipoRecorrido,
                                  TipoTurno = r.TipoTurno,
                                  idRecorrido = r.Id,
                                  descLineaAsignada = r.Empresa.Substring(0, 3) + " - L:" + r.Linea + "-" + r.TipoTurno.Substring(0, 1) + "-" + r.TipoRecorrido
                              }).ToList();


            /// inicializo variable.
            var recorrido = recorridos.FirstOrDefault();
            foreach (var r in recorridos)
            {
                string gRes = "";
                ToExcelIndexCol((int)r.idRecorrido, ref gRes);
                if (r.Empresa.Substring(0, 3) + r.TipoTurno.Substring(0, 1) + gRes == codigoRecorrido)
                {
                    recorrido = r;
                    break;
                }
                else
                    recorrido = null;

            }



            if (recorrido != null)
            {
                Checkin ch = new Checkin();
                dc.AddToCheckin(ch);


                ch.IdUsuario = idUsuario;
                ch.IdRecorrido = recorrido.idRecorrido;
                ch.Latitud = Latitud;
                ch.Longitud = Longitud;
                ch.FechaHora = DateTime.Now;

                string recorridoDesc = "(" + recorrido.idRecorrido + ")";
                var legajo = (from r in dc.DomiciliosPersonal
                              where r.Id == idUsuario
                              && r.RutaConCambio.Contains(recorridoDesc)
                              select r).FirstOrDefault();

                bool cambiaRuta = false;
                if (legajo != null)
                {

                    legajo.RutaConCambio = legajo.RutaConCambio.Replace(recorridoDesc, "");
                    cambiaRuta = true;
                }


                dc.SaveChanges();


                return new
                {
                    resultado = "ok",
                    mensaje = "El checkIn se realizo con exito.",
                    CambioRecorrido = cambiaRuta,
                    ruta = recorrido
                };
            }
            else
            {
                return new
                {
                    resultado = "er",
                    mensaje = "El checkIn no se pudo realizar, por favor tome contacto con el administrador."
                };
            }

        }

    }

    public void ToExcelIndexCol(int n, ref string res)
    {
        if (n == 0)
        {
            return;
        }
        else
        {
            int r = n % 26;
            n = n / 26;
            if (r == 0)
                ToExcelIndexCol(n - 1, ref res);
            else
                ToExcelIndexCol(n, ref res);

            if (r == 0)
            {
                res += "Z";
                if (n == 1)
                    return;
            }
            res += Char.ConvertFromUtf32(r + 64);
        }
    }

    private static string GetMd5Hash(MD5 md5Hash, string input)
    {

        //string source = "123456";
        //using (MD5 md5Hash = MD5.Create())
        //{
        //    string hash = GetMd5Hash(md5Hash, source);
        //}


        // Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }


    [WebMethod(EnableSession = true)]
    public object checkInConsult(string FDesde, string FHasta, int? Recorrido, int? Legajo)
    {
        using (EntidadesConosud dc = new EntidadesConosud())
        {
            DateTime FechaDesde = DateTime.Parse(FDesde);
            DateTime FechaHasta = DateTime.Parse(FHasta).AddDays(1);

            // LEGAJO - DNI, APELLIDO NOMBRE,EMPRESA,Fecha de registro,Hora de registro,Línea de registro

            var recorridosResult = (from c in dc.Checkin
                                        join r in dc.CabeceraRutasTransportes on c.IdRecorrido equals r.Id into cab
                                        from r in cab.DefaultIfEmpty()
                                    join l in dc.DomiciliosPersonal on c.IdUsuario equals l.Id
                                    join e in dc.Empresa on l.Empresa equals e.IdEmpresa
                                    where c.FechaHora >= FechaDesde && c.FechaHora <= FechaHasta
                                    select new
                                    {
                                        c.IdRecorrido,
                                        c.IdUsuario,
                                        c.FechaHora,
                                        Linea = c.IdRecorrido == 0 ? "- registro con código inválido -" : r.Empresa + " - LINEA " + r.Linea + " - " + r.TipoTurno + " - " + r.TipoRecorrido,
                                        IdLineaAsignada = l.LineaAsignada,
                                        IdLineaAsignadaVuelta = l.LineaAsignadaVuelta,
                                        LineaAsignada = l.objLineaAsignada.Empresa + " - LINEA " + l.objLineaAsignada.Linea + " - " + l.objLineaAsignada.TipoTurno + " - " + l.objLineaAsignada.TipoRecorrido,
                                        LineaAsignadaVuelta = l.objLineaAsignadaVuelta != null ? l.objLineaAsignadaVuelta.Empresa + " - LINEA " + l.objLineaAsignadaVuelta.Linea + " - " + l.objLineaAsignadaVuelta.TipoTurno + " - " + l.objLineaAsignadaVuelta.TipoRecorrido : "",
                                        l.NombreLegajo,
                                        l.Legajo,
                                        e.RazonSocial,
                                        c.CheckInValido
                                    }).ToList();


            if (Recorrido != null)
                recorridosResult = recorridosResult.Where(c => 
                    c.IdRecorrido == Recorrido || ((c.IdLineaAsignada == Recorrido || c.IdLineaAsignadaVuelta == Recorrido) && c.IdRecorrido == 0 )
                ).ToList();


            var result = from r in recorridosResult
                         select new
                         {

                             r.IdRecorrido,
                             r.IdUsuario,
                             FechaRegistro = r.FechaHora.Value.ToShortDateString(),
                             HoraRegistro = r.FechaHora.Value.ToShortTimeString(),
                             r.Legajo,
                             r.NombreLegajo,
                             r.Linea,
                             r.RazonSocial,
                             CheckInValido = r.IdRecorrido == 0 ? "Codigo Incorrecto" : (!r.CheckInValido.HasValue || !r.CheckInValido.Value) ? "Linea Incorrecta" : "Linea Correcta",
                             r.LineaAsignada,
                             r.LineaAsignadaVuelta,
                         };

            HttpContext.Current.Session.Add("checkinConsultResult", result.ToList<dynamic>());
            return result;

        }

    }

    private long getIdFromQR(string QR)
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            Dictionary<string, object> datos = new Dictionary<string, object>();

            var rutas = (from d in dc.CabeceraRutasTransportes
                         select new
                         {
                             d.Id,
                             qr = d.Empresa.Substring(0, 3) + d.TipoTurno.Substring(0, 1),
                         }).ToList();


            foreach (var r in rutas)
            {
                string qr1 = "";
                ToExcelIndexCol((int)r.Id, ref qr1);
                if (r.qr + qr1 == QR)
                {
                    return r.Id;
                }

            }

        }

        return 0;
    }
}
