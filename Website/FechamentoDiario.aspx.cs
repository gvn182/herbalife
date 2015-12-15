using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.Relatorio;
using DAL;
using System.Web.UI.HtmlControls;
using System.Linq.Expressions;

namespace Front
{
    public partial class FechamentoDiario : System.Web.UI.Page
    {
        FechamentoDiarioBLL BLL = new FechamentoDiarioBLL();
        TB_USUARIO User { get { return (TB_USUARIO)HttpContext.Current.Session["User"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {

            HtmlGenericControl liMovimentacao = (HtmlGenericControl)Page.Master.FindControl("liRelatorio");
            liMovimentacao.Attributes.Add("class", "active");

            HtmlAnchor txtBreadMenu = (HtmlAnchor)Page.Master.FindControl("txtBreadMenu");
            txtBreadMenu.InnerHtml = "Relatório";

            HtmlGenericControl txtBreadPage = (HtmlGenericControl)Page.Master.FindControl("txtBreadPage");
            txtBreadPage.InnerHtml = "Fechamento Diário";

            HtmlGenericControl Menu = (HtmlGenericControl)Page.Master.FindControl("liFechamentoDiario");
            Menu.Attributes.Add("class", "active");


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
                GdrPesquisa.HeaderRow.Cells[1].Text = "Total clientes";
                GdrPesquisa.HeaderRow.Cells[2].Text = "Total entradas crédito";
                GdrPesquisa.HeaderRow.Cells[3].Text = "Total entradas débito";
                GdrPesquisa.HeaderRow.Cells[4].Text = "Total entradas outros";
                GdrPesquisa.HeaderRow.Cells[5].Text = "Total entradas dinheiro";
                GdrPesquisa.HeaderRow.Cells[6].Text = "Abertura caixa";
                GdrPesquisa.HeaderRow.Cells[7].Text = "Ganho acessos";
                GdrPesquisa.HeaderRow.Cells[8].Text = "Ganho produto";
                GdrPesquisa.HeaderRow.Cells[9].Text = "Ganho total";
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
            List<FechamentoDiarioBLL.FechamentoDiarioREP> List = BLL.GetFechamentoDiario(User.ID, txtDataInicialFinal.Value);
            Session["ListFechamento"] = List;
            GdrPesquisa.DataSource = List;
            GdrPesquisa.DataBind();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            BLL.ExportToExcel(Session["ListFechamento"], this);
        }
       
        protected void GdrPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.Cells[0].Text != "Total")
            {
                e.Row.Attributes.Add("style", "cursor:pointer;");
                e.Row.Attributes.Add("title", "Clique para ver detalhes");
                e.Row.Attributes.Add("onclick", "RowClick('" + e.Row.RowIndex.ToString() + "')");
            }

        }
        protected void GdrPesquisa_Sorting(object sender, GridViewSortEventArgs e)
        {
            //re-run the query, use linq to sort the objects based on the arg.
            //perform a search using the constraints given 
            //you could have this saved in Session, rather than requerying your datastore
            List<FechamentoDiarioBLL.FechamentoDiarioREP> myGridResults = (List<FechamentoDiarioBLL.FechamentoDiarioREP>)Session["ListFechamento"];


            if (myGridResults != null)
            {
                var param = Expression.Parameter(typeof(FechamentoDiarioBLL.FechamentoDiarioREP), e.SortExpression);
                var sortExpression = Expression.Lambda<Func<FechamentoDiarioBLL.FechamentoDiarioREP, object>>(Expression.Convert(Expression.Property(param, e.SortExpression), typeof(object)), param);


                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GdrPesquisa.DataSource = myGridResults.AsQueryable<FechamentoDiarioBLL.FechamentoDiarioREP>().OrderBy(sortExpression);
                    GridViewSortDirection = SortDirection.Descending;
                }
                else
                {
                    GdrPesquisa.DataSource = myGridResults.AsQueryable<FechamentoDiarioBLL.FechamentoDiarioREP>().OrderByDescending(sortExpression);
                    GridViewSortDirection = SortDirection.Ascending;
                };
                GdrPesquisa.DataBind();
            }
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
    }
}