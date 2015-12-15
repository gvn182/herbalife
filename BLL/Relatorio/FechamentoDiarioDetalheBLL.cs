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
    public class FechamentoDiarioDetalheBLL
    {
        FechamentoDiarioDetalheDAL DAL = new FechamentoDiarioDetalheDAL();
        UsuarioBLL UserBLL = new UsuarioBLL();
        public class FechamentoDiarioDetalheREP
        {
            
            public string Data { get; set; }
            public string Ordem { get; set; }
            public string Nome { get; set; }
            public string Status { get; set; }
            public string Observacao { get; set; }
            public string Produtos { get; set; }
            public string Acessos { get; set; }
            public string Total { get; set; }

        }
        public List<FechamentoDiarioDetalheREP> GetFechamentoDiarioDetalhe(int ID_Usuario, String Data)
        {
            String DataInicial = Data.Split('-')[0].Trim();
            String DataFinal = Data.Split('-')[1].Trim();

            TB_DETALHE_USUARIO UserDetail = UserBLL.GetDetailByIdUsuario(ID_Usuario);
            List<TB_VENDA> ProdutosVendidos = DAL.GetAllProdutosVendidos(ID_Usuario, DateTime.ParseExact(DataInicial, "dd/MM/yyyy", null), DateTime.ParseExact(DataFinal, "dd/MM/yyyy", null));
            List<FechamentoDiarioDetalheREP> LstRetorno = new List<FechamentoDiarioDetalheREP>();
            List<DateTime> DifferentDates = ProdutosVendidos.Select(x => x.Data).Distinct().ToList();

            foreach (DateTime DiffDate in DifferentDates)
            {
                int Acesso = 0;
                List<TB_VENDA> CurrentProducts = ProdutosVendidos.Where(x => x.Data == DiffDate).ToList();
                foreach (TB_VENDA Venda in CurrentProducts)
                {

                    Acesso++;
                    FechamentoDiarioDetalheREP NewFechamento = new FechamentoDiarioDetalheREP();
                    NewFechamento.Data = Venda.Data.ToString("dd/MM/yyyy");
                    NewFechamento.Nome = Venda.TB_CLIENTE.Nome.ToString();
                    NewFechamento.Ordem = Acesso.ToString();
                    NewFechamento.Acessos = Venda.TB_VENDA_PRODUTO.Where(x => x.TB_PRODUTO.COD_HERBALIFE == 9999 || x.TB_PRODUTO.COD_HERBALIFE == 9910 || x.TB_PRODUTO.COD_HERBALIFE == 9920 || x.TB_PRODUTO.COD_HERBALIFE == 9930).Sum(x => x.Preco_Final).ToString("N2");
                    NewFechamento.Produtos = Venda.TB_VENDA_PRODUTO.Where(x => x.TB_PRODUTO.COD_HERBALIFE != 9999 && x.TB_PRODUTO.COD_HERBALIFE != 9910 && x.TB_PRODUTO.COD_HERBALIFE != 9920 && x.TB_PRODUTO.COD_HERBALIFE != 9930).Sum(x => x.Preco_Final).ToString("N2");
                    NewFechamento.Total = (decimal.Parse(NewFechamento.Produtos) + decimal.Parse(NewFechamento.Acessos)).ToString("N2");
                    NewFechamento.Status = Venda.Tipo_Acesso;
                    NewFechamento.Observacao = Venda.Observacao;
                    LstRetorno.Add(NewFechamento);

                }
            }
            if (LstRetorno.Count > 0)
            LstRetorno.Add(new FechamentoDiarioDetalheREP()
            {
                Data = "Total",
                Nome = String.Empty,
                Ordem = LstRetorno.Count.ToString(),
                Produtos = LstRetorno.Sum(x => decimal.Parse(x.Produtos)).ToString("N2"),
                Acessos = LstRetorno.Sum(x => decimal.Parse(x.Acessos)).ToString("N2"),
                Total = LstRetorno.Sum(x => decimal.Parse(x.Total)).ToString("N2"),
                Status = String.Empty,
                Observacao = String.Empty
            });
            return LstRetorno;
        }
        public void ExportToExcel(Object List, Page page)
        {
            List<FechamentoDiarioDetalheREP> Content = (List<FechamentoDiarioDetalheREP>)List;
            DataTable Result = DTable.ToDataTable(Content);
            GenericExcelExport Exc = new GenericExcelExport();
            Exc.ExportToExcel("Fechamento diario detalhe " + DateTime.Now.ToString("dd.MM.yyyy HH.mm") + ".xls", page, Result);
        }
    }
}
