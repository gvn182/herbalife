using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Data;
using Util;

namespace BLL.Relatorio
{
    public class MovimentacaoEstoqueBLL
    {
        EstoqueBLL BLL = new EstoqueBLL();
        public class MovimentacaoEstoqueREP
        {
            public string Data { get; set; }
            public string Produto { get; set; }
            public string Qtd { get; set; }
            public string Motivo { get; set; }
        }
        public List<MovimentacaoEstoqueREP> GetEstoque(String Data, int User_ID, String Produto, String Status)
        {

            List<MovimentacaoEstoqueREP> Result;

            if (Produto != "-1")
            {
                Result = BLL.GetAllBy(Produto.ToString(), Data, Status).Select(x => new MovimentacaoEstoqueREP
                {
                    Data = x.Data.ToString("dd/MM/yyyy"),
                    Motivo = x.Motivo,
                    Produto = x.TB_PRODUTO.DESCRICAO.ToString(),
                    Qtd = x.Qtd.ToString()

                }).ToList<MovimentacaoEstoqueREP>();
            }
            else
            {
                Result = BLL.GetAllBy(User_ID, Data, Status).Select(x => new MovimentacaoEstoqueREP
                {
                    Data = x.Data.ToString("dd/MM/yyyy"),
                    Motivo = x.Motivo,
                    Produto = x.TB_PRODUTO.DESCRICAO.ToString(),
                    Qtd = x.Qtd.ToString()

                }).ToList<MovimentacaoEstoqueREP>();
            }
            if(Result.Count > 0)
            Result.Add(new MovimentacaoEstoqueREP()
            {
                Data = "Total",
                Motivo = String.Empty,
                Produto =   String.Empty,
                Qtd = Result.Sum(x => int.Parse(x.Qtd)).ToString()
            });
            return Result;
        }
        public void ExportToExcel(Object List, Page page)
        {
            List<MovimentacaoEstoqueREP> Content = (List<MovimentacaoEstoqueREP>)List;
            DataTable Result = DTable.ToDataTable(Content);
            GenericExcelExport Exc = new GenericExcelExport();
            Exc.ExportToExcel("Movimentacao Estoque " + DateTime.Now.ToString("dd.MM.yyyy HH.mm") + ".xls", page, Result);
        }
    }
}
