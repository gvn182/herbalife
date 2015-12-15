using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.Relatorio;
using DAL;
using System.Web.UI.HtmlControls;

namespace Front
{
    public partial class FechamentoDiarioDetalhe : System.Web.UI.Page
    {
        FechamentoDiarioDetalheBLL BLL = new FechamentoDiarioDetalheBLL();
        TB_USUARIO User { get { return (TB_USUARIO)HttpContext.Current.Session["User"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {

            HtmlGenericControl liMovimentacao = (HtmlGenericControl)Page.Master.FindControl("liRelatorio");
            liMovimentacao.Attributes.Add("class", "active");

            HtmlAnchor txtBreadMenu = (HtmlAnchor)Page.Master.FindControl("txtBreadMenu");
            txtBreadMenu.InnerHtml = "Relatório";

            HtmlGenericControl txtBreadPage = (HtmlGenericControl)Page.Master.FindControl("txtBreadPage");
            txtBreadPage.InnerHtml = "Fechamento Diário Detalhe";

            HtmlGenericControl Menu = (HtmlGenericControl)Page.Master.FindControl("liFechamentoDiarioDetalhe");
            Menu.Attributes.Add("class", "active");
        }
        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            if (!IsPostBack)
            {
                if (Request.QueryString["Data"] != null)
                {
                    String Data = Request.QueryString["Data"].ToString();
                    txtDataInicialFinal.Value = Data + " - " + Data;
                    Fill();
                }
            }

        }
        protected void GdrPesquisa_DataBound(object sender, EventArgs e)
        {
            if (GdrPesquisa.Rows.Count > 0)
            {
                btnExcel.Visible = true;
                GdrPesquisa.HeaderRow.Cells[5].Text = "Produtos (R$)";
                GdrPesquisa.HeaderRow.Cells[6].Text = "Acessos (R$)";
                GdrPesquisa.HeaderRow.Cells[7].Text = "Total (R$)";
            }
            else
            {
                btnExcel.Visible = false;
            }
        }
        public void BtnPesquisar_Click(object sender, EventArgs e)
        {
            Fill();
        }
        protected void GdrPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.Cells[0].Text != "Total")
            {
                e.Row.Attributes.Add("style", "cursor:pointer;");
                e.Row.Attributes.Add("title", "Clique para ver detalhes");
                e.Row.Attributes.Add("onclick", "RowClickDetalhe('" + e.Row.RowIndex.ToString() + "')");
            }

        }
        private void Fill()
        {
            List<FechamentoDiarioDetalheBLL.FechamentoDiarioDetalheREP> List = BLL.GetFechamentoDiarioDetalhe(User.ID, txtDataInicialFinal.Value);
            Session["ListFechamentoDetalhe"] = List;
            GdrPesquisa.DataSource = List;
            GdrPesquisa.DataBind();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            BLL.ExportToExcel(Session["ListFechamentoDetalhe"], this);
        }
    }
}