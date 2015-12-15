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
    public class VendaItemClienteBLL
    {
        VendaItemClienteDAL DAL = new VendaItemClienteDAL();
        UsuarioBLL UserBLL = new UsuarioBLL();
        public class VendaItemClienteREP
        {
            public string Data { get; set; }
            public string Cliente { get; set; }
            public string Produto { get; set; }
            public string Valor_Produto { get; set; }
            public string Desconto { get; set; }
            public string Valor_Final { get; set; }
        }
        public List<VendaItemClienteREP> GetFechamentoDiario(int ID_Usuario, string ID_Cliente, string ID_Produto, String Data)
        {
            String DataInicial = Data.Split('-')[0].Trim();
            String DataFinal = Data.Split('-')[1].Trim();
            List<TB_VENDA_PRODUTO> ProdutosVendidos;
            if (ID_Cliente == "-1" && ID_Produto == "-1")
            {
                ProdutosVendidos = DAL.GetAllProdutosVendidos(ID_Usuario, DateTime.ParseExact(DataInicial, "dd/MM/yyyy", null), DateTime.ParseExact(DataFinal, "dd/MM/yyyy", null));
            }
            else if (ID_Cliente != "-1" && ID_Produto == "-1")
            {
                ProdutosVendidos = DAL.GetAllProdutosVendidos(ID_Usuario,int.Parse(ID_Cliente), DateTime.ParseExact(DataInicial ,"dd/MM/yyyy", null), DateTime.ParseExact(DataFinal, "dd/MM/yyyy", null));
            }
            else if (ID_Cliente == "-1" && ID_Produto != "-1")
            {
                ProdutosVendidos = DAL.GetAllProdutosVendidos(ID_Usuario, ID_Produto, DateTime.ParseExact(DataInicial, "dd/MM/yyyy", null), DateTime.ParseExact(DataFinal, "dd/MM/yyyy", null));
            }
            else
            {
                ProdutosVendidos = DAL.GetAllProdutosVendidos(ID_Usuario, int.Parse(ID_Produto), int.Parse(ID_Cliente), DateTime.ParseExact(DataInicial, "dd/MM/yyyy", null), DateTime.ParseExact(DataFinal, "dd/MM/yyyy", null));
            }

            List<VendaItemClienteREP> LstRetorno = new List<VendaItemClienteREP>();
            List<DateTime> DifferentDates = ProdutosVendidos.Select(x => x.TB_VENDA.Data).Distinct().ToList();

            foreach (DateTime TheDate in DifferentDates)
            {

                List<TB_VENDA_PRODUTO> ProdutosData = ProdutosVendidos.Where(x => x.TB_VENDA.Data == TheDate).ToList();
                foreach (TB_VENDA_PRODUTO Item in ProdutosData)
                {
                    VendaItemClienteREP NewFechamento = new VendaItemClienteREP();
                    NewFechamento.Data = TheDate.ToString("dd/MM/yyyy");
                    NewFechamento.Cliente = Item.TB_VENDA.TB_CLIENTE.Nome;
                    NewFechamento.Desconto = Item.Desconto.ToString();
                    NewFechamento.Produto = Item.TB_PRODUTO.COD_HERBALIFE + " - " + Item.TB_PRODUTO.DESCRICAO.ToString();
                    NewFechamento.Valor_Final = Item.Preco_Final.ToString("N2");
                    NewFechamento.Valor_Produto = Item.Preco.ToString("N2");
                    LstRetorno.Add(NewFechamento);
                }
                
                

            }
            if (LstRetorno.Count > 0)
            LstRetorno.Add(new VendaItemClienteREP()
            {
                Data = "Total",
                Cliente = String.Empty,
                Desconto = LstRetorno.Sum(x => decimal.Parse(x.Desconto)).ToString(),
                Produto = String.Empty,
                Valor_Final = LstRetorno.Sum(x => decimal.Parse(x.Valor_Final)).ToString("N2"),
                Valor_Produto = LstRetorno.Sum(x => decimal.Parse(x.Valor_Produto)).ToString("N2")
            });
            return LstRetorno;
        }
        public void ExportToExcel(Object List, Page page)
        {
            List<VendaItemClienteREP> Content = (List<VendaItemClienteREP>)List;
            DataTable Result = DTable.ToDataTable(Content);
            GenericExcelExport Exc = new GenericExcelExport();
            Exc.ExportToExcel("Venda Cliente Produto " + DateTime.Now.ToString("dd.MM.yyyy HH.mm") + ".xls",page,Result);
        }
    }
}
