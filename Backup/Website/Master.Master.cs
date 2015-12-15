using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.IO;

namespace Front
{
    public partial class Master : System.Web.UI.MasterPage
    {
        TB_USUARIO User;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                User = (TB_USUARIO)Session["User"];
                lblUsuario.Text = User.Login;
                TB_ACESSO Acesso_User = User.TB_ACESSO.FirstOrDefault();
                if (Acesso_User.DataFinal < DateTime.Now)
                {
                    if (Path.GetFileName(Request.Path) != "Pagamento.aspx" && Path.GetFileName(Request.Path) != "PagamentoEfetuado.aspx")
                        Response.Redirect("Pagamento.aspx");
                }
            }
            else
            {
                if (Path.GetFileName(Request.Path) != "Cadastro.aspx" && Path.GetFileName(Request.Path) != "TrocarSenha.aspx" && Path.GetFileName(Request.Path) != "PagamentoEfetuado.aspx")
                Response.Redirect("Login.aspx");

            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {

            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoServerCaching();
            HttpContext.Current.Response.Cache.SetNoStore();

          
            
        }
    }
}