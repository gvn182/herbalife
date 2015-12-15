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
    public class DespesaBLL
    {
        RelDespesasDAL DAL = new RelDespesasDAL();
        UsuarioBLL UserBLL = new UsuarioBLL();
        public class RelDespesaREP
        {
            public string Data { get; set; }
            public string Descricao { get; set; }
            public string ValorTotal { get; set; }
        }
        public List<RelDespesaREP> GetDespesas(int ID_Usuario, String Data)
        {
            String DataInicial = Data.Split('-')[0].Trim();
            String DataFinal = Data.Split('-')[1].Trim();

            List<TB_DESPESA> Despesas = DAL.GetAllDespesas(ID_Usuario, DateTime.ParseExact(DataInicial, "dd/MM/yyyy", null), DateTime.ParseExact(DataFinal, "dd/MM/yyyy", null));
            List<RelDespesaREP> LstRetorno = new List<RelDespesaREP>();
            List<DateTime> DifferentDates = Despesas.Select(x => x.Data).Distinct().ToList();
            foreach (DateTime TheDate in DifferentDates)
            {

                List<TB_DESPESA> ThisDespesa = Despesas.Where(x => x.Data == TheDate).ToList();

                foreach (TB_DESPESA Item in ThisDespesa)
                {
                    RelDespesaREP NewFechamento = new RelDespesaREP();
                    NewFechamento.Data = TheDate.ToString("dd/MM/yyyy");
                    NewFechamento.Descricao = Item.Descricao;
                    NewFechamento.ValorTotal = Item.ValorTotal.ToString("N2");
                    LstRetorno.Add(NewFechamento);
                }


            }
            if (LstRetorno.Count > 0)
                LstRetorno.Add(new RelDespesaREP()
            {
                Data = "Total",
                ValorTotal = LstRetorno.Sum(x => decimal.Parse(x.ValorTotal)).ToString("N2")
            });
            return LstRetorno;
        }
        public void ExportToExcel(Object List, Page page)
        {
            List<RelDespesaREP> Content = (List<RelDespesaREP>)List;
            DataTable Result = DTable.ToDataTable(Content);
            GenericExcelExport Exc = new GenericExcelExport();
            Exc.ExportToExcel("Despesas " + DateTime.Now.ToString("dd.MM.yyyy HH.mm") + ".xls", page, Result);
        }
    }
}
