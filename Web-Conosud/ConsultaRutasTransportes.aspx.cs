using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Telerik.Web.UI;
using System.Xml.Linq;
using System.Xml;

public partial class ConsultaRutasTransportes : System.Web.UI.Page
{
    public class infoPuntosCercanos
    {

        public double masCercano { get; set; }
        public CabeceraRutasTransportes cabeceraIda { get; set; }
        public long idPuntoIda { get; set; }
        public long idCab { get; set; }

    }

    public class infoPuntoEncontrado
    {

        public List<object> Ruta { get; set; }
        public string Linea { get; set; }
        public string Empresa { get; set; }
        public string Turno { get; set; }
        public string Horarios { get; set; }
        public string TipoUnidad { get; set; }
        public string TipoTurno { get; set; }
        public string TipoRecorrido { get; set; }

    }

    private static Double deg2rad(Double deg)
    {
        return (deg * Math.PI / 180.0);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [WebMethod(EnableSession = true)]
    public static object CargarKML()
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            CabeceraRutasTransportes cab = new CabeceraRutasTransportes();
            cab.Empresa = "ANDESMAR S.A.";
            cab.HorariosSalida = "06.15  – 18.15 hs ";
            cab.HorariosLlegada = ": 7.15 – 19,15 hs";
            cab.TipoUnidad = "Minibus";
            cab.Turno = "1 y 2 (TURNO)";
            cab.Linea = "LINEA N° 4 (LUZURIAGA - MAIPU CENTRO - PERDRIEL)";



            string docName = @"C:\Desarrollo\Repositorio\Infolegacy\Conosud\Analisis\Transportes\LINEA 4 TURNO.xml";
            Dictionary<string, object> datos = new Dictionary<string, object>();
            XmlDocument doc = new XmlDocument();
            doc.Load(docName);
            var listaCoordenadas = doc.ChildNodes[1].ChildNodes[0].ChildNodes[11].ChildNodes[4].ChildNodes[2].ChildNodes;
            int contador = 0;
            List<object> dd = new List<object>();
            foreach (var item in listaCoordenadas)
            {
                int r;
                if ((item as XmlElement).LocalName == "coord")
                {
                    Math.DivRem(contador, 1, out r);
                    if (r == 0)
                    {
                        RutasTransportes ruta = new RutasTransportes();
                        ruta.Departamento = "MAIPU";
                        ruta.Latitud = (item as XmlElement).InnerXml.Split(' ')[1].ToString().Replace(".", ",");
                        ruta.Longitud = (item as XmlElement).InnerXml.Split(' ')[0].ToString().Replace(".", ",");
                        ruta.objCabecera = cab;
                        //dd.Add(new object[] { (item as XmlElement).InnerXml.Split(' ')[0], (item as XmlElement).InnerXml.Split(' ')[1] });
                    }
                    contador++;

                }
            }

            dc.AddToCabeceraRutasTransportes(cab);
            dc.SaveChanges();
        }


        //string docName = @"C:\Desarrollo\Repositorio\Infolegacy\Conosud\Analisis\Transportes\LINEA 4 TURNO.xml";
        //Dictionary<string, object> datos = new Dictionary<string, object>();
        //XmlDocument doc = new XmlDocument();
        //doc.Load(docName);
        //var listaCoordenadas = doc.ChildNodes[1].ChildNodes[0].ChildNodes[11].ChildNodes[4].ChildNodes[2].ChildNodes;
        //int contador = 0;
        //List<object> dd = new List<object>();
        //foreach (var item in listaCoordenadas)
        //{
        //    int r;
        //    if ((item as XmlElement).LocalName == "coord")
        //    {
        //        Math.DivRem(contador,1, out r);
        //        if (r == 0)
        //        {
        //            dd.Add(new object[] { (item as XmlElement).InnerXml.Split(' ')[0], (item as XmlElement).InnerXml.Split(' ')[1] });

        //            //List<string> valores = new List<string>();
        //            //valores.Add((item as XmlElement).InnerXml.Split(' ')[0]);
        //            //valores.Add((item as XmlElement).InnerXml.Split(' ')[1]);
        //            //datos.Add(contador.ToString(), valores);

        //        }
        //        contador++;

        //    }
        //}

        return null;
        //return datos.Take(1000).ToList();




    }

    [WebMethod(EnableSession = true)]
    public static object BuscarLineaTransporte(Double lat, Double lon, string TipoTurno)
    {
        long earthRadius = 6371000;
        Double latFrom = deg2rad(lat);
        Double lonFrom = deg2rad(lon);
        List<object> valores = new List<object>();


        using (EntidadesConosud dc = new EntidadesConosud())
        {
            Double masCercano = 10000;

            CabeceraRutasTransportes cabeceraIda = null;
            long idPuntoIda = 0;

            CabeceraRutasTransportes cabeceraRegreso = null;
            long idPuntoRegreso = 0;


            var puntos = (from p in dc.RutasTransportes
                          where p.objCabecera.TipoTurno != "Temporal"
                          && p.objCabecera.TipoTurno == TipoTurno
                          select p).ToList();

            List<RutasTransportes> puntosIda;
            List<RutasTransportes> puntosRegreso;
            SortedList<double, infoPuntosCercanos> infoPuntosIda = new SortedList<double, infoPuntosCercanos>();
            SortedList<double, infoPuntosCercanos> infoPuntosRegreso = new SortedList<double, infoPuntosCercanos>();
            infoPuntosCercanos primerLineaIda = null;
            infoPuntosCercanos segundaLineaIda = null;
            infoPuntosCercanos primerLineaRegreso = null;
            infoPuntosCercanos segundaLineaRegreso = null;


            #region  Busqueda del punto mas cercano para las rutas IDA y VUELTA

            puntosIda = (from p in puntos
                         where p.objCabecera.TipoRecorrido == "IDA"
                         select p).ToList();

            var puntosIdaGrupo = (from p in puntosIda
                                  group p by p.Cabecera.Value into g
                                  select new
                                  {
                                      idCab = g.Key,
                                      puntos = g
                                  }).ToList();

            foreach (var itemG in puntosIdaGrupo)
            {
                foreach (var item in itemG.puntos)
                {
                    Double latTo = deg2rad(Convert.ToDouble(item.Latitud));
                    Double lonTo = deg2rad(Convert.ToDouble(item.Longitud));

                    Double latDelta = latTo - latFrom;
                    Double lonDelta = lonTo - lonFrom;

                    Double angle = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(latDelta / 2), 2) + Math.Cos(latFrom) * Math.Cos(latTo) * Math.Pow(Math.Sin(lonDelta / 2), 2)));
                    Double distancia = (angle * earthRadius) / 1000;
                    if (distancia < masCercano)
                    {
                        masCercano = distancia;
                        cabeceraIda = item.objCabecera;
                        idPuntoIda = item.Id;
                    }
                }
                infoPuntosCercanos info = new infoPuntosCercanos();
                info.masCercano = masCercano;
                info.cabeceraIda = cabeceraIda;
                info.idPuntoIda = idPuntoIda;
                info.idCab = itemG.idCab;

                if (!infoPuntosIda.ContainsKey(masCercano))
                    infoPuntosIda.Add(masCercano, info);
                else
                    infoPuntosIda.Add(masCercano + (Convert.ToDouble(itemG.idCab) / 10000000), info);
            }

            /// Busco los dos mas cercanos en la ida
            primerLineaIda = infoPuntosIda.OrderBy(w => w.Key).Take(1).Select(w => w.Value).FirstOrDefault();
            segundaLineaIda = infoPuntosIda.OrderBy(w => w.Key).Skip(1).Take(1).Select(w => w.Value).FirstOrDefault();


            if (TipoTurno == "DIURNO")
            {
                puntosRegreso = (from p in puntos
                                 where p.objCabecera.TipoRecorrido != "IDA"
                                 select p).ToList();

                var puntosRegresoGrupo = (from p in puntosRegreso
                                          group p by p.Cabecera.Value into g
                                          select new
                                          {
                                              idCab = g.Key,
                                              puntos = g
                                          }).ToList();

                foreach (var itemG in puntosRegresoGrupo)
                {
                    foreach (var item in itemG.puntos)
                    {
                        Double latTo = deg2rad(Convert.ToDouble(item.Latitud));
                        Double lonTo = deg2rad(Convert.ToDouble(item.Longitud));

                        Double latDelta = latTo - latFrom;
                        Double lonDelta = lonTo - lonFrom;

                        Double angle = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(latDelta / 2), 2) + Math.Cos(latFrom) * Math.Cos(latTo) * Math.Pow(Math.Sin(lonDelta / 2), 2)));
                        Double distancia = (angle * earthRadius) / 1000;
                        if (distancia < masCercano)
                        {
                            masCercano = distancia;
                            cabeceraIda = item.objCabecera;
                            idPuntoIda = item.Id;
                        }
                    }
                    infoPuntosCercanos info = new infoPuntosCercanos();
                    info.masCercano = masCercano;
                    info.cabeceraIda = cabeceraIda;
                    info.idPuntoIda = idPuntoIda;
                    info.idCab = itemG.idCab;

                    if (!infoPuntosRegreso.ContainsKey(masCercano))
                        infoPuntosRegreso.Add(masCercano, info);
                    else
                        infoPuntosRegreso.Add(masCercano + (Convert.ToDouble(itemG.idCab) / 10000000), info);
                }

                /// Busco los dos mas cercanos en el Regreso
                primerLineaRegreso = infoPuntosRegreso.OrderBy(w => w.Key).Take(1).Select(w => w.Value).FirstOrDefault();
                segundaLineaRegreso = infoPuntosRegreso.OrderBy(w => w.Key).Skip(1).Take(1).Select(w => w.Value).FirstOrDefault();
            }
            #region Codigo Original

            //foreach (var item in puntosIda)
            //{

            //    Double latTo = deg2rad(Convert.ToDouble(item.Latitud));
            //    Double lonTo = deg2rad(Convert.ToDouble(item.Longitud));

            //    Double latDelta = latTo - latFrom;
            //    Double lonDelta = lonTo - lonFrom;

            //    Double angle = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(latDelta / 2), 2) + Math.Cos(latFrom) * Math.Cos(latTo) * Math.Pow(Math.Sin(lonDelta / 2), 2)));
            //    Double distancia = (angle * earthRadius) / 1000;
            //    if (distancia < masCercano)
            //    {
            //        masCercano = distancia;
            //        cabeceraIda = item.objCabecera;
            //        idPuntoIda = item.Id;
            //    }


            //}

            ///// Busco para las lineas de regreso
            //masCercano = 10000;
            //foreach (var item in puntosRegreso)
            //{
            //    Double latTo = deg2rad(Convert.ToDouble(item.Latitud));
            //    Double lonTo = deg2rad(Convert.ToDouble(item.Longitud));

            //    Double latDelta = latTo - latFrom;
            //    Double lonDelta = lonTo - lonFrom;

            //    Double angle = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(latDelta / 2), 2) + Math.Cos(latFrom) * Math.Cos(latTo) * Math.Pow(Math.Sin(lonDelta / 2), 2)));
            //    Double distancia = (angle * earthRadius) / 1000;
            //    if (distancia < masCercano)
            //    {
            //        masCercano = distancia;
            //        cabeceraRegreso = item.objCabecera;
            //        idPuntoRegreso = item.Id;
            //    }


            //}
            #endregion

            #endregion

            //else
            //{
            //    #region Busqueda del punto mas cercano para las rutas IDA
            //    //foreach (var item in puntos)
            //    //{
            //    //    Double latTo = deg2rad(Convert.ToDouble(item.Latitud));
            //    //    Double lonTo = deg2rad(Convert.ToDouble(item.Longitud));

            //    //    Double latDelta = latTo - latFrom;
            //    //    Double lonDelta = lonTo - lonFrom;

            //    //    Double angle = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(latDelta / 2), 2) + Math.Cos(latFrom) * Math.Cos(latTo) * Math.Pow(Math.Sin(lonDelta / 2), 2)));
            //    //    Double distancia = (angle * earthRadius) / 1000;
            //    //    if (distancia < masCercano)
            //    //    {
            //    //        masCercano = distancia;
            //    //        cabeceraIda = item.objCabecera;
            //    //        idPuntoIda = item.Id;
            //    //    }


            //    //}
            //    #endregion

            //}



            /// Regla de Negocio
            /// 1. Deberia devolver las lista de puntos del recorrido mar cercano 
            /// 2. y los 6 puntos al rededor del punto mas cercano para RE calcular segun la api de google el punto mas cercano

            #region Recupero todos lo puntos para el recorrido de la ida

            //var datos = (from p in puntos.Where(w => w.Cabecera == cabeceraIda.Id).ToList()
            //             select new
            //             {
            //                 Key = Convert.ToDouble(p.Latitud),
            //                 Value = Convert.ToDouble(p.Longitud)
            //             }).ToList();

            /////2.
            //var ptos = (from p in puntos.Where(w => w.Cabecera == cabeceraIda.Id && w.Id >= idPuntoIda - 0 && w.Id <= idPuntoIda + 0).ToList()
            //            select new
            //            {
            //                Key = Convert.ToDouble(p.Latitud),
            //                Value = Convert.ToDouble(p.Longitud)
            //            }).ToList();

            //valores.Add("Ruta", datos.ToList());
            //valores.Add("PuntosCercanos", ptos.ToList());
            //valores.Add("Linea", cabeceraIda.Linea);
            //valores.Add("Empresa", cabeceraIda.Empresa);
            //valores.Add("Turno", cabeceraIda.Turno);
            //valores.Add("Horarios", cabeceraIda.HorariosSalida + " - " + cabeceraIda.HorariosLlegada);
            //valores.Add("TipoUnidad", cabeceraIda.TipoUnidad);
            //valores.Add("TipoTurno", TipoTurno);
            //valores.Add("TipoRecorrido", cabeceraIda.TipoRecorrido);

            var datos = (from p in puntos.Where(w => w.Cabecera == primerLineaIda.idCab).ToList()
                         select new
                         {
                             Key = Convert.ToDouble(p.Latitud),
                             Value = Convert.ToDouble(p.Longitud)
                         }).ToList();

            infoPuntoEncontrado infoPrimerIda = new infoPuntoEncontrado();
            infoPrimerIda.Ruta = datos.ToList<object>();
            infoPrimerIda.Linea = primerLineaIda.cabeceraIda.Linea;
            infoPrimerIda.Empresa = primerLineaIda.cabeceraIda.Empresa;
            infoPrimerIda.Turno = primerLineaIda.cabeceraIda.Turno;
            infoPrimerIda.Horarios = primerLineaIda.cabeceraIda.HorariosSalida + " - " + primerLineaIda.cabeceraIda.HorariosLlegada;
            infoPrimerIda.TipoUnidad = primerLineaIda.cabeceraIda.TipoUnidad;
            infoPrimerIda.TipoTurno = TipoTurno;
            infoPrimerIda.TipoRecorrido = primerLineaIda.cabeceraIda.TipoRecorrido;

            valores.Add(infoPrimerIda);


            datos = (from p in puntos.Where(w => w.Cabecera == segundaLineaIda.idCab).ToList()
                     select new
                     {
                         Key = Convert.ToDouble(p.Latitud),
                         Value = Convert.ToDouble(p.Longitud)
                     }).ToList();

            infoPuntoEncontrado infoSegundaIda = new infoPuntoEncontrado();
            infoSegundaIda.Ruta = datos.ToList<object>();
            infoSegundaIda.Linea = segundaLineaIda.cabeceraIda.Linea;
            infoSegundaIda.Empresa = segundaLineaIda.cabeceraIda.Empresa;
            infoSegundaIda.Turno = segundaLineaIda.cabeceraIda.Turno;
            infoSegundaIda.Horarios = segundaLineaIda.cabeceraIda.HorariosSalida + " - " + segundaLineaIda.cabeceraIda.HorariosLlegada;
            infoSegundaIda.TipoUnidad = segundaLineaIda.cabeceraIda.TipoUnidad;
            infoSegundaIda.TipoTurno = TipoTurno;
            infoSegundaIda.TipoRecorrido = segundaLineaIda.cabeceraIda.TipoRecorrido;

            valores.Add(infoSegundaIda);

            #endregion


            if (TipoTurno == "DIURNO")
            {
                #region Solo si el recorrido es de DIURNO busco todos lo puntos para el recorrido en la vuelta
                //var datosAlt = (from p in puntos.Where(w => w.Cabecera == cabeceraRegreso.Id).ToList()
                //                select new
                //                {
                //                    Key = Convert.ToDouble(p.Latitud),
                //                    Value = Convert.ToDouble(p.Longitud)
                //                }).ToList();


                //var ptosAlt = (from p in puntos.Where(w => w.Cabecera == cabeceraRegreso.Id && w.Id >= idPuntoRegreso - 0 && w.Id <= idPuntoRegreso + 0).ToList()
                //               select new
                //               {
                //                   Key = Convert.ToDouble(p.Latitud),
                //                   Value = Convert.ToDouble(p.Longitud),
                //                   id = p.Id
                //               }).ToList();

                //valores.Add("RutaAlt", datosAlt.ToList());
                //valores.Add("PuntosCercanosAlt", ptosAlt.ToList());
                //valores.Add("LineaAlt", cabeceraRegreso.Linea);
                //valores.Add("EmpresaAlt", cabeceraRegreso.Empresa);
                //valores.Add("TurnoAlt", cabeceraRegreso.Turno);
                //valores.Add("HorariosAlt", cabeceraRegreso.HorariosSalida + " - " + cabeceraIda.HorariosLlegada);
                //valores.Add("TipoUnidadAlt", cabeceraRegreso.TipoUnidad);
                //valores.Add("TipoTurnoAlt", TipoTurno);
                //valores.Add("TipoRecorridoAlt", cabeceraRegreso.TipoRecorrido);

                 datos = (from p in puntos.Where(w => w.Cabecera == primerLineaRegreso.idCab).ToList()
                             select new
                             {
                                 Key = Convert.ToDouble(p.Latitud),
                                 Value = Convert.ToDouble(p.Longitud)
                             }).ToList();

                infoPuntoEncontrado infoPrimerRegreso = new infoPuntoEncontrado();
                infoPrimerRegreso.Ruta = datos.ToList<object>();
                infoPrimerRegreso.Linea = primerLineaRegreso.cabeceraIda.Linea;
                infoPrimerRegreso.Empresa = primerLineaRegreso.cabeceraIda.Empresa;
                infoPrimerRegreso.Turno = primerLineaRegreso.cabeceraIda.Turno;
                infoPrimerRegreso.Horarios = primerLineaRegreso.cabeceraIda.HorariosSalida + " - " + primerLineaRegreso.cabeceraIda.HorariosLlegada;
                infoPrimerRegreso.TipoUnidad = primerLineaRegreso.cabeceraIda.TipoUnidad;
                infoPrimerRegreso.TipoTurno = TipoTurno;
                infoPrimerRegreso.TipoRecorrido = primerLineaRegreso.cabeceraIda.TipoRecorrido;

                valores.Add(infoPrimerRegreso);


                datos = (from p in puntos.Where(w => w.Cabecera == segundaLineaRegreso.idCab).ToList()
                         select new
                         {
                             Key = Convert.ToDouble(p.Latitud),
                             Value = Convert.ToDouble(p.Longitud)
                         }).ToList();

                infoPuntoEncontrado infoSegundaRegreso = new infoPuntoEncontrado();
                infoSegundaRegreso.Ruta = datos.ToList<object>();
                infoSegundaRegreso.Linea = segundaLineaRegreso.cabeceraIda.Linea;
                infoSegundaRegreso.Empresa = segundaLineaRegreso.cabeceraIda.Empresa;
                infoSegundaRegreso.Turno = segundaLineaRegreso.cabeceraIda.Turno;
                infoSegundaRegreso.Horarios = segundaLineaRegreso.cabeceraIda.HorariosSalida + " - " + segundaLineaRegreso.cabeceraIda.HorariosLlegada;
                infoSegundaRegreso.TipoUnidad = segundaLineaRegreso.cabeceraIda.TipoUnidad;
                infoSegundaRegreso.TipoTurno = TipoTurno;
                infoSegundaRegreso.TipoRecorrido = segundaLineaRegreso.cabeceraIda.TipoRecorrido;

                valores.Add(infoSegundaRegreso);

                #endregion
            }

            return valores;


            /// TERMINAR DE HACER EL MISMO CODIGO PARA LA VUELTA Y DEVOLVER TODOS LOS DATOS 
            /// QUE SERIAN 2 O 4 CONJUNTO DE DATOS.
        }
    }

}