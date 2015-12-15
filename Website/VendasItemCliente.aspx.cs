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
    public partial class VendasItemCliente : System.Web.UI.Page
    {
        VendaItemClienteBLL BLL = new VendaItemClienteBLL();
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
            txtBreadPage.InnerHtml = "Vendas Produto";

            HtmlGenericControl Menu = (HtmlGenericControl)Page.Master.FindControl("liVendasCliente");
            Menu.Attributes.Add("class", "active");

        }
        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            if (!IsPostBack)
            {

                ddCliente.DataSource = BLLCliente.GetAll(User.ID.ToString());
                ddCliente.DataTextField = "Nome";
                ddCliente.DataValueField = "ID";
                ddCliente.DataBind();
                ddCliente.Items.Insert(0, new ListItem("Todos", "-1"));

                ddProduto.DataSource = BLLProd.GetAll(User.ID.ToString()).Select(x => new { Descricao = x.COD_HERBALIFE + " - " + x.DESCRICAO, ID = x.ID });
                ddProduto.DataTextField = "Descricao";
                ddProduto.DataValueField = "ID";
                ddProduto.DataBind();
                ddProduto.Items.Insert(0, new ListItem("Todos", "-1"));

                if (Request.QueryString["Data"] != null && Request.QueryString["Cliente"] != null)
                {
                    String Data = Request.QueryString["Data"].ToString();
                    String Cliente = Request.QueryString["Cliente"].ToString();
                    txtDataInicialFinal.Value = Data + " - " + Data;
                    ddCliente.SelectedIndex = ddCliente.Items.IndexOf(ddCliente.Items.FindByText(Cliente));

                    Fill();
                }
            }
        }
        protected void GdrPesquisa_DataBound(object sender, EventArgs e)
        {

            if (GdrPesquisa.Rows.Count > 0)
            {
                btnExcel.Visible = true;
                GdrPesquisa.HeaderRow.Cells[3].Text = "Valor produto (R$)";
                GdrPesquisa.HeaderRow.Cells[4].Text = "Desconto (%)";
                GdrPesquisa.HeaderRow.Cells[5].Text = "Valor final (R$)";
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
            List<VendaItemClienteBLL.VendaItemClienteREP> List = BLL.GetFechamentoDiario(User.ID,ddCliente.Value,ddProduto.Value, txtDataInicialFinal.Value);
            Session["ListItemCliente"] = List;
            GdrPesquisa.DataSource = List;
            GdrPesquisa.DataBind();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            BLL.ExportToExcel(Session["ListItemCliente"], this);
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