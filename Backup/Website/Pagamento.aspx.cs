using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DAL;
using BLL;

namespace Front
{
    public partial class Pagamento : System.Web.UI.Page
    {
        TB_USUARIO User { get { return (TB_USUARIO)HttpContext.Current.Session["User"]; } }
        PagamentoBLL BLL = new PagamentoBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlAnchor txtBreadMenu = (HtmlAnchor)Page.Master.FindControl("txtBreadMenu");
            txtBreadMenu.InnerHtml = "Assinatura";

            HtmlGenericControl txtBreadPage = (HtmlGenericControl)Page.Master.FindControl("txtBreadPage");
            txtBreadPage.InnerHtml = "HBMAX";
        }
        public void btnUmMes_Click(object sender, EventArgs e)
        {
            Response.Redirect(BLL.NovoPagamento(User,1,"39,90").ToString());
        }
        public void btnTresMeses_Click(object sender, EventArgs e)
        {
            Response.Redirect(BLL.NovoPagamento(User, 3, "113,70").ToString());
        }
        public void btnSeisMeses_Click(object sender, EventArgs e)
        {
            Response.Redirect(BLL.NovoPagamento(User, 6, "215,45").ToString());
        }
        public void btnUmAno_Click(object sender, EventArgs e)
        {
            Response.Redirect(BLL.NovoPagamento(User, 12, "406,90").ToString());
        }
    }
}