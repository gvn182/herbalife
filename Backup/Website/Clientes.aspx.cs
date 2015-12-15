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
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl liCadastro = (HtmlGenericControl)Page.Master.FindControl("liCadastro");
            liCadastro.Attributes.Add("class", "active");

            HtmlAnchor txtBreadMenu = (HtmlAnchor)Page.Master.FindControl("txtBreadMenu");
            txtBreadMenu.InnerHtml = "Cadastro";

            HtmlGenericControl txtBreadPage = (HtmlGenericControl)Page.Master.FindControl("txtBreadPage");
            txtBreadPage.InnerHtml = "Cliente";

            HtmlGenericControl Menu = (HtmlGenericControl)Page.Master.FindControl("liCliente");
            Menu.Attributes.Add("class", "active");
        }
        [WebMethod(EnableSession = true)]
        public static object OpcoesComoSoube()
        {
            List<ListItem> LstOpcoes = new List<ListItem>();
            LstOpcoes.Add(new ListItem { Text = "Folheto", Value = "Folheto" });
            LstOpcoes.Add(new ListItem { Text = "Internet", Value = "Internet" });
            LstOpcoes.Add(new ListItem { Text = "Outro meio", Value = "Outro meio" });

            var Opcoes = LstOpcoes.Select(c => new { DisplayText = c.Text, Value = c.Value });
            return new { Result = "OK", Options = Opcoes };
        }
        [WebMethod(EnableSession = true)]
        public static object OpcoesTipo()
        {
            List<ListItem> LstOpcoes = new List<ListItem>();
            LstOpcoes.Add(new ListItem { Text = "Novo Cliente", Value = "Novo Cliente" });
            LstOpcoes.Add(new ListItem { Text = "Novo Distribuidor", Value = "Novo Distribuidor" });
            LstOpcoes.Add(new ListItem { Text = "Indicação", Value = "Indicação" });
            LstOpcoes.Add(new ListItem { Text = "Repetidor", Value = "Repetidor" });

            var Opcoes = LstOpcoes.Select(c => new { DisplayText = c.Text, Value = c.Value });
            return new { Result = "OK", Options = Opcoes };
        }
        [WebMethod(EnableSession = true)]
        public static object OpcoesClientes()
        {
            CheckSession();
            TB_USUARIO User = (TB_USUARIO)HttpContext.Current.Session["User"];
            ClienteBLL BLL = new ClienteBLL();
            List<TB_CLIENTE> LstsClientes = BLL.GetAll(User.ID.ToString());

            var Opcoes = LstsClientes.Select(c => new { DisplayText = c.Nome, Value = c.ID.ToString() });
            return new { Result = "OK", Options = Opcoes };
        }

        private static void CheckSession()
        {
           if (HttpContext.Current.Session["User"] == null)
                HttpContext.Current.Response.Redirect("Login.aspx");
        }
        [WebMethod(EnableSession = true)]
        public static object ListaClientes(string filtro, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            CheckSession();
            TB_USUARIO User = (TB_USUARIO)HttpContext.Current.Session["User"];
            ClienteBLL BLL = new ClienteBLL();
            return BLL.GetAll(User.ID.ToString(), filtro, jtStartIndex, jtPageSize, jtSorting);
            
        }

        [WebMethod(EnableSession = true)]
        public static object AtualizarCliente(ClienteBLL.ClienteREP record)
        {
            TB_USUARIO User = (TB_USUARIO)HttpContext.Current.Session["User"];
            ClienteBLL BLL = new ClienteBLL();
            int Resu = BLL.Update(User.ID.ToString(), record.ID_Cliente, record.ComoSoube, record.Email, record.Nome, record.Telefone, record.Tipo, record.Indicacao);
            return new { Result = "OK" };
        }
        [WebMethod(EnableSession = true)]
        public static object AddNewCliente(ClienteBLL.ClienteREP record)
        {
            TB_USUARIO User = (TB_USUARIO)HttpContext.Current.Session["User"];
            ClienteBLL BLL = new ClienteBLL();
            ClienteBLL.ClienteREP Prod = BLL.Add(User.ID.ToString(), record.ComoSoube, record.Email, record.Nome, record.Telefone, record.Tipo, record.Indicacao);
            return new { Result = "OK", Record = Prod };
        }
        [WebMethod(EnableSession = true)]
        public static object ExcluirCliente(string ID_Cliente)
        {
            try
            {
                ClienteBLL BLL = new ClienteBLL();
                int Result = BLL.Delete(ID_Cliente);

                if (Result == 1)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Já foi efetuada uma venda para esse cliente, não é possivel remove-lo." };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }
    }
}