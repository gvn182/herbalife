using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BLL;
using DAL;
using System.Linq.Expressions;

namespace Front
{
    public partial class Estoque : System.Web.UI.Page
    {
        ProdutoBLL ProdBLL = new ProdutoBLL();
        EstoqueBLL EstBLL = new EstoqueBLL();
        TB_USUARIO User { get { return (TB_USUARIO)HttpContext.Current.Session["User"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl liMovimentacao = (HtmlGenericControl)Page.Master.FindControl("liMovimentacao");
            liMovimentacao.Attributes.Add("class", "active");

            HtmlAnchor txtBreadMenu = (HtmlAnchor)Page.Master.FindControl("txtBreadMenu");
            txtBreadMenu.InnerHtml = "Movimentação";

            HtmlGenericControl txtBreadPage = (HtmlGenericControl)Page.Master.FindControl("txtBreadPage");
            txtBreadPage.InnerHtml = "Estoque";

            HtmlGenericControl Menu = (HtmlGenericControl)Page.Master.FindControl("liEstoque");
            Menu.Attributes.Add("class", "active");
        }
        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            if (!IsPostBack)
            {
                FillPesquisa();
            }
        }
        public void BtnPesquisar_Click(object sender, EventArgs e)
        {
            FillPesquisa();
        }
        private void FillPesquisa()
        {
            List<ProdutoBLL.ProdutoEstoqueREP> LstResultado = ProdBLL.GetAllEstoque(User.ID.ToString(), txtFiltro.Value);
            Session["DataSourcePesquisa"] = LstResultado;
            GdrPesquisa.DataSource = LstResultado;
            GdrPesquisa.DataBind();
        }
        protected void OnItemClick(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int RowIndex = int.Parse(btn.CommandArgument);
            RowIndex = RowIndex - GdrPesquisa.PageIndex * GdrPesquisa.PageSize;
            int ID = int.Parse(GdrPesquisa.Rows[RowIndex].Cells[1].Text);
            if (btn.CommandName.Equals("Historico"))
            {
                FillModalHistorico(ID);
            }
            else if (btn.CommandName.Equals("Adicionar"))
            {
                FillModalAdicionar(ID);
            }
            else
            {
                FillModalRemover(ID);
            }
        }

        private void FillModalHistorico(int ID_Produto)
        {
            TB_PRODUTO Prod = ProdBLL.GetProdutoBy(ID_Produto);
            HdnSelectedID.Value = Prod.ID.ToString();
            lblCodigoHistorico.Text = Prod.COD_HERBALIFE.ToString();
            lblProdutoHistorico.Text = Prod.DESCRICAO.ToString();
            ClientScript.RegisterStartupScript(this.GetType(), "OpenModalHistorico", "OpenModalHistorico();", true);
        }

        private void FillModalAdicionar(int ID_Produto)
        {
            TB_PRODUTO Prod = ProdBLL.GetProdutoBy(ID_Produto);
            HdnSelectedID.Value = Prod.ID.ToString();
            lblCodigoProdutoAdicionar.Text = Prod.COD_HERBALIFE.ToString();
            lblProdutoAdicionar.Text = Prod.DESCRICAO.ToString();
            lblQtdAtualAdicionar.Text = Prod.QTD_ESTOQUE.ToString();
            ClientScript.RegisterStartupScript(this.GetType(), "OpenModalAdicionar", "OpenModalAdicionar();", true);
        }

        private void FillModalRemover(int ID_Produto)
        {
            TB_PRODUTO Prod = ProdBLL.GetProdutoBy(ID_Produto);
            HdnSelectedID.Value = Prod.ID.ToString();
            lblCodigoProdutoRemover.Text = Prod.COD_HERBALIFE.ToString();
            lblProdutoRemover.Text = Prod.DESCRICAO.ToString();
            lblQtdAtualRemover.Text =Prod.QTD_ESTOQUE.ToString();
            ClientScript.RegisterStartupScript(this.GetType(), "OpenModalRemover", "OpenModalRemover();", true);
        }
        protected void btnSalvarRemover_Click(object sender, EventArgs e)
        {
            ProdBLL.RemoveEstoque(int.Parse(HdnSelectedID.Value), int.Parse(txtQtdRemover.Value), ddMotivo.SelectedValue);
            FillPesquisa();
            ClientScript.RegisterStartupScript(this.GetType(), "AlertEstoque", "MessageBox('Estoque atualizado com sucesso');", true);
        }
        protected void btnSalvarAdicionar_Click(object sender, EventArgs e)
        {
            ProdBLL.AdicionaEstoque(int.Parse(HdnSelectedID.Value), int.Parse(txtQtdAdicionar.Value),"Inserção Manual");
            FillPesquisa();
            ClientScript.RegisterStartupScript(this.GetType(), "AlertEstoque", "MessageBox('Estoque atualizado com sucesso');", true);
        }
        protected void GdrPesquisa_DataBound(object sender, EventArgs e)
        {

            for (int i = 0; i < GdrPesquisa.Rows.Count; i++)
            {
                GdrPesquisa.HeaderRow.Cells[1].Visible = false;
                GdrPesquisa.Rows[i].Cells[1].Visible = false;

            }
        }
        protected void GdrPesquisa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GdrPesquisa.DataSource = Session["DataSourcePesquisa"];
            GdrPesquisa.PageIndex = e.NewPageIndex;
            GdrPesquisa.DataBind();
        }
        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }
        protected void GdrConteudo_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<ProdutoBLL.ProdutoEstoqueREP> myGridResults = (List<ProdutoBLL.ProdutoEstoqueREP>)Session["DataSourcePesquisa"];

            if (myGridResults != null)
            {
                var param = Expression.Parameter(typeof(ProdutoBLL.ProdutoEstoqueREP), e.SortExpression);
                var sortExpression = Expression.Lambda<Func<ProdutoBLL.ProdutoEstoqueREP, object>>(Expression.Convert(Expression.Property(param, e.SortExpression), typeof(object)), param);


                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GdrPesquisa.DataSource = myGridResults.AsQueryable<ProdutoBLL.ProdutoEstoqueREP>().OrderBy(sortExpression).ToList();
                    GridViewSortDirection = SortDirection.Descending;
                }
                else
                {
                    GdrPesquisa.DataSource = myGridResults.AsQueryable<ProdutoBLL.ProdutoEstoqueREP>().OrderByDescending(sortExpression).ToList();
                    GridViewSortDirection = SortDirection.Ascending;
                };

                GdrPesquisa.DataBind();
            }
        }

        protected void btnPesquisarHistorico_Click(object sender, EventArgs e)
        {
            GdrHistorico.DataSource = EstBLL.GetAllBy(HdnSelectedID.Value.ToString(), txtDataInicialFinal.Text, ddFiltro.SelectedValue.ToString());
            GdrHistorico.DataBind();
            ClientScript.RegisterStartupScript(this.GetType(), "OpenModalHistorico", "OpenModalHistorico();", true);
        }

        protected void GdrHistorico_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < GdrHistorico.Rows.Count; i++)
            {
                GdrHistorico.HeaderRow.Cells[0].Visible = false;
                GdrHistorico.HeaderRow.Cells[1].Visible = false;
                GdrHistorico.Rows[i].Cells[0].Visible = false;
                GdrHistorico.Rows[i].Cells[1].Visible = false;
            }
        }

    }
}