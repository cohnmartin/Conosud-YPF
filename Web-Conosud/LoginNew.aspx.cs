using System;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;
using Entidades;
using System.Web.Services;
using System.Web;

public partial class LoginNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //// Put user code to initialize the page here
            Session.Abandon();
            Application.Clear();
            FormsAuthentication.SignOut();
            Session.Timeout = 60;


            /// CODIGO PARA VER TEMA DE MAILS
            //try
            //{
            //    //prepare pop client
            //    // TODO: Replace username and password with your own credentials.
            //    Pop3.Pop3MailClient DemoClient = new Pop3.Pop3MailClient("dtcwin087.ferozo.com", 995, true, "martin.cohn@conosudsrlgestionva.com.ar", "Mc691400");
            //    DemoClient.IsAutoReconnect = true;

            //    //remove the following line if no tracing is needed
            //    DemoClient.Trace += new Pop3.TraceHandler(Console.WriteLine);
            //    DemoClient.ReadTimeout = 60000; //give pop server 60 seconds to answer

            //    //establish connection
            //    DemoClient.Connect();

            //    //get mailbox statistics
            //    int NumberOfMails, MailboxSize;
            //    DemoClient.GetMailboxStats(out NumberOfMails, out MailboxSize);

            //    //get a list of mails
            //    List<int> EmailIds;
            //    DemoClient.GetEmailIdList(out EmailIds);

            //    //get a list of unique mail ids
            //    List<Pop3.EmailUid> EmailUids;
            //    DemoClient.GetUniqueEmailIdList(out EmailUids);

            //    //get email size
            //    DemoClient.GetEmailSize(2);

            //    //get email
            //    string Email;
            //    DemoClient.GetRawEmail(2, out Email);

            //    //delete email
            //    DemoClient.DeleteEmail(2);

            //    //get a list of mails
            //    List<int> EmailIds2;
            //    DemoClient.GetEmailIdList(out EmailIds2);

            //    //undelete all emails
            //    DemoClient.UndeleteAllEmails();

            //    //ping server
            //    DemoClient.NOOP();

            //    //test some error conditions
            //    DemoClient.GetRawEmail(1000000, out Email);
            //    DemoClient.DeleteEmail(1000000);


            //    //close connection
            //    DemoClient.Disconnect();

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine();
            //    Console.WriteLine("Run Time Error Occured:");
            //    Console.WriteLine(ex.Message);
            //    Console.WriteLine(ex.StackTrace);
            //}
        }
    }

    [WebMethod(EnableSession = true)]
    public static object Login(string usuario, string clave)
    {

        EntidadesConosud dc = new EntidadesConosud();


        List<Entidades.SegUsuario> usuarios = (from u in dc.SegUsuario
                                               where u.Login == usuario && u.Password == clave
                                               select u).ToList();

        if (usuarios.Count > 0)
        {

            if (usuarios.First().SegUsuarioRol.Count > 0)
            {

                HttpContext.Current.Session.Add("idusu", usuarios.First().IdSegUsuario);
                HttpContext.Current.Session.Add("idusuario", usuarios.First().IdSegUsuario);
                HttpContext.Current.Session.Add("nombreusu", usuarios.First().Login);
                HttpContext.Current.Session.Add("usuario", usuarios.First());

                if (usuarios.First().Empresa != null)
                {
                    HttpContext.Current.Session.Add("TipoUsuario", "Cliente");
                    HttpContext.Current.Session.Add("IdEmpresaContratista", usuarios.First().Empresa.IdEmpresa);
                }
                else
                {
                    HttpContext.Current.Session.Add("TipoUsuario", "NoCliente");
                    HttpContext.Current.Session.Add("IdEmpresaContratista", null);
                }


                //HttpContext.Current.Response.Redirect("~/Default.aspx");
                return "";

            }
            else
            {
                //ScriptManager.RegisterStartupScript(HttpContext.Current., typeof(Page), "alertesinpermisos", "ShowUsuarioSinPermisos();", true);
                return "Usuario sin permisos";
            }

        }
        else
        {
            //FailureText.Text = "Usuario o clave incorrectos!";
            //UserName.Text = string.Empty;
            return "Usuario o clave incorrectos!";
        }

    }


    protected void LoginButton_Click(object sender, EventArgs e)
    {

        //EntidadesConosud dc = new EntidadesConosud();




        //List<Entidades.SegUsuario> usuarios = (from u in dc.SegUsuario
        //                    where u.Login == this.UserName.Text && u.Password == this.Password.Text
        //                    select u).ToList();

        //if (usuarios.Count > 0)
        //{
        //    //if (!usuarios.First().EmpresaReference.IsLoaded){usuarios.First().EmpresaReference.Load();}
        //    //if (!usuarios.First().SegUsuarioRol.IsLoaded){usuarios.First().SegUsuarioRol.Load();}
        //    //if (!usuarios.First().SegUsuarioRol.IsLoaded){usuarios.First().SegUsuarioRol.Load();}


        //    if (usuarios.First().SegUsuarioRol.Count > 0)
        //    {

        //        this.Session["idusu"] = usuarios.First().IdSegUsuario;
        //        this.Session["idusuario"] = usuarios.First().IdSegUsuario;
        //        this.Session["nombreusu"] = usuarios.First().Login;
        //        this.Session["usuario"] = usuarios.First();

        //        if (usuarios.First().Empresa != null)
        //        {
        //            this.Session["TipoUsuario"] = "Cliente";
        //            this.Session["IdEmpresaContratista"] = usuarios.First().Empresa.IdEmpresa;
        //        }
        //        else
        //        {
        //            this.Session["TipoUsuario"] = "NoCliente";
        //            this.Session["IdEmpresaContratista"] = null;
        //        }

        //        Response.Redirect("~/Default.aspx");
        //        this.FailureText.Text = string.Empty;

        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alertesinpermisos", "ShowUsuarioSinPermisos();", true);
        //    }

        //}
        //else
        //{
        //    this.FailureText.Text = "Usuario o clave incorrectos!";
        //    this.UserName.Text = string.Empty;
        //}

    }
}
