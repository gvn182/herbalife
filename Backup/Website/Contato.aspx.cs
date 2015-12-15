using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Configuration;

namespace Front
{
    public partial class Contato : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static void SendMail(String Nome, String Email, String Mensagem)
        {
            Util.Email Eml = new Util.Email();
            String From = ConfigurationManager.AppSettings["ContatoFrom"].ToString();
            Eml.SendEmail("Contato", From, "Email de: " + Nome, "Email: " + Email + "<br/>" + Mensagem);
        }
    }
}