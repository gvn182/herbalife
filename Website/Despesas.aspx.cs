using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using DAL;
using BLL;
using System.Web.UI.HtmlControls;

namespace Front
{
    public partial class Despesas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl liCadastro = (HtmlGenericControl)Page.Master.FindControl("liMovimentacao");
            liCadastro.Attributes.Add("class", "active");

            HtmlAnchor txtBreadMenu = (HtmlAnchor)Page.Master.FindControl("txtBreadMenu");
            txtBreadMenu.InnerHtml = "Movimentação";

            HtmlGenericControl txtBreadPage = (HtmlGenericControl)Page.Master.FindControl("txtBreadPage");
            txtBreadPage.InnerHtml = "Despesa";

            HtmlGenericControl Menu = (HtmlGenericControl)Page.Master.FindControl("liDespesa");
            Menu.Attributes.Add("class", "active");
        }
        private static void CheckSession()
        {
            if (HttpContext.Current.Session["User"] == null)
                HttpContext.Current.Response.Redirect("Login.aspx");
        }
        [WebMethod(EnableSession = true)]
        public static object ListaDespesas(string filtro, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            CheckSession();
            TB_USUARIO User = (TB_USUARIO)HttpContext.Current.Session["User"];
            DespesaBLL BLL = new DespesaBLL();
            return BLL.GetAll(User.ID.ToString(), filtro, jtStartIndex, jtPageSize, jtSorting);

        }

        [WebMethod(EnableSession = true)]
        public static object AtualizarDespesa(DespesaBLL.DespesaREP record)
        {
            CheckSession();
            TB_USUARIO User = (TB_USUARIO)HttpContext.Current.Session["User"];
            DespesaBLL BLL = new DespesaBLL();
            int Resu = BLL.Update(User.ID.ToString(), record.Codigo, record.Data, record.Descricao, record.ValorTotal);
            if (Resu == 1)
                return new { Result = "OK" };
            else
                return new { Result = "ERROR", Message = "Já existe um registro com esse código herbalife" };
        }
        [WebMethod(EnableSession = true)]
        public static object AddNewDespesa(DespesaBLL.DespesaREP record)
        {
            CheckSession();
            TB_USUARIO User = (TB_USUARIO)HttpContext.Current.Session["User"];
            DespesaBLL BLL = new DespesaBLL();
            DespesaBLL.DespesaREP Prod = BLL.Add(User.ID.ToString(), record.Data, record.Descricao, record.ValorTotal);
            if (Prod != null)
                return new { Result = "OK", Record = Prod };
            else
                return new { Result = "ERROR", Message = "Erro ao inserir" };
        }
        [WebMethod(EnableSession = true)]
        public static object ExcluirDespesa(string Codigo)
        {
            try
            {
                DespesaBLL BLL = new DespesaBLL();
                int Result = BLL.Delete(Codigo);

                if (Result == 1)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Erro ao excluir" };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }
    }
}