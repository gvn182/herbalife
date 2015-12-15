using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using DAL.Relatorio;
using System.Web;
using System.Web.UI;
using Util;
using System.Data;

namespace BLL.Relatorio
{
    public class FechamentoDiarioBLL
    {
        FechamentoDiarioDAL DAL = new FechamentoDiarioDAL();
        UsuarioBLL UserBLL = new UsuarioBLL();
        public class FechamentoDiarioREP
        {
            public string Data { get; set; }
            public string Total_Clientes { get; set; }
            public string Total_Entradas_Credito { get; set; }
            public string Total_Entradas_Debito { get; set; }
            public string Total_Entradas_Outros { get; set; }
            public string Total_Entradas_Dinheiro { get; set; }
            public string AberturaCaixa { get; set; }
            public string GanhoAcessos { get; set; }
            public string GanhoProduto { get; set; }
            public string GanhoTotal { get; set; }
            public string Verde { get; set; }
            public string Vermelho { get; set; }
        }
        public List<FechamentoDiarioREP> GetFechamentoDiario(int ID_Usuario, String Data)
        {
            String DataInicial = Data.Split('-')[0].Trim();
            String DataFinal = Data.Split('-')[1].Trim();
            VendaBLL BLLVenda = new VendaBLL();

            TB_DETALHE_USUARIO UserDetail = UserBLL.GetDetailByIdUsuario(ID_Usuario);
            List<TB_ABERTURA_CAIXA> Caixas = BLLVenda.GetAberturas(ID_Usuario, DateTime.ParseExact(DataInicial, "dd/MM/yyyy", null), DateTime.ParseExact(DataFinal, "dd/MM/yyyy", null));
            List<TB_VENDA_PRODUTO> ProdutosVendidos = DAL.GetAllProdutosVendidos(ID_Usuario, DateTime.ParseExact(DataInicial, "dd/MM/yyyy", null), DateTime.ParseExact(DataFinal, "dd/MM/yyyy", null));
            List<FechamentoDiarioREP> LstRetorno = new List<FechamentoDiarioREP>();
            List<DateTime> DifferentDates = ProdutosVendidos.Select(x => x.TB_VENDA.Data).Distinct().ToList();
            List<TB_VENDA> LstVendas = DAL.GetAllVendas(ID_Usuario, DateTime.ParseExact(DataInicial, "dd/MM/yyyy", null), DateTime.ParseExact(DataFinal, "dd/MM/yyyy", null));
            foreach (DateTime TheDate in DifferentDates)
            {

                List<TB_VENDA_PRODUTO> ProdutosData = ProdutosVendidos.Where(x => x.TB_VENDA.Data == TheDate).ToList();
                TB_ABERTURA_CAIXA Abertura = Caixas.FirstOrDefault(x => x.Data == TheDate);
                
                FechamentoDiarioREP NewFechamento = new FechamentoDiarioREP();
                NewFechamento.Data = TheDate.ToString("dd/MM/yyyy");
                NewFechamento.Total_Entradas_Credito = ProdutosData.Where(x => x.TB_VENDA.Forma_Pagamento == "Crédito").Sum(x => x.Preco_Final).ToString("N2");
                NewFechamento.Total_Entradas_Debito = ProdutosData.Where(x => x.TB_VENDA.Forma_Pagamento == "Débito").Sum(x => x.Preco_Final).ToString("N2");
                NewFechamento.Total_Entradas_Outros = ProdutosData.Where(x => x.TB_VENDA.Forma_Pagamento == "Outro").Sum(x => x.Preco_Final).ToString("N2");
                NewFechamento.Total_Entradas_Dinheiro = ProdutosData.Where(x => x.TB_VENDA.Forma_Pagamento == "Dinheiro").Sum(x => x.Preco_Final).ToString("N2");
                NewFechamento.GanhoAcessos = ProdutosData.Where(x => x.TB_PRODUTO.COD_HERBALIFE == 9999 || x.TB_PRODUTO.COD_HERBALIFE == 9910 || x.TB_PRODUTO.COD_HERBALIFE == 9920 || x.TB_PRODUTO.COD_HERBALIFE == 9930).Sum(x => x.Preco_Final).ToString("N2");
                NewFechamento.GanhoProduto = ProdutosData.Where(x => x.TB_PRODUTO.COD_HERBALIFE != 9999 && x.TB_PRODUTO.COD_HERBALIFE != 9910 && x.TB_PRODUTO.COD_HERBALIFE != 9920 && x.TB_PRODUTO.COD_HERBALIFE != 9930).Sum(x => x.Preco_Final).ToString("N2");
                NewFechamento.GanhoTotal = ProdutosData.Sum(x => x.Preco_Final).ToString("N2");
                NewFechamento.Verde = UserDetail.Nivel_Qualificacao == "Supervisor" ? (decimal.Parse(NewFechamento.GanhoTotal) / 3m).ToString("N2") : (decimal.Parse(NewFechamento.GanhoTotal) / 5m).ToString("N2");
                NewFechamento.Vermelho = (decimal.Parse(NewFechamento.GanhoTotal) - decimal.Parse(NewFechamento.Verde)).ToString("N2");
                NewFechamento.Total_Clientes = LstVendas.Where(x => x.Data == TheDate).Count().ToString();
                NewFechamento.AberturaCaixa = Abertura.Valor.ToString("N2");
                LstRetorno.Add(NewFechamento);

            }
            if(LstRetorno.Count > 0)
            LstRetorno.Add(new FechamentoDiarioREP()
            {
                Data = "Total",
                Total_Entradas_Credito = LstRetorno.Sum(x => decimal.Parse(x.Total_Entradas_Credito)).ToString("N2"),
                Total_Entradas_Debito = LstRetorno.Sum(x => decimal.Parse(x.Total_Entradas_Debito)).ToString("N2"),
                Total_Entradas_Outros = LstRetorno.Sum(x => decimal.Parse(x.Total_Entradas_Outros)).ToString("N2"),
                Total_Entradas_Dinheiro = LstRetorno.Sum(x => decimal.Parse(x.Total_Entradas_Dinheiro)).ToString("N2"),
                GanhoAcessos = LstRetorno.Sum(x => decimal.Parse(x.GanhoAcessos)).ToString("N2"),
                GanhoProduto = LstRetorno.Sum(x => decimal.Parse(x.GanhoProduto)).ToString("N2"),
                GanhoTotal = LstRetorno.Sum(x => decimal.Parse(x.GanhoTotal)).ToString("N2"),
                Verde = LstRetorno.Sum(x => decimal.Parse(x.Verde)).ToString("N2"),
                Vermelho = LstRetorno.Sum(x => decimal.Parse(x.Vermelho)).ToString("N2"),
                Total_Clientes = LstRetorno.Sum(x => int.Parse(x.Total_Clientes)).ToString(),
                AberturaCaixa = LstRetorno.Sum(x=> decimal.Parse(x.AberturaCaixa)).ToString("N2")
            });
            return LstRetorno;
        }
        public void ExportToExcel(Object List, Page page)
        {
            List<FechamentoDiarioREP> Content = (List<FechamentoDiarioREP>)List;
            DataTable Result = DTable.ToDataTable(Content);
            GenericExcelExport Exc = new GenericExcelExport();
            Exc.ExportToExcel("Fechamento diario " + DateTime.Now.ToString("dd.MM.yyyy HH.mm") + ".xls",page,Result);
        }
    }
}
