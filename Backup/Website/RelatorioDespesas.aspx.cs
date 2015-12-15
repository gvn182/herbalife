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
    public partial class RelatorioDespesas : System.Web.UI.Page
    {
        DespesaBLL BLL = new DespesaBLL();
        TB_USUARIO User { get { return (TB_USUARIO)HttpContext.Current.Session["User"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {

            HtmlGenericControl liMovimentacao = (HtmlGenericControl)Page.Master.FindControl("liRelatorio");
            liMovimentacao.Attributes.Add("class", "active");

            HtmlAnchor txtBreadMenu = (HtmlAnchor)Page.Master.FindControl("txtBreadMenu");
            txtBreadMenu.InnerHtml = "Relatório";

            HtmlGenericControl txtBreadPage = (HtmlGenericControl)Page.Master.FindControl("txtBreadPage");
            txtBreadPage.InnerHtml = "Despesas";

            HtmlGenericControl Menu = (HtmlGenericControl)Page.Master.FindControl("liDespesas");
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
                GdrPesquisa.HeaderRow.Cells[2].Text = "Valor (R$)";
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
            List<DespesaBLL.RelDespesaREP> List = BLL.GetDespesas(User.ID, txtDataInicialFinal.Value);
            Session["ListDespesa"] = List;
            GdrPesquisa.DataSource = List;
            GdrPesquisa.DataBind();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            BLL.ExportToExcel(Session["ListDespesa"], this);
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