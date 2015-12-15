using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Util;

namespace BLL
{

    public class DespesaBLL
    {
        UsuarioBLL BLLUser = new UsuarioBLL();
        DespesaDAL DAL = new DespesaDAL();
        public class DespesaREP
        {
            public string ID_Cliente { get; set; }
            public string Codigo { get; set; }
            public string Data { get; set; }
            public string Descricao { get; set; }
            public string ValorTotal { get; set; }
        }
        public List<TB_DESPESA> GetAll(string ID_Empresa)
        {
            return DAL.GetAll(int.Parse(ID_Empresa), "ID", false);
        }
        public object GetAll(string ID_Empresa, string filter, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            String Sort = jtSorting.Split(' ')[0];
            bool Desc = jtSorting.Split(' ')[1] == "ASC" ? false : true;

            List<DespesaREP> Lista = DAL.GetAll(int.Parse(ID_Empresa), Sort, Desc).Select(x => new DespesaREP
            {
                Codigo = x.ID.ToString(),
                Descricao = x.Descricao,
                ID_Cliente = x.ID.ToString(),
                Data = x.Data.ToString("dd/MM/yyyy"),
                ValorTotal = x.ValorTotal.ToString("N2")
            }).ToList<DespesaREP>();

            if (filter != String.Empty)
            {
                filter = filter.ToUpper();
                Lista = Lista.Where(x => x.Descricao.ToUpper().Contains(filter)).ToList();
                return new { Result = "OK", Records = Lista, TotalRecordCount = Lista.Count };
            }
            int ListCount = Lista.Count;
            Lista = Lista.Skip(jtStartIndex).Take(jtPageSize).ToList();
            return new { Result = "OK", Records = Lista, TotalRecordCount = ListCount };
        }

        public int Delete(string ID_Produto)
        {
            TB_DESPESA Desp = DAL.GetBy(int.Parse(ID_Produto));
            DAL.Delete(Desp);
            return 1;
        }
        public DespesaREP Add(string ID_Usuario, string Data, string Descricao, string Valor)
        {
            TB_DESPESA Cli = new TB_DESPESA();
            Cli.Data = DateTime.ParseExact(Data, "dd/MM/yyyy", null);
            Cli.Descricao = Descricao;
            Cli.ValorTotal = decimal.Parse(Valor);
            Cli.ID_Usuario = int.Parse(ID_Usuario);

            DAL.Add(Cli);

            return new DespesaREP { Data = Cli.Data.ToString("dd/MM/yyyy"), Descricao = Cli.Descricao, ValorTotal = Cli.ValorTotal.ToString("N2") };
        }

        public int Update(string ID_Usuario, string ID, string Data, string Descricao, string Valor)
        {
            TB_DESPESA Cli = DAL.GetBy(int.Parse(ID));

            Cli.Data = DateTime.ParseExact(Data, "dd/MM/yyyy", null);
            Cli.Descricao = Descricao;
            Cli.ValorTotal = decimal.Parse(Valor);
            DAL.Update();
            return 1;
        }

        public TB_DESPESA GetBy(String ID)
        {
            return DAL.GetBy(int.Parse(ID));
        }
    }
}
