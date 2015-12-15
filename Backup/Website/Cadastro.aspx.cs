using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using DAL;
using System.Web.Services;
using System.Collections.Specialized;
using System.Web.UI.HtmlControls;
using Util;

namespace Front
{
    public partial class Cadastro : System.Web.UI.Page
    {
        UsuarioBLL UserBLL = new UsuarioBLL();
        SecurityUTL SecUtl = new SecurityUTL();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            HtmlAnchor txtBreadMenu = (HtmlAnchor)Page.Master.FindControl("txtBreadMenu");
            txtBreadMenu.InnerHtml = "Cadastro";

            HtmlGenericControl txtBreadPage = (HtmlGenericControl)Page.Master.FindControl("txtBreadPage");
            txtBreadPage.InnerHtml = "Confirmação";

             HtmlGenericControl ulMenu = (HtmlGenericControl)Page.Master.FindControl("ulMenu");
             ulMenu.Visible = false;

             if (Request.QueryString["Confirmacao"] != null)
             {
                 String ID = SecUtl.Decrypting(Request.QueryString["Confirmacao"].ToString());
                 int outResult;
                 if (int.TryParse(ID, out outResult))
                 {
                     TB_USUARIO Usr = UserBLL.GetBy(outResult);
                     if (Usr != null)
                     {
                         if (Usr.Email_Confirmado)
                             Response.Redirect("Dashboard.aspx");
                         else
                             Session["User"] = Usr;
                     }
                     else
                         Response.Redirect("Login.aspx");
                 }
                 else
                     Response.Redirect("Login.aspx");
             }
             else
                 Response.Redirect("Login.aspx");

        }
        [WebMethod]
        public static void AddDetalhes(String Nome, String CEP, String Endereco, String Numero, String Complemento, String Bairro, String Cidade, String Estado, String RG, String CPF, String Telefone, String Celular, String Nivel)
        {
            UsuarioBLL UserBLL = new UsuarioBLL();
            TB_USUARIO User = (TB_USUARIO)HttpContext.Current.Session["User"];
            String ID_Usuario = User.ID.ToString();
            UserBLL.AddDetalhes(ID_Usuario, Nome, CEP, Endereco, Numero, Complemento, Bairro, Cidade, Estado, RG, CPF, Telefone, Celular, Nivel);
            UserBLL.ValidaEmail(int.Parse(ID_Usuario));
        }
        [WebMethod]
        public static TB_ENDERECO GetEndereco(String CEP)
        {
            UsuarioBLL UserBLL = new UsuarioBLL();
            return UserBLL.GetEndereco(CEP);
        }

      
    }
}