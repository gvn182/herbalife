using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using DAL.Relatorio;

namespace BLL
{
    public class VendaBLL
    {
        ClienteBLL CliBLL = new ClienteBLL();
        ProdutoBLL ProdBLL = new ProdutoBLL();
        VendaDAL DAL = new VendaDAL();
        EstoqueBLL EstBLL = new EstoqueBLL();
        FechamentoDiarioDetalheDAL DALFechamentoDiario = new FechamentoDiarioDetalheDAL();
        public class VendaProdutoREP
        {
            public String ID_Produto { get; set; }
            public String Produto { get; set; }
            public String Preco { get; set; }
            public String Desconto { get; set; }
            public String PrecoComDesconto { get; set; }
            public String AfetaEstoque { get; set; }
        }
        public class VendaProdutosEstornoREP
        {
            public string ID_Venda { get; set; }
            public string Data { get; set; }
            public string Ordem { get; set; }
            public string Nome { get; set; }
            public string Status { get; set; }
            public string Produtos { get; set; }
            public string Acessos { get; set; }
            public string Total { get; set; }

        }

        public object NovaVenda(int ID_Usuario, String Data, String ID_Cliente, String Pagamento, String Obs, List<VendaProdutoREP> LstProdutos)
        {
            List<TB_VENDA> LstVendasAnteriores = GetVendasBy(ID_Usuario, ID_Cliente);
            TB_CLIENTE Cli = CliBLL.GetBy(ID_Cliente);

            String Tipo = Cli.Tipo;
            if (!Tipo.Equals("Repetidor"))
            {
                String Indicacao = Cli.Indicacao.HasValue ? Cli.Indicacao.Value.ToString() : String.Empty;
                CliBLL.Update(Cli.ID_Usuario.ToString(), Cli.ID.ToString(), Cli.ComoSoube, Cli.Email, Cli.Nome, Cli.Telefone, "Repetidor", Indicacao);
            }
            TB_VENDA NovaVenda = new TB_VENDA();
            NovaVenda.Data = DateTime.ParseExact(Data, "dd/MM/yyyy", null);
            NovaVenda.ID_Cliente = int.Parse(ID_Cliente);
            NovaVenda.ID_Usuario = ID_Usuario;
            NovaVenda.Data_Insercao = DateTime.Now;
            NovaVenda.Forma_Pagamento = Pagamento;
            NovaVenda.Total_Produtos = LstProdutos.Sum(x => decimal.Parse(x.PrecoComDesconto));
            NovaVenda.Tipo_Acesso = Tipo;
            NovaVenda.Observacao = Obs;
            var ListProdutoQtd = LstProdutos.GroupBy(l => l.ID_Produto)
                          .Select(lg =>
                                new
                                {
                                    ID_Produto = lg.Key,
                                    Descricao = lg.FirstOrDefault(x => x.ID_Produto == lg.Key).Produto,
                                    Qtd = lg.Count()
                                });


            foreach (var item in ListProdutoQtd)
            {
                TB_PRODUTO Prod = ProdBLL.GetProdutoBy(int.Parse(item.ID_Produto));
                if (Prod.AFETA_ESTOQUE)
                {
                    if (Prod.QTD_ESTOQUE < item.Qtd)
                    {
                        object ResultadoFAIL = new { Status = "FAIL", Message = "Quantidade em estoque do produto <b>[" + item.Descricao + "]</b> Indisponivel" };
                        return ResultadoFAIL;
                    }
                }
            }

            NovaVenda = DAL.ADD(NovaVenda);
            AddProdutos(NovaVenda, LstProdutos);

            object ResultadoSUCCESS = new { Status = "SUCCESS", S = Tipo, Nome = NovaVenda.TB_CLIENTE.Nome, Valor = NovaVenda.Total_Produtos.ToString("N") };
            return ResultadoSUCCESS;
        }
        private void AddProdutos(TB_VENDA NovaVenda, List<VendaProdutoREP> LstProdutos)
        {
            foreach (VendaProdutoREP Item in LstProdutos)
            {
                String CODHerbalife = Item.Produto.Split('-')[0].Trim();
                DAL.ADD(new TB_VENDA_PRODUTO
                {
                    Desconto = decimal.Parse(Item.Desconto.Replace('.', ',')),
                    ID_Venda = NovaVenda.ID,
                    Preco = decimal.Parse(Item.Preco),
                    Preco_Final = decimal.Parse(Item.PrecoComDesconto),
                    ID_Produto = int.Parse(Item.ID_Produto)
                });

                if (ProdBLL.GetProdutoBy(int.Parse(Item.ID_Produto)).AFETA_ESTOQUE)
                    ProdBLL.RemoveEstoque(int.Parse(Item.ID_Produto), 1, "Venda");
            }
        }
        public List<TB_VENDA> GetVendasBy(int ID_Usuario, String ID_Cliente)
        {
            return DAL.GetVendasBy(ID_Usuario, int.Parse(ID_Cliente));
        }
        public void Estornar(int ID_Venda)
        {
            List<TB_VENDA_PRODUTO> LstVendaProd = ProdBLL.GetProdutosVendaBy(ID_Venda);

            foreach (TB_VENDA_PRODUTO Item in LstVendaProd)
            {
                if (Item.TB_PRODUTO.AFETA_ESTOQUE)
                    ProdBLL.AdicionaEstoque(Item.ID_Produto, 1, "Estorno");
            }
            DAL.RemoveAllItensVenda(ID_Venda);
            DAL.RemoverVenda(ID_Venda);
        }
        public List<VendaProdutosEstornoREP> GetVendasProdutosEstorno(int ID_Usuario, String Data)
        {
            String DataInicial = Data.Split('-')[0].Trim();
            String DataFinal = Data.Split('-')[1].Trim();

            List<TB_VENDA> ProdutosVendidos = DALFechamentoDiario.GetAllProdutosVendidos(ID_Usuario, DateTime.ParseExact(DataInicial, "dd/MM/yyyy", null), DateTime.ParseExact(DataFinal, "dd/MM/yyyy", null));
            List<VendaProdutosEstornoREP> LstRetorno = new List<VendaProdutosEstornoREP>();
            List<DateTime> DifferentDates = ProdutosVendidos.Select(x => x.Data).Distinct().ToList();

            foreach (DateTime DiffDate in DifferentDates)
            {
                int Acesso = 0;
                List<TB_VENDA> CurrentProducts = ProdutosVendidos.Where(x => x.Data == DiffDate).ToList();
                foreach (TB_VENDA Venda in CurrentProducts)
                {

                    Acesso++;
                    VendaProdutosEstornoREP NewFechamento = new VendaProdutosEstornoREP();
                    NewFechamento.ID_Venda = Venda.ID.ToString();
                    NewFechamento.Data = Venda.Data.ToString("dd/MM/yyyy");
                    NewFechamento.Nome = Venda.TB_CLIENTE.Nome.ToString();
                    NewFechamento.Ordem = Acesso.ToString();
                    NewFechamento.Produtos = Venda.TB_VENDA_PRODUTO.Where(x => x.TB_PRODUTO.COD_HERBALIFE != 9999).Sum(x => x.Preco_Final).ToString("N2");
                    NewFechamento.Acessos = Venda.TB_VENDA_PRODUTO.Where(x => x.TB_PRODUTO.COD_HERBALIFE == 9999).Sum(x => x.Preco_Final).ToString("N2");
                    NewFechamento.Total = (decimal.Parse(NewFechamento.Produtos) + decimal.Parse(NewFechamento.Acessos)).ToString("N2");
                    NewFechamento.Status = Venda.Tipo_Acesso;
                    LstRetorno.Add(NewFechamento);
                }
            }
            return LstRetorno;
        }

        public bool CheckCaixa(string ID_Usuario, string Date)
        {
            DateTime DTAbertura = Convert.ToDateTime(Date);
            return DAL.CheckCaixa(int.Parse(ID_Usuario), DTAbertura);
        }

        public void AbreCaixa(string ID_Usuario, string Date, string Valor)
        {
            DateTime DTAbertura = Convert.ToDateTime(Date);
            decimal DecValue = Decimal.Parse(Valor);
            int ID_User = int.Parse(ID_Usuario);
            DAL.AbreCaixa(ID_User, DTAbertura, DecValue);
        }

        public List<TB_ABERTURA_CAIXA> GetAberturas(int ID_Usuario, DateTime dateTime, DateTime dateTime_2)
        {
        return DAL.GetAberturas(ID_Usuario, dateTime, dateTime_2);
        }
    }
}
