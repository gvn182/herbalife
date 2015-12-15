using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using BLL;
using DAL;

namespace Front
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //UsuarioBLL BLL = new UsuarioBLL();
            //TB_USUARIO Usr = BLL.GetBy("gvn007", "giovanni");
            //HttpContext.Current.Session["User"] = Usr;
            //Response.Redirect("VendasItemCliente.aspx");
        }

        [WebMethod]
        public static int NovoUsuario(string Usuario, string Senha, string Email, string Telefone)
        {
            UsuarioBLL BLL = new UsuarioBLL();
            return BLL.Add(Usuario, Senha, Email, Telefone);
        }
        [WebMethod]
        public static bool EsqueciMinhaSenha(string Email)
        {
            UsuarioBLL BLL = new UsuarioBLL();
            return BLL.EsqueciSenha(Email);
            
        }

       
        [WebMethod]
        public static int CheckUser(string Usuario, string Senha)
        {
            UsuarioBLL BLL = new UsuarioBLL();
            TB_USUARIO Usr = BLL.GetBy(Usuario, Senha);

            if (Usr != null)
            {
                if (Usr.Email_Confirmado)
                {

                    HttpContext.Current.Session["User"] = Usr;
                    return Usr.ID;
                }
                else
                {
                    BLL.ReenviaConfirmacao(Usr);
                    return -1;
                }
            }
            return -2;
        }
    }
}