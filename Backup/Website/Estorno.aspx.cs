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
using BLL.Relatorio;

namespace Front
{
    public partial class Estorno : System.Web.UI.Page
    {
        VendaBLL BLL_Venda = new VendaBLL();
        TB_USUARIO User { get { return (TB_USUARIO)HttpContext.Current.Session["User"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl liMovimentacao = (HtmlGenericControl)Page.Master.FindControl("liMovimentacao");
            liMovimentacao.Attributes.Add("class", "active");

            HtmlAnchor txtBreadMenu = (HtmlAnchor)Page.Master.FindControl("txtBreadMenu");
            txtBreadMenu.InnerHtml = "Movimentação";

            HtmlGenericControl txtBreadPage = (HtmlGenericControl)Page.Master.FindControl("txtBreadPage");
            txtBreadPage.InnerHtml = "Estorno";

            HtmlGenericControl Menu = (HtmlGenericControl)Page.Master.FindControl("liEstorno");
            Menu.Attributes.Add("class", "active");

            if (!IsPostBack)
            {
                if (Request.QueryString["Data"] != null)
                {
                    String Data = Request.QueryString["Data"].ToString();
                    txtFiltro.Value = Data + " - " + Data;
                    FillPesquisa();
                }
            }
        }
        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            //if (!IsPostBack)
            //{
            //   FillPesquisa();
            //}
        }
        public void BtnPesquisar_Click(object sender, EventArgs e)
        {
            FillPesquisa();
        }
        private void FillPesquisa()
        {
            List<BLL.VendaBLL.VendaProdutosEstornoREP> LstResultado = BLL_Venda.GetVendasProdutosEstorno(User.ID, txtFiltro.Value);
            Session["DataSourcePesquisa"] = LstResultado;
            GdrPesquisa.DataSource = LstResultado;
            GdrPesquisa.DataBind();
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
            List<BLL.VendaBLL.VendaProdutosEstornoREP> myGridResults = (List<BLL.VendaBLL.VendaProdutosEstornoREP>)Session["DataSourcePesquisa"];

            if (myGridResults != null)
            {
                var param = Expression.Parameter(typeof(BLL.VendaBLL.VendaProdutosEstornoREP), e.SortExpression);
                var sortExpression = Expression.Lambda<Func<BLL.VendaBLL.VendaProdutosEstornoREP, object>>(Expression.Convert(Expression.Property(param, e.SortExpression), typeof(object)), param);


                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GdrPesquisa.DataSource = myGridResults.AsQueryable<BLL.VendaBLL.VendaProdutosEstornoREP>().OrderBy(sortExpression).ToList();
                    GridViewSortDirection = SortDirection.Descending;
                }
                else
                {
                    GdrPesquisa.DataSource = myGridResults.AsQueryable<BLL.VendaBLL.VendaProdutosEstornoREP>().OrderByDescending(sortExpression).ToList();
                    GridViewSortDirection = SortDirection.Ascending;
                };

                GdrPesquisa.DataBind();
            }
        }
        protected void OnItemClick(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int RowIndex = int.Parse(btn.CommandArgument);
            RowIndex = RowIndex - GdrPesquisa.PageIndex * GdrPesquisa.PageSize;
            int ID = int.Parse(GdrPesquisa.Rows[RowIndex].Cells[1].Text);
            if (btn.CommandName.Equals("Estorno"))
            {
                BLL_Venda.Estornar(ID);
                FillPesquisa();
                ClientScript.RegisterStartupScript(this.GetType(), "AlertEstorno", "MessageBox('Estorno efetuado com sucesso');", true);
            }
        }
        protected void GdrPesquisa_DataBound(object sender, EventArgs e)
        {

            for (int i = 0; i < GdrPesquisa.Rows.Count; i++)
            {
                GdrPesquisa.HeaderRow.Cells[1].Visible = false;
                GdrPesquisa.Rows[i].Cells[1].Visible = false;

            }
        }
     
    }
}