using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.Relatorio;
using DAL;
using System.Web.UI.HtmlControls;
using BLL;

namespace Front
{
    public partial class MovimentacaoEstoque : System.Web.UI.Page
    {
        MovimentacaoEstoqueBLL BLL = new MovimentacaoEstoqueBLL();
        TB_USUARIO User { get { return (TB_USUARIO)HttpContext.Current.Session["User"]; } }
        ClienteBLL BLLCliente = new ClienteBLL();
        ProdutoBLL BLLProd = new ProdutoBLL();
        protected void Page_Load(object sender, EventArgs e)
        {

            HtmlGenericControl liMovimentacao = (HtmlGenericControl)Page.Master.FindControl("liRelatorio");
            liMovimentacao.Attributes.Add("class", "active");

            HtmlAnchor txtBreadMenu = (HtmlAnchor)Page.Master.FindControl("txtBreadMenu");
            txtBreadMenu.InnerHtml = "Relatório";

            HtmlGenericControl txtBreadPage = (HtmlGenericControl)Page.Master.FindControl("txtBreadPage");
            txtBreadPage.InnerHtml = "Movimentação Produto";

            HtmlGenericControl Menu = (HtmlGenericControl)Page.Master.FindControl("liMovimentacaoProduto");
            Menu.Attributes.Add("class", "active");

        }
        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            if (!IsPostBack)
            {

                ddProduto.DataSource = BLLProd.GetAllAfetaEstoque(User.ID.ToString()).Select(x => new { Descricao = x.COD_HERBALIFE + " - " + x.DESCRICAO, ID = x.ID });
                ddProduto.DataTextField = "Descricao";
                ddProduto.DataValueField = "ID";
                ddProduto.DataBind();
                ddProduto.Items.Insert(0, new ListItem("Todos", "-1"));

                //if (Request.QueryString["Data"] != null && Request.QueryString["Cliente"] != null)
                //{
                //    String Data = Request.QueryString["Data"].ToString();
                //    String Cliente = Request.QueryString["Cliente"].ToString();
                //    txtDataInicialFinal.Value = Data + " - " + Data;
                //    ddCliente.SelectedIndex = ddCliente.Items.IndexOf(ddCliente.Items.FindByText(Cliente));

                //    Fill();
                //}
            }
        }
        protected void GdrPesquisa_DataBound(object sender, EventArgs e)
        {

            if (GdrPesquisa.Rows.Count > 0)
            {
                btnExcel.Visible = true;
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
        private void Fill()
        {
            List<MovimentacaoEstoqueBLL.MovimentacaoEstoqueREP> List = BLL.GetEstoque(txtDataInicialFinal.Value, User.ID,ddProduto.Value, ddFiltro.SelectedValue);
            Session["ListItemEstoque"] = List;
            GdrPesquisa.DataSource = List;
            GdrPesquisa.DataBind();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            BLL.ExportToExcel(Session["ListItemEstoque"], this);
        }

        protected void GdrPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    e.Row.Attributes.Add("style", "cursor:pointer;");
            //    e.Row.Attributes.Add("title", "Clique para ver detalhes");
            //    e.Row.Attributes.Add("onclick", "RowClick('" + e.Row.RowIndex.ToString() + "')");
            //}

        }
    }
}