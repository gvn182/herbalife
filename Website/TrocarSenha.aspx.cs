using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using DAL;
using BLL;
using Util;

namespace Front
{
    public partial class TrocarSenha : System.Web.UI.Page
    {
        SecurityUTL SecUtl = new SecurityUTL();
        UsuarioBLL UserBLL = new UsuarioBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlAnchor txtBreadMenu = (HtmlAnchor)Page.Master.FindControl("txtBreadMenu");
            txtBreadMenu.InnerHtml = "Configurações";

            HtmlGenericControl txtBreadPage = (HtmlGenericControl)Page.Master.FindControl("txtBreadPage");
            txtBreadPage.InnerHtml = "Alterar senha";

            if (Request.QueryString["Reenvio"] != null)
            {
                String ID = SecUtl.Decrypting(Request.QueryString["Reenvio"].ToString());
                int outResult;
                if (int.TryParse(ID, out outResult))
                {
                    TB_USUARIO Usr = UserBLL.GetBy(outResult);
                    if (Usr != null)
                    {
                        Session["Reenvio"] = true;
                        Session["QueryID"] = Usr.ID.ToString();
                    }
                    else
                        Response.Redirect("Login.aspx");
                }
                else
                    Response.Redirect("Login.aspx");
            }

        }
        [WebMethod]
        public static int Salvar(String SenhaAntinga, String NovaSenha)
        {

            UsuarioBLL BLL = new UsuarioBLL();
            SecurityUTL SecUtl = new SecurityUTL();
            bool isReenvio = HttpContext.Current.Session["Reenvio"] != null ? true : false;
            if (isReenvio)
            {
                String ID = HttpContext.Current.Session["QueryID"].ToString();
                TB_USUARIO CurrUser = BLL.GetBy(int.Parse(ID));
                HttpContext.Current.Session["Reenvio"] = null;
                HttpContext.Current.Session["QueryID"] = null;
                HttpContext.Current.Session["User"] = CurrUser;
                return BLL.ChangePass(CurrUser.ID.ToString(), NovaSenha);

            }
            else
            {
                TB_USUARIO CurrUser = (TB_USUARIO)HttpContext.Current.Session["User"];
                return BLL.ChangePass(CurrUser.Login, SenhaAntinga, NovaSenha);
            }
        }
    }
}