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
    public partial class Produtos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl liCadastro = (HtmlGenericControl)Page.Master.FindControl("liCadastro");
            liCadastro.Attributes.Add("class", "active");

            HtmlAnchor txtBreadMenu = (HtmlAnchor)Page.Master.FindControl("txtBreadMenu");
            txtBreadMenu.InnerHtml = "Cadastro";

            HtmlGenericControl txtBreadPage = (HtmlGenericControl)Page.Master.FindControl("txtBreadPage");
            txtBreadPage.InnerHtml = "Produto";

            HtmlGenericControl Menu = (HtmlGenericControl)Page.Master.FindControl("liProduto");
            Menu.Attributes.Add("class", "active");
        }
        private static void CheckSession()
        {
            if (HttpContext.Current.Session["User"] == null)
                HttpContext.Current.Response.Redirect("Login.aspx");
        }
        
            [WebMethod(EnableSession = true)]
        public static object OpcoesEstoque()
        {
            ProdutoBLL BLL = new ProdutoBLL();
            List<ListItem> Itens = new List<ListItem>();
            Itens.Add(new ListItem("Sim", "Sim"));
            Itens.Add(new ListItem("Não", "Não"));

            var Opcoes = Itens.Select(c => new { DisplayText = c.Text, Value = c.Value });
            return new { Result = "OK", Options = Opcoes };
        }
        [WebMethod(EnableSession = true)]
        public static object OpcoesUnidade()
        {
            ProdutoBLL BLL = new ProdutoBLL();
            List<ProdutoBLL.UnidadeREP> Units = BLL.GetAllUnidades();

            var Opcoes = Units.Select(c => new { DisplayText = c.Descricao, Value = c.ID });
            return new { Result = "OK", Options = Opcoes };
        }
        [WebMethod(EnableSession = true)]
        public static object ListaProdutos(string filtro, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            CheckSession();
            TB_USUARIO User = (TB_USUARIO)HttpContext.Current.Session["User"];
            ProdutoBLL BLL = new ProdutoBLL();
            return BLL.GetAll(User.ID.ToString(), filtro, jtStartIndex, jtPageSize, jtSorting);
            
        }

        [WebMethod(EnableSession = true)]
        public static object AtualizarProduto(ProdutoBLL.ProdutoREP record)
        {
            CheckSession();
            TB_USUARIO User = (TB_USUARIO)HttpContext.Current.Session["User"];
            ProdutoBLL BLL = new ProdutoBLL();
            int Resu = BLL.Update(record.ID_Produto.ToString(), User.ID.ToString(), record.Descricao.ToString(), record.UNID.ToString(), record.PV.ToString(), record.Preco.ToString(), record.Gasto_Estimado.ToString(), record.Estoque_Minimo.ToString(), record.Afeta_Estoque.ToString());
            if (Resu == 1)
                return new { Result = "OK" };
            else
                return new { Result = "ERROR", Message = "Já existe um registro com esse código herbalife" };
        }
        [WebMethod(EnableSession = true)]
        public static object AddNewProduto(ProdutoBLL.ProdutoREP record)
        {
            CheckSession();
            TB_USUARIO User = (TB_USUARIO)HttpContext.Current.Session["User"];
            ProdutoBLL BLL = new ProdutoBLL();
            ProdutoBLL.ProdutoREP Prod = BLL.Add(User.ID.ToString(), record.COD_Herbalife, record.Descricao, record.UNID, record.PV, record.Preco, record.Gasto_Estimado, record.Estoque_Minimo, record.Afeta_Estoque);
            if (Prod != null)
                return new { Result = "OK", Record = Prod };
            else
                return new { Result = "ERROR", Message = "Já existe um registro com esse código herbalife" };
        }
        [WebMethod(EnableSession = true)]
        public static object ExcluirProduto(string ID_Produto)
        {
            try
            {
                ProdutoBLL BLL = new ProdutoBLL();
                int Result = BLL.Delete(ID_Produto);

                if (Result == 1)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Este produto já foi utilizado, não é possivel remove-lo" };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }
    }
}