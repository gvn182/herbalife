using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using BLL;
using DAL;
using BLL.Relatorio;

namespace Front
{
    public partial class Venda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl liMovimentacao = (HtmlGenericControl)Page.Master.FindControl("liMovimentacao");
            liMovimentacao.Attributes.Add("class", "active");

            HtmlAnchor txtBreadMenu = (HtmlAnchor)Page.Master.FindControl("txtBreadMenu");
            txtBreadMenu.InnerHtml = "Movimentação";

            HtmlGenericControl txtBreadPage = (HtmlGenericControl)Page.Master.FindControl("txtBreadPage");
            txtBreadPage.InnerHtml = "Venda";

            HtmlGenericControl Menu = (HtmlGenericControl)Page.Master.FindControl("liVenda");
            Menu.Attributes.Add("class", "active");
        }


        [WebMethod]
        public static object GetAllProdutos()
        {
            CheckSession();
            TB_USUARIO User = (TB_USUARIO)HttpContext.Current.Session["User"];
            ProdutoBLL BLL = new ProdutoBLL();
            List<TB_PRODUTO> Lst = BLL.GetAll(User.ID.ToString());
            var Opcoes = Lst.Select(c => new { Text = c.COD_HERBALIFE.ToString() + " - " + c.DESCRICAO, Value = c.ID });
            return Opcoes;
        }
        [WebMethod]
        public static object GetAllClientes()
        {
            CheckSession();
            TB_USUARIO User = (TB_USUARIO)HttpContext.Current.Session["User"];
            ClienteBLL BLL = new ClienteBLL();
            List<TB_CLIENTE> Lst = BLL.GetAll(User.ID.ToString());
            var Opcoes = Lst.Select(c => new { Text = c.Codigo + " - " + c.Nome, Value = c.ID });
            return Opcoes;
        }
        [WebMethod]
        public static object GetValorProduto(String ID)
        {
            CheckSession();
            TB_USUARIO User = (TB_USUARIO)HttpContext.Current.Session["User"];
            ProdutoBLL BLL = new ProdutoBLL();
            TB_PRODUTO Prod = BLL.GetProdutoBy(int.Parse(ID));

            object Produto = new { Preco = Prod.PRECO.ToString("N"), Qtd_Estoque = Prod.QTD_ESTOQUE, Qtd_Minima = Prod.ESTOQUE_MINIMO, Cod_Herbalife = Prod.COD_HERBALIFE.ToString(), Requer_Estoque = Prod.AFETA_ESTOQUE };
            return Produto;
        }
        [WebMethod]
        public static object GetAcessosCliente(String ID)
        {
            CheckSession();
            TB_USUARIO User = (TB_USUARIO)HttpContext.Current.Session["User"];
            VendaBLL BLL = new VendaBLL();
            ClienteBLL BLLCliente = new ClienteBLL();
            TB_CLIENTE Cli = BLLCliente.GetBy(ID);

            object Retorno = new { Acessos = BLL.GetVendasBy(User.ID, ID).Count, Nome = Cli.Nome };
            return Retorno;
        }
        [WebMethod]
        public static object SalverVenda(String Data, String ID_Cliente, String Pagamento,String Obs, List<BLL.VendaBLL.VendaProdutoREP> LstProdutos)
        {
            CheckSession();
            TB_USUARIO User = (TB_USUARIO)HttpContext.Current.Session["User"];
            VendaBLL BLL = new VendaBLL();
            return BLL.NovaVenda(User.ID, Data, ID_Cliente, Pagamento, Obs, LstProdutos);
        }
        [WebMethod(EnableSession = true)]
        public static object AddNovoCliente(ClienteBLL.ClienteREP record)
        {
            CheckSession();
            TB_USUARIO User = (TB_USUARIO)HttpContext.Current.Session["User"];
            ClienteBLL BLL = new ClienteBLL();
            ClienteBLL.ClienteREP Prod = BLL.Add(User.ID.ToString(), record.ComoSoube, record.Email, record.Nome, record.Telefone, record.Tipo, record.Indicacao);
            return new { Result = "OK", Record = Prod };
        }
        [WebMethod(EnableSession = true)]
        public static object CheckCaixa(string Date)
        {
            CheckSession();
            TB_USUARIO User = (TB_USUARIO)HttpContext.Current.Session["User"];
            VendaBLL BLL = new VendaBLL();
            if (BLL.CheckCaixa(User.ID.ToString(), Date))
            {
                return new { Result = "TRUE" };
            }
            else
            {
                return new { Result = "FALSE" };
            }
        }
          [WebMethod(EnableSession = true)]
        public static object AbreCaixa(string Date, string Valor)
        {
            TB_USUARIO User = (TB_USUARIO)HttpContext.Current.Session["User"];
            VendaBLL BLL = new VendaBLL();
            BLL.AbreCaixa(User.ID.ToString(), Date, Valor);
            return new { Result = "OK" };
        }
        [WebMethod(EnableSession = true)]
        public static object GetAcessos(string Date)
        {
            CheckSession();
            TB_USUARIO User = (TB_USUARIO)HttpContext.Current.Session["User"];
            Date = Date + " - " + Date;
            FechamentoDiarioDetalheBLL BLL = new FechamentoDiarioDetalheBLL();
            List<FechamentoDiarioDetalheBLL.FechamentoDiarioDetalheREP> List = BLL.GetFechamentoDiarioDetalhe(User.ID, Date);

            var Lista = List.Select(c => new { Ordem = c.Ordem, Status = c.Status, Nome = c.Nome, Valor = c.Total });
            return Lista;

        }
        private static void CheckSession()
        {
            if (HttpContext.Current.Session["User"] == null)
                HttpContext.Current.Response.Redirect("Login.aspx");
        }

    }
}